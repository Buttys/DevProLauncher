using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Net;

namespace YGOPro_Launcher
{
    public static class LauncherHelper
    {

        public static string RequestWebData(string url)
        {
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                using (StreamReader Reader = new StreamReader(webresponse.GetResponseStream()))
                {
                    return Reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static string EncodePassword(string password)
        {
            var salt = System.Text.Encoding.UTF8.GetBytes("&^%£$Ugdsgs:;");
            var userpassword = System.Text.Encoding.UTF8.GetBytes(password);

            var hmacMD5 = new HMACMD5(salt);
            var saltedHash = hmacMD5.ComputeHash(userpassword);


            //Convert encoded bytes back to a 'readable' string
            return Convert.ToBase64String(saltedHash);
        }
        public static string[] OpenFileWindow(string title, string startpath, string filefilter, bool multiselect)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = startpath;
            dialog.Title = title;
            dialog.Filter = filefilter;
            dialog.Multiselect = true;
            if ((dialog.ShowDialog() == DialogResult.OK))
            {
                return dialog.FileNames;
            }
            return null;
        }

        public delegate void UpdateUserInfo();
        public static UpdateUserInfo GameClosed;
        public static void RunGame(string arg)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfos = new ProcessStartInfo(Program.Config.LauncherDir + Program.Config.GameExe, arg);
                process.StartInfo = startInfos;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.WorkingDirectory = Program.Config.LauncherDir;
                process.Exited += new EventHandler(UpdateInfo);
                process.EnableRaisingEvents = true;
                process.Start();
            }
            catch
            {
                MessageBox.Show("Cannot find: " +Program.Config.LauncherDir + Program.Config.GameExe);
            }
        }
        static void UpdateInfo(object sender, EventArgs e)
        {
            if (GameClosed != null)
                GameClosed();
        }

        private static bool InstantExsists(string processname)
        {
            Process[] pname = Process.GetProcessesByName(processname);
            if (pname.Length == 0)
                return true;
            else
                return false;

        }
        public static Process checkInstance()
        {
            Process cProcess = Process.GetCurrentProcess();
            Process[] aProcesses = Process.GetProcessesByName(cProcess.ProcessName);

            foreach (Process process in aProcesses)
            {
                if (process.Id != cProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location == cProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        public static void GenerateConfig(string roominfo)
        {
            if ((File.Exists(Program.Config.LauncherDir + "system.CONF")))
            {
                File.Delete(Program.Config.LauncherDir + "system.CONF");
            }
            char slash = '/';
            char commer = ',';
            string[] xparts = null;
            string GameName = null;
            xparts = roominfo.Split(slash);
            GameName = xparts[3];
            string[] GameNameParts = GameName.Split(commer);
            StreamWriter writer = new StreamWriter(Program.Config.LauncherDir + "system.CONF");
            writer.WriteLine("#config file");
            writer.WriteLine("#nickname & gamename should be less than 20 characters");
            writer.WriteLine("#Generated using " + roominfo);
            writer.WriteLine("use_d3d = " +Convert.ToInt32(Program.Config.Enabled3D));
            writer.WriteLine(("antialias = " + Program.Config.Antialias));
            writer.WriteLine("errorlog = 1");
            writer.WriteLine(("nickname = " + Program.UserInfo.Username + "$" + Program.UserInfo.LoginKey));
            writer.WriteLine("gamename = " + GameName);
            writer.WriteLine(("roompass ="));
            writer.WriteLine(("lastdeck = " + Program.Config.DefaultDeck));
            writer.WriteLine(("textfont = " + Program.Config.TextFont + " " + Convert.ToInt32(Program.Config.TextSize)));
            writer.WriteLine("numfont = fonts/arialbd.ttf");
            writer.WriteLine(("serverport = " + Program.Config.GamePort));
            writer.WriteLine(("lastip = " + Program.Config.ServerAddress));
            writer.WriteLine(("lastport = " + Program.Config.GamePort));
            writer.WriteLine(("fullscreen = " + Convert.ToInt32(Program.Config.Fullscreen)));
            writer.WriteLine(("enable_sound = " + Convert.ToInt32(Program.Config.EnableSound)));
            writer.WriteLine(("enable_music = " + Convert.ToInt32(Program.Config.EnableMusic)));
            writer.Close();
        }
        public static string GenerateString()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            return GuidString;
        }

        public static string GetUID()
        {
            if (RegEditor.Read("Software\\devpro\\", "UID") != null)
            {
                return RegEditor.Read("Software\\devpro\\", "UID");
            }
            else
            {
                RegEditor.CreateDirectory("Software\\devpro\\");
                RegEditor.Write("Software\\devpro\\", "UID", GenerateString());
                return RegEditor.Read("Software\\devpro\\", "UID");
            }
        }
    }
}
