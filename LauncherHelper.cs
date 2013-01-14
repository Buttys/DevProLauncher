using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Net;
using YGOPro_Launcher.CardDatabase;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace YGOPro_Launcher
{
    public static class LauncherHelper
    {

        private static Dictionary<string, int> Banlists = new Dictionary<string, int>();

        public static void LoadBanlist()
        {
            if (!File.Exists(Program.Config.LauncherDir + "lflist.conf"))
                return;
            Banlists.Clear();
            var lines = File.ReadAllLines(Program.Config.LauncherDir + "lflist.conf");

            foreach (string nonTrimmerLine in lines)
            {
                string line = nonTrimmerLine.Trim();
                if (line.StartsWith("!"))
                {
                    Banlists.Add(line.Substring(1), Banlists.Count);
                }
            }
        }

        public static string[] GetBanListArray()
        {
            List<string> keys = new List<string>();
            foreach (string key in Banlists.Keys)
            {
                keys.Add(key);
            }

            return keys.ToArray();
        }

        public static string GetBanListFromInt(int value)
        {
            foreach (string key in GetBanListArray())
            {
                if (Banlists[key] == value)
                    return key;
            }
            return "Unknown";
        }

        public static int GetBanListValue(string key)
        {
            if (Banlists.ContainsKey(key))
                return Banlists[key];
            else
                return 0;
        }

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

        public static string StringToBinary(string s)
        {
            string output = "";
            foreach (char c in s.ToCharArray())
            {
                for (int i = 128; i >= 1; i /= 2)
                {
                    if (((int)c & i) > 0)
                    {
                        output += "1";
                    }
                    else
                    {
                        output += "0";
                    }
                }
            }
            return output;
        }

      

        public static string BinaryToString(string binary)
        {
                int numOfBytes = binary.Length / 8;
                byte[] bytes = new byte[numOfBytes];
                for (int i = 0; i < numOfBytes; ++i)
                {
                    bytes[i] = Convert.ToByte(binary.Substring(8 * i, 8), 2);
                }

                return Encoding.ASCII.GetString(bytes);
        }

        public static string StringToBase64(string text)
        {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(bytes);
        }

        public static string Base64toString(string text)
        {
                byte[] bytes = Convert.FromBase64String(text);
                return Encoding.UTF8.GetString(bytes);
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

        public static UpdateUserInfo DeckEditClosed;

        public static void RunGame(string arg, EventHandler onExit = null)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo startInfos = new ProcessStartInfo(Program.Config.LauncherDir + Program.Config.GameExe, arg);
                process.StartInfo = startInfos;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.WorkingDirectory = Program.Config.LauncherDir;
                if(arg == "-j")
                    process.Exited += new EventHandler(UpdateInfo);
                if(arg == "-d")
                    process.Exited += new EventHandler(RefreshDeckList);
                if (onExit != null)
                {
                    process.Exited += new EventHandler(onExit);
                }
                process.EnableRaisingEvents = true;
                process.Start();
            }
            catch
            {
                MessageBox.Show("Cannot find: " + Program.Config.LauncherDir + Program.Config.GameExe);
            }
        }

        static void UpdateInfo(object sender, EventArgs e)
        {
            if (GameClosed != null)
                GameClosed();
        }

        static void RefreshDeckList(object sender, EventArgs e)
        {
            if (DeckEditClosed != null)
                DeckEditClosed();
        }

        private static bool InstantExsists(string processname)
        {
            Process[] pname = Process.GetProcessesByName(processname);
            if (pname.Length == 0)
                return true;
            else
                return false;

        }

        public static bool checkInstance()
        {
            Process cProcess = Process.GetCurrentProcess();
            Process[] aProcesses = Process.GetProcessesByName(cProcess.ProcessName);
            int count = 0;
            foreach (Process process in aProcesses)
            {
                if (process.Id != cProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location == cProcess.MainModule.FileName)
                    {
                        count++;
                    }
                }
            }
            return (count > 0);
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
            writer.WriteLine("use_d3d = " + Convert.ToInt32(Program.Config.Enabled3D));
            writer.WriteLine(("antialias = " + Program.Config.Antialias));
            writer.WriteLine("errorlog = 1");
            writer.WriteLine(("nickname = " + Program.UserInfo.Username + "$" + Program.UserInfo.LoginKey));
            writer.WriteLine("gamename = " + GameName);
            writer.WriteLine(("roompass ="));
            writer.WriteLine(("lastdeck = " + Program.Config.DefaultDeck));
            writer.WriteLine("textfont = fonts/" + Program.Config.GameFont + " " + Program.Config.FontSize);
            writer.WriteLine("numfont = fonts/arialbd.ttf");
            writer.WriteLine(("serverport = " + Program.Config.GamePort));
            writer.WriteLine(("lastip = " + Program.Config.ServerAddress));
            writer.WriteLine(("lastport = " + Program.Config.GamePort));
            writer.WriteLine(("fullscreen = " + Convert.ToInt32(Program.Config.Fullscreen)));
            writer.WriteLine(("enable_sound = " + Convert.ToInt32(Program.Config.EnableSound)));
            writer.WriteLine(("enable_music = " + Convert.ToInt32(Program.Config.EnableMusic)));
            writer.WriteLine("use_skin = " + Convert.ToInt32(Program.Config.UseSkin));
            writer.WriteLine("auto_card_placing = " + Convert.ToInt32(Program.Config.AutoPlacing));
            writer.WriteLine("random_card_placing = " + Convert.ToInt32(Program.Config.RandomPlacing));
            writer.WriteLine("auto_chain_order = " + Convert.ToInt32(Program.Config.AutoChain));
            writer.WriteLine("no_delay_for_chain = " + Convert.ToInt32(Program.Config.NoChainDelay));
            writer.Close();
        }

        public static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = "";
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed && !String.IsNullOrEmpty(tempMac) && tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }
            return macAddress;
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

        public static CardsManager CardManager;
    }
}
