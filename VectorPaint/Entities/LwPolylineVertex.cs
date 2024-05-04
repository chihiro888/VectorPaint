using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class LwPolylineVertex
    {
        private Vector2 position;
        private double bulge;

        public LwPolylineVertex() : this(Vector2.Zero, 0.0) 
        { 
        }

        public LwPolylineVertex(Vector2 position) : this(position, 0.0)
        {
        }

        public LwPolylineVertex(Vector2 position, double bulge)
        {
            this.position = position;
            this.bulge = bulge;
        }

        public LwPolylineVertex(double x, double y): this(new Vector2(x, y), 0.0)
        {
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public double Bulge
        {
            get { return bulge; }
            set { bulge = value; }  
        }
    }
}
