using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace XmlParser
{
    class XmlParser
    {
        private XmlDocument xmldoc = null;

        public XmlParser(string path)
        {
            XmlFileHandler.ReadFrom(ref xmldoc, path);
        }

        private XmlNodeList getNodeClasses()
        {
            return xmldoc.GetElementsByTagName("nodeclass");
        }

        public void getGroups(ref List<GroupBean> groups)
        {
            if (groups == null) groups = new List<GroupBean>();

            XmlNodeList xmlNodeList = getNodeClasses();
            foreach (XmlNode node in xmlNodeList)
            {
                GroupBean gb = new GroupBean();
                gb.id = getNodeAttrValue(node, AttrType.Id);
                gb.group_type = getNodeAttrValue(node, AttrType.Type);
                getNodes(gb, node);
                groups.Add(gb);
            }
        }

        private XmlNodeList getChildern(XmlNode nodeclass)
        {
            return nodeclass.ChildNodes;
        }

        private void getNodes(GroupBean gb, XmlNode nodeclass) 
        {
            XmlNodeList xmlNodeList = getChildern(nodeclass);
            foreach(XmlNode node in xmlNodeList)
            {
                NodeBean nodebean = new NodeBean();

                nodebean.id = getNodeAttrValue(node, AttrType.Id);
                nodebean.group = gb;

                XmlAttributeCollection collection = getNodeAttrs(node);
                foreach(XmlAttribute atrr in collection)
                {
                    nodebean.AddAttribute(atrr.Name, atrr.Value);
                }

                gb.lchildern.Add(nodebean);
            }
        }

        private XmlNodeList getNetworks()
        {
            return xmldoc.GetElementsByTagName("network");
        }

        public void getNetworks(ref List<NetworkBean> networks)
        {
            if (networks == null) networks = new List<NetworkBean>();
 
            XmlNodeList xmlNodeList = getNetworks();
            foreach (XmlNode node in xmlNodeList)
            {
                NetworkBean network = new NetworkBean();

                network.group1 = XmlMapping.findGroupById(getNodeAttrValue(node, AttrType.Target));
                network.group2 = XmlMapping.findGroupById(getNodeAttrValue(node, AttrType.Source));

                getLinks(network, node);

                networks.Add(network);
            }
        }

        private void getLinks(NetworkBean network, XmlNode networkNode)
        {
            XmlNodeList xmlNodeList = getChildern(networkNode);
            foreach (XmlNode node in xmlNodeList)
            {
                LinkBean link = new LinkBean();
                string target = getNodeAttrValue(node, AttrType.Target);
                string source = getNodeAttrValue(node, AttrType.Source);
                link.target = XmlMapping.findNodeBeanById(target, network.group2);
                link.source = XmlMapping.findNodeBeanById(source, network.group1);

                network.llb.Add(link);
            }
        }

        public XmlNodeList getCertainNodeChildern(XmlNode father)
        {
            return father.ChildNodes;
        }

        enum AttrType
        {
            Id,
            Title,
            Target,
            TargetType,
            SourceType,
            Source,
            Type,
            Value
        }

        private String getNodeAttrValue(XmlNode node, AttrType atr)
        {
            string type = null;
            switch (atr)
            {
                case AttrType.Id: type = "id"; break;
                case AttrType.Source: type = "source"; break;
                case AttrType.SourceType: type = "sourceType"; break;
                case AttrType.Target: type = "target"; break;
                case AttrType.TargetType: type = "targetType"; break;
                case AttrType.Title: type = "title"; break;
                case AttrType.Type: type = "type"; break;
                case AttrType.Value: type = "value"; break;
            }
            if (String.IsNullOrEmpty(type)) return null;
            return node.Attributes[type].Value;
        }

        public XmlAttributeCollection getNodeAttrs(XmlNode node)
        {
            return node.Attributes;
        }

        public XmlNode findNodeById(string value)
        {
            return xmldoc.SelectSingleNode("//nodes/nodeclass/node[@id=" + value + "]");
        }
    }
}
