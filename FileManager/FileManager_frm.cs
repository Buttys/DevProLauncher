using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace YGOPro_Launcher
{
    public partial class FileManager_frm : Form
    {
        private string FileLocation;
        private string FileType;
        private object InfoWindow;


        public FileManager_frm(string name, string dir, string filetype)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            FileLocation = dir;
            FileType = filetype;
            RefreshFileList();
            Name = name;
            if (name == "Decks")
            {
                    InfoWindow = new CardInfoControl();
                    tableLayoutPanel1.Controls.Add((CardInfoControl)InfoWindow, 1, 0);
                
            }
            else if (name == "Replays")
            {
                InfoWindow = new ReplayInfoControl();
                tableLayoutPanel1.Controls.Add((ReplayInfoControl)InfoWindow, 1, 0);
            }

            this.FileList.MouseUp += new MouseEventHandler(this.OnListMouseUp);
            this.FileList.SelectedIndexChanged +=new EventHandler(FileList_SelectedIndexChanged);
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
            if(Program.Config.LauncherDir == "")
            {
                if (Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "/" + FileLocation))
                Process.Start(Path.GetDirectoryName(Application.ExecutablePath)+ "/" + FileLocation);
                else
                    MessageBox.Show(Path.GetDirectoryName(Application.ExecutablePath) + "/" + FileLocation + Program.LanguageManager.Translation.fileMsgNoExist);
            }
            else
                if (Directory.Exists(FileLocation))
                        Process.Start(FileLocation);
                else
                    MessageBox.Show(FileLocation + Program.LanguageManager.Translation.fileMsgNoExist);
        }

        private void GameBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/20000,U,Replay");
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
                string replayDir = (Program.Config.LauncherDir == "" ? Path.GetDirectoryName(Application.ExecutablePath) + "/" : "") + FileLocation;
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
            Input_frm input = new Input_frm("Rename", Program.LanguageManager.Translation.fileNewName, Program.LanguageManager.Translation.fileInputConfirm, "Cancel");
            if ((!(FileList.SelectedItems.Count > 1)))
            {

                if ((input.ShowDialog() == DialogResult.OK))
                {
                    try
                    {
                        File.Copy(FileLocation + FileList.Items[FileList.SelectedIndex].ToString() + FileType, FileLocation + input.InputBox.Text + FileType);
                        File.Delete(FileLocation + FileList.Items[FileList.SelectedIndex].ToString() + FileType);
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

            if ((MessageBox.Show(Program.LanguageManager.Translation.fileAskDelete, "Delete " + this.Name, MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                try
                {
                    foreach (string fileitem in FileList.SelectedItems)
                    {
                        File.Delete(FileLocation + fileitem.ToString() + FileType);
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
            if ((Directory.Exists(FileLocation)))
            {
                string[] files = Directory.GetFiles(FileLocation);
                foreach (string item in files)
                {
                    FileList.Items.Add(Path.GetFileNameWithoutExtension(item));
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
                    ContextMenuStrip strip = new ContextMenuStrip();
                    ToolStripMenuItem item2 = new ToolStripMenuItem("Rename");
                    ToolStripMenuItem item3 = new ToolStripMenuItem("Delete");
                    item2.Click += new EventHandler(this.RenameItem);
                    item3.Click += new EventHandler(this.DeleteItem);
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
            if (Name == "Decks")
            {
                ((CardInfoControl)InfoWindow).LoadDeck((Program.Config.LauncherDir == "" ? Path.GetDirectoryName(Application.ExecutablePath) + "/":"") + FileLocation +  FileList.SelectedItem.ToString() + ".ydk");
            }
            else if (Name == "Replays")
            {
                ((ReplayInfoControl)InfoWindow).ReadReplay((Program.Config.LauncherDir == "" ? Path.GetDirectoryName(Application.ExecutablePath) + "/":"") +FileLocation + FileList.SelectedItem.ToString() + ".yrp");
            }
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            string[] importfiles = LauncherHelper.OpenFileWindow("Import " + Text, "", "(*" + FileType + ")|*" + FileType + ";", true);
            if (importfiles != null)
            {
                foreach (string file in importfiles)
                {
                    try
                    {
                        File.Copy(file, FileLocation + Path.GetFileNameWithoutExtension(file) + FileType);
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
            StringBuilder Decklist = new StringBuilder();

            if (InfoWindow is CardInfoControl)
            {
                if (((CardInfoControl)InfoWindow).DeckList.Items.Count == 0)
                    return;
                foreach (object item in ((CardInfoControl)InfoWindow).DeckList.Items)
                {
                    if (item.ToString().StartsWith("--"))
                    {
                        Decklist.AppendLine(item.ToString());
                    }
                    else
                    {
                        Decklist.AppendLine(item.ToString() + " x" + ((CardInfoControl)InfoWindow).CardList[item.ToString()].Amount);
                    }
                }
            }
            else if (InfoWindow is ReplayInfoControl)
            {
                if (((ReplayInfoControl)InfoWindow).DeckList.Items.Count == 0)
                    return;
                foreach (object item in ((ReplayInfoControl)InfoWindow).DeckList.Items)
                {
                    if (item.ToString().StartsWith("--"))
                    {
                        Decklist.AppendLine(item.ToString());
                    }
                    else
                    {
                        Decklist.AppendLine(item.ToString() + " x" + ((ReplayInfoControl)InfoWindow).CardList[item.ToString()].Amount);
                    }
                }
            }

            Clipboard.SetText(Decklist.ToString());
        }
        
    }
}
