using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class Arc : EntityObject
    {
        private Vector3 center;
        private double radius;
        private double startAngle;
        private double endAngle;
        private double thickness;

        public Arc() : this(Vector3.Zero, 1.0, 0.0, 180.0)
        {
        }

        public Arc(Vector3 center, double radius, double start, double end) : base(EntityType.Arc)
        {
            this.Center = center;
            this.Radius = radius;
            this.StartAngle = start;
            this.EndAngle = end;
            this.Thickness = 0.0;
        }

        public Vector3 Center
        {
            get { return center; }
            set { center = value; }
        }

        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public double StartAngle
        {
            get { return startAngle; }
            set { startAngle = value; }
        }

        public double EndAngle
        {
            get { return endAngle; }
            set { endAngle = value; }
        }

        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public double Diameter
        {
            get { return this.Radius*2; }
        }

        public Vector3 StartPoint
        {
            get
            {
                double x = radius * Math.Cos(startAngle * Methods.Method.DegToRad);
                double y = radius * Math.Sin(startAngle * Methods.Method.DegToRad);
                return new Vector3(center.X + x, center.Y + y);
            }
        }

        public Vector3 EndPoint
        {
            get
            {
                double x = radius * Math.Cos((startAngle + endAngle) * Methods.Method.DegToRad);
                double y = radius * Math.Sin((startAngle + endAngle) * Methods.Method.DegToRad);
                return new Vector3(center.X + x, center.Y + y);
            }
        }

        public override object CopyOrMove(Vector3 fromPoint, Vector3 toPoint)
        {
            Vector3 c = this.center.CopyOrMove(fromPoint, toPoint);

            return new Arc
            {
                Center = c,
                Radius = this.radius,
                StartAngle = this.startAngle,
                EndAngle = this.endAngle,
                Thickness = this.thickness,
                IsVisible = this.isVisible
            };
        }

        public override object Rotate2D(Vector3 basePoint, Vector3 targetPoint)
        {
            Vector3 c = this.center.Rotate2D(basePoint, targetPoint);
            Vector3 startpoint = this.StartPoint.Rotate2D(basePoint, targetPoint);
            Vector3 endpoint = this.EndPoint.Rotate2D(basePoint, targetPoint);

            double start = c.AngleWith(startpoint);
            double end = c.AngleWith(endpoint);

            if (end > start)
                end -= start;
            else
                end += 360.0 - start;

            return new Arc
            {
                Center = c,
                Radius = this.radius,
                StartAngle = startAngle,
                EndAngle = end,
                Thickness = this.thickness,
                IsVisible = this.isVisible
            };
        }

        public override object Scale(Vector3 basePoint, double value)
        {
            Vector3 c = this.center.Scale(basePoint, value);
            double r = this.radius * value;

            return new Arc
            {
                Center = c,
                Radius = r,
                StartAngle = this.startAngle,
                EndAngle = this.endAngle,
                Thickness = this.thickness,
                IsVisible = this.isVisible
            };
        }

        public override object Clone()
        {
            return new Arc
            {
                Center = this.center,
                Radius = this.radius,
                StartAngle = this.startAngle,
                EndAngle = this.endAngle,
                Thickness = this.thickness,

                // EntityObject properties
                IsVisible = this.IsVisible,
            };
        }
    }
}
