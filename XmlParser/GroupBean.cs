using System;
using System.Collections.Generic;
using System.Text;

namespace XmlParser
{
    public class GroupBean
    {
        public string id;
        public string group_type;
        public List<NodeBean> lchildern;

        public GroupBean()
        {
            lchildern = new List<NodeBean>();
        }

        public static bool Equals(GroupBean bean1, GroupBean bean2)
        {
            return (bean1 == null || bean2 == null) ? ((bean1 == null) ? (bean2 == null) : false) : (bean1.id.Equals(bean2.id));
        }
    }
}
