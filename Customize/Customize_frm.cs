using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using DevPro_CardManager;
using System.Drawing.Imaging;

namespace YGOPro_Launcher
{
    public partial class Customize_frm : Form
    {

        public Dictionary<ContentType, Content> Data = new Dictionary<ContentType, Content>();
        public ContentType contentView = ContentType.Covers;
        Dictionary<string, Theme> Themes = new Dictionary<string, Theme>();
        public string SelectedTheme = "None";

        public Customize_frm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            ViewSelect.SelectedIndex = 0;
            this.Data.Add(ContentType.Covers, new Content { AssetPath = "Assets/Covers/", IconSize = new Size(177, 252), GameItem = "textures\\cover.jpg", FileType = ".jpg", ImageSize = new Size(178,254) });
            this.Data.Add(ContentType.Backgrounds, new Content { AssetPath = "Assets/Backgrounds/", IconSize = new Size(256, 256), GameItem = "textures\\bg.jpg", FileType = ".jpg", ImageSize = new Size(1024, 640) });
            this.Data.Add(ContentType.Field, new Content { AssetPath = "Assets/Field/", IconSize = new Size(256, 256), GameItem = "textures\\field.png", FileType = ".png", ImageSize = new Size(256, 256) });
            this.Data.Add(ContentType.FieldTransparent, new Content { AssetPath = "Assets/FieldTransparent/", IconSize = new Size(256, 256), GameItem = "textures\\field-transparent.png", FileType = ".png", ImageSize = new Size(256, 256) });
            this.Data.Add(ContentType.Numbers, new Content { AssetPath = "Assets/Numbers/", IconSize = new Size(256, 256), GameItem = "textures\\number.png", FileType = ".png", ImageSize = new Size(320, 256) });
            this.Data.Add(ContentType.Mask, new Content { AssetPath = "Assets/Mask/", IconSize = new Size(254, 254), GameItem = "textures\\mask.png", FileType = ".png", ImageSize = new Size(254, 254) });
            this.Data.Add(ContentType.Attack_Icon, new Content { AssetPath = "Assets/AttackIcon/", IconSize = new Size(128, 128), GameItem = "textures\\attack.png", FileType = ".png", ImageSize = new Size(128, 128) });
            this.Data.Add(ContentType.Activate_Circle, new Content { AssetPath = "Assets/ActivateCircle/", IconSize = new Size(128, 128), GameItem = "textures\\\\act.png", FileType = ".png", ImageSize = new Size(128, 128) });
            this.Data.Add(ContentType.Chain, new Content { AssetPath = "Assets/ChainIcon/", IconSize = new Size(128, 128), GameItem = "textures\\chain.png", FileType = ".png", ImageSize = new Size(128, 128) });
            this.Data.Add(ContentType.Target, new Content { AssetPath = "Assets/TargetIcon/", IconSize = new Size(34, 34), GameItem = "textures\\target.png", FileType = ".png", ImageSize = new Size(34, 34) });
            this.Data.Add(ContentType.Equip, new Content { AssetPath = "Assets/EquipIcon/", IconSize = new Size(34, 34), GameItem = "textures\\equip.png", FileType = ".png", ImageSize = new Size(34, 34) });
            this.Data.Add(ContentType.Rock, new Content { AssetPath = "Assets/Rock/", IconSize = new Size(89, 128), GameItem = "textures\\f2.jpg", FileType = ".jpg", ImageSize = new Size(89, 128) });
            this.Data.Add(ContentType.Paper, new Content { AssetPath = "Assets/Paper/", IconSize = new Size(89, 128), GameItem = "textures\\f3.jpg", FileType = ".jpg", ImageSize = new Size(89, 128) });
            this.Data.Add(ContentType.Scissors, new Content { AssetPath = "Assets/Scissors/", IconSize = new Size(89, 128), GameItem = "textures\\f1.jpg", FileType = ".jpg", ImageSize = new Size(89, 128) });
            this.Data.Add(ContentType.LifePoints_Color, new Content { AssetPath = "Assets/LPColor/", IconSize = new Size(16, 16), GameItem = "textures\\lp.png", FileType = ".png", ImageSize = new Size(16, 16) });
            this.Data.Add(ContentType.LifePoints_Bar, new Content { AssetPath = "Assets/LPBar/", IconSize = new Size(200, 20), GameItem = "textures\\lpf.png", FileType = ".png", ImageSize = new Size(200, 20) });
            this.Data.Add(ContentType.Negated, new Content { AssetPath = "Assets/Negated/", IconSize = new Size(128, 128), GameItem = "textures\\negated.png", FileType = ".png", ImageSize = new Size(128, 128) });
            this.Data.Add(ContentType.Music, new Content { AssetPath = "Assets/Music/", IconSize = new Size(40, 40), GameItem = "sound\\", FileType = ".mp3" });
            this.Data.Add(ContentType.Sound_Effects, new Content { AssetPath = "Assets/SoundEffects/", IconSize = new Size(40, 40), GameItem = "sound\\", FileType = ".wav" });



            this.ContentList.View = System.Windows.Forms.View.LargeIcon;
            LoadAssets();
            LoadThemeFiles();

            this.ContentList.MouseUp += new MouseEventHandler(OnListViewMouseUp);
            ViewSelect.SelectedIndexChanged += new EventHandler(SelectedIndex_Changed);
            ThemeSelect.SelectedIndexChanged += new EventHandler(SelectedTheme_Changed);

            contentView = ContentType.Covers;
            ApplyTranslation();
        }

        public  void ApplyTranslation()
        {
            RemoveThemeBtn.Text = Program.LanguageManager.Translation.cusRemoveBtn;
            AddThemeBtn.Text = Program.LanguageManager.Translation.cusAddThemeBtn;
            AddContentBtn.Text = Program.LanguageManager.Translation.cusAddContentBtn;
            PreviewBtn.Text = Program.LanguageManager.Translation.cusPreview;
            BackUpBtn.Text = Program.LanguageManager.Translation.cusBackup;
            label2.Text = Program.LanguageManager.Translation.cusLabelTheme;
            label1.Text = Program.LanguageManager.Translation.cusLabelCont;
        }

        public void SaveTheme(string themename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Theme));
            TextWriter textWriter = new StreamWriter("Assets/Themes/" + Themes[themename].ThemeName + ".YGOTheme");
            serializer.Serialize(textWriter, Themes[themename]);
            textWriter.Close();
        }

        public List<object> ItemKeys()
        {
            List<object> keydata = new List<object>();

            foreach (ContentType key in Data.Keys)
            {
                keydata.Add(key);
            }

            return keydata;

        }

        public List<object> ThemeKeys()
        {
            List<object> keydata = new List<object>();

            foreach (string key in Themes.Keys)
            {
                keydata.Add(key);
            }

            return keydata;
        }

        void LoadThemeFiles()
        {

            //create theme folder
            if (!Directory.Exists("Assets/Themes/"))
            {
                Directory.CreateDirectory("Assets/Themes/");
                //return;
            }
            string[] files = Directory.GetFiles("Assets/Themes/");

            foreach (string file in files)
            {
                if (file.Contains(".YGOTheme"))
                {
                    LoadTheme(file);
                    ThemeSelect.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
            }

        }

        void LoadTheme(string filepath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Theme));
            TextReader textReader = new StreamReader(filepath);
            Themes[Path.GetFileNameWithoutExtension(filepath)] = (Theme)deserializer.Deserialize(textReader);
            textReader.Close();
        }
        public ThemeObject getThemeObject(ContentType type)
        {
            if (Themes.ContainsKey(SelectedTheme))
            {
                return Themes[SelectedTheme].getThemeObject(type);
            }
            else
            {
                return null;
            }
        }

        public void AddTheme(string ThemeName)
        {
            if (!Themes.ContainsKey(ThemeName))
            {
                Themes.Add(ThemeName, new Theme() { ThemeName = ThemeName });
                SaveTheme(ThemeName);
            }
            else
            {
                MessageBox.Show("Theme already exists.", "Exists");
            }
        }
        public bool ThemeExists(string name)
        {
            if (!Themes.ContainsKey(name)) return false; else return true;
        }

        public void AddThemeItem(ContentType type, string path, string filename)
        {
            Themes[SelectedTheme].AddItem(type, path, filename);
        }

        public Content GetCurrentItemSet()
        {
            return Data[contentView];
        }

        void LoadAssets()
        {
            ContentList.Items.Clear();
            foreach (ContentType key in ItemKeys())
            {
                if (!Directory.Exists(Data[key].AssetPath)) Directory.CreateDirectory(Data[key].AssetPath);
                string[] itempath = Directory.GetFiles(Data[key].AssetPath);
                Data[key].Images = new ImageList();
                Data[key].Images.TransparentColor = Color.Transparent;
                Data[key].Images.ColorDepth = ColorDepth.Depth16Bit;
                Data[key].Images.ImageSize = Data[key].IconSize;
                foreach (string item in itempath)
                {
                    if (key == ContentType.Music || key == ContentType.Sound_Effects)
                    {
                        AddIconImage(Data[key].Images, item);
                    }
                    else
                    {
                        AddImage(Data[key].Images, item);

                    }
                }
            }
            ChangeImageView();
        }

        public void RefreshList()
        {
            foreach (ContentType key in ItemKeys())
            {
                Data[key].Images.Images.Clear();
            }
            LoadAssets();
        }

        void ChangeImageView()
        {
            this.ContentList.Items.Clear();
            this.ContentList.LargeImageList = Data[contentView].Images;
            string[] itempaths = Directory.GetFiles(Data[contentView].AssetPath);
            foreach (string item in itempaths)
            {
                string imagename = Path.GetFileNameWithoutExtension(item);
                this.ContentList.Items.Add(imagename, imagename);
            }
        }

        public void SelectedIndex_Changed(object sender, EventArgs e)
        {
            ComboBox control = (ComboBox)sender;
            if (control.Text == "Covers") contentView = ContentType.Covers;
            if (control.Text == "Backgrounds") contentView = ContentType.Backgrounds;
            if (control.Text == "Attack") contentView = ContentType.Attack_Icon;
            if (control.Text == "Activate") contentView = ContentType.Activate_Circle;
            if (control.Text == "Chain") contentView = ContentType.Chain;
            if (control.Text == "Equip") contentView = ContentType.Equip;
            if (control.Text == "Rock") contentView = ContentType.Rock;
            if (control.Text == "Paper") contentView = ContentType.Paper;
            if (control.Text == "Sissors") contentView = ContentType.Scissors;
            if (control.Text == "Life Points Color") contentView = ContentType.LifePoints_Color;
            if (control.Text == "Life Points Bar") contentView = ContentType.LifePoints_Bar;
            if (control.Text == "Target") contentView = ContentType.Target;
            if (control.Text == "Negated") contentView = ContentType.Negated;
            if (control.Text == "Music") contentView = ContentType.Music;
            if (control.Text == "SoundEffects") contentView = ContentType.Sound_Effects;
            if (control.Text == "Field") contentView = ContentType.Field;
            if (control.Text == "FieldTransparent") contentView = ContentType.FieldTransparent;
            if (control.Text == "Mask") contentView = ContentType.Mask;
            if (control.Text == "Numbers") contentView = ContentType.Numbers;

            ChangeImageView();
            RefreshInstalledThemeItems();
        }

        public void SelectedTheme_Changed(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox)sender;
            SelectedTheme = box.Text;
            RefreshList();
            RefreshInstalledThemeItems();
        }

        public void RefreshInstalledThemeItems()
        {
            foreach (ListViewItem item in this.ContentList.Items)
            {
                if (getThemeObject(contentView) != null)
                {
                    if (item.Text == getThemeObject(contentView).Filename)
                    {
                        item.BackColor = Color.Green;
                        item.ForeColor = Color.White;
                    }
                }
            }
        }

        void AddImage(ImageList type, string path)
        {
            string imagename = Path.GetFileNameWithoutExtension(path);
            try
            {
                type.Images.Add(imagename, Image.FromFile(path));
            }
            catch (OutOfMemoryException)
            {
                if (MessageBox.Show(imagename + " failed to load and could have a bad format. Can i delete it to prevent future errors?", 
                    "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }
        void AddIconImage(ImageList type, string path)
        {
            string imagename = Path.GetFileNameWithoutExtension(path);
            Icon iconforFile = SystemIcons.WinLogo;

            iconforFile = Icon.ExtractAssociatedIcon(path);
            type.Images.Add(imagename, iconforFile);

        }

        public void InstallAsset(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            string itempath = Data[contentView].AssetPath + item.Text;
            if (File.Exists(itempath + Data[contentView].FileType))
            {
                try
                {
                    File.Copy(Data[contentView].AssetPath + item.Text + Data[contentView].FileType, Data[contentView].GameItem, true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }

            MessageBox.Show("Success!", "Install");


        }

        public void InstallAsset(ContentType type, string path)
        {
            string itempath = path;
            if (File.Exists(itempath))
            {
                try
                {
                    File.Copy(itempath, Data[type].GameItem, true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }

            //MessageBox.Show("Success!", "Install");


        }


        public void DealteAsset(object sender, EventArgs args)
        {

            ListViewItem item = this.ContentList.SelectedItems[0];
            if (MessageBox.Show("Are you sure you want to delete " + item.Text, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Data[contentView].Images.Images.RemoveByKey(item.Text);
                this.ContentList.Items.Remove(item);
                File.Delete(Data[contentView].AssetPath + item.Text + Data[contentView].FileType);
            }

        }
        void ApplyToTheme(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            if (MessageBox.Show("Apply " + item.Text + "  to current theme?  ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //if (ApplyTheme != null) ApplyTheme(Data[contentView].AssetPath + item.Text + Data[contentView].FileType);
                if (SelectedTheme == "None") if (MessageBox.Show("No theme selected", "No theme") == DialogResult.OK) return;

                Themes[SelectedTheme].AddItem(contentView, Data[contentView].AssetPath + item.Text + Data[contentView].FileType, item.Text);
                SaveTheme(SelectedTheme);
                RefreshList();
                RefreshInstalledThemeItems();
            }
        }

        void RemoveFromTheme(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            if (MessageBox.Show("Remove " + item.Text + "  from current theme?  ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //if (ApplyTheme != null) ApplyTheme(Data[contentView].AssetPath + item.Text + Data[contentView].FileType);
                if (SelectedTheme == "None") if (MessageBox.Show("No theme selected", "No theme") == DialogResult.OK) return;

                Themes[SelectedTheme].RemoveItem(contentView);
                SaveTheme(SelectedTheme);
                RefreshList();
                RefreshInstalledThemeItems();
            }
        }

        public void DealteTheme(string themename)
        {
            if (File.Exists("Assets/Themes/" + themename + ".YGOTheme")) File.Delete("Assets/Themes/" + themename + ".YGOTheme");
            Themes.Remove(themename);
        }
        void AddAsset(string path)
        {
            AddImage(Data[contentView].Images, path);
        }

        private void OnListViewMouseUp(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;

            if (e.Button == MouseButtons.Right)
            {
                ListViewItem item = listView.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    ContextMenuStrip mnu = new ContextMenuStrip();
                    if (contentView != ContentType.Music && contentView != ContentType.Sound_Effects)
                    {
                        ToolStripMenuItem mnuInstall = new ToolStripMenuItem("Install");
                        ToolStripMenuItem mnuApplyTheme = new ToolStripMenuItem("Set to current theme");
                        ToolStripMenuItem mnuRemoveFromTheme = new ToolStripMenuItem("Remove from current theme");
                        ToolStripMenuItem mnuDealte = new ToolStripMenuItem("Delete");

                        mnuInstall.Click += new EventHandler(InstallAsset);
                        mnuApplyTheme.Click += new EventHandler(ApplyToTheme);
                        mnuDealte.Click += new EventHandler(DealteAsset);
                        mnuRemoveFromTheme.Click += new EventHandler(RemoveFromTheme);

                        if (SelectedTheme == "None")
                        {
                            mnu.Items.AddRange(new ToolStripItem[] { mnuInstall, mnuDealte });
                        }
                        else
                        {
                            if (item.BackColor != Color.Green)
                            {
                                mnu.Items.AddRange(new ToolStripItem[] { mnuInstall, mnuApplyTheme, mnuDealte });
                            }
                        }

                        if (item.BackColor == Color.Green)
                        {
                            mnu.Items.AddRange(new ToolStripItem[] { mnuInstall, mnuRemoveFromTheme, mnuDealte });
                        }
                    }
                    else
                    {
                        if (contentView == ContentType.Music)
                        {
                            ToolStripMenuItem mnuplaymusic = new ToolStripMenuItem("Play");
                            ToolStripMenuItem mnumenumusic = new ToolStripMenuItem("Apply to menu music");
                            ToolStripMenuItem mnudeckmusic = new ToolStripMenuItem("Apply to deck editor music");
                            ToolStripMenuItem mnubattlemusic = new ToolStripMenuItem("Apply to battle music");
                            ToolStripMenuItem mnuadcantagemusic = new ToolStripMenuItem("Apply to advantage battle music");
                            ToolStripMenuItem mnudisadvantagemusic = new ToolStripMenuItem("Apply to disadvantage battle music");

                            mnuplaymusic.Click += new EventHandler(PlayMusic);
                            mnumenumusic.Click += new EventHandler(mnumenumusic_Click);
                            mnudeckmusic.Click += new EventHandler(mnudeckmusic_Click);
                            mnubattlemusic.Click += new EventHandler(mnubattlemusic_Click);
                            mnuadcantagemusic.Click += new EventHandler(mnuadcantagemusic_Click);
                            mnudisadvantagemusic.Click += new EventHandler(mnudisadvantagemusic_Click);

                            mnu.Items.AddRange(new ToolStripItem[] { mnuplaymusic, new ToolStripSeparator(), mnumenumusic, mnudeckmusic, mnubattlemusic, mnuadcantagemusic, mnudisadvantagemusic });
                        }
                    }
                    if (contentView == ContentType.Sound_Effects)
                    {
                        ToolStripMenuItem mnuplaymusic = new ToolStripMenuItem("Play");

                        mnuplaymusic.Click += new EventHandler(PlayMusic);

                        mnu.Items.AddRange(new ToolStripItem[] { mnuplaymusic, new ToolStripSeparator() });
                    }

                    mnu.Show(this, e.Location);

                }
                else
                {
                    ContextMenuStrip mnu = new ContextMenuStrip();
                    ToolStripMenuItem mnuAdd = new ToolStripMenuItem("Add");

                    mnuAdd.Click += new EventHandler(AddNewAsset);

                    mnu.Items.AddRange(new ToolStripItem[] { mnuAdd });
                    mnu.Show(this, e.Location);
                }
            }
        }

        void mnumenumusic_Click(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            installmusic(Data[contentView].AssetPath + item.Text + Data[contentView].FileType, Data[contentView].GameItem + "menu" + Data[contentView].FileType);
        }
        void mnudeckmusic_Click(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            installmusic(Data[contentView].AssetPath + item.Text + Data[contentView].FileType, Data[contentView].GameItem + "deck" + Data[contentView].FileType);
        }
        void mnubattlemusic_Click(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            installmusic(Data[contentView].AssetPath + item.Text + Data[contentView].FileType, Data[contentView].GameItem + "song" + Data[contentView].FileType);
        }
        void mnuadcantagemusic_Click(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            installmusic(Data[contentView].AssetPath + item.Text + Data[contentView].FileType, Data[contentView].GameItem + "song-advantage" + Data[contentView].FileType);
        }
        void mnudisadvantagemusic_Click(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            installmusic(Data[contentView].AssetPath + item.Text + Data[contentView].FileType, Data[contentView].GameItem + "song-disadvantage" + Data[contentView].FileType);
        }

        void PlayMusic(object sender, EventArgs args)
        {
            ListViewItem item = this.ContentList.SelectedItems[0];
            string combinepath = Path.Combine(Application.ExecutablePath, "../");
            string fullpath = Path.GetFullPath(combinepath);
            string musicpath = Path.GetFullPath(fullpath + Data[contentView].AssetPath + item.Text + Data[contentView].FileType);
            if (File.Exists(musicpath))
            {
                MessageBox.Show("Not Implemented.");

                //Process.Start( "wmplayer.exe",musicpath);
            }

        }

        void installmusic(string file, string replacefile)
        {
            try
            {
                File.Copy(file, replacefile, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public string OpenFileWindow(string title, string filefilter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Application.ExecutablePath;
            string relativePath = Path.Combine(assemblyLocation, "../");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = title;

            fileDialog.Filter = filefilter;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                return fileDialog.FileName;
            }

            return null;
        }

        public void AddNewAsset(object sender, EventArgs args)
        {

            string filetoadd = OpenFileWindow("Add " + contentView.ToString(), Data[contentView].FileType.ToUpper() + "(*" + Data[contentView].FileType + ")|*" + Data[contentView].FileType + ";");

            if (filetoadd == null) return;
            try
            {
                string filename = Path.GetFileNameWithoutExtension(filetoadd);
                if (Data[contentView].FileType == ".png" || Data[contentView].FileType == ".jpg")
                {
                    if (MessageBox.Show("Resize image to the correct size?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ImageResizer.SaveImage(filetoadd, Data[contentView].AssetPath + filename + Data[contentView].FileType,
                            (Data[contentView].FileType == ".png" ? ImageFormat.Png : ImageFormat.Jpeg), Data[contentView].ImageSize);
                    }
                    else
                    {
                        File.Copy(filetoadd, Data[contentView].AssetPath + filename + Data[contentView].FileType);
                    }


                }
                else
                {
                    File.Copy(filetoadd, Data[contentView].AssetPath + filename + Data[contentView].FileType);
                }
                RefreshList();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        public void AddNewAsset(string path)
        {
            if (path == null) return;
            try
            {
                string filename = Path.GetFileNameWithoutExtension(path);
                File.Copy(path, Data[contentView].AssetPath + filename + Data[contentView].FileType);
                RefreshList();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public void ApplyTheme(string themename)
        {
            foreach (ThemeObject themeobject in Themes[themename].Items)
            {
                InstallAsset(themeobject.Type, themeobject.Filepath);
            }
        }

        private void PreviewBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig("ygopro:/" + Program.Config.ServerAddress + "/" + Program.Config.GamePort + "/20000,U,Edit");
            LauncherHelper.RunGame("-r");
        }

        private void AddThemeBtn_Click(object sender, EventArgs e)
        {
            Input_frm form = new Input_frm("Add Theme", "Enter theme name", "Add", "Cancel");
           
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Themes.ContainsKey(form.InputBox.Text))
                {
                    MessageBox.Show("Theme already exsists!", "Error", MessageBoxButtons.OK);
                    return;
                }
                AddTheme(form.InputBox.Text);
                ThemeSelect.Items.Add(form.InputBox.Text);
                ThemeSelect.SelectedItem = form.InputBox.Text;
            }
        }

        private void RemoveThemeBtn_Click(object sender, EventArgs e)
        {
            if (ThemeSelect.Text == "") return;
            if (MessageBox.Show("Are you sure you want to remove " + ThemeSelect.SelectedItem.ToString(), "Remove theme", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                File.Delete("Assets/Themes/" + ThemeSelect.SelectedItem.ToString() + ".YGOTheme");
                Themes.Remove(ThemeSelect.SelectedItem.ToString());
                ThemeSelect.Items.Remove(ThemeSelect.SelectedItem);
                
            }
        }

        private void AddContentBtn_Click(object sender, EventArgs e)
        {
            AddNewAsset(null, EventArgs.Empty);
        }

        private void BackUpBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to back up the current game Textures?", "Backup Textures", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (!ThemeExists("Backup Theme")) AddTheme("Backup Theme");
                SelectedTheme = "Backup Theme";
                ThemeSelect.SelectedItem = "Backup Theme";

                foreach (ContentType type in ItemKeys())
                {
                    if (type != ContentType.Music && type != ContentType.Sound_Effects)
                    {
                        try
                        {
                            string GeneratedString = LauncherHelper.GenerateString();
                            File.Copy(Data[type].GameItem, Data[type].AssetPath + GeneratedString + Data[type].FileType);
                            AddThemeItem(type, Data[type].AssetPath + GeneratedString + Data[type].FileType, GeneratedString);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                RefreshList();
                RefreshInstalledThemeItems();
                SaveTheme("Backup Theme");
                ThemeSelect.Items.Add("Backup Theme");
                ThemeSelect.SelectedItem = "Backup Theme";
            }
        }
    }

    public class Content
    {
        public string AssetPath;
        public string GameItem;
        public string FileType;
        public ImageList Images;
        public Size IconSize;
        public Size ImageSize;
    }

    [Serializable]
    public class Theme
    {
        public string ThemeName;
        public List<ThemeObject> Items = new List<ThemeObject>();

        public void AddItem(ContentType type, string filepath, string filename)
        {
            foreach (ThemeObject item in Items)
            {
                if (item.Type == type)
                {
                    item.Filepath = filepath;
                    item.Filename = filename;
                    return;
                }
            }
            Items.Add(new ThemeObject() { Type = type, Filepath = filepath, Filename = filename });
        }

        public void RemoveItem(ContentType type)
        {
            foreach (ThemeObject item in Items)
            {
                if (item.Type == type)
                {
                    Items.Remove(item);
                    return;
                }
            }
        }

        public ThemeObject getThemeObject(ContentType type)
        {
            foreach (ThemeObject item in Items)
            {
                if (item.Type == type)
                {
                    return item;
                }
            }
            return null;
        }
    }
    [Serializable]
    public class ThemeObject
    {
        public ContentType Type;
        public string Filepath;
        public string Filename;
    }

}
