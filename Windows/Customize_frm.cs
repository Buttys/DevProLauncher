using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Drawing.Imaging;
using DevProLauncher.Windows.Enums;
using DevProLauncher.Helpers;
using DevProLauncher.Windows.MessageBoxs;

namespace DevProLauncher.Windows
{
    public sealed partial class CustomizeFrm : Form
    {

        public Dictionary<ContentType, Content> Data = new Dictionary<ContentType, Content>();
        public ContentType ContentView = ContentType.Covers;
        readonly Dictionary<string, Theme> m_themes = new Dictionary<string, Theme>();
        public string SelectedTheme = "None";

        public CustomizeFrm()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            ViewSelect.SelectedIndex = 0;
            Data.Add(ContentType.Covers, new Content { AssetPath = "Assets/Covers/", IconSize = new Size(177, 252), GameItem = "textures\\cover.jpg", FileType = ".jpg", ImageSize = new Size(178,254) });
            Data.Add(ContentType.Backgrounds, new Content { AssetPath = "Assets/Backgrounds/", IconSize = new Size(256, 256), GameItem = "textures\\bg.jpg", FileType = ".jpg", ImageSize = new Size(1024, 640) });
            Data.Add(ContentType.GameBackgrounds, new Content { AssetPath = "Assets/GameBackgrounds/", IconSize = new Size(256, 256), GameItem = "textures\\bg2.jpg", FileType = ".jpg", ImageSize = new Size(1024, 640) });
            Data.Add(ContentType.Field, new Content { AssetPath = "Assets/Field/", IconSize = new Size(256, 256), GameItem = "textures\\field.png", FileType = ".png", ImageSize = new Size(256, 256) });
            Data.Add(ContentType.FieldTransparent, new Content { AssetPath = "Assets/FieldTransparent/", IconSize = new Size(256, 256), GameItem = "textures\\field-transparent.png", FileType = ".png", ImageSize = new Size(256, 256) });
            Data.Add(ContentType.Numbers, new Content { AssetPath = "Assets/Numbers/", IconSize = new Size(256, 256), GameItem = "textures\\number.png", FileType = ".png", ImageSize = new Size(320, 256) });
            Data.Add(ContentType.Mask, new Content { AssetPath = "Assets/Mask/", IconSize = new Size(254, 254), GameItem = "textures\\mask.png", FileType = ".png", ImageSize = new Size(254, 254) });
            Data.Add(ContentType.AttackIcon, new Content { AssetPath = "Assets/AttackIcon/", IconSize = new Size(128, 128), GameItem = "textures\\attack.png", FileType = ".png", ImageSize = new Size(128, 128) });
            Data.Add(ContentType.ActivateCircle, new Content { AssetPath = "Assets/ActivateCircle/", IconSize = new Size(128, 128), GameItem = "textures\\\\act.png", FileType = ".png", ImageSize = new Size(128, 128) });
            Data.Add(ContentType.Chain, new Content { AssetPath = "Assets/ChainIcon/", IconSize = new Size(128, 128), GameItem = "textures\\chain.png", FileType = ".png", ImageSize = new Size(128, 128) });
            Data.Add(ContentType.Target, new Content { AssetPath = "Assets/TargetIcon/", IconSize = new Size(34, 34), GameItem = "textures\\target.png", FileType = ".png", ImageSize = new Size(34, 34) });
            Data.Add(ContentType.Equip, new Content { AssetPath = "Assets/EquipIcon/", IconSize = new Size(34, 34), GameItem = "textures\\equip.png", FileType = ".png", ImageSize = new Size(34, 34) });
            Data.Add(ContentType.Rock, new Content { AssetPath = "Assets/Rock/", IconSize = new Size(89, 128), GameItem = "textures\\f2.jpg", FileType = ".jpg", ImageSize = new Size(89, 128) });
            Data.Add(ContentType.Paper, new Content { AssetPath = "Assets/Paper/", IconSize = new Size(89, 128), GameItem = "textures\\f3.jpg", FileType = ".jpg", ImageSize = new Size(89, 128) });
            Data.Add(ContentType.Scissors, new Content { AssetPath = "Assets/Scissors/", IconSize = new Size(89, 128), GameItem = "textures\\f1.jpg", FileType = ".jpg", ImageSize = new Size(89, 128) });
            Data.Add(ContentType.LifePointsColor, new Content { AssetPath = "Assets/LPColor/", IconSize = new Size(16, 16), GameItem = "textures\\lp.png", FileType = ".png", ImageSize = new Size(16, 16) });
            Data.Add(ContentType.LifePointsBar, new Content { AssetPath = "Assets/LPBar/", IconSize = new Size(200, 20), GameItem = "textures\\lpf.png", FileType = ".png", ImageSize = new Size(200, 20) });
            Data.Add(ContentType.Negated, new Content { AssetPath = "Assets/Negated/", IconSize = new Size(128, 128), GameItem = "textures\\negated.png", FileType = ".png", ImageSize = new Size(128, 128) });
            Data.Add(ContentType.Music, new Content { AssetPath = "Assets/Music/", IconSize = new Size(40, 40), GameItem = "sound\\", FileType = ".mp3" });
            Data.Add(ContentType.SoundEffects, new Content { AssetPath = "Assets/SoundEffects/", IconSize = new Size(40, 40), GameItem = "sound\\", FileType = ".wav" });



            ContentList.View = View.LargeIcon;
            LoadAssets();
            LoadThemeFiles();

            ContentList.MouseUp += OnListViewMouseUp;
            ViewSelect.SelectedIndexChanged += SelectedIndex_Changed;
            ThemeSelect.SelectedIndexChanged += SelectedTheme_Changed;

            ContentView = ContentType.Covers;
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
            var serializer = new XmlSerializer(typeof(Theme));
            TextWriter textWriter = new StreamWriter("Assets/Themes/" + m_themes[themename].ThemeName + ".YGOTheme");
            serializer.Serialize(textWriter, m_themes[themename]);
            textWriter.Close();
        }

        public List<object> ItemKeys()
        {
            return Data.Keys.Cast<object>().ToList();
        }

        public List<object> ThemeKeys()
        {
            return m_themes.Keys.Cast<object>().ToList();
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
// ReSharper disable AssignNullToNotNullAttribute
                    ThemeSelect.Items.Add(Path.GetFileNameWithoutExtension(file));
// ReSharper restore AssignNullToNotNullAttribute
                }
            }

        }

        void LoadTheme(string filepath)
        {       
            var deserializer = new XmlSerializer(typeof(Theme));
            TextReader textReader = new StreamReader(filepath);
            try
            {
    // ReSharper disable AssignNullToNotNullAttribute
                if (!m_themes.ContainsKey(Path.GetFileNameWithoutExtension(filepath)))
                    m_themes[Path.GetFileNameWithoutExtension(filepath)] = (Theme)deserializer.Deserialize(textReader);
    // ReSharper restore AssignNullToNotNullAttribute
                textReader.Close();
            }
            catch (Exception)
            {
                textReader.Close();
                if(File.Exists(filepath))
                    File.Delete(filepath);
            }

        }
        public ThemeObject GetThemeObject(ContentType type)
        {
            if (m_themes.ContainsKey(SelectedTheme))
            {
                return m_themes[SelectedTheme].GetThemeObject(type);
            }
            return null;
        }

        public void AddTheme(string themeName)
        {
            if (!m_themes.ContainsKey(themeName))
            {
                m_themes.Add(themeName, new Theme { ThemeName = themeName });
                SaveTheme(themeName);
            }
            else
            {
                MessageBox.Show("Theme already exists.", "Exists");
            }
        }
        public bool ThemeExists(string name)
        {
            return m_themes.ContainsKey(name);
        }

        public void AddThemeItem(ContentType type, string path, string filename)
        {
            m_themes[SelectedTheme].AddItem(type, path, filename);
        }

        public Content GetCurrentItemSet()
        {
            return Data[ContentView];
        }

        void LoadAssets()
        {
            ContentList.Items.Clear();
            foreach (ContentType key in ItemKeys())
            {
                if (!Directory.Exists(Data[key].AssetPath)) Directory.CreateDirectory(Data[key].AssetPath);
                string[] itempath = Directory.GetFiles(Data[key].AssetPath);
                Data[key].Images = new ImageList
                    {
                        TransparentColor = Color.Transparent,
                        ColorDepth = ColorDepth.Depth16Bit,
                        ImageSize = Data[key].IconSize
                    };
                foreach (string item in itempath)
                {
                    if (key == ContentType.Music || key == ContentType.SoundEffects)
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
            ContentList.Items.Clear();
            ContentList.LargeImageList = Data[ContentView].Images;
            string[] itempaths = Directory.GetFiles(Data[ContentView].AssetPath);
            foreach (string item in itempaths)
            {
                var imagename = Path.GetFileNameWithoutExtension(item);
                ContentList.Items.Add(imagename, imagename);
            }
        }

        public void SelectedIndex_Changed(object sender, EventArgs e)
        {
            var control = (ComboBox)sender;
            if (control.Text == "Covers") ContentView = ContentType.Covers;
            if (control.Text == "Backgrounds") ContentView = ContentType.Backgrounds;
            if(control.Text == "GameBackgrounds") ContentView = ContentType.GameBackgrounds;
            if (control.Text == "Attack") ContentView = ContentType.AttackIcon;
            if (control.Text == "Activate") ContentView = ContentType.ActivateCircle;
            if (control.Text == "Chain") ContentView = ContentType.Chain;
            if (control.Text == "Equip") ContentView = ContentType.Equip;
            if (control.Text == "Rock") ContentView = ContentType.Rock;
            if (control.Text == "Paper") ContentView = ContentType.Paper;
            if (control.Text == "Sissors") ContentView = ContentType.Scissors;
            if (control.Text == "Life Points Color") ContentView = ContentType.LifePointsColor;
            if (control.Text == "Life Points Bar") ContentView = ContentType.LifePointsBar;
            if (control.Text == "Target") ContentView = ContentType.Target;
            if (control.Text == "Negated") ContentView = ContentType.Negated;
            if (control.Text == "Music") ContentView = ContentType.Music;
            if (control.Text == "SoundEffects") ContentView = ContentType.SoundEffects;
            if (control.Text == "Field") ContentView = ContentType.Field;
            if (control.Text == "FieldTransparent") ContentView = ContentType.FieldTransparent;
            if (control.Text == "Mask") ContentView = ContentType.Mask;
            if (control.Text == "Numbers") ContentView = ContentType.Numbers;

            ChangeImageView();
            RefreshInstalledThemeItems();
        }

        public void SelectedTheme_Changed(object sender, EventArgs e)
        {
            var box = (ComboBox)sender;
            SelectedTheme = box.Text;
            RefreshList();
            RefreshInstalledThemeItems();
        }

        public void RefreshInstalledThemeItems()
        {
            foreach (ListViewItem item in ContentList.Items)
            {
                if (GetThemeObject(ContentView) != null)
                {
                    if (item.Text == GetThemeObject(ContentView).Filename)
                    {
                        item.BackColor = Color.Green;
                        item.ForeColor = Color.White;
                    }
                }
            }
        }

        void AddImage(ImageList type, string path)
        {
            var imagename = Path.GetFileNameWithoutExtension(path);
            try
            {
                type.Images.Add(imagename, Image.FromStream(new MemoryStream(File.ReadAllBytes(path))));
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
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Somthing when wrong.");
            }
        }
        void AddIconImage(ImageList type, string path)
        {
            var imagename = Path.GetFileNameWithoutExtension(path);
            var iconforFile = Icon.ExtractAssociatedIcon(path);
            type.Images.Add(imagename, iconforFile ?? SystemIcons.WinLogo);

        }

        public void InstallAsset(object sender, EventArgs args)
        {
            ListViewItem item = ContentList.SelectedItems[0];
            string itempath = Data[ContentView].AssetPath + item.Text;
            if (File.Exists(itempath + Data[ContentView].FileType))
            {
                try
                {
                    File.Copy(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem, true);
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
                }
            }

            //MessageBox.Show("Success!", "Install");


        }


        public void DealteAsset(object sender, EventArgs args)
        {

            ListViewItem item = ContentList.SelectedItems[0];
            if (MessageBox.Show("Are you sure you want to delete " + item.Text, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Data[ContentView].Images.Images.RemoveByKey(item.Text);
                ContentList.Items.Remove(item);
                File.Delete(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType);
            }

        }
        void ApplyToTheme(object sender, EventArgs args)
        {
            ListViewItem item = ContentList.SelectedItems[0];
            if (MessageBox.Show("Apply " + item.Text + "  to current theme?  ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //if (ApplyTheme != null) ApplyTheme(Data[contentView].AssetPath + item.Text + Data[contentView].FileType);
                if (SelectedTheme == "None") if (MessageBox.Show("No theme selected", "No theme") == DialogResult.OK) return;

                m_themes[SelectedTheme].AddItem(ContentView, Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, item.Text);
                SaveTheme(SelectedTheme);
                RefreshList();
                RefreshInstalledThemeItems();
            }
        }

        void RemoveFromTheme(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            if (MessageBox.Show("Remove " + item.Text + "  from current theme?  ", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //if (ApplyTheme != null) ApplyTheme(Data[contentView].AssetPath + item.Text + Data[contentView].FileType);
                if (SelectedTheme == "None") if (MessageBox.Show("No theme selected", "No theme") == DialogResult.OK) return;

                m_themes[SelectedTheme].RemoveItem(ContentView);
                SaveTheme(SelectedTheme);
                RefreshList();
                RefreshInstalledThemeItems();
            }
        }

        public void DealteTheme(string themename)
        {
            if (File.Exists("Assets/Themes/" + themename + ".YGOTheme")) File.Delete("Assets/Themes/" + themename + ".YGOTheme");
            m_themes.Remove(themename);
        }

        private void OnListViewMouseUp(object sender, MouseEventArgs e)
        {
            var listView = sender as ListView;

            if (e.Button == MouseButtons.Right)
            {
                if (listView != null)
                {
                    var item = listView.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        var mnu = new ContextMenuStrip();
                        if (ContentView != ContentType.Music && ContentView != ContentType.SoundEffects)
                        {
                            var mnuInstall = new ToolStripMenuItem("Install");
                            var mnuApplyTheme = new ToolStripMenuItem("Set to current theme");
                            var mnuRemoveFromTheme = new ToolStripMenuItem("Remove from current theme");
                            var mnuDealte = new ToolStripMenuItem("Delete");

                            mnuInstall.Click += InstallAsset;
                            mnuApplyTheme.Click += ApplyToTheme;
                            mnuDealte.Click += DealteAsset;
                            mnuRemoveFromTheme.Click += RemoveFromTheme;

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
                            if (ContentView == ContentType.Music)
                            {
                                var mnuplaymusic = new ToolStripMenuItem("Play");
                                var mnumenumusic = new ToolStripMenuItem("Apply to menu music");
                                var mnudeckmusic = new ToolStripMenuItem("Apply to deck editor music");
                                var mnubattlemusic = new ToolStripMenuItem("Apply to battle music");
                                var mnuadcantagemusic = new ToolStripMenuItem("Apply to advantage battle music");
                                var mnudisadvantagemusic = new ToolStripMenuItem("Apply to disadvantage battle music");
                                var mnuvictorymusic = new ToolStripMenuItem("Apply to Victory music");
                                var mnulosemusic = new ToolStripMenuItem("Apply to Lose music");

                                mnuplaymusic.Click += PlayMusic;
                                mnumenumusic.Click += mnumenumusic_Click;
                                mnudeckmusic.Click += mnudeckmusic_Click;
                                mnubattlemusic.Click += mnubattlemusic_Click;
                                mnuadcantagemusic.Click += mnuadcantagemusic_Click;
                                mnudisadvantagemusic.Click += mnudisadvantagemusic_Click;
                                mnuvictorymusic.Click += mnuvictorymusic_Click;
                                mnulosemusic.Click += mnulosemusic_Click;

                                mnu.Items.AddRange(new ToolStripItem[] { mnuplaymusic, new ToolStripSeparator(), mnumenumusic, mnudeckmusic, mnubattlemusic, mnuadcantagemusic, mnudisadvantagemusic });
                            }
                        }
                        if (ContentView == ContentType.SoundEffects)
                        {
                            var mnuplaymusic = new ToolStripMenuItem("Play");

                            mnuplaymusic.Click += PlayMusic;

                            mnu.Items.AddRange(new ToolStripItem[] { mnuplaymusic, new ToolStripSeparator() });
                        }

                        mnu.Show(this, e.Location);

                    }
                    else
                    {
                        var mnu = new ContextMenuStrip();
                        var mnuAdd = new ToolStripMenuItem("Add");

                        mnuAdd.Click += AddNewAsset;

                        mnu.Items.AddRange(new ToolStripItem[] { mnuAdd });
                        mnu.Show(this, e.Location);
                    }
                }
            }
        }

        void mnumenumusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "menu" + Data[ContentView].FileType);
        }
        void mnudeckmusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "deck" + Data[ContentView].FileType);
        }
        void mnubattlemusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "song" + Data[ContentView].FileType);
        }
        void mnuadcantagemusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "song-advantage" + Data[ContentView].FileType);
        }
        void mnudisadvantagemusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "song-disadvantage" + Data[ContentView].FileType);
        }
        void mnuvictorymusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "duelwin" + Data[ContentView].FileType);
        }
        void mnulosemusic_Click(object sender, EventArgs args)
        {
            var item = ContentList.SelectedItems[0];
            installmusic(Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType, Data[ContentView].GameItem + "duellose" + Data[ContentView].FileType);
        }

        void PlayMusic(object sender, EventArgs args)
        {
            ListViewItem item = ContentList.SelectedItems[0];
            string combinepath = Path.Combine(Application.ExecutablePath, "../");
            string fullpath = Path.GetFullPath(combinepath);
            string musicpath = Path.GetFullPath(fullpath + Data[ContentView].AssetPath + item.Text + Data[ContentView].FileType);
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
            var fileDialog = new OpenFileDialog();

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

            string filetoadd = OpenFileWindow("Add " + ContentView.ToString(), Data[ContentView].FileType.ToUpper() + "(*" + Data[ContentView].FileType + ")|*" + Data[ContentView].FileType + ";");

            if (filetoadd == null) return;
            try
            {
                var filename = Path.GetFileNameWithoutExtension(filetoadd);
                if (Data[ContentView].FileType == ".png" || Data[ContentView].FileType == ".jpg")
                {
                    if (MessageBox.Show("Resize image to the correct size?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ImageResizer.SaveImage(filetoadd, Data[ContentView].AssetPath + filename + Data[ContentView].FileType,
                            (Data[ContentView].FileType == ".png" ? ImageFormat.Png : ImageFormat.Jpeg), Data[ContentView].ImageSize);
                    }
                    else
                    {
                        File.Copy(filetoadd, Data[ContentView].AssetPath + filename + Data[ContentView].FileType);
                    }


                }
                else
                {
                    File.Copy(filetoadd, Data[ContentView].AssetPath + filename + Data[ContentView].FileType);
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
                File.Copy(path, Data[ContentView].AssetPath + filename + Data[ContentView].FileType);
                RefreshList();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        public void ApplyTheme(string themename)
        {
            foreach (ThemeObject themeobject in m_themes[themename].Items)
            {
                InstallAsset(themeobject.Type, themeobject.Filepath);
            }
        }

        private void PreviewBtn_Click(object sender, EventArgs e)
        {
            LauncherHelper.GenerateConfig();
            LauncherHelper.RunGame("-r");
        }

        private void AddThemeBtn_Click(object sender, EventArgs e)
        {
            var form = new InputFrm("Add Theme", "Enter theme name", "Add", "Cancel");
           
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (m_themes.ContainsKey(form.InputBox.Text))
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
            if (MessageBox.Show("Are you sure you want to remove " + ThemeSelect.SelectedItem, "Remove theme", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                File.Delete("Assets/Themes/" + ThemeSelect.SelectedItem + ".YGOTheme");
                m_themes.Remove(ThemeSelect.SelectedItem.ToString());
                ThemeSelect.Items.Remove(ThemeSelect.SelectedItem);
                
            }
        }

        private void AddContentBtn_Click(object sender, EventArgs e)
        {
            AddNewAsset(null, EventArgs.Empty);
        }

        private void BackUpBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to back up the current game Textures?", "Backup Textures", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!ThemeExists("Backup Theme")) AddTheme("Backup Theme");
                SelectedTheme = "Backup Theme";
                ThemeSelect.SelectedItem = "Backup Theme";

                foreach (ContentType type in ItemKeys())
                {
                    if (type != ContentType.Music && type != ContentType.SoundEffects)
                    {
                        try
                        {
                            var generatedString = LauncherHelper.GenerateString();
                            File.Copy(Data[type].GameItem, Data[type].AssetPath + generatedString + Data[type].FileType);
                            AddThemeItem(type, Data[type].AssetPath + generatedString + Data[type].FileType, generatedString);
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
            Items.Add(new ThemeObject { Type = type, Filepath = filepath, Filename = filename });
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

        public ThemeObject GetThemeObject(ContentType type)
        {
            return Items.FirstOrDefault(item => item.Type == type);
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
