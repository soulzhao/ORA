using System;
using System.Collections.Generic;
using System.Text;

namespace XmlParser
{
    public class MyXml
    {
        public static List<GroupBean> groups = null;
        public static List<NetworkBean> networks = null;

        public MyXml(string filename)
        {
            XmlMapping mapping = new XmlMapping(filename);
        }

        public static NodeBean findNodeById(string j)
        {
            NodeBean nbean = null;
            foreach(GroupBean gbean in groups)
            {
                nbean = XmlMapping.findNodeBeanById(j, gbean);
                if (nbean != null) break;
            }
            return nbean;
        }

        public static GroupBean findGroupById(string id)
        {
            return groups.Find(delegate(GroupBean groupBean)
            {
                return groupBean.id.Equals(id);
            });
        }

        public static List<NodeBean> getAllNodes()
        {
            List<NodeBean> lnb = new List<NodeBean>();
            foreach (GroupBean gbean in groups)
            {
                lnb.AddRange(gbean.lchildern);
            }
            return lnb;
        }

        public static int getNodesCount()
        {
            return getAllNodes().Count;
        }

        public static List<LinkBean> getAllLinks()
        {
            List<LinkBean> llb = new List<LinkBean>();
            foreach (NetworkBean lb in networks)
            {
                llb.AddRange(lb.llb);
            }
            return llb;
        }
    }
}
