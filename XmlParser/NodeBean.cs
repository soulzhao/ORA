using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XmlParser
{
    public class NodeBean
    {
        public string id;
        public Hashtable attributes;
        public GroupBean group;

        public NodeBean()
        {
            this.attributes = new Hashtable();
        }

        public NodeBean(string id, GroupBean group)
        {
            this.id = id;
            this.group = group;
            this.attributes = new Hashtable();
        }

        public bool AddAttribute(object name, string value)
        {
            try
            {
                attributes.Add(name, value);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool Equals(NodeBean bean1, NodeBean bean2)
        {
            return (bean1 == null || bean2 == null) ? ((bean1 == null) ? (bean2 == null) : false) : (bean1.id.Equals(bean2.id));
        }

        public bool Equals(NodeBean bean)
        {
            return (this == null || bean == null) ? ((this == null) ? (bean == null) : false) : (this.id.Equals(bean.id));
        }
    }
}
