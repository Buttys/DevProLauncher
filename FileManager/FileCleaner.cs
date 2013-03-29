using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace YGOPro_Launcher.FileManager
{
    public partial class FileCleaner : Form
    {
        public FileCleaner()
        {
            InitializeComponent();
        }
        private void SetText(string text)
        {
            Invoke(new Action<string>(InternalSetText), text);
        }
        public void InternalSetText(string text)
        {
            this.Text = text;
        }

        private void FileCleaner_Load(object sender, EventArgs e)
        {
            CleanFiles();
        }
        public void SetProgress(int percent)
        {
            Invoke(new Action<int>(InternalSetProgress), percent);
        }

        private void InternalSetProgress(int percent)
        {
            updateBar.Value = percent;
        }

        private void CleanFiles()
        {
            string[] picfiles = null;
            string[] thumbnailfiles = null;
            string[] fieldfiles = null;
            string[] scriptfiles = null;

            if (Directory.Exists("pics/"))
                picfiles = Directory.GetFiles("pics/");
            else
                picfiles = new string[0];
            if (Directory.Exists("pics/thumbnail/"))
                thumbnailfiles = Directory.GetFiles("pics/thumbnail/");
            else
                thumbnailfiles = new string[0];
            if (Directory.Exists("pics/field/"))
                fieldfiles = Directory.GetFiles("pics/field/");
            else
                fieldfiles = new string[0];
            if (Directory.Exists("script/"))
                scriptfiles = Directory.GetFiles("script/");
            else
                scriptfiles = new string[0];

            int filecount = picfiles.Length + thumbnailfiles.Length + fieldfiles.Length + scriptfiles.Length;
            double percent = 0;


            for (int i = 0; i < picfiles.Length; i++)
            {
                int id = 0;
                Int32.TryParse(Path.GetFileNameWithoutExtension(picfiles[i]),out id);
                if(id == 0)
                    continue;
                if(LauncherHelper.CardManager.FromId(id) == null)
                {
                    try { File.Delete(picfiles[i]); }
                    catch { }//ignore
                }
                percent = (double)i / filecount;
                int percentInt = (int)(percent * 100);
                if (percentInt > 100) percentInt = 100;
                if (percentInt < 0) percentInt = 0;
                SetText("Cleaning Files - " + picfiles[i]);
                SetProgress(percentInt);

            }
            for (int i = 0; i < thumbnailfiles.Length; i++)
            {
                int id = 0;
                Int32.TryParse(Path.GetFileNameWithoutExtension(thumbnailfiles[i]), out id);
                if (id == 0)
                    continue;
                if (LauncherHelper.CardManager.FromId(id) == null)
                {
                    try { File.Delete(thumbnailfiles[i]); }
                    catch { }
                }
                percent = (double)i + picfiles.Length / filecount;
                int percentInt = (int)(percent * 100);
                if (percentInt > 100) percentInt = 100;
                if (percentInt < 0) percentInt = 0;
                SetText("Cleaning Files - " + thumbnailfiles[i]);
                SetProgress(percentInt);

            }
            for (int i = 0; i < fieldfiles.Length; i++)
            {
                int id = 0;
                Int32.TryParse(Path.GetFileNameWithoutExtension(fieldfiles[i]), out id);
                if (id == 0)
                    continue;
                if (LauncherHelper.CardManager.FromId(id) == null)
                {
                    try { File.Delete(fieldfiles[i]); }
                    catch { }
                }
                percent = (double)i + picfiles.Length + thumbnailfiles.Length / filecount;
                int percentInt = (int)(percent * 100);
                if (percentInt > 100) percentInt = 100;
                if (percentInt < 0) percentInt = 0;
                SetText("Cleaning Files - " + fieldfiles[i]);
                SetProgress(percentInt);

            }
            for (int i = 0; i < scriptfiles.Length; i++)
            {
                int id = 0;
                string file = Path.GetFileNameWithoutExtension(scriptfiles[i]);
                if (file.Length == 0)
                    continue;
                if(!Int32.TryParse(file.Substring(1), out id))
                    continue;
                if (LauncherHelper.CardManager.FromId(id) == null)
                {
                    try { File.Delete(scriptfiles[i]); }
                    catch { }
                }
                percent = (double)i + picfiles.Length + thumbnailfiles.Length + fieldfiles.Length / filecount;
                int percentInt = (int)(percent * 100);
                if (percentInt > 100) percentInt = 100;
                if (percentInt < 0) percentInt = 0;
                SetText("Cleaning Files - " + scriptfiles[i]);
                SetProgress(percentInt);
            }
            Program.Config.NewUpdate = false;
            Program.SaveConfig(Program.ConfigurationFilename, Program.Config);
            DialogResult = DialogResult.OK;
        }
    }
}
