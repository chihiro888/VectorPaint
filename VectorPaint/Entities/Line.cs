using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    // #003 - draw a line
    public class Line: EntityObject
    {
        private Vector3 startPoint;
        private Vector3 endPoint;
        private double thickness;

        public Line() : this(Vector3.Zero, Vector3.Zero)
        {
        }

        public Line(Vector3 start, Vector3 end): base(EntityType.Line)
        {
            this.startPoint = start;
            this.endPoint = end;
            this.Thickness = 0.0;
        }

        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }


        public Vector3 EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public Vector3 StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }

        public double Length
        {
            get
            {
                double dx = endPoint.X - startPoint.X;
                double dy = endPoint.Y - startPoint.Y;
                double dz = endPoint.Z - startPoint.Z;

                return Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }
        }

        public double Angle
        {
            get
            {
                double angle = Math.Atan2((endPoint.Y - startPoint.Y), (endPoint.X - startPoint.X)) * 180.0 / Math.PI;
                if (angle < 0)
                    angle += 360.0;
                return angle;
            }
        }

        public override object CopyOrMove(Vector3 fromPoint, Vector3 toPoint)
        {
            Vector3 startPoint = this.startPoint.CopyOrMove(fromPoint, toPoint);
            Vector3 endPoint = this.endPoint.CopyOrMove(fromPoint, toPoint);

            return new Line
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                Thickness = this.thickness,
                IsVisible = this.isVisible
            };
        }

        public override object Rotate2D(Vector3 basePoint, Vector3 targetPoint)
        {
            Vector3 startPoint = this.startPoint.Rotate2D(basePoint, targetPoint);
            Vector3 endPoint = this.endPoint.Rotate2D(basePoint, targetPoint);

            return new Line
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                Thickness = this.thickness,
                IsVisible = this.isVisible
            };
        }

        public override object Scale(Vector3 basePoint, double value)
        {
            Vector3 startPoint = this.startPoint.Scale(basePoint, value);
            Vector3 endPoint = this.endPoint.Scale(basePoint, value);

            return new Line
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                Thickness = this.thickness,
                IsVisible = this.isVisible
            };
        }

        public override object Clone()
        {
            return new Line
            {
                StartPoint = this.startPoint,
                EndPoint = this.endPoint,
                Thickness = this.thickness,

                // EntityObject properties
                IsVisible = this.IsVisible,
            };
        }
    }
}
