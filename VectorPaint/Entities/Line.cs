using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class Line
    {
        private Vector3 startPoint;
        private Vector3 endPoint;
        private double thickness;

        public Line() : this(Vector3.Zero, Vector3.Zero)
        {
        }

        public Line(Vector3 start, Vector3 end)
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
    }
}
