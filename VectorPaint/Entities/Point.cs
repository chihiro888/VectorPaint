using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class Point
    {
        private Vector3 vector3;
        private double thickness;

        public Point()
        {
            this.Position = Vector3.Zero;
            this.Tickness = 0.0;
        }

        public Point(Vector3 position)
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
            get { return vector3; }
            set { vector3 = value; }
        }
    }
}
