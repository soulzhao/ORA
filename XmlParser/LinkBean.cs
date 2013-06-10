using System;
using System.Collections.Generic;
using System.Text;

namespace XmlParser
{
    public class LinkBean
    {
        public NodeBean source;
        public NodeBean target;

        public LinkBean() { }

        public LinkBean(NodeBean src, NodeBean tag)
        {
            this.source = src;
            this.target = tag;
        }

        public bool Equals(LinkBean bean)
        {
            if (bean == null) return false;

            return (source.Equals(bean.source)
                && target.Equals(bean.target));
        }
    }
}
