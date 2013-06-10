using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ORA.draw
{
    public class RouteDrawer//:IDrawing
    {
        public PointDrawer spoint;
        public PointDrawer epoint;

        public long id; // 所属道路Id

        public RouteDrawer(RouteDrawer r)
        {
            this.spoint = new PointDrawer(r.spoint);
            this.epoint = new PointDrawer(r.epoint);
            this.id = r.id;
        }

        public RouteDrawer(PointDrawer s, PointDrawer e)
        {
            spoint = new PointDrawer(s);
            epoint = new PointDrawer(e);
            this.id = 0;
        }

        public RouteDrawer(PointDrawer s, PointDrawer e, long id)
        {
            spoint = new PointDrawer(s);
            epoint = new PointDrawer(e);
            this.id = id;
        }

        public RouteDrawer(string id1, string name1, double sx, double sy, 
                           string id2, string name2, double ex, double ey, long id)
        {
            spoint = new PointDrawer(id1, name1, sx, sy);
            epoint = new PointDrawer(id2, name2, ex, ey);
            this.id = id;
        }

        private static void DrawRoute(RouteType type, RouteDrawer r, Graphics g)
        {
            if (r != null)
            {
                Pen MyPen;
                switch (type)
                {
                    case RouteType.EDAGE:
                        MyPen = new Pen(Color.Red, 2);
                        Paint(r, g, MyPen);
                        break;
                    case RouteType.GROUP:
                        MyPen = new Pen(Color.LightBlue, 3);
                        Paint(r, g, MyPen);
                        break;
                    case RouteType.NORMAL:
                        MyPen = new Pen(Color.White, 1);
                        Paint(r, g, MyPen);
                        break;
                }
            }
        }

        private static void Paint(RouteDrawer r, Graphics g, Pen MyPen)
        {
            g.DrawLine(MyPen,
                       new Point((int)r.spoint.Longtitude, (int)r.spoint.Latitude),
                       new Point((int)r.epoint.Longtitude, (int)r.epoint.Latitude));
        }

        private static void DrawRoutes(RouteType type, RouteDrawer[] r, Graphics g)
        {
            for (int i = 0; i < r.GetLength(0); i++)
            {
                DrawRoute(type, r[i], g);
            }
        }

        /// <summary>
        /// 绘制路径
        /// </summary>
        /// <param name="type">路径类型</param>
        /// <param name="r">数据类型</param>
        /// <param name="g">绘画板</param>
        public static void Draw(object type, object r, Graphics g)
        {
            if (r.GetType() == typeof(RouteDrawer))
            {
                DrawRoute((RouteType)type, (RouteDrawer)r, g);
            }
            else if (r.GetType() == typeof(RouteDrawer[]))
            {
                DrawRoutes((RouteType)type, (RouteDrawer[])r, g);
            }
        }
    }
}
