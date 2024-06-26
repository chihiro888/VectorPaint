﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorPaint.Entities;

namespace VectorPaint
{
    // #002- vector and draw a point
    public static class GraphicsExtension
    {
        private static float Height;

        // #010 - scroll bar
        private static float XScroll;
        private static float YScroll;
        private static float ScaleFactor;

        private static Pen extpen = new Pen(Color.Gray, 0);

        // #002- vector and draw a point
        public static void SetParameters(this System.Drawing.Graphics g, float xscroll, float yscroll, float scalefactor, float height)
        {
            // #010 - scroll bar
            XScroll = xscroll;
            YScroll = yscroll;
            ScaleFactor = scalefactor;

            Height = height;

            // #010 - scroll bar
            extpen.DashPattern = new float[] { 1.5f / scalefactor, 2.0f / scalefactor };
        }

        // #002- vector and draw a point
        public static void SetTransform(this System.Drawing.Graphics g)
        {
            g.PageUnit = System.Drawing.GraphicsUnit.Millimeter;
            g.TranslateTransform(0, Height);

            // #010 - scroll bar
            g.ScaleTransform(ScaleFactor, -ScaleFactor);
            g.TranslateTransform(-XScroll / ScaleFactor, YScroll / ScaleFactor);
        }

        // #002- vector and draw a point
        public static void DrawPoint(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Point point)
        {
            g.SetTransform();
            System.Drawing.PointF p = point.Position.ToPointF;
            if (!point.IsSelected)
                g.DrawEllipse(pen, p.X - 1, p.Y - 1, 2, 2);
            else
                g.DrawEllipse(extpen, p.X - 1, p.Y - 1, 2, 2);
            g.ResetTransform();
        }

        // #003 - draw a line
        public static void DrawLine(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Line line)
        {
            g.SetTransform();
            if (!line.IsSelected)
                g.DrawLine(pen, line.StartPoint.ToPointF, line.EndPoint.ToPointF);
            else
                g.DrawLine(extpen, line.StartPoint.ToPointF, line.EndPoint.ToPointF);
            g.ResetTransform();
        }

        // #008 - draw an arc
        public static void DrawArc(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Arc arc)
        {
            float x = (float)(arc.Center.X - arc.Radius);
            float y = (float)(arc.Center.Y - arc.Radius);
            float d = (float)arc.Diameter;

            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(x, y, d, d);

            g.SetTransform();
            if (!arc.IsSelected)
                g.DrawArc(pen, rect, (float)arc.StartAngle, (float)arc.EndAngle);
            else
                g.DrawArc(extpen, rect, (float)arc.StartAngle, (float)arc.EndAngle);
            g.ResetTransform();
        }


        // # 004 - draw a circle
        public static void DrawCircle(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Circle circle)
        {
            float x = (float)(circle.Center.X - circle.Radius);
            float y = (float)(circle.Center.Y - circle.Radius);
            float d = (float)circle.Diameter;
            g.SetTransform();
            if (!circle.IsSelected)
                g.DrawEllipse(pen, x, y, d, d);
            else
                g.DrawEllipse(extpen, x, y, d, d);
            g.ResetTransform();
        }

        // #005 - draw an ellipse
        public static void DrawEllipse(this System.Drawing.Graphics g, System.Drawing.Pen pen, Entities.Ellipse ellipse)
        {
        
            SetTransform(g);
            g.TranslateTransform(ellipse.Center.ToPointF.X, ellipse.Center.ToPointF.Y);
            g.RotateTransform((float)ellipse.Rotation);
            if (!ellipse.IsSelected)
                g.DrawEllipse(pen, -(float)ellipse.MajorAxis, -(float)ellipse.MinorAxis, (float)ellipse.MajorAxis * 2, (float)ellipse.MinorAxis * 2);
            else
                g.DrawEllipse(extpen, -(float)ellipse.MajorAxis, -(float)ellipse.MinorAxis, (float)ellipse.MajorAxis * 2, (float)ellipse.MinorAxis * 2);
            g.ResetTransform();
        }

        // #011 - draw a polyline
        public static void DrawLwPolyline(this Graphics g, Pen pen, LwPolyline polyline)
        {
            foreach(EntityObject entity in polyline.Explode())
            {
                if (!polyline.IsSelected)
                    g.DrawEntity(pen, entity);
                else
                    g.DrawEntity(extpen, entity);
            }
        }

        // #018 - Entity object class
        public static void DrawEntity(this Graphics g, Pen pen, EntityObject entity)
        {
            switch (entity.Type)
            {
                case EntityType.Arc:
                    g.DrawArc(pen, entity as Arc);
                    break;
                case EntityType.Ellipse:
                    g.DrawEllipse(pen, entity as Ellipse);
                    break;
                case EntityType.Circle:
                    g.DrawCircle(pen, entity as Circle);
                    break;
                case EntityType.Line:
                    g.DrawLine(pen, entity as Line);
                    break;
                case EntityType.LwPolyline:
                    g.DrawLwPolyline(pen, entity as LwPolyline);
                    break;
                case EntityType.Point:
                    g.DrawPoint(pen, entity as Entities.Point);
                    break;

            }
        }

        // #037 - Copy and move
        public static void ExtendedOfModify(this Graphics g, Pen pen, int modifyIndex, List<EntityObject> entities, Vector3 fromPoint, Vector3 toPoint)
        {
            g.DrawLine(pen, new Line(fromPoint, toPoint));

            for(int i=0; i<entities.Count; i++)
            {
                if (entities[i].IsSelected)
                {
                    switch (modifyIndex)
                    {
                        // Copy
                        case 0:
                            g.DrawEntity(pen, entities[i].CopyOrMove(fromPoint, toPoint) as EntityObject);
                            break;
                        // Move
                        case 1:
                            g.DrawEntity(pen, entities[i].CopyOrMove(fromPoint, toPoint) as EntityObject);
                            break;
                        // Rotate
                        case 2:
                            g.DrawEntity(pen, entities[i].Rotate2D(fromPoint, toPoint) as EntityObject);
                            break;
                        // Scale
                        case 3:
                            g.DrawEntity(pen, entities[i].Scale(fromPoint, fromPoint.DistanceFrom(toPoint)) as EntityObject);
                            break;
                    }
                }
            }
        }
    }
}
