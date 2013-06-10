using System;
using System.Collections.Generic;
using System.Text;

namespace XmlParser
{
    public class NetworkBean
    {
        public GroupBean group1 = null;
        public GroupBean group2 = null;
        public List<LinkBean> llb;

        public NetworkBean()
        {
             llb = new List<LinkBean>();
        }

        public NetworkBean(GroupBean g1, GroupBean g2)
        {
            this.group1 = g1;
            this.group2 = g2;
            llb = new List<LinkBean>();
        }
    }
}
