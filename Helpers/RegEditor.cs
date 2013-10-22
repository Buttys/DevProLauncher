using System;
using Microsoft.Win32;

namespace DevProLauncher.Helpers
{
    public static class RegEditor
    {
       public static bool Write(string path, string key, object value)
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.CreateSubKey(path);
                // Save the value
                if (sk1 != null) sk1.SetValue(key, value);

                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                //failed to write
                return false;
            }
        }
        public static bool CreateDirectory(string path)
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser;
                if (rk != null) rk.CreateSubKey(path);
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public static bool DeleteRegTree(string path)
        {
            try
            {
                // Setting
                RegistryKey rk = Registry.CurrentUser;
                RegistryKey sk1 = rk.OpenSubKey(path);

                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(path);

                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
        }

        public static string Read(string path, string key)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey sk1 = rk.OpenSubKey(path);

            if (sk1 == null)
                return null;

            try
            {
                return (string)sk1.GetValue(key);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
        }
    }
}
