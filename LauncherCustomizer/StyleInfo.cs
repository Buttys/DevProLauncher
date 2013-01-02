using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace YGOPro_Launcher
{
    public class StyleInfo
    {
        //store custom style information here
        public Color FormColor = Color.Black;
        public Color FormTextColor = Color.White;
        public Color LabelTextColor = Color.White;
        public Color LabelBackColor = Color.Black;

        public static void Save(string filepath,StyleInfo info)
        {
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(StyleInfo));
                TextWriter textWriter = new StreamWriter(filepath);
                serializer.Serialize(textWriter, info);
                textWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Saving " + filepath);
            }
        }

        public static StyleInfo Load(string filepath)
        {
            if (!Directory.Exists(filepath)
                || !File.Exists(filepath))
            {
                MessageBox.Show("File not found");
                return new StyleInfo();
            }
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(StyleInfo));
                TextReader textReader = new StreamReader(filepath);
                StyleInfo style = (StyleInfo)deserializer.Deserialize(textReader);
                textReader.Close();
                return style;

            }
            catch (Exception)
            {
                MessageBox.Show("Error Laoding " + filepath);
                return new StyleInfo();
            }
        }
    }
}
