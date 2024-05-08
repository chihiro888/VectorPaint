using System;
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
        public static double Epsilon = 1e-12;

        // #006 - intersection of two lines
        public static bool IsEqual(double d1, double d2)
        {
            return IsEqual(d1, d2, Epsilon);
        }

        // #006 - intersection of two lines
        public static bool IsEqual(double d1, double d2, double epsilon)
        {
            return IsZero(d1 - d2, epsilon);
        }

        // #006 - intersection of two lines
        public static bool IsZero(double d, double epsilon)
        {
            return d >= -epsilon && d <= epsilon;
        }

        // #034 - distance from a point to a polyline
        public static bool IsZero(double d)
        {
            return IsZero(d, Epsilon);
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

        // #008 - draw an arc
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

        // #014 - draw an arc with 3 points
        public static Arc GetArcWithCenterStartEnd(Vector3 center, Vector3 startPoint, Vector3 endPoint)
        {
            double start = LineAngle(center, startPoint);
            double end = LineAngle(center, endPoint);
            double radius = center.DistanceFrom(startPoint);

            if (end > start)
                end -= start;
            else
                end += 360.0 - start;

            return new Arc(center, radius, start, end);
        }

        // #015 - Draw an arc (center, start, angle)
        public static Arc GetArcWithCenterStartAngle(Vector3 center, Vector3 startPoint, double angle)
        {
            double start = LineAngle(center, startPoint);
            double end = angle + start;
            double radius = center.DistanceFrom(startPoint);

            if (end > start)
                end -= start;
            else
                end += 360.0 - start;

            return new Arc(center, radius, start, end);
        }

        // #016 - Draw an arc (center, start, length of chord)
        public static Arc GetArcWithCenterStartLength(Vector3 center, Vector3 startPoint, double length)
        {
            Arc arc = new Arc();
            double start = LineAngle(center, startPoint);
            double radius = center.DistanceFrom(startPoint);

            if (length <= radius * 2)
            {
                double a = (2 * radius * radius - length * length) / (2 * radius * radius);
                double end = Math.Acos(a) * 180.0 / Math.PI;
                arc = new Arc(center, radius, start, end);
            }

            return arc;
        }

        // #017 - Draw an arc (start, end, angle)
        public static Arc GetArcWithStartEndAngle(Vector3 startPoint, Vector3 endPoint, double angle)
        {
            Arc arc = new Arc();

            double length = startPoint.DistanceFrom(endPoint);
            double radius = Math.Sqrt(length * length / (1 - Math.Cos(angle * Math.PI / 180.0))/2);

            if (length <= radius * 2)
            {
                double a = (180.0 - angle) / 2;
                a += LineAngle(startPoint, endPoint);

                double x = radius * Math.Cos(a * Math.PI / 180.0) + startPoint.X;
                double y = radius * Math.Cos(a * Math.PI / 180.0) + startPoint.Y;
                Vector3 center = new Vector3(x, y);

                double start = LineAngle(center, startPoint);
                double end = LineAngle(center, endPoint);

                if (end > start)
                    end -= start;
                else
                    end += 360.0 - start;

                arc = new Arc(center, radius, start, end);
            }

            return arc;
        }

        // #019 - draw a circle with 2 points
        public static Circle GetCircleWithTwoPoints(Vector3 firstPoint, Vector3 secondPoint)
        {
            double radius = firstPoint.DistanceFrom(secondPoint) / 2;
            double x = (secondPoint.X + firstPoint.X) / 2;
            double y = (secondPoint.Y + firstPoint.Y) / 2;

            return new Circle(new Vector3(x, y), radius);
        }

        // #020 - draw an elliptical arc
        public static Ellipse GetEllipticalArc(Vector3 center, Vector3 firstPoint, Vector3 secondPoint, Vector3 thirdPoint, Vector3 fourthPoint)
        {
            Ellipse elp = GetEllipse(center, firstPoint, secondPoint);
            double start = LineAngle(center, thirdPoint);
            double end = LineAngle(center, fourthPoint);

            end = (end > start) ? end - start : end - start + 360.0;
            start -= elp.Rotation;

            elp.StartAngle = start;
            elp.EndAngle = end;

            return elp;
        }

        // #035 - Index of entities
        public static int GetSegmentIndex(List<EntityObject> entities, Vector3 mousePosition, PointF[] cursor_rect, out Vector3 PointOnSegment)
        {
            bool flags = false;
            Vector3 poSegment = new Vector3(0,0,0);

            for(int i =0; i < entities.Count; i++)
            {
                switch(entities[i].Type)
                {
                    case EntityType.Arc:
                        double d = DistancePointToArc(entities[i] as Arc, mousePosition, out poSegment);
                        break;
                    case EntityType.Circle:
                        d = DistancePointToCircle(entities[i] as Circle, mousePosition, out poSegment);
                        break;
                    case EntityType.Ellipse:
                        d = DistancePointToEllipse(entities[i] as Ellipse, mousePosition, out poSegment);
                        break;
                    case EntityType.Line:
                        d = DistanceFromLine(entities[i] as Line, mousePosition, out poSegment);
                        break;
                    case EntityType.LwPolyline:
                        d = DistancePointToLwPolyline(entities[i] as LwPolyline, mousePosition, out poSegment);
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
                        entities[i].Select();
                        flags = true;
                        return i;
                    }
                }
            }
            PointOnSegment = Vector3.NaN;
            return -1;
        }

        // #035 - Index of entities
        private static bool IsPointInPolyline(PointF[] cursor_rect, Vector3 point)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();   
            path.AddPolygon(cursor_rect);

            return path.IsVisible(point.ToPointF);
        }

        // #026 - intercetion between a line and an ellipse
        public static double DegToRad = Math.PI / 180.0;

        public static List<Vector3> LineEllipseIntersection(Line line, Ellipse ellipse)
        {
            double x1 = line.StartPoint.X;
            double y1 = line.StartPoint.Y;
            double x2 = line.EndPoint.X;   
            double y2 = line.EndPoint.Y;

            double xc = ellipse.Center.X;
            double yc = ellipse.Center.Y;

            double a = ellipse.MajorAxis;
            double b = ellipse.MinorAxis;

            double angle = ellipse.Rotation * DegToRad;

            double X, Y;
            List<Vector3> result = new List<Vector3>();

            double x21 = x2 - x1;
            double y21 = y2 - y1;

            x1 -= xc;
            y1 -= yc;

            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);

            double A1 = b * b * cos * cos + a * a * sin * sin;
            double B1 = a * a * cos * cos + b * a * sin * sin;
            double C1 = 2 * (b * b - a * a) * sin * cos;

            double A = A1 * x21 * x21 + B1 * y21 * y21 + C1 * x21 * y21;
            double B = 2 * A1 * x1 * x21 + 2 * B1 * y1 * y21 + C1 * x1 * y21 + C1 * x21 * y1;
            double C = A1 * x1 * x1 + B1 * y1 * y1 + C1 * x1 * y1 - a * a * b * b;

            double Delta = B * B - 4 * A * C;

            List<double> t_value = new List<double>();

            if (Delta == 0)
                t_value.Add(-B / 2 / A);
            else if (Delta > 0)
            {
                t_value.Add((-B + Math.Sqrt(Delta)) / 2 / A);
                t_value.Add((-B - Math.Sqrt(Delta)) / 2 / A);
            }

            foreach(double t in t_value)
            {
                if ((t>=0.0) && (t<=1.0))
                {
                    X = x1 + x21 * t + xc;
                    Y = y1 + y21 * t + yc;
                    result.Add(new Vector3(X, Y));
                }
            }

            return result;
        }

        // #027 - intercetion between a line and an ellipse
        public static List<Vector3> LineCircleIntersection(Line line, Circle circle)
        {
            List<Vector3> result = new List<Vector3>();
            double x1 = line.StartPoint.X;
            double y1 = line.StartPoint.Y;
            double x2 = line.EndPoint.X;
            double y2 = line.EndPoint.Y;

            double xc = circle.Center.X;
            double yc = circle.Center.Y;
            double r = circle.Radius;

            double dx = x2 - x1;
            double dy = y2 - y1;

            x1 -= xc;
            y1 -= yc;

            double A = dx * dx + dy * dy;
            double B = 2 * (x1 * dx + y1 * dy);
            double C = x1 * x1 + y1 * y1 - r * r;

            double Delta = B * B - 4 * A * C;

            List<double> t_values = new List<double>();

            if (Delta == 0)
                t_values.Add(-B / 2 / A);
            else if (Delta > 0)
            {
                t_values.Add((-B + Math.Sqrt(Delta)) / 2 / A);
                t_values.Add((-B - Math.Sqrt(Delta)) / 2 / A);
            }

            foreach (double t in t_values)
            {
                if((t>=0.0) && (t<=1.0))
                {
                    double X = x1 + dx * t + xc;
                    double Y = y1 + dy * t + yc;
                    result.Add(new Vector3(X, Y));
                }
            }

            return result;
        }

        // #028 - intercetion between a line and an arc
        public static List<Vector3> LineArcIntersection(Line line, Arc arc)
        {
            List<Vector3> result = new List<Vector3>();
            List<Vector3> list = LineCircleIntersection(line, new Circle(arc.Center, arc.Radius));
            foreach(Vector3 v in list)
            {
                if (IsPointOnArc(arc, v))
                    result.Add(v);
            }

            return result;
        }

        // #028 - intercetion between a line and an arc
        private static bool IsPointOnArc(Arc arc, Vector3 v)
        {
            Line line = new Line(arc.Center, v);

            double angle = line.Angle;
            double start = arc.StartAngle;
            double end = (arc.EndAngle + arc.StartAngle) % 360.0;

            if ((start < end) && (start <= angle) && (angle <= end))
                return true;
            else if ((start > end) && (angle >= start) && (angle <= 360.0))
                return true;
            else if ((start > end) && (angle >= 0) && (angle <= end))
                return true;
            else
                return false;
        }

        // #029 - distance from a point to an ellipse
        public static double DistancePointToEllipse(Ellipse ellipse, Vector3 point, out Vector3 PointOnEllipse)
        {
            double a = ellipse.MajorAxis;
            double b = ellipse.MinorAxis;
            double angle = ellipse.Rotation * DegToRad;
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            double xc = ellipse.Center.X;
            double yc = ellipse.Center.Y;

            double x1 = point.X;
            double y1 = point.Y;

            x1 -= xc;
            y1 -= yc;

            double xr = cos * x1 + sin * y1;
            double yr = -sin * x1 + cos * y1;

            double xt = cos;
            double yt = sin;
            double x = 0, y = 0;

            foreach(int i in Enumerable.Range(0, 3))
            {
                x = a * xt;
                y = b * yt;

                double ex = (a * a - b * b) * Math.Pow(xt, 3) / a;
                double ey = (b * b - a * a) * Math.Pow(yt, 3) / b;

                double xq = Math.Abs(xr) - ex;
                double yq = Math.Abs(yr) - ey;

                double nx = Math.Sqrt((x - ex) * (x - ex) + (y - ey) * (y - ey)) / Math.Sqrt(xq * xq + yq * yq);

                xt = (xq * nx + ex) / a;
                yt = (yq * nx + ey) / b;

                double nt = Math.Sqrt(xt * xt + yt * yt);
                xt /= nt;
                yt /= nt;
            }
            x = CopySing(x, xr);
            y = CopySing(y, yr);

            PointOnEllipse = new Vector3(cos * x - sin * y + xc, sin * x + cos * y + yc);

            return point.DistanceFrom(PointOnEllipse);
        }

        // #029 - distance from a point to an ellipse
        public static double CopySing(double a, double b)
        {
            return a * Math.Sign(b);
        }

        // #030 - distance from a point to a circle
        public static double DistancePointToCircle(Circle circle, Vector3 point, out Vector3 PointOnCircle)
        {
            double d = point.DistanceFrom(circle.Center) - circle.Radius;
            double angle = LineAngle(point, circle.Center);

            double x = d * Math.Cos(angle * DegToRad) + point.X;
            double y = d * Math.Sin(angle * DegToRad) + point.Y;

            PointOnCircle = new Vector3(x, y);

            return point.DistanceFrom(PointOnCircle);
        }

        // #031 - distance from a point to an arc
        public static double DistancePointToArc(Arc arc, Vector3 point, out Vector3 PointOnArc)
        {
            double d = point.DistanceFrom(arc.Center) - arc.Radius;
            double angle = LineAngle(point, arc.Center);

            double x = d * Math.Cos(angle * DegToRad) + point.X;
            double y = d * Math.Sin(angle * DegToRad) + point.Y;

            Vector3 result = new Vector3(x, y);

            double r_angle = LineAngle(arc.Center, result);

            if((r_angle >= arc.StartAngle) && (r_angle <= (arc.StartAngle + arc.EndAngle)))
            {
                PointOnArc = result;
            }
            else
            {
                double dist1 = arc.StartPoint.DistanceFrom(point);
                double dist2 = arc.EndPoint.DistanceFrom(point);

                if (dist1 < dist2)
                {
                    d = dist1;
                    PointOnArc = arc.StartPoint;
                }
                else
                {
                    d = dist2;
                    PointOnArc = arc.EndPoint;
                }
            }

            return Math.Abs(d);
        }

        // #034 - distance from a point to a polyline
        public static double DistancePointToLwPolyline(LwPolyline polyline, Vector3 point, out Vector3 PointOnLwPolyline)
        {
            double result = double.MaxValue;
            PointOnLwPolyline = new Vector3(0, 0, 0);

            foreach(EntityObject entity in polyline.Explode())
            {
                switch(entity.Type)
                {
                    case EntityType.Line:
                        double d = DistanceFromLine(entity as Line, point, out Vector3 v);
                        if (d < result)
                        {
                            PointOnLwPolyline = v;
                            result = d;
                        }
                        break;
                    case EntityType.Arc:
                        d = DistancePointToArc(entity as Arc, point, out v);
                        if (d < result)
                        {
                            PointOnLwPolyline = v;
                            result = d;
                        }
                        break;
                }
            }

            return result;
        }

        // #037 - Copy and move
        public static void Modify1Selection(int modifyIndex, List<EntityObject> entities, Vector3 fromPoint, Vector3 toPoint)
        {
            for (int i=0; i<entities.Count; i++)
            {
                if (entities[i].IsSelected)
                {
                    switch(modifyIndex)
                    {
                        // Copy
                        case 0:
                            entities.Add(entities[i].CopyOrMove(fromPoint, toPoint) as EntityObject);
                            break;
                        // Move
                        case 1:
                            entities[i] = entities[i].CopyOrMove(fromPoint, toPoint) as EntityObject;
                            entities[i].DeSelect();
                            break;
                        // Rotate
                        case 2:
                            entities[i] = entities[i].Rotate2D(fromPoint, toPoint) as EntityObject;
                            entities[i].DeSelect();
                            break;
                        // Scale
                        case 3:
                            entities[i] = entities[i].Scale(fromPoint, fromPoint.DistanceFrom(toPoint)) as EntityObject;
                            entities[i].DeSelect();
                            break;
                    }
                }
            }
        }
    }
}
