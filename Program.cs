using System.Threading;
using System.Windows.Forms;
using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using YGOPro_Launcher.Config;
using YGOPro_Launcher.Login;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace YGOPro_Launcher
{
    static class Program
    {
        public const string Version = "173100";
        public static Configuration Config;
        public static LanguageManager LanguageManager;
        public static NetClient ServerConnection;
        public static UserData UserInfo;
        public const string ConfigurationFilename = "launcher.conf";
        public static Login_frm LoginWindow;
        public static Authenticator LoginService;
        public static List<Server> ServerList = new List<Server>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool forcelogin = false;
            foreach(string arg in args)
            {
                if (arg == "-r")
                {
                    int timeout = 0;
                    while (LauncherHelper.checkInstance())
                    {
                        if (timeout == 3)
                        {
                            if (MessageBox.Show(LanguageManager.Translation.pmsbProgRun) == DialogResult.OK)
                            {
                                return;
                            }
                        }
                        Thread.Sleep(500);
                        timeout++;
                    }
                }
                if (arg == "-l")
                {
                    forcelogin = true;
                }
            }

            Config = new Configuration();
            LoadConfig(Program.ConfigurationFilename);
            if (!Config.DebugMode)
                //AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (forcelogin)
            {
                Config.AutoLogin = false;
            }

            if (Config.DebugMode)
            {
               ServerList.Add(new Server()
               { ServerName = "Debug", ServerAddress = "86.0.24.143", ServerPort = 7911, GamePort = 7922, ChatAddress =  "86.0.24.143", ChatPort = 6666});
            }

            LanguageManager = new LanguageManager();
            //LanguageManager.Save("English");    
            LanguageManager.Load(Config.Language);
            
            if (LauncherHelper.checkInstance())
                if (MessageBox.Show(LanguageManager.Translation.pmsbProgRun) == DialogResult.OK)
                    return;

            UserInfo = new UserData();
            ServerConnection = new NetClient();
            
            if (!Config.DebugMode)
            {
                if (CheckUpdates())
                    return;

                CheckServerInfo();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Server serverinfo = null;

            foreach (Server server in ServerList)
            {
                if (server.ServerName == Config.DefaultServer)
                    serverinfo = server;
            }


            LoginService = new Authenticator(Config.DefaultUsername, Config.Password, ServerConnection, UserInfo);
            
            if (serverinfo != null)
            {
                if (!ServerConnection.Connect(Config.ServerAddress, Config.ServerPort))
                {
                    MessageBox.Show(LanguageManager.Translation.pMsbErrorToServer);
                }

                if (Config.AutoLogin && Config.DefaultUsername.Length < 15)
                {

                    LoginService.Authenticate();
                    Thread.Sleep(2000);
                }
            }

            if (UserInfo.Username == "" && UserInfo.LoginKey == "")
            {
                LoginWindow = new Login_frm(Config, ServerConnection, LoginService);

                if (LoginWindow.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            if (!ServerConnection.IsConnected)
            {
                return;
            }

            if (UserInfo.Username != "" && UserInfo.LoginKey != "")
            {
                if(forcelogin)
                    LoadConfig(ConfigurationFilename);
                Application.Run(new Main_frm());
            }
            else
            {
                Config.AutoLogin = false;
                SaveConfig(ConfigurationFilename, Config);
                MessageBox.Show(LanguageManager.Translation.pMsbBadLog);
            }

        }

        public static bool CheckUpdates()
        {
            string updateLink = Config.UpdaterAddress;
            const string updaterName = "YgoUpdater.exe";
            const string dllName = "ICSharpCode.SharpZipLib.dll";
            WebClient client = new WebClient { Proxy = null };
            string result = "";
            try
            {
                result= client.DownloadString(updateLink + "?v=" + Version);
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

            if (result.Contains("|"))
            {
                string[] data = result.Split('|');

                File.WriteAllBytes(Path.Combine(Application.StartupPath, updaterName), Properties.Resources.YgoUpdater);
                File.WriteAllBytes(Path.Combine(Application.StartupPath, dllName), Properties.Resources.ICSharpCode_SharpZipLib);

                Process updateProcess = new Process();
                updateProcess.StartInfo.FileName = Path.Combine(Application.StartupPath, updaterName);
                updateProcess.StartInfo.Arguments = data[1] + " " + Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location);
                updateProcess.Start();
            }

            return true;
        }

        public static bool CheckServerInfo()
        {
            string updateLink = Config.ServerInfoAddress;
            WebClient client = new WebClient { Proxy = null };
            string result = "";
            try
            {
                result = client.DownloadString(updateLink + "?v=" + Version);
            }
            catch
            {
                ServerList.Add(new Server()
                { ServerName = Config.ServerName, ServerAddress = Config.ServerAddress, ServerPort = Config.ServerPort, 
                    GamePort = Config.GamePort, ChatAddress = Config.ChatServerAddress, ChatPort = Config.ChatPort });
                return false;
            }
            if (result == "KO")
                return false;

            try
            {
                string[] servers = result.Split(';');
                foreach (string server in servers)
                {
                    string[] infos = server.Split(',');
                    ServerList.Add(new Server()
                    { ServerName = infos[0], ServerAddress = infos[1], ServerPort = Convert.ToInt32(infos[2]), 
                        GamePort = Convert.ToInt32(infos[3]), ChatAddress = infos[5], ChatPort = Convert.ToInt32(infos[4]) });
                }
            }
            catch
            {
                MessageBox.Show("Incorrect server string format");
                return false;
            }

            return true;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception ?? new Exception();

            File.WriteAllText("crash_" + DateTime.UtcNow.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt", exception.ToString());

            ErrorReport_frm error = new ErrorReport_frm(exception);
            error.ShowDialog();

            Console.WriteLine(exception.ToString());
            Process.GetCurrentProcess().Kill();
        }

        public static void SaveConfig(string filename, Configuration config)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Configuration));
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
                XmlSerializer deserializer = new XmlSerializer(typeof(Configuration));
                TextReader textReader = new StreamReader(filename);
                Program.Config = (Configuration)deserializer.Deserialize(textReader);
                textReader.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Error Loading " + filename);
            }
        }
    }
    public class Server
    {
        public string ServerName;
        public string ServerAddress;
        public int ServerPort;
        public int GamePort;
        public string ChatAddress;
        public int ChatPort;
    }
}
