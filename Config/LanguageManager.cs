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

        private string path = "Language/";

        public LanguageManager()
        {
            Translation = new LanguageInfo();
        }

        public void Save(string language)
        {            
            if (!Directory.Exists(path + language))
                Directory.CreateDirectory(path + language);
            XmlSerializer serializer = new XmlSerializer(typeof(LanguageInfo));
            TextWriter textWriter = new StreamWriter(path + language + "/" + language + ".xml");
            serializer.Serialize(textWriter, Translation);
            textWriter.Close();
        }

        public void Load(string language)
        {
            if (!Directory.Exists(path + language)
                || !File.Exists(path + language + "/" + language + ".xml"))
            {
                MessageBox.Show("File not found");
                return;
            }
            XmlSerializer deserializer = new XmlSerializer(typeof(LanguageInfo));
            TextReader textReader = new StreamReader(path + language + "/" + language + ".xml");
            Translation = (LanguageInfo)deserializer.Deserialize(textReader);
            textReader.Close();
            Loaded = true;
        }
    }
}
