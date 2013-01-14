using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace YGOPro_Launcher.Config
{
    public class LanguageManager
    {

        public LanguageInfo Translation { get; set; }

        public bool Loaded { get; set; }

        public const string Path = "Language/";

        public LanguageManager()
        {
            Translation = new LanguageInfo();
        }

        public void Save(string language)
        {            
            if (!Directory.Exists(Path + language))
                Directory.CreateDirectory(Path + language);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LanguageInfo));
                TextWriter textWriter = new StreamWriter("../../"+ Path + language + "/" + language + ".xml");
                serializer.Serialize(textWriter, Translation);
                textWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Saving " + language);
            }
        }

        public void Load(string language)
        {
            if (!Directory.Exists(Path + language)
                || !File.Exists(Path + language + "/" + language + ".xml"))
            {
                MessageBox.Show("File not found");
                return;
            }
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(LanguageInfo));
                TextReader textReader = new StreamReader(Path + language + "/" + language + ".xml");
                Translation = (LanguageInfo)deserializer.Deserialize(textReader);
                textReader.Close();
                Loaded = true;
                if(File.Exists(Path + language + "/strings.conf"))
                    File.Copy(Path + language + "/strings.conf", "strings.conf", true);
                if (File.Exists(Path + language + "/cards.cdb"))
                    File.Copy(Path + language + "/cards.cdb", "cards.cdb", true);
                else
                {
                    if (File.Exists(Path +"English/cards.cdb"))
                        File.Copy(Path + "English/cards.cdb", "cards.cdb", true);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error Laoding " + language);
            }
        }
    }
}
