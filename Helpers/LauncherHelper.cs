using System;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using DevProLauncher.Network.Data;
using DevProLauncher.Windows;

namespace DevProLauncher.Helpers
{
    public static class LauncherHelper
    {

        private static readonly Dictionary<string, int> Banlists = new Dictionary<string, int>();

        public static CardsManager CardManager = new CardsManager();

        public static bool TestConnection()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                foreach (NetworkInterface nic in nics)
                {
                    if (
                        (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel) &&
                        nic.OperationalStatus == OperationalStatus.Up)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static void SyncCloud(object sender, EventArgs e)
        {
            DropBoxSynch_frm dbsfrm = new DropBoxSynch_frm();

            dbsfrm.ShowDialog();
        }

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
            return Banlists.Keys.ToArray();
        }

        public static string GetBanListFromInt(int value)
        {
            foreach (string key in GetBanListArray().Where(key => Banlists[key] == value))
            {
                return key;
            }
            return "Unknown";
        }

        public static int GetBanListValue(string key)
        {
            return Banlists.ContainsKey(key) ? Banlists[key] : 0;
        }

        public static string RequestWebData(string url)
        {
            try
            {
                var webrequest = (HttpWebRequest)WebRequest.Create(url);
                var webresponse = (HttpWebResponse)webrequest.GetResponse();
// ReSharper disable AssignNullToNotNullAttribute
                using (var reader = new StreamReader(webresponse.GetResponseStream()))
// ReSharper restore AssignNullToNotNullAttribute
                {
                    return reader.ReadToEnd();
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
            var salt = Encoding.UTF8.GetBytes("&^%£$Ugdsgs:;");
            var userpassword = Encoding.UTF8.GetBytes(password);

            var hmacMD5 = new HMACMD5(salt);
            var saltedHash = hmacMD5.ComputeHash(userpassword);


            //Convert encoded bytes back to a 'readable' string
            return Convert.ToBase64String(saltedHash);
        }

        public static string StringToBinary(string s)
        {
            string output = "";
            foreach (char c in s)
            {
                for (int i = 128; i >= 1; i /= 2)
                {
                    if ((c & i) > 0)
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
                var bytes = new byte[numOfBytes];
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

        public static string Base64ToString(string text)
        {
                byte[] bytes = Convert.FromBase64String(text);
                return Encoding.UTF8.GetString(bytes);
        }

        public static string[] OpenFileWindow(string title, string startpath, string filefilter, bool multiselect)
        {
            var dialog = new OpenFileDialog
                {
                    InitialDirectory = startpath,
                    Title = title,
                    Filter = filefilter,
                    Multiselect = true
                };
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
                var process = new Process();
                var startInfos = new ProcessStartInfo(Program.Config.LauncherDir + Program.Config.GameExe, arg);
                process.StartInfo = startInfos;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.StartInfo.WorkingDirectory = Program.Config.LauncherDir;
                if(arg == "-j")
                    process.Exited += UpdateInfo;
                if(arg == "-d")
                    process.Exited += RefreshDeckList;
                if (onExit != null)
                {
                    process.Exited += onExit;
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

        public static bool CheckInstance()
        {
            var cProcess = Process.GetCurrentProcess();
            var aProcesses = Process.GetProcessesByName(cProcess.ProcessName);
            int count = aProcesses.Where(process => process.Id != cProcess.Id).Count(process => Assembly.GetExecutingAssembly().Location == cProcess.MainModule.FileName);
            return (count > 0);
        }

        public static void GenerateConfig(ServerInfo server, string roominfo)
        {
            if ((File.Exists(Program.Config.LauncherDir + "system.CONF")))
            {
                File.Delete(Program.Config.LauncherDir + "system.CONF");
            }
            string gameName = roominfo;
            var writer = new StreamWriter(Program.Config.LauncherDir + "system.CONF");
            writer.WriteLine("#config file");
            writer.WriteLine("#nickname & gamename should be less than 20 characters");
            writer.WriteLine("#Generated using " + roominfo);
            writer.WriteLine("use_d3d = " + Convert.ToInt32(Program.Config.Enabled3D));
            writer.WriteLine(("antialias = " + Program.Config.Antialias));
            writer.WriteLine("errorlog = 1");
            writer.WriteLine(("nickname = " + Program.UserInfo.username + "$" + Program.LoginKey));
            writer.WriteLine("gamename = " + gameName);
            writer.WriteLine(("roompass ="));
            writer.WriteLine(("lastdeck = " + Program.Config.DefaultDeck));
            writer.WriteLine("textfont = fonts/" + Program.Config.GameFont + " " + Program.Config.FontSize);
            writer.WriteLine("numfont = fonts/arialbd.ttf");
            writer.WriteLine(("serverport = " + server.serverPort));
            writer.WriteLine(("lastip = " + server.serverAddress));
            writer.WriteLine(("lastport = " + server.serverPort));
            writer.WriteLine(("fullscreen = " + Convert.ToInt32(Program.Config.Fullscreen)));
            writer.WriteLine(("enable_sound = " + Convert.ToInt32(Program.Config.EnableSound)));
            writer.WriteLine(("enable_music = " + Convert.ToInt32(Program.Config.EnableMusic)));
            writer.WriteLine("skin_index = " + Convert.ToInt32(Program.Config.Skin));
            writer.WriteLine("auto_card_placing = " + Convert.ToInt32(Program.Config.AutoPlacing));
            writer.WriteLine("random_card_placing = " + Convert.ToInt32(Program.Config.RandomPlacing));
            writer.WriteLine("auto_chain_order = " + Convert.ToInt32(Program.Config.AutoChain));
            writer.WriteLine("no_delay_for_chain = " + Convert.ToInt32(Program.Config.NoChainDelay));
            writer.Close();
        }
        public static void GenerateConfig()
        {
            if ((File.Exists(Program.Config.LauncherDir + "system.CONF")))
            {
                File.Delete(Program.Config.LauncherDir + "system.CONF");
            }
            var writer = new StreamWriter(Program.Config.LauncherDir + "system.CONF");
            writer.WriteLine("#config file");
            writer.WriteLine("#nickname & gamename should be less than 20 characters");
            writer.WriteLine("use_d3d = " + Convert.ToInt32(Program.Config.Enabled3D));
            writer.WriteLine(("antialias = " + Program.Config.Antialias));
            writer.WriteLine("errorlog = 1");
            writer.WriteLine("nickname = ");
            writer.WriteLine("roompass =");
            writer.WriteLine(("lastdeck = " + Program.Config.DefaultDeck));
            writer.WriteLine("textfont = fonts/" + Program.Config.GameFont + " " + Program.Config.FontSize);
            writer.WriteLine("numfont = fonts/arialbd.ttf");
            writer.WriteLine(("fullscreen = " + Convert.ToInt32(Program.Config.Fullscreen)));
            writer.WriteLine(("enable_sound = " + Convert.ToInt32(Program.Config.EnableSound)));
            writer.WriteLine(("enable_music = " + Convert.ToInt32(Program.Config.EnableMusic)));
            writer.WriteLine("skin_index = " + Convert.ToInt32(Program.Config.Skin));
            writer.WriteLine("auto_card_placing = " + Convert.ToInt32(Program.Config.AutoPlacing));
            writer.WriteLine("random_card_placing = " + Convert.ToInt32(Program.Config.RandomPlacing));
            writer.WriteLine("auto_chain_order = " + Convert.ToInt32(Program.Config.AutoChain));
            writer.WriteLine("no_delay_for_chain = " + Convert.ToInt32(Program.Config.NoChainDelay));
            writer.Close();
        }
        public static void GenerateConfig(bool isreplay, string file = "")
        {
            if ((File.Exists(Program.Config.LauncherDir + "system.CONF")))
            {
                File.Delete(Program.Config.LauncherDir + "system.CONF");
            }
            var writer = new StreamWriter(Program.Config.LauncherDir + "system.CONF");
            writer.WriteLine("#config file");
            writer.WriteLine("#nickname & gamename should be less than 20 characters");
            writer.WriteLine("use_d3d = " + Convert.ToInt32(Program.Config.Enabled3D));
            writer.WriteLine(("antialias = " + Program.Config.Antialias));
            writer.WriteLine("errorlog = 1");
            writer.WriteLine(("nickname = " + Program.UserInfo.username + "$" + Program.LoginKey));
            writer.WriteLine(("roompass ="));
            writer.WriteLine(("lastdeck = " + Program.Config.DefaultDeck));
            writer.WriteLine("textfont = fonts/" + Program.Config.GameFont + " " + Program.Config.FontSize);
            writer.WriteLine("numfont = fonts/arialbd.ttf");
            writer.WriteLine(("fullscreen = " + Convert.ToInt32(Program.Config.Fullscreen)));
            writer.WriteLine(("enable_sound = " + Convert.ToInt32(Program.Config.EnableSound)));
            writer.WriteLine(("enable_music = " + Convert.ToInt32(Program.Config.EnableMusic)));
            writer.WriteLine("skin_index = " + Convert.ToInt32(Program.Config.Skin));
            writer.WriteLine("auto_card_placing = " + Convert.ToInt32(Program.Config.AutoPlacing));
            writer.WriteLine("random_card_placing = " + Convert.ToInt32(Program.Config.RandomPlacing));
            writer.WriteLine("auto_chain_order = " + Convert.ToInt32(Program.Config.AutoChain));
            writer.WriteLine("no_delay_for_chain = " + Convert.ToInt32(Program.Config.NoChainDelay));
            if (isreplay)
                writer.WriteLine("lastreplay = " + file);
            else
                writer.WriteLine("lastpuzzle = " + file);
            writer.Close();
        }

        public static void GenerateCheckmateConfig(ServerInfo server,string username,string password)
        {
            if ((File.Exists(Program.Config.LauncherDir + "system.CONF")))
            {
                File.Delete(Program.Config.LauncherDir + "system.CONF");
            }
            var writer = new StreamWriter(Program.Config.LauncherDir + "system.CONF");
            writer.WriteLine("#config file");
            writer.WriteLine("#nickname & gamename should be less than 20 characters");
            writer.WriteLine("use_d3d = " + Convert.ToInt32(Program.Config.Enabled3D));
            writer.WriteLine(("antialias = " + Program.Config.Antialias));
            writer.WriteLine("errorlog = 1");
            writer.WriteLine("nickname = " + username +"$" + password);
            writer.WriteLine("gamename =");
            writer.WriteLine("roompass =");
            writer.WriteLine("lastdeck = " + Program.Config.DefaultDeck);
            writer.WriteLine("textfont = fonts/" + Program.Config.GameFont + " " + Program.Config.FontSize);
            writer.WriteLine("numfont = fonts/arialbd.ttf");
            writer.WriteLine("serverport = " + server.serverPort);
            writer.WriteLine("lastip = " + server.serverAddress);
            writer.WriteLine("lastport = " + server.serverPort);
            writer.WriteLine("fullscreen = " + Convert.ToInt32(Program.Config.Fullscreen));
            writer.WriteLine("enable_sound = " + Convert.ToInt32(Program.Config.EnableSound));
            writer.WriteLine("enable_music = " + Convert.ToInt32(Program.Config.EnableMusic));
            writer.WriteLine("skin_index = " + Convert.ToInt32(Program.Config.Skin));
            writer.WriteLine("auto_card_placing = " + Convert.ToInt32(Program.Config.AutoPlacing));
            writer.WriteLine("random_card_placing = " + Convert.ToInt32(Program.Config.RandomPlacing));
            writer.WriteLine("auto_chain_order = " + Convert.ToInt32(Program.Config.AutoChain));
            writer.WriteLine("no_delay_for_chain = " + Convert.ToInt32(Program.Config.NoChainDelay));
            writer.Close();
        }

        public static string GetMacAddress()
        {
            const int minMacAddrLength = 12;
            string macAddress = "";
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed && !String.IsNullOrEmpty(tempMac) && tempMac.Length >= minMacAddrLength)
                {
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }
            return macAddress;
        }

        public static string GenerateString()
        {
            var g = Guid.NewGuid();
            var guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            guidString = guidString.Replace("/", "");
            return guidString;
        }

        public static string GetUID()
        {
            if (RegEditor.Read("Software\\devpro\\", "UID") != null)
            {
                return RegEditor.Read("Software\\devpro\\", "UID");
            }

            RegEditor.CreateDirectory("Software\\devpro\\");
            RegEditor.Write("Software\\devpro\\", "UID", GenerateString());
            return RegEditor.Read("Software\\devpro\\", "UID");
        }
    }
}
