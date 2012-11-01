﻿using System;
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
        private Language lang = new Language();

        public FileManager_frm(string name, string dir, string filetype)
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            FileLocation = dir;
            FileType = filetype;
            RefreshFileList();
            this.FileList.MouseUp += new MouseEventHandler(this.OnListMouseUp);
            this.RenameBtn.Click += new EventHandler(RenameItem);
            this.DeleteBtn.Click += new EventHandler(DeleteItem);
            this.OpenBtn.Click += new EventHandler(OpenBtn_Click);
            this.GameBtn.Click += new EventHandler(GameBtn_Click);

            lang.Load(Program.Config.language + ".conf");
            newText();
        }

        private void newText()
        {
            RenameBtn.Text = lang.fileBtnRename;
            DeleteBtn.Text = lang.fileBtnDelete;
            OpenBtn.Text = lang.fileBtnFolder;
            GameBtn.Text = lang.fileBtnGame;
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            if(Program.Config.LauncherDir == "")
            {
                if (Directory.Exists(Path.GetDirectoryName(Application.ExecutablePath) + "/" + FileLocation))
                Process.Start(Path.GetDirectoryName(Application.ExecutablePath)+ "/" + FileLocation);
                else
                    MessageBox.Show(Path.GetDirectoryName(Application.ExecutablePath) + "/" + FileLocation + lang.fileMsgNoExist);
            }
            else
                if (Directory.Exists(FileLocation))
                        Process.Start(FileLocation);
                else
                    MessageBox.Show(FileLocation + lang.fileMsgNoExist);
        }

        private void GameBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/20000,U,Replay");
            LauncherHelper.RunGame((Name == "Decks" ? "-d" : "-r"));
        }

        public void RenameItem(object sender, EventArgs e)
        {
            if ((FileList.SelectedItems.Count == 0))
            {
                MessageBox.Show(lang.fileMsgNoSelect);
                return;
            }
            Input_frm input = new Input_frm("Rename", lang.fileNewName, lang.fileInputConfirm, lang.optionBtnCancel);
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
                MessageBox.Show(lang.fileMsbMulti, "Rename Error");

            }
        }

        public void DeleteItem(object sender, EventArgs e)
        {
            if ((FileList.SelectedItems.Count == 0))
            {
                MessageBox.Show(lang.fileMsgNoSelect);
                return;
            }

            if ((MessageBox.Show(lang.fileAskDelete + " ",lang.fileBtnDelete  + " " + this.Name, MessageBoxButtons.YesNo) == DialogResult.Yes))
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
        
    }
}
