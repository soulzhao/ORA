using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ORA.draw
{
    class TextDrawer//:IDrawing
    {
        //
        // 表明字体的位置
        //
        int x;
        int y;
        string text;

        public TextDrawer(string t, int x, int y)
        {
            this.text = t;
            this.x = x;
            this.y = y;
        }
        public TextDrawer(TextDrawer p)
        {
            this.text = p.text;
            this.x = p.x;
            this.y = p.y;
        }

        private static void DrawText(PointType type, TextDrawer p , Graphics g)
        {
            Font myFont = null;
            SolidBrush blackBrush = null;
            switch(type)
            {
                case PointType.CENTER:
                    myFont = new Font("Verdana", 14);
                    blackBrush = new SolidBrush(Color.Red);
                    g.DrawString(p.text, myFont, blackBrush, p.x, p.y);
                    break;
                case PointType.EDAGE:
                    myFont = new Font("Verdana", 12);
                    blackBrush = new SolidBrush(Color.Red);
                    g.DrawString(p.text, myFont, blackBrush, p.x, p.y);
                    break;
                case PointType.NORMAL:
                    myFont = new Font("Courier New", 8);
                    blackBrush = new SolidBrush(Color.Green);
                    g.DrawString(p.text, myFont, blackBrush, p.x, p.y);
                    break;
                case PointType.GROUP:
                    myFont = new Font("Verdana", 12);
                    blackBrush = new SolidBrush(Color.RoyalBlue);
                    g.DrawString(p.text, myFont, blackBrush, p.x, p.y);
                    break;
            }
        }

        private static void DrawTexts(PointType type, TextDrawer[] t , Graphics g)
        {
            for (int i = 0; i < t.GetLength(0); i++)
            {
                DrawText(type, t[i], g);
            }
        }

        /// <summary>
        ///  绘制文本
        /// </summary>
        /// <param name="type">文本类型</param>
        /// <param name="p">数据类型</param>
        /// <param name="g">绘画板</param>
        public static void Draw(object type, object p, Graphics g)
        {
             if (p.GetType() == typeof(TextDrawer))
            {
                DrawText((PointType)type, (TextDrawer)p, g);
            }
            else if (p.GetType() == typeof(TextDrawer[]))
            {
                DrawTexts((PointType)type, (TextDrawer[])p, g);
            }
        }
    }
}
