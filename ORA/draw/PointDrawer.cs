using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ORA.draw
{
    public class PointDrawer
    {
        public string id;
        public string name;
        public double Longtitude; // x
        public double Latitude;   // y

        public PointDrawer(string id, string name ,double x, double y)
        {
            this.id = id;
            Longtitude = x;
            Latitude = y;
            this.name = name;
        }

        public PointDrawer(PointDrawer p)
        {
            id = p.id;
            Longtitude = p.Longtitude;
            Latitude = p.Latitude;
            name = p.name;
        }

        private static void DrawPoint(PointType type, PointDrawer point, Graphics g)
        {
            Pen MyPen;
            switch (type)
            {
                case PointType.CENTER:
                    MyPen = new Pen(Color.Purple, 4);
                    g.DrawEllipse(MyPen, (float)point.Longtitude, (float)point.Latitude, 8, 8);
                    break;
                case PointType.EDAGE:
                    MyPen = new Pen(Color.Blue, 2);
                    g.DrawEllipse(MyPen, (float)point.Longtitude, (float)point.Latitude, 4, 4);
                    break;
                case PointType.NORMAL:
                    MyPen = new Pen(Color.White, 2);
                    g.DrawEllipse(MyPen, (float)(point.Longtitude), (float)point.Latitude, 3, 3);
                    break;
                case PointType.GROUP:
                    MyPen = new Pen(Color.Blue, 3);
                    g.DrawEllipse(MyPen, (float)point.Longtitude, (float)point.Latitude, 5, 5);
                    break;
            }
        }

        private static void DrawPoints(PointType type, PointDrawer[] points, Graphics g)
        {
            for (int i = 0; i < points.GetLength(0); i++)
            {
                DrawPoint(type, points[i], g);
            }
        }

        /// <summary>
        /// 绘制点
        /// </summary>
        /// <param name="type">点类型</param>
        /// <param name="p">数据类型</param>
        /// <param name="g">绘画板</param>
        public static void Draw(object type, object p, Graphics g)
        {
            if (p.GetType() == typeof(PointDrawer))
            {
                DrawPoint((PointType)type, (PointDrawer)p, g);
            }
            else if (p.GetType() == typeof(PointDrawer[]))
            {
                DrawPoints((PointType)type, (PointDrawer[])p, g);
            }
        }
    }
}