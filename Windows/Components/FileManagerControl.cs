using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevProLauncher.Helpers;
using DevProLauncher.Windows.MessageBoxs;

namespace DevProLauncher.Windows.Components
{
    public sealed partial class FileManagerControl : Form
    {
        private readonly string _fileLocation;
        private readonly string _fileType;
        private readonly object _infoWindow;


        public FileManagerControl(string name, string dir, string filetype)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            _fileLocation = dir;
            _fileType = filetype;
            RefreshFileList();
            Name = name;
            if (name == "Decks")
            {
                    _infoWindow = new CardInfoControl();
                    tableLayoutPanel1.Controls.Add((CardInfoControl)_infoWindow, 1, 0);
                
            }
            else if (name == "Replays")
            {
                _infoWindow = new ReplayInfoControl();
                tableLayoutPanel1.Controls.Add((ReplayInfoControl)_infoWindow, 1, 0);
            }

            FileList.MouseUp += OnListMouseUp;
            FileList.SelectedIndexChanged +=FileList_SelectedIndexChanged;
            ApplyTranslation();
        }

        private void ApplyTranslation()
        {
            if (Program.LanguageManager.Loaded)
            {
                RenameBtn.Text = Program.LanguageManager.Translation.fileBtnRename;
                DeleteBtn.Text = Program.LanguageManager.Translation.fileBtnDelete;
                OpenBtn.Text = Program.LanguageManager.Translation.fileBtnFolder;
                GameBtn.Text = Program.LanguageManager.Translation.fileBtnGame;
                RefreshBtn.Text = Program.LanguageManager.Translation.fileBtnRefresh;
                ImportBtn.Text = Program.LanguageManager.Translation.fileBtnImport;
            }
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Program.Config.LauncherDir + _fileLocation))
// ReSharper disable AssignNullToNotNullAttribute
                Process.Start(Path.GetDirectoryName(Program.Config.LauncherDir + _fileLocation));
// ReSharper restore AssignNullToNotNullAttribute
            else
                MessageBox.Show(Program.Config.LauncherDir + _fileLocation + Program.LanguageManager.Translation.fileMsgNoExist);
        }

        private void GameBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig();
            if (Name == "Decks")
            {
                LauncherHelper.RunGame("-d");    
            } else
            {
                if (FileList.SelectedItem == null)
                {
                    MessageBox.Show("Choose a replay first!");
                    return;
                }
                string replayDir = Program.Config.LauncherDir + _fileLocation;
                if (!Directory.Exists(replayDir))
                {
                    MessageBox.Show("Replay directory doesn't exist!");
                    return;
                }
                string fileName = replayDir + FileList.SelectedItem + ".yrp";
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("Selected file does not exist!");
                    return;
                }
                string tempFile = replayDir + "000000000000000000000000000000000000000000000.yrp";
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
                File.Copy(fileName, tempFile);
                LauncherHelper.RunGame("-r", (evSender, evArgs) => File.Delete(tempFile));
            }
            
        }

        public void RenameItem(object sender, EventArgs e)
        {
            if ((FileList.SelectedItems.Count == 0))
            {
                MessageBox.Show(Program.LanguageManager.Translation.fileMsgNoSelect);
                return;
            }
            var input = new InputFrm("Rename", Program.LanguageManager.Translation.fileNewName, Program.LanguageManager.Translation.fileInputConfirm, "Cancel");
            if ((!(FileList.SelectedItems.Count > 1)))
            {

                if ((input.ShowDialog() == DialogResult.OK))
                {
                    try
                    {
                        File.Copy(Program.Config.LauncherDir + _fileLocation + FileList.Items[FileList.SelectedIndex] + _fileType, Program.Config.LauncherDir + _fileLocation + input.InputBox.Text + _fileType);
                        File.Delete(Program.Config.LauncherDir + _fileLocation + FileList.Items[FileList.SelectedIndex] + _fileType);
                        RefreshFileList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(Program.LanguageManager.Translation.fileMsbMulti, "Rename Error");

            }
        }

        public void DeleteItem(object sender, EventArgs e)
        {
            if ((FileList.SelectedItems.Count == 0))
            {
                MessageBox.Show(Program.LanguageManager.Translation.fileMsgNoSelect);
                return;
            }

            if ((MessageBox.Show(Program.LanguageManager.Translation.fileAskDelete, "Delete " + Name, MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                try
                {
                    foreach (string fileitem in FileList.SelectedItems)
                    {
                        File.Delete(Program.Config.LauncherDir + _fileLocation + fileitem.ToString(CultureInfo.InvariantCulture) + _fileType);
                    }
                    RefreshFileList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        public void RefreshFileList()
        {
            FileList.Items.Clear();
            if ((Directory.Exists(Program.Config.LauncherDir + _fileLocation)))
            {
                string[] files = Directory.GetFiles(Program.Config.LauncherDir + _fileLocation);
                foreach (string item in files)
                {
                    if(item.EndsWith(_fileType))
// ReSharper disable AssignNullToNotNullAttribute
                        FileList.Items.Add(Path.GetFileNameWithoutExtension(item));
// ReSharper restore AssignNullToNotNullAttribute
                }
            }
        }


        private void OnListMouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right))
            {
                FileList.SelectedIndex = FileList.IndexFromPoint(e.X, e.Y);
                if (((FileList.SelectedItem != null)))
                {
                    var strip = new ContextMenuStrip();
                    var item2 = new ToolStripMenuItem("Rename");
                    var item3 = new ToolStripMenuItem("Delete");
                    item2.Click += RenameItem;
                    item3.Click += DeleteItem;
                    strip.Items.AddRange(new ToolStripItem[] {
					item2,
					item3
				});
                    strip.Show(this, e.Location);
                }
            }
        }

        private void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FileList.SelectedIndex == -1)
                return;

            if (Name == "Decks")
            {
                ((CardInfoControl)_infoWindow).LoadDeck(Program.Config.LauncherDir + _fileLocation +  FileList.SelectedItem + ".ydk");
            }
            else if (Name == "Replays")
            {
                ((ReplayInfoControl)_infoWindow).ReadReplay(Program.Config.LauncherDir + _fileLocation + FileList.SelectedItem + ".yrp");
            }
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            string[] importfiles = LauncherHelper.OpenFileWindow("Import " + Text, "", "(*" + _fileType + ")|*" + _fileType + ";", true);
            if (importfiles != null)
            {
                foreach (string file in importfiles)
                {
                    try
                    {
                        File.Copy(file, Program.Config.LauncherDir + _fileLocation + Path.GetFileNameWithoutExtension(file) + _fileType);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                RefreshFileList();
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            RefreshFileList();
        }

        private void CopyDeckBtn_Click(object sender, EventArgs e)
        {
            var decklist = new StringBuilder();

            var window = _infoWindow as CardInfoControl;
            if (window != null)
            {
                if (window.DeckList.Items.Count == 0)
                    return;
                foreach (object item in window.DeckList.Items)
                {
                    if (item.ToString().StartsWith("--"))
                    {
                        decklist.AppendLine(item.ToString());
                    }
                    else
                    {
                        decklist.AppendLine(item + " x" + window.CardList[item.ToString()].Amount);
                    }
                }
            }
            else
            {
                var control = _infoWindow as ReplayInfoControl;
                if (control != null)
                {
                    if (control.DeckList.Items.Count == 0)
                        return;
                    foreach (object item in control.DeckList.Items)
                    {
                        if (item.ToString().StartsWith("--"))
                        {
                            decklist.AppendLine(item.ToString());
                        }
                        else
                        {
                            decklist.AppendLine(item + " x" + control.CardList[item.ToString()].Amount);
                        }
                    }
                }
            }

            Clipboard.SetText(decklist.ToString());
        }
        
    }
}
