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

namespace YGOPro_Launcher
{
    static class Program
    {

        public const string Version = "165000";
        public static Configuration Config;
        public static LanguageManager LanguageManager;
        public static NetClient ServerConnection;
        public static UserData UserInfo;
        public const string ConfigurationFilename = "launcher.conf";
        public static Login_frm LoginWindow;
        public static Authenticator LoginService;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
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
            }

            Config = new Configuration();
            LoadConfig(Program.ConfigurationFilename);
            if(!Config.DebugMode)
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (Config.DebugMode)
            {
                Config.ServerAddress = "86.0.24.143";
                Config.GamePort = 7911;
                Config.ServerPort = 7922;
                Config.ChatPort = 6666;
                Config.ServerName = "Debug";
            }

            if (File.Exists("ygopro_vs.exe") && !File.Exists("devpro.dll"))
            {
                File.Copy("ygopro_vs.exe", "devpro.dll");
                File.Delete("ygopro_vs.exe");
                Config.GameExe = "devpro.dll";
                SaveConfig(ConfigurationFilename,Config);
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

            LoginService = new Authenticator(Config.DefaultUsername, Config.Password, ServerConnection, UserInfo);

            if(!ServerConnection.Connect(Config.ServerAddress, Config.ServerPort))
            {
                MessageBox.Show(LanguageManager.Translation.pMsbErrorToServer);
            }

            if (Config.AutoLogin && Config.DefaultUsername.Length < 15)
            {           

                LoginService.Authenticate();
                Thread.Sleep(2000);
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
                Application.Run(new Main_frm());
            else
            {
                Config.AutoLogin = false;
                SaveConfig(ConfigurationFilename,Config);
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
                Console.WriteLine("Unable to connect to update server");
                return false;
            }
            if (result == "KO")
                return false;

            try
            {
                string[] infos = result.Split(',');
                Config.ServerName = infos[0];
                Config.ServerAddress = infos[1];
                Config.ServerPort = Convert.ToInt32(infos[2]);
                Config.GamePort = Convert.ToInt32(infos[3]);
                Config.ChatPort = Convert.ToInt32(infos[4]);
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
                MessageBox.Show("File not found");
                return;
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
}
