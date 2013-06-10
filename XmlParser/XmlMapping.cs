using System;
using System.Collections.Generic;
using System.Text;

namespace XmlParser
{
    class XmlMapping
    {
        public static GroupBean findGroupById(string id)
        {
            if(MyXml.groups == null) return null;

            int i = 0;
            for (; i < MyXml.groups.Count; i++)
            {
                if (MyXml.groups[i].id.Equals(id)) break;
            }

            return MyXml.groups[i];
        }

        public static NodeBean findNodeBeanById(string id, GroupBean gb)
        {
            if (gb == null) return null;

            return gb.lchildern.Find(delegate(NodeBean nodebean){
                return nodebean.id.Equals(id);
            });
        }

        private XmlParser xmlParser;

        public XmlMapping(string filename)
        {
            xmlParser = new XmlParser(filename);
            initialize();
        }

        private void initialize()
        {
            initializeGroups();
            initializeNetworks();
        }

        private void initializeGroups()
        {
            xmlParser.getGroups(ref MyXml.groups);
        }

        private void initializeNetworks()
        {
            xmlParser.getNetworks(ref MyXml.networks);
        }
    }
}
