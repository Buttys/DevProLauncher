using System;
using System.Windows.Forms;
using DevProLauncher.Controller;
using System.Collections.Generic;

namespace DevProLauncher.Windows
{
    public partial class DropBoxSynch_frm : Form
    {
        dbsettings db;

        /// <summary>
        /// Ctor, just loading some Settings
        /// </summary>
        public DropBoxSynch_frm()
        {

            InitializeComponent();

            db = new dbsettings();

            LoadSettings();
            this.deckCB.CheckedChanged += new System.EventHandler(this.deckCB_CheckedChanged);
            this.allCB.CheckedChanged += new System.EventHandler(this.allCB_CheckedChanged);
            this.allCB.CheckedChanged += new System.EventHandler(this.allCB_CheckedChanged);
            this.replayCB.CheckedChanged += new System.EventHandler(this.replayCB_CheckedChanged);
            this.texturesCB.CheckedChanged += new System.EventHandler(this.texturesCB_CheckedChanged);
            this.skinsCB.CheckedChanged += new System.EventHandler(this.skinsCB_CheckedChanged);
            this.soundsCB.CheckedChanged += new System.EventHandler(this.soundsCB_CheckedChanged);

        }

        /// <summary>
        /// Loading the Settings, for Ctor
        /// </summary>
        private void LoadSettings()
        {

            db.loadDefault();
            if (!String.IsNullOrEmpty(Properties.Settings.Default.DropBoxSaveSettings))
            {
                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
            }
            else
            {
                Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
                Properties.Settings.Default.Save();
            }


            deckCB.Checked = db.Deck;

            replayCB.Checked = db.Replay;

            skinsCB.Checked = db.Skins;

            soundsCB.Checked = db.Sounds;

            texturesCB.Checked = db.Texture;

            allCB.Checked = db.All;
            if (db.All)
            {

                //deckCB.Checked = true;
                deckCB.Enabled = false;

                //replayCB.Checked = true;
                replayCB.Enabled = false;

                //skinsCB.Checked = true;
                skinsCB.Enabled = false;

                //soundsCB.Checked = true;
                soundsCB.Enabled = false;

                //texturesCB.Checked = true;
                texturesCB.Enabled = false;

            }
            else
            {
                deckCB.Enabled = true;

                replayCB.Enabled = true;

                skinsCB.Enabled = true;

                soundsCB.Enabled = true;

                texturesCB.Enabled = true;

            }



        }

        /// <summary>
        /// Binds an Account if necessary and syncs your files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitBtn_Click(object sender, EventArgs e)
        {

            DropBoxController.syncAcc();

            //DropBoxController.filesyncAsync();

        }

        private void DropBoxSynch_frm_Load(object sender, EventArgs e)
        {
            DropBoxController.getUserToken();
            DropBoxController.filesyncAsync();
        }

        #region checkboxevents
        private void deckCB_CheckedChanged(object sender, EventArgs e)
        {

            if (deckCB.Checked)
            {
                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Deck = true;

            }
            else
            {
                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Deck = false;
            }



            Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
            Properties.Settings.Default.Save();

        }

        private void replayCB_CheckedChanged(object sender, EventArgs e)
        {


            if (replayCB.Checked)
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Replay = true;
            }
            else
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Replay = false;
            }



            Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
            Properties.Settings.Default.Save();
        }

        private void skinsCB_CheckedChanged(object sender, EventArgs e)
        {

            if (skinsCB.Checked)
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Skins = true;
            }
            else
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Skins = false;
            }


            Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
            Properties.Settings.Default.Save();
        }

        private void soundsCB_CheckedChanged(object sender, EventArgs e)
        {


            if (soundsCB.Checked)
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Sounds = true;
            }
            else
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Sounds = false;
            }


            Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
            Properties.Settings.Default.Save();
        }

        private void texturesCB_CheckedChanged(object sender, EventArgs e)
        {


            if (texturesCB.Checked)
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Texture = true;
            }
            else
            {

                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.Texture = false;
            }


            Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
            Properties.Settings.Default.Save();
        }

        private void allCB_CheckedChanged(object sender, EventArgs e)
        {

            if (allCB.Checked == true)
            {
                deckCB.Checked = true;
                deckCB.Enabled = false;

                replayCB.Checked = true;
                replayCB.Enabled = false;

                skinsCB.Checked = true;
                skinsCB.Enabled = false;

                soundsCB.Checked = true;
                soundsCB.Enabled = false;

                texturesCB.Checked = true;
                texturesCB.Enabled = false;


                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.All = true;
            }
            else
            {
                deckCB.Enabled = true;

                replayCB.Enabled = true;

                skinsCB.Enabled = true;

                soundsCB.Enabled = true;

                texturesCB.Enabled = true;


                db.LoadFromString(Properties.Settings.Default.DropBoxSaveSettings);
                db.All = false;
            }

            Properties.Settings.Default.DropBoxSaveSettings = db.ToString();
            Properties.Settings.Default.Save();

        }
        #endregion

       
    }
}
