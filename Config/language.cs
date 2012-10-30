using System;
using System.IO;

namespace YGOPro_Launcher.Config
{
    public class language
    {
        // Login_frm.cs
        public string LoginUserName;
        public string LoginPassWord;
        public string LoginLoginButton;
        public string LoginRegisterButton;

        public void Load(string languageFileName)
        {
            if (!File.Exists(languageFileName))
                return;

            var lines = File.ReadAllLines(languageFileName);

            foreach (string nonTrimmerLine in lines)
            {
                string line = nonTrimmerLine.Trim();
                if (line.Equals(string.Empty) || !line.Contains("=") || line.StartsWith("#")) continue;

                string[] data = line.Split('=');
                string variable = data[0].Trim().ToLower();
                string value = data[1].Trim();
                switch (variable)
                {
                    case "loginusername":
                        LoginUserName = value;
                        break;
                    case "loginpassword":
                        LoginPassWord = value;
                        break;
                    case "loginloginbutton":
                        LoginLoginButton = value;
                        break;
                    case "loginregisterbutton":
                        LoginRegisterButton = value;
                        break;
                }
            }
        }
    }
}
