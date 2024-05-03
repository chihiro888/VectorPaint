using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    public static class GraphicsExtension
    {
        private static float Height;

        public static void SetParameters(this System.Drawing.Graphics g, float height)
        {
            Height = height;
        }

        public static void SetTransform(this System.Drawing.Graphics g)
        {
            g.PageUnit = System.Drawing.GraphicsUnit.Millimeter;
            g.TranslateTransform(0, Height);
            g.ScaleTransform(1.0f, -1.0f);
        }

        public static void DrawPoint(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Point point)
        {
            g.SetTransform();
            System.Drawing.PointF p = point.Position.ToPointF;
            g.DrawEllipse(pen, p.X - 1, p.Y - 1, 2, 2);
            g.ResetTransform();
        }
    }
}
