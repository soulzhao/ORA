using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using XmlParser;
using ORA.Utils;

namespace ORA.draw
{
    class Drawer
    {
        private Graphics graph;
        private int width;
        private int height;

        static List<PointDrawer> pl = null;
        static List<RouteDrawer> lrd = null;

        public Drawer(Graphics g, int w, int h)
        {
            graph = g;
            width = w;
            height = h;
        }

        private RouteDrawer DrawConverter(LinkBean linkbean)
        {
            // 建立在xml文档中ID按顺序排列的基础上
            int source = int.Parse(linkbean.source.id);
            int target = int.Parse(linkbean.target.id);
            return new RouteDrawer(pl[source], pl[target]);
        }

        private List<RouteDrawer> DrawConverter(List<LinkBean> linkbean)
        {
            List<RouteDrawer> lrd = new List<RouteDrawer>();
            foreach(LinkBean lb in linkbean)
            {
                lrd.Add(DrawConverter(lb));
            }
            return lrd;
        }

        private PointDrawer DrawConverter(NodeBean nodebean)
        {
            return new PointDrawer(nodebean.id,
                nodebean.attributes["name"].ToString(),
                RandomShuffle.RandDouble(), 
                RandomShuffle.RandDouble());
        }

        private List<PointDrawer> DrawConverter(List<NodeBean> list_nodebean)
        {
            List<PointDrawer> lp = new List<PointDrawer>();
            foreach(NodeBean nb in list_nodebean)
            {
                lp.Add(DrawConverter(nb));
            }
            return lp;
        }

        /// <summary>
        /// 绘制初始画板
        /// </summary>
        public void DrawSimuMap(List<NodeBean> lnb, List<LinkBean> lnr)
        {
            if(pl == null)
            {
                pl = (DrawConverter(lnb));
            }

            // 将pl转化为实际值
            List<PointDrawer> lpd = convertToTrueListPoints(pl);
            PointDrawer.Draw(PointType.NORMAL, lpd.ToArray(), this.graph);

            //
            List<TextDrawer> tl = new List<TextDrawer>();
            foreach(PointDrawer p in lpd)
            {
                tl.Add(new TextDrawer(p.name, (int)(p.Longtitude + 10), (int)(p.Latitude - 5)));
            }
            TextDrawer.Draw(TextType.NORMAL, tl.ToArray(), this.graph);

            // 将lrd转化为实际值
            if(lrd == null){
                lrd = DrawConverter(lnr);
            }

            List<RouteDrawer> lld = convertToTrueListRoutes(lrd, lpd);

            RouteDrawer.Draw(RouteType.NORMAL, lld.ToArray(), this.graph);
        }

        /// <summary>
        /// 清空画板
        /// </summary>
        public void ClearSimuMap()
        {
            if (graph != null)
            {
                graph.Clear(Color.Black);
            }
        }


        public void Dispose()
        {
            if (graph != null)
            {
                graph.Dispose();
            }
        }

        /// <summary>
        /// 通用绘图方式
        /// </summary>
        /// <param name="item">绘制项目</param>
        /// <param name="name">绘图名称</param>
        /// <param name="type">绘制类型</param>
        private void Draw(DrawItem item, object type, object obj)
        {
            switch (item)
            {
                case DrawItem.POINT:
                    PointDrawer.Draw(type, obj, graph); // 空缺处可以是PointDrawer 也可以是PointDrawer[]
                    break;
                case DrawItem.ROUTE:
                    RouteDrawer.Draw(type, obj, graph); // 空缺处可以是RouteDrawer 也可以是RouteDrawer[]
                    break;
                case DrawItem.TEXT:
                    break;
            }
        }

        private PointDrawer findPointerById(string id)
        {
            return pl.Find(delegate(PointDrawer point)
            {
                return point.id.Equals(id);
            });
        }

        public void DrawGroup(GroupBean groupbean)
        {
            if (groupbean == null) return;
            List<PointDrawer> lp_temp = convertToTrueListPoints(groupbean.lchildern);

            Draw(DrawItem.POINT, PointType.GROUP, lp_temp.ToArray());

            List<RouteDrawer> lr_temp = null;
            lr_temp = new List<RouteDrawer>();
            foreach(NetworkBean networkbean in MyXml.networks)
            {
                List<LinkBean> l = networkbean.llb.FindAll(delegate(LinkBean lb)
                {
                    return groupbean.lchildern.Contains(lb.source) &&
                           groupbean.lchildern.Contains(lb.target);
                });
                if(l != null && l.Count > 0) lr_temp.AddRange(DrawConverter(l));
            }

            List<RouteDrawer> lld = convertToTrueListRoutes(lr_temp, lp_temp);

            if (lld != null)
                Draw(DrawItem.ROUTE, RouteType.GROUP, lld.ToArray());

            graph.Flush();
        }

        public void DrawPointer(NodeBean nodebean)
        {
            List<PointDrawer> lpd_temp = new List<PointDrawer>();
            List<RouteDrawer> lrd_temp = new List<RouteDrawer>();

            foreach(RouteDrawer rd in lrd)
            {
                if (rd.spoint.id.Equals(nodebean.id))
                {
                    lpd_temp.Add(rd.epoint);
                    lrd_temp.Add(rd);
                }
                if (rd.epoint.id.Equals(nodebean.id))
                {
                    lpd_temp.Add(rd.spoint);
                    lrd_temp.Add(rd);
                }
            }

            PointDrawer p = findPointerById(nodebean.id);
            PointDrawer p_new = new PointDrawer(p.id, p.name, p.Longtitude * width, p.Latitude * height);
            Draw(DrawItem.POINT, PointType.CENTER, p_new);

            List<PointDrawer> lpp = convertToTrueListPoints(lpd_temp);
            Draw(DrawItem.POINT, PointType.EDAGE, lpp.ToArray());

            List<RouteDrawer> ll = convertToTrueListRoutes(lrd_temp, lpp, p_new);
            Draw(DrawItem.ROUTE, RouteType.EDAGE, ll.ToArray());
            graph.Flush();
        }

        private List<PointDrawer> convertToTrueListPoints(List<NodeBean> lnb)
        {
            List<PointDrawer> lp_temp = new List<PointDrawer>();
            foreach (NodeBean node in lnb)
            {
                PointDrawer p = findPointerById(node.id);
                if (p != null) lp_temp.Add(new PointDrawer(p.id, p.name, p.Longtitude * width, p.Latitude * height));
            }
            return lp_temp;
        }

        private List<PointDrawer> convertToTrueListPoints(List<PointDrawer> lpl)
        {
            List<PointDrawer> lpd = new List<PointDrawer>();
            foreach (PointDrawer p in lpl)
            {
                PointDrawer pd = new PointDrawer(p.id, p.name, p.Longtitude * width, p.Latitude * height);
                lpd.Add(pd);
            }
            return lpd;
        }

        private List<RouteDrawer> convertToTrueListRoutes(List<RouteDrawer> lr_temp, 
                                                          List<PointDrawer> lp_temp)
        {
            List<RouteDrawer> lld = new List<RouteDrawer>();
            foreach (RouteDrawer ld in lr_temp)
            {
                PointDrawer sp = lp_temp.Find(delegate(PointDrawer pd)
                {
                    return pd.id.Equals(ld.spoint.id);
                });
                PointDrawer ep = lp_temp.Find(delegate(PointDrawer pd)
                {
                    return pd.id.Equals(ld.epoint.id);
                });
                if (sp != null && ep != null)
                {
                    RouteDrawer rd = new RouteDrawer(sp, ep, ld.id);
                    lld.Add(rd);
                }
            }
            return lld;
        }

        private List<RouteDrawer> convertToTrueListRoutes(List<RouteDrawer> lr_temp,
                                                          List<PointDrawer> lp_temp, 
                                                          PointDrawer center)
        {
            List<RouteDrawer> lld = new List<RouteDrawer>();
            foreach (RouteDrawer ld in lr_temp)
            {
                PointDrawer sp = lp_temp.Find(delegate(PointDrawer pd)
                {
                    return pd.id.Equals(ld.spoint.id);
                });
                PointDrawer ep = lp_temp.Find(delegate(PointDrawer pd)
                {
                    return pd.id.Equals(ld.epoint.id);
                });
                if (sp != null && ep != null)
                {
                    RouteDrawer rd = new RouteDrawer(sp, ep, ld.id);
                    lld.Add(rd);
                }
                else if(sp == null || ep == null)
                {
                    RouteDrawer rd = (sp == null) ? 
                        new RouteDrawer(center, ep, ld.id) : new RouteDrawer(sp ,center, ld.id);
                    lld.Add(rd);
                }
            }
            return lld;
        }
    }
}
