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
        private readonly string m_fileLocation;
        private readonly string m_fileType;
        private readonly object m_infoWindow;


        public FileManagerControl(string name, string dir, string filetype)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            m_fileLocation = dir;
            m_fileType = filetype;
            RefreshFileList();
            Name = name;

            if (name == "Replays")
            {
                m_infoWindow = new ReplayInfoControl();
                tableLayoutPanel1.Controls.Add((ReplayInfoControl)m_infoWindow, 1, 0);
            }
            else
                tableLayoutPanel1.ColumnStyles.RemoveAt(1);

            FileList.MouseUp += OnListMouseUp;
            FileList.SelectedIndexChanged +=FileList_SelectedIndexChanged;
            FileList.DoubleClick += GameBtn_Click;
            ApplyTranslation();
        }

        public void ApplyTranslation()
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
            if (Directory.Exists(Program.Config.LauncherDir + m_fileLocation))
// ReSharper disable AssignNullToNotNullAttribute
                Process.Start(Path.GetDirectoryName(Program.Config.LauncherDir + m_fileLocation));
// ReSharper restore AssignNullToNotNullAttribute
            else
                MessageBox.Show(Program.Config.LauncherDir + m_fileLocation + Program.LanguageManager.Translation.fileMsgNoExist);
        }

        private void GameBtn_Click(object sender, EventArgs e)
        {
            if (Name == "Decks")
            {
                LauncherHelper.GenerateConfig();
                LauncherHelper.RunGame("-d");    
            } else
            {
                if (FileList.SelectedItem == null)
                {
                    MessageBox.Show("Choose a replay first!");
                    return;
                }
                string replayDir = Program.Config.LauncherDir + m_fileLocation;
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
                LauncherHelper.GenerateConfig(true, FileList.SelectedItem + ".yrp");
                LauncherHelper.RunGame("-r");
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
                        File.Copy(Program.Config.LauncherDir + m_fileLocation + FileList.Items[FileList.SelectedIndex] + m_fileType, Program.Config.LauncherDir + m_fileLocation + input.InputBox.Text + m_fileType);
                        File.Delete(Program.Config.LauncherDir + m_fileLocation + FileList.Items[FileList.SelectedIndex] + m_fileType);
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
                        File.Delete(Program.Config.LauncherDir + m_fileLocation + fileitem.ToString(CultureInfo.InvariantCulture) + m_fileType);
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
            if ((Directory.Exists(Program.Config.LauncherDir + m_fileLocation)))
            {
                string[] files = Directory.GetFiles(Program.Config.LauncherDir + m_fileLocation);
                foreach (string item in files)
                {
                    if(item.EndsWith(m_fileType))
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
            
            if (Name == "Replays")
            {
                ((ReplayInfoControl)m_infoWindow).ReadReplay(Program.Config.LauncherDir + m_fileLocation + FileList.SelectedItem + ".yrp");
            }
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            string[] importfiles = LauncherHelper.OpenFileWindow("Import " + Text, "", "(*" + m_fileType + ")|*" + m_fileType + ";", true);
            if (importfiles != null)
            {
                foreach (string file in importfiles)
                {
                    try
                    {
                        File.Copy(file, Program.Config.LauncherDir + m_fileLocation + Path.GetFileNameWithoutExtension(file) + m_fileType);
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
        
    }
}
