using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ORA.draw
{
    public enum DrawItem
    {
        POINT, // 点元素
        ROUTE, // 线路元素
        GROUP, // 绘制出中心点及所有与中心点相关的路径以及点
        TEXT   // 点的名称
    }

    public enum PointType
    {
        NORMAL,  // 正常状态的点
        GROUP,   // 点击group节点时候 高亮
        EDAGE,   // 其他TreeNode被点击时, 若被牵扯，则高亮
        CENTER   // 其对应的TreeNode被点击时高亮
    }

    public enum TextType
    {
        NORMAL,  // 正常状态的点
        GROUP,   // 点击group节点时候 高亮
        EDAGE,   // 其他TreeNode被点击时, 若被牵扯，则高亮
        CENTER   // 其对应的TreeNode被点击时高亮
    }

    public enum RouteType
    {
        NORMAL,  // 正常状态的点
        GROUP,   // 点击group节点时候 高亮
        EDAGE,   // TreeNode被点击时, 若被牵扯, 则高亮
    }

    interface IDrawing
    {
        void Draw(object type, object d, Graphics g);
    }
}
