using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XmlParser
{
    class XmlFileHandler
    {
        public static void SaveTo(XmlDocument doc, string path)
        {
            XmlTextWriter writer = new XmlTextWriter(path, null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }

        public static void ReadFrom(ref XmlDocument doc, string path)
        {
            XmlReader reader = XmlReader.Create(path);
            if (doc == null) 
            {
                doc = new XmlDocument();
            }
            doc.Load(path);
            reader.Close();
        }
    }
}
