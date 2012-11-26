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

namespace YGOPro_Launcher
{
    static class Program
    {

        public const string Version = "152200";
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
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

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
            Config.Load(Program.ConfigurationFilename);

            if (File.Exists("ygopro_vs.exe") && !File.Exists("devpro.dll"))
            {
                File.Copy("ygopro_vs.exe", "devpro.dll");
                File.Delete("ygopro_vs.exe");
                Config.GameExe = "devpro.dll";
                Config.Save(ConfigurationFilename);
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
                Config.Save(ConfigurationFilename);
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
    }
}
