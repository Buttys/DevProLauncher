using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevProLauncher.Windows;
using DevProLauncher.Network;
using DevProLauncher.Network.Data;
using DevProLauncher.Config;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Diagnostics;
using DevProLauncher.Windows.MessageBoxs;
using DevProLauncher.Helpers;

namespace DevProLauncher
{
    static class Program
    {
        public const string Version = "197100";
        public static Configuration Config;
        public static LanguageManager LanguageManager;
        public static ChatClient ChatServer;
        public static UserData UserInfo;
        public static int LoginKey = 0;
        public const string ConfigurationFilename = "launcher.conf";
        public static Dictionary<string, ServerInfo> ServerList = new Dictionary<string, ServerInfo>();
        public static MainFrm MainForm;
        public static ServerInfo Server;
        public static ServerInfo Checkmate;
        public static Random Rand = new Random();

        [STAThread]
        static void Main()
        {
            Config = new Configuration();
            LoadConfig(ConfigurationFilename);
#if !DEBUG
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
#endif
            //new update server - Forced change to prevent resting a users config
            Config.UpdaterAddress = "http://ygopro.de/launcher/version.php";
            Config.ServerInfoAddress = "http://ygopro.de/launcher/server.php";

            LanguageManager = new LanguageManager();
            //LanguageManager.Save("English");    
            LanguageManager.Load(Config.Language);

            if (LauncherHelper.CheckInstance())
                if (MessageBox.Show(LanguageManager.Translation.pmsbProgRun) == DialogResult.OK)
                    return;

            ChatServer = new ChatClient();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (LauncherHelper.TestConnection())
            {
#if !DEBUG
                if (CheckUpdates())
                    return;
                CheckServerInfo(); 
#endif
            }
            else MessageBox.Show("An internet connection is required to play online.");
#if DEBUG
            Config.ServerAddress = "86.0.24.143";
            Config.ChatPort = 8933;
            //Config.GamePort = 6666;
            Server = new ServerInfo("DevPro", "86.0.24.143", 3333);
#endif

            MainForm = new MainFrm();
            Application.Run(MainForm);
        }

        public static void SaveConfig(string filename, Configuration config)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Configuration));
                TextWriter textWriter = new StreamWriter(filename);
                serializer.Serialize(textWriter, config);
                textWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Saving " + filename);
            }
        }

        public static void LoadConfig(string filename)
        {
            if (!File.Exists(filename))
            {
                SaveConfig(filename, new Configuration());
            }
            try
            {
                var deserializer = new XmlSerializer(typeof(Configuration));
                TextReader textReader = new StreamReader(filename);
                Config = (Configuration)deserializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading " + filename);
            }
        }

        public static bool CheckUpdates()
        {
            string updateLink = Config.UpdaterAddress;
            const string updaterName = "YgoUpdater.exe";
            const string dllName = "ICSharpCode.SharpZipLib.dll";
            var client = new WebClient { Proxy = null };
            string result;
            try
            {
                result = client.DownloadString(updateLink + "?v=" + Version);
            }
            catch
            {
                Console.WriteLine("Unable to connect to update server");
                return false;
            }


            if (result.Equals("OK"))
                return false;



            if (result.Equals("KO"))
            {
                MessageBox.Show(LanguageManager.Translation.pMsbOldVers,
                    "DevPro - Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (!result.StartsWith("KO"))
            {
                MessageBox.Show("Error checking for update.",
                    "DevPro - Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (result.Contains("|"))
            {
                Config.NewUpdate = true;
                SaveConfig(ConfigurationFilename, Config);

                string[] data = result.Split(new [] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                File.WriteAllBytes(Path.Combine(Application.StartupPath, updaterName), Properties.Resources.YgoUpdater);
                File.WriteAllBytes(Path.Combine(Application.StartupPath, dllName), Properties.Resources.ICSharpCode_SharpZipLib);

                var updateProcess = new Process
                    {
                        StartInfo =
                            {
                                FileName = Path.Combine(Application.StartupPath, updaterName),
                                Arguments =
                                    data[1] + " " +
                                    Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location)
                            }
                    };
                updateProcess.Start();
            }

            return true;
        }

        public static bool CheckServerInfo()
        {
            string updateLink = Config.ServerInfoAddress;
            var client = new WebClient { Proxy = null };
            string result;
            try
            {
                result = client.DownloadString(updateLink + "?v=" + Version);
            }
            catch
            {
                return false;
            }
            if (result.Equals("KO"))
                return false;

            if (!result.StartsWith("OK"))
                return false;

            try
            {
                string[] serverinfo = result.Split(new [] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                Config.ServerAddress = serverinfo[1];
                Config.ChatPort = int.Parse(serverinfo[2]);
                Config.GamePort = int.Parse(serverinfo[3]);
                Server = new ServerInfo("DevPro",serverinfo[1],int.Parse(serverinfo[4]));
                Checkmate = new ServerInfo("Checkmate", serverinfo[5], int.Parse(serverinfo[6]));
            }
            catch
            {
                MessageBox.Show("Incorrect server string format");
                return false;
            }

            return true;
        }

        public static void ChangeUsername(string username)
        {
            UserInfo.username = username;
            MainForm.UpdateUsername();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception ?? new Exception();

            File.WriteAllText("crash_" + DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt", exception.ToString());

            var error = new ErrorReportFrm(exception);
            error.ShowDialog();

            Console.WriteLine(exception.ToString());
            Process.GetCurrentProcess().Kill();
        }

    }
}
