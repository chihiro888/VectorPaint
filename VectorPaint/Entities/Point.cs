using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class Point: EntityObject
    {
        private Vector3 position;
        private double thickness;

        public Point():this(Vector3.Zero)
        {
        }

        public Point(Vector3 position) : base(EntityType.Point)
        {
            this.Position = position;
            this.Tickness = 0.0;
        }

        public double Tickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public override object Clone()
        {
            return new Entities.Point
            {
                Position = this.position,
                thickness = this.thickness,

                // EntityObject properties
                IsVisible = this.IsVisible
            };
        }
    }
}
