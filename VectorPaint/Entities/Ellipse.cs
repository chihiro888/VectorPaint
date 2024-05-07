using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class Ellipse: EntityObject  
    {
        private Vector3 center;
        private double majorAxis;
        private double minorAxis;
        private double rotation;
        private double startAngle;
        private double endAngle;
        private double thickness;

        public Ellipse(Vector3 center, double majoraxis, double minoraxis) : base(EntityType.Ellipse)
        {
            this.Center = center;
            this.MajorAxis = majoraxis;
            this.MinorAxis = minoraxis;
            this.Rotation = 0.0;
            this.startAngle = 0.0;
            this.Thickness = 0.0;
        }

        public Ellipse(): this(Vector3.Zero, 1.0, 0.5)
        {
        }

        public Vector3 Center
        {
            get { return this.center; }
            set { this.center = value; }
        }

        public double MajorAxis
        {
            get { return this.majorAxis; }
            set { this.majorAxis = value; }
        }

        public double MinorAxis
        {
            get { return this.minorAxis; }
            set { this.minorAxis = value; }
        }

        public double Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        public double StartAngle
        {
            get { return this.startAngle; }
            set { this.startAngle = value; }
        }

        public double EndAngle
        {
            get { return this.endAngle; }
            set { this.endAngle = value; }
        }

        public double Thickness
        {
            get { return this.thickness; }
            set { this.thickness = value; }
        }

        public override object Clone()
        {
            return new Ellipse
            {
                center = this.center,
                MajorAxis = this.majorAxis,
                MinorAxis = this.minorAxis,
                Rotation = this.rotation,
                StartAngle = this.startAngle,
                EndAngle = this.endAngle,
                Thickness = this.thickness,

                // EntityObject properties
                IsVisible = this.IsVisible,
            };
        }
    }
}
