using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DevProLauncher.Helpers;
using DropNet;
using System.Net.NetworkInformation;
using DevProLauncher.Config;
using System.Windows.Forms;
using DevProLauncher.Windows;
using System.IO;

namespace DevProLauncher.Controller
{
    class DropBoxController
    {

        static public void syncAcc(bool resync)
        {

            //Testing Connection

            try
            {
                Ping pSender = new Ping();

                PingReply pResult = pSender.Send("8.8.8.8");

                if (pResult.Status == IPStatus.Success)
                {
                    Console.WriteLine("Internet available");
                }
                else
                {
                    Exception exc = new Exception("no internetconnection");

                    throw exc;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please make sure your connection to the internet is available");
                return;
            }



            DropNetClient dbClient = new DropNetClient(Program.Config.AppKey, Program.Config.AppSecret);
            dbClient.UseSandbox = true;

            if (String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserToken) || resync)
            {
                try
                {
                    dbClient.GetToken();
                }
                catch (Exception)
                {

                    throw;
                }


                var url = dbClient.BuildAuthorizeUrl();

                Browser_frm browser = new Browser_frm(url.ToString());
                browser.ShowDialog();

                try
                {

                    var accessToken = dbClient.GetAccessToken();

                    MessageBox.Show(accessToken.Token.ToString());

                    Properties.Settings.Default.DropBoxUserSecret = accessToken.Secret;
                    Properties.Settings.Default.DropBoxUserToken = accessToken.Token;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Login Saved");


                }
                catch (Exception)
                {

                    throw;
                }


            }
            else
            {
                MessageBox.Show("already synchronized");
            }

        }

        static public void filesync()
        {

            //Testing Connection

            if (!LauncherHelper.TestConnection())
                return;


            if (String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserToken))
            {

                syncAcc(false);

            }

            DropNetClient dbClient = new DropNetClient(Program.Config.AppKey, Program.Config.AppSecret, Properties.Settings.Default.DropBoxUserToken, Properties.Settings.Default.DropBoxUserSecret)
                {
                    UseSandbox = true
                };

            DropNet.Models.MetaData meta;

            try
            {
                meta = dbClient.GetMetaData("/");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.StackTrace + "\n maybe your account is not in the Dropbox Dev group for this App");
                return;
            }
           

            List<string> filelist = new List<string>();

            if (!Directory.Exists(Application.StartupPath + "/deck/"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/deck/");
            }

            if (!Directory.Exists(Application.StartupPath + "/replay/"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/replay/");
            }
            
            foreach (string item in Directory.GetFiles(Application.StartupPath + "/deck/"))
	        {
		        filelist.Add(item);
	        }

            
            foreach (string item in Directory.GetFiles(Application.StartupPath + "/replay/"))
	        {
		        filelist.Add(item);
	        }

            
            foreach (DropNet.Models.MetaData file in meta.Contents)
            {

                if (Path.GetExtension(file.Name) == ".ydk")
                {

                    if (File.Exists(Application.StartupPath + "/deck/" + file.Name))
                    {
                        if (DateTime.Compare(file.ModifiedDate, File.GetLastWriteTime(Application.StartupPath + "/deck/" + file.Name)) < 0)
                        {
                            FileStream fs = new FileStream(Application.StartupPath + "/deck/" + file.Name, FileMode.Create);
                            fs.Write(dbClient.GetFile(file.Name), 0, dbClient.GetFile(file.Name).Length);
                            fs.Close();
                        }
                        else if (DateTime.Compare(file.ModifiedDate, File.GetLastWriteTime(Application.StartupPath + "/deck/" + file.Name)) > 0)
                        {
                            FileStream fs = File.OpenRead(Application.StartupPath + "/deck/" + file.Name);
                            byte[] upfile = new byte[fs.Length];
                            fs.Read(upfile, 0, upfile.Length);
                            fs.Close();
                            dbClient.UploadFile("/", file.Name, upfile);
                        }
                    }
                    else
                    {
                        FileStream fs = new FileStream(Application.StartupPath + "/deck/" + file.Name, FileMode.Create);
                        fs.Write(dbClient.GetFile(file.Name), 0, dbClient.GetFile(file.Name).Length);
                        fs.Close();
                    }

                }
                else if (Path.GetExtension(file.Name) == ".yrp")
                {

                    if (File.Exists(Application.StartupPath + "/replay/" + file.Name))
                    {
                        if (DateTime.Compare(file.ModifiedDate, File.GetLastWriteTime(Application.StartupPath + "/deck/" + file.Name)) < 0)
                        {
                            FileStream fs = new FileStream(Application.StartupPath + "/replay/" + file.Name, FileMode.Create);
                            fs.Write(dbClient.GetFile(file.Name), 0, dbClient.GetFile(file.Name).Length);
                            fs.Close();
                        }
                        else if (DateTime.Compare(file.ModifiedDate, File.GetLastWriteTime(Application.StartupPath + "/deck/" + file.Name)) > 0)
                        {
                            FileStream fs = File.OpenRead(Application.StartupPath + "/replay/" + file.Name);
                            byte[] upfile = new byte[fs.Length];
                            fs.Read(upfile, 0, upfile.Length);
                            fs.Close();
                            dbClient.UploadFile("/", file.Name, upfile);
                        }
                    }
                    else
                    {
                        FileStream fs = new FileStream(Application.StartupPath + "/replay/" + file.Name, FileMode.Create);
                        fs.Write(dbClient.GetFile(file.Name), 0, dbClient.GetFile(file.Name).Length);
                        fs.Close();
                    }

                }

            }

            if (filelist.Count > meta.Contents.Count)
            {
                foreach (string file in filelist)
                {
                    if (Path.GetExtension(file) == ".ydk")
                    {
                        FileStream fs = File.OpenRead(Application.StartupPath + "/deck/" + Path.GetFileName(file));
                        byte[] upfile = new byte[fs.Length];
                        fs.Read(upfile, 0, upfile.Length);
                        fs.Close();
                        dbClient.UploadFile("/", Path.GetFileName(file), upfile);
                    }
                    else if (Path.GetExtension(file) == ".yrp")
                    {
                        FileStream fs = File.OpenRead(Application.StartupPath + "/replay/" + Path.GetFileName(file));
                        byte[] upfile = new byte[fs.Length];
                        fs.Read(upfile, 0, upfile.Length);
                        fs.Close();
                        dbClient.UploadFile("/", Path.GetFileName(file), upfile);
                    }
                    
                }
                
            }

            //MessageBox.Show("yeah!!");


        }

    }
}
