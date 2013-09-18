﻿using System;
using System.Collections.Generic;
using DevProLauncher.Helpers;
using DropNet;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

namespace DevProLauncher.Controller
{
    class DropBoxController
    {
        /// <summary>
        /// This method is binding an dropboxacc to the dropboxapp
        /// </summary>
        static public void syncAcc()
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
#if DEBUG
            dbClient.UseSandbox = true;
#endif

            if (String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserToken))
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
                Process.Start(url.ToString());

            }
        }

        /// <summary>
        /// this method will generate a useraccesstoken (if your dbaccount is already bound to the app)
        /// </summary>
        static public void getUserToken()
        {
            DropNetClient dbClient = new DropNetClient(Program.Config.AppKey, Program.Config.AppSecret);
#if DEBUG
            dbClient.UseSandbox = true;
#endif
            try
            {
                var accessToken = dbClient.GetAccessToken();

                MessageBox.Show(accessToken.Token);

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

        /// <summary>
        /// up/download all your files to dropbox automatically
        /// </summary>
        static public void filesync()
        {

            //Testing Connection

            if (!LauncherHelper.TestConnection())
                return;

            dbsettings db = new dbsettings();
            db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);

            if (String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserToken))
            {
                syncAcc();
            }

            if (db.Deck)
            {
                syncFolder("deck/");
            }
            if (db.Replay)
            {
                syncFolder("replay/");
            }
            if (db.Skins)
            {
                syncFolder("skins/");
            }
            if (db.Sounds)
            {
                syncFolder("sound/");
            }
            if (db.Texture)
            {
                syncFolder("textures/");
            }

        }

        /// <summary>
        /// up/download all your files to dropbox automatically (Async)
        /// </summary>
        static public void filesyncAsync()
        {
            //Testing Connection
            BackgroundWorker bwSync = new BackgroundWorker();


            if (!LauncherHelper.TestConnection())
                return;


            if (String.IsNullOrEmpty(Properties.Settings.Default.DropBoxUserToken))
            {
                syncAcc();
            }

            bwSync.DoWork += new DoWorkEventHandler(bwSync_DoWork);
            bwSync.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSync_RunWorkerCompleted);

            bwSync.RunWorkerAsync();
        }

        /// <summary>
        /// Will be fired when everythin is syncronized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">e.Result shows a short, or with stacktrace in dbgmode, statusmessage </param>
        static void bwSync_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //do something
        }

        /// <summary>
        /// Calls the syncFolder method asyncron
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void bwSync_DoWork(object sender, DoWorkEventArgs e)
        {
            dbsettings db = new dbsettings();
            db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);

            try
            {
                if (db.Deck)
                {
                    e.Result = "starting filesync (\"\\deck\")";
                    syncFolder("deck/");
                }
                if (db.Replay)
                {
                    e.Result = "starting filesync (\"\\replay\")";
                    syncFolder("replay/");
                }
                if (db.Skins)
                {
                    e.Result = "starting filesync (\"\\skins\")";
                    syncFolder("skins/");
                }
                if (db.Sounds)
                {
                    e.Result = "sounds excluded because fileaccessviolation while sync";
                    // syncFolder("sound/");
                }
                if (db.Texture)
                {
                    e.Result = "starting filesync (\"\\textures\")";
                    syncFolder("textures/");
                }
                e.Result = "finished filesync";
            }
            catch (Exception ex)
            {
                e.Result = e.Result + "\n\n" + ex.Message;
#if DEBUG
                e.Result = e.Result + "\n\n" + ex.StackTrace;
                MessageBox.Show(e.Result.ToString());
#endif
            }

        }

        /// <summary>
        /// This method does all the dirty work for you (syncs any folder with db)
        /// </summary>
        /// <param name="folder">the folder you want to sync</param>
        static private void syncFolder(string folder)
        {
            DropNetClient dbClient = new DropNetClient(Program.Config.AppKey, Program.Config.AppSecret, Properties.Settings.Default.DropBoxUserToken, Properties.Settings.Default.DropBoxUserSecret);

#if DEBUG
            dbClient.UseSandbox = true;
            Program.Config.LauncherDir = "C:/Program Files (x86)/DevPro/";
#endif

            DropNet.Models.MetaData meta;

            try
            {

                meta = dbClient.GetMetaData("/" + folder);
            }
            catch (Exception exc)
            {
                try
                {
                    dbClient.CreateFolder("/" + folder);
                    meta = dbClient.GetMetaData("/" + folder);
                }
                catch (Exception)
                {
                    MessageBox.Show(exc.StackTrace + "\n maybe your account is not in the Dropbox Dev group for this App");
                    return;
                }


            }

            List<string> filelist = new List<string>();
            List<string> dirlist = new List<string>();
            //Program.Config.LauncherDir + "/" + 
            if (!Directory.Exists(Program.Config.LauncherDir + folder))
            {
                Directory.CreateDirectory(Program.Config.LauncherDir + folder);
            }

            foreach (string item in Directory.GetFiles(Program.Config.LauncherDir + folder))
            {
                filelist.Add(item);
            }

            foreach (string item in Directory.GetDirectories(Program.Config.LauncherDir + folder))
            {
                dirlist.Add(item);
            }

            if (filelist.Count == 0 && dirlist.Count != 0)
            {
                foreach (string item in dirlist)
                {
                    syncFolder(folder + Path.GetFileNameWithoutExtension(item) + "/");
                    return;
                }
            }

            foreach (DropNet.Models.MetaData file in meta.Contents)
            {


                if (File.Exists(Program.Config.LauncherDir + folder + file.Name))
                {
                    if (DateTime.Compare(file.ModifiedDate, File.GetLastWriteTime(Program.Config.LauncherDir + folder + file.Name)) < 0)
                    {
                        FileStream fs = new FileStream(Program.Config.LauncherDir + folder + file.Name, FileMode.Create);
                        fs.Write(dbClient.GetFile(file.Name), 0, dbClient.GetFile(file.Name).Length);
                        fs.Close();
                    }
                    else if (DateTime.Compare(file.ModifiedDate, File.GetLastWriteTime(Program.Config.LauncherDir + folder + file.Name)) > 0)
                    {
                        FileStream fs = File.OpenRead(Program.Config.LauncherDir + folder + file.Name);
                        byte[] upfile = new byte[fs.Length];
                        fs.Read(upfile, 0, upfile.Length);
                        fs.Close();
                        dbClient.UploadFile("/" + folder, file.Name, upfile);
                    }
                }
                else
                {
                    FileStream fs = new FileStream(Program.Config.LauncherDir + folder + file.Name, FileMode.Create);
                    fs.Write(dbClient.GetFile(file.Name), 0, dbClient.GetFile(file.Name).Length);
                    fs.Close();
                }


            }

            if (filelist.Count + dirlist.Count > meta.Contents.Count)
            {


                foreach (string file in filelist)
                {

                    FileStream fs = File.OpenRead(Program.Config.LauncherDir + folder + Path.GetFileName(file));
                    byte[] upfile = new byte[fs.Length];
                    fs.Read(upfile, 0, upfile.Length);
                    fs.Close();
                    dbClient.UploadFile("/" + folder, Path.GetFileName(file), upfile);

                }
            }
        }
    }
}

/// <summary>
/// Settingsclass
/// </summary>
public partial class dbsettings
{

    private bool deck;
    private bool replay;
    private bool skins;
    private bool sounds;
    private bool texture;
    private bool all;

    public override string ToString()
    {
        string result = "";
        try
        {
            result += deck.ToString() + "|";
            result += replay.ToString() + "|";
            result += skins.ToString() + "|";
            result += sounds.ToString() + "|";
            result += texture.ToString() + "|";
            result += all.ToString() + "|";

            return result;
        }
        catch (Exception)
        {
            return base.ToString();
        }

    }

    public void LoadFromString(string Settings)
    {
        string partialbool = "";
        int counter = 1;

        foreach (char item in Settings)
        {
            if (item == '|')
            {
                if (partialbool == "True")
                {
                    if (counter == 1)
                    {
                        deck = true;
                        counter++;
                    }
                    else if (counter == 2)
                    {
                        replay = true;
                        counter++;
                    }
                    else if (counter == 3)
                    {
                        skins = true;
                        counter++;
                    }
                    else if (counter == 4)
                    {
                        sounds = true;
                        counter++;
                    }
                    else if (counter == 5)
                    {
                        texture = true;
                        counter++;
                    }
                    else if (counter == 6)
                    {
                        all = true;
                        counter++;
                    }

                    partialbool = "";
                }
                else
                {
                    if (counter == 1)
                    {
                        deck = false;
                        counter++;
                    }
                    else if (counter == 2)
                    {
                        replay = false;
                        counter++;
                    }
                    else if (counter == 3)
                    {
                        skins = false;
                        counter++;
                    }
                    else if (counter == 4)
                    {
                        sounds = false;
                        counter++;
                    }
                    else if (counter == 5)
                    {
                        texture = false;
                        counter++;
                    }
                    else if (counter == 6)
                    {
                        all = false;
                        counter++;
                    }

                    partialbool = "";
                }
            }
            else
            {
                partialbool += item;
            }
        }
    }

    public void loadDefault()
    {
        bool value = false;
        deck = true;
        replay = true;
        skins = value;
        sounds = value;
        texture = value;
        all = value;
    }

    public void setAll(bool value)
    {
        deck = value;
        replay = value;
        skins = value;
        sounds = value;
        texture = value;
        all = value;
    }


    public bool Deck
    {
        get
        {
            return deck;
        }

        set
        {
            deck = value;
        }

    }
    public bool Replay
    {
        get
        {
            return replay;
        }

        set
        {
            replay = value;
        }

    }
    public bool Skins
    {
        get
        {
            return skins;
        }

        set
        {
            skins = value;
        }

    }
    public bool Sounds
    {
        get
        {
            return sounds;
        }

        set
        {
            sounds = value;
        }

    }
    public bool Texture
    {
        get
        {
            return texture;
        }

        set
        {
            texture = value;
        }

    }
    public bool All
    {

        get
        {
            return all;
        }
        set
        {
            all = value;
        }
    }

}