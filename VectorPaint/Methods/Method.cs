﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using VectorPaint.Entities;

namespace VectorPaint.Methods
{
    public class Method
    {
        // #005 - draw an ellipse
        public static double LineAngle(Vector3 start, Vector3 end)
        {
            double angle = Math.Atan2((end.Y - start.Y), (end.X - start.X)) * 180.0 / Math.PI;
            if (angle < 0.0)
                angle += 360.0;
            return angle;
        }

        // #005 - draw an ellipse
        public static Entities.Ellipse GetEllipse(Vector3 center, Vector3 firstPoint, Vector3 secondPoint)
        {
            double major = center.DistanceFrom(firstPoint);
            double minor = center.DistanceFrom(secondPoint);
            double angle = LineAngle(center, firstPoint);
            Entities.Ellipse elp = new Entities.Ellipse(center, major, minor);
            elp.Rotation = angle;
            return elp;
        }

        // #006 - intersection of two lines
        private static bool IsPointOnLine(Line line1, Vector3 point)
        {
            return IsEqual(line1.Length, line1.StartPoint.DistanceFrom(point) + line1.EndPoint.DistanceFrom(point));
        }

        // #006 - intersection of two lines
        private static double Epsilon = 1e-12;

        // #006 - intersection of two lines
        private static bool IsEqual(double d1, double d2)
        {
            return IsEqual(d1, d2, Epsilon);
        }

        // #006 - intersection of two lines
        private static bool IsEqual(double d1, double d2, double epsilon)
        {
            return IsZero(d1 - d2, epsilon);
        }

        // #006 - intersection of two lines
        private static bool IsZero(double d, double epsilon)
        {
            return d >= -epsilon && d <= epsilon;
        }

        // #006 - intersection of two lines
        public static Vector3 LineLineIntersection(Entities.Line line1, Entities.Line line2, bool extended = false)
        {
            Vector3 result;
            Vector3 p1 = line1.StartPoint;
            Vector3 p2 = line1.EndPoint;
            Vector3 p3 = line2.StartPoint;
            Vector3 p4 = line2.EndPoint;

            double dx12 = p2.X - p1.X;
            double dy12 = p2.Y - p1.Y;
            double dx34 = p4.X - p3.X;
            double dy34 = p4.Y - p3.Y;

            double denominator = (dy12 * dx34 - dx12 * dy34);
            double k1 = ((p1.X - p3.Y) * dy34 + (p3.Y - p1.Y) * dx34) / denominator;

            if (double.IsInfinity(k1))
                return new Vector3(double.NaN, double.NaN);

            result = new Vector3(p1.X + dx12 * k1, p1.Y + dy12 * k1);

            if (extended)
                return result;
            else
            {
                if (IsPointOnLine(line1, result) && IsPointOnLine(line2, result))
                    return result;
                else
                    return new Vector3(double.NaN, double.NaN);
            }
        }

        // #007 - draw a circle with 3 points
        public static Circle GetCircleWith3Point(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            double x1 = (p1.X + p2.X) / 2;
            double y1 = (p1.Y + p2.Y) / 2;
            double dx1 = p2.X - p1.X;
            double dy1 = p2.Y - p1.Y;

            double x2 = (p2.X + p3.X) / 2;
            double y2 = (p2.Y + p3.Y) / 2;
            double dx2 = p3.X - p2.X;
            double dy2 = p3.Y - p2.Y;

            Line line1 = new Line(new Vector3(x1, y1), new Vector3(x1 - dy1, y1 + dx1));
            Line line2 = new Line(new Vector3(x2, y2), new Vector3(x2 - dy2, y2 + dx2));

            Vector3 center = LineLineIntersection(line1, line2, true);

            double dx = center.X - p1.X;
            double dy = center.Y - p1.Y;

            double radius = Math.Sqrt(dx * dx + dy * dy);

            return new Circle(center, radius);
        }

        // #008 - draw an arc
        public static Arc GetArcWith3Points(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            double start, end;
            Arc result = new Arc();

            Circle c = GetCircleWith3Point(p1, p2, p3);

            if (c.Radius > 0)
            {
                if (DeterminePointOfLine(new Line(p1, p3), p2) < 0)
                {
                    start = LineAngle(c.Center, p3);
                    end = LineAngle(c.Center, p1);
                }
                else
                {
                    start = LineAngle(c.Center, p1);
                    end = LineAngle(c.Center, p3);
                }
                if (end > start)
                    end -= start;
                else
                    end += 360.0 - start;

                result = new Arc(c.Center, c.Radius, start, end);
            }

            return result;
        }

        private static double DeterminePointOfLine(Line line, Vector3 v)
        {
            return (v.X - line.StartPoint.X) * (line.EndPoint.Y - line.StartPoint.Y) - (v.Y - line.StartPoint.Y) * (line.EndPoint.X - line.StartPoint.X);
        }

        // #012 - draw a rectangle
        public static LwPolyline PointToRect(Vector3 firstCorner, Vector3 secondCorner, out int direction)
        {
            double x = Math.Min(firstCorner.X, secondCorner.X);
            double y = Math.Min(firstCorner.Y, secondCorner.Y);
            double width = Math.Abs(secondCorner.X - firstCorner.X);
            double height = Math.Abs(secondCorner.Y - firstCorner.Y);

            double dx = secondCorner.X - firstCorner.X;

            List<LwPolylineVertex> vertexes = new List<LwPolylineVertex>();
            vertexes.Add(new LwPolylineVertex(x, y));
            vertexes.Add(new LwPolylineVertex(x + width, y));
            vertexes.Add(new LwPolylineVertex(x + width, y + height));
            vertexes.Add(new LwPolylineVertex(x, y + height));

            if (dx > 0) direction = 1;
            else if (dx < 0) direction = 2;
            else direction = -1;

            return new LwPolyline(vertexes, true);
        }

        // #013 - draw a polygon
        public static LwPolyline GetPolygon(Vector3 center, Vector3 secondPoint, int sidesQty, int inscribed)
        {
            List<LwPolylineVertex> vertexs = new List<LwPolylineVertex>();
            double sides_angle = 360.0 / sidesQty;
            double radius = center.DistanceFrom(secondPoint);
            double lineangle = LineAngle(center, secondPoint);

            if (inscribed == 1)
            {
                lineangle -= sides_angle / 2.0;
                radius /= Math.Cos(sides_angle / 180.0 * Math.PI / 2.0);
            }

            for (int i=0; i<sidesQty; i++)
            {
                double x = center.X + radius * Math.Cos(lineangle / 180.0 * Math.PI);
                double y = center.Y + radius * Math.Sin(lineangle / 180.0 * Math.PI);

                vertexs.Add(new LwPolylineVertex(x, y));
                lineangle += sides_angle;
            }

            return new LwPolyline(vertexs, true);
        }

        // #021 - custom cursor
        public static Bitmap SetCursor(int index, float size, Color color)
        {
            Bitmap bmp = new Bitmap((int)size + 1, (int)size + 1);
            float cx = size / 2;
            float cy = size / 2;
            PointF[] points;

            using(Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.Transparent);
                switch(index)
                {
                    // default cursor
                    case 0:
                        break;
                    // drawing cursor
                    case 1:
                        points = new PointF[]
                        {
                            new PointF(cx, 0),
                            new PointF(2 * cx, cy),
                            new PointF(cx, 2 * cy),
                            new PointF(0, cy)
                        };

                        gr.DrawLine(new Pen(color, 2.0f), points[0], points[2]);
                        gr.DrawLine(new Pen(color, 2.0f), points[1], points[3]);
                        break;
                    // editing cursor
                    case 2:
                        points = new PointF[]
                        {
                                new PointF(1, 1),
                                new PointF(2 * cx - 1, 1),
                                new PointF(2 * cx - 1, 2 * cy - 1),
                                new PointF(1, 2 * cy - 1)
                        };

                        gr.DrawPolygon(new Pen(color, 2.0f), points);
                        break;
                }
                return bmp;
            }
        }

        // #022 - distance from a point to a line
        public static double DistanceFromLine(Line line, Vector3 point, out Vector3 closest, bool IsExtended = false)
        {
            double x1 = line.StartPoint.X;
            double y1 = line.StartPoint.Y;
            double x2 = line.EndPoint.X;
            double y2 = line.EndPoint.Y;

            double x = point.X;
            double y = point.Y;

            double dx = x2 - x1;
            double dy = y2 - y1;

            if ((dx == 0) && (dy == 0))
            {
                closest = line.StartPoint;
                dx = x - x1;
                dy = y - y1;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            double k = ((x - x1) * dx + (y - y1) * dy) / (dx * dx + dy * dy);

            closest = new Vector3(x1 + k * dx, y1 + k * dy);
            dx = x - closest.X;
            dy = y - closest.Y;

            if (!IsExtended)
            {
                if (k < 0)
                {
                    closest = new Vector3(x1, y1);
                    dx = x - x1;
                    dy = y - y1;
                }
                else if (k > 1)
                {
                    closest = new Vector3(x2, y2);
                    dx = x - x2;
                    dy = y - y2;
                }
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }

        // #035 - Index of entities
        public static int GetSegmentIndex(List<EntityObject> entities, Vector3 mousePosition, PointF[] cursor_rect, out Vector3 PointOnSegment)
        {
            bool flags = false;
            int index = -1;
            Vector3 poSegment = new Vector3(0,0,0);

            for(int i =0; i < entities.Count; i++)
            {
                switch(entities[i].Type)
                {
                    case EntityType.Arc:
                        //
                        break;
                    case EntityType.Circle:
                        //
                        break;
                    case EntityType.Ellipse:
                        //
                        break;
                    case EntityType.Line:
                        //
                        break;
                    case EntityType.LwPolyline:
                        //
                        break;
                    case EntityType.Point:
                        poSegment = (entities[i] as Entities.Point).Position;
                        break;
                }
                if (!flags)
                {
                    if(IsPointInPolyline(cursor_rect, poSegment))
                    {
                        PointOnSegment = poSegment;
                        flags = true;
                        return i;
                    }
                }
            }
            PointOnSegment = Vector3.NaN;
            return -1;
        }

        private static bool IsPointInPolyline(PointF[] cursor_rect, Vector3 point)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();   
            path.AddPolygon(cursor_rect);

            return path.IsVisible(point.ToPointF);
        }
    }
}
