

using System.Windows.Forms;
using System;
using System.Net;
using System.IO;
using System.Diagnostics;
namespace YGOPro_Launcher
{
    static class Program
    {

        public static string Version = "240000";
        public static Configuration Config;
        public static NetClient ServerConnection;
        public static UserData UserInfo;
        //public static Main MainForm;
        public static Login_frm LoginWindow;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            if (LauncherHelper.checkInstance() != null)
                if (MessageBox.Show("Program already running") == DialogResult.OK)
                    return;

            Config = new Configuration();
            UserInfo = new UserData();
            Config.Load();
            if (CheckUpdates())
                return;

            CheckServerInfo();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ServerConnection = new NetClient();

            if(!ServerConnection.Connect(Config.ServerAddress, Config.ServerPort))
            {
                MessageBox.Show("Error Connecting to server");
            }

            LoginWindow = new Login_frm();
            if (LoginWindow.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (UserInfo.Username != "" && UserInfo.LoginKey != "")
                Application.Run(new Main_frm());
            else
                MessageBox.Show("Bad Login");

        }

        public static bool CheckUpdates()
        {
            string updateLink = Config.UpdaterAddress;
            const string updaterName = "YgoUpdater.exe";
            const string dllName = "ICSharpCode_SharpZipLib.dll";
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
                MessageBox.Show("You have a too old version of the launcher. Please reinstall it.",
                    "TDOANE - Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            
            Console.WriteLine(exception.ToString());
            Process.GetCurrentProcess().Kill();
        }
    }
}
