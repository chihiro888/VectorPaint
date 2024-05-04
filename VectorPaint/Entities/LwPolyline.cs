using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class LwPolyline
    {
        private List<LwPolylineVertex> vertexes;
        private PolylineTypeFlags flags;
        private double thickness;

        public LwPolyline(): this(new List<LwPolylineVertex>(), false)
        {
        }

        public LwPolyline(List<LwPolylineVertex> vertexes, bool IsClosed)
        {
            if (vertexes == null) throw new ArgumentNullException(nameof(vertexes));
            this.vertexes = vertexes;
            this.flags = IsClosed ? PolylineTypeFlags.CloseLwPolyline : PolylineTypeFlags.OpenLwPolyline;
            this.thickness = 0.0;
        }

        public List<LwPolylineVertex> Vertexes
        {
            get { return this.vertexes; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(vertexes));
                this.vertexes = value;
            }
        }

        public PolylineTypeFlags Flags
        {
            get { return this.flags; }
            set { this.flags = value; }
        }

        public bool IsClosed
        {
            get {
                return (this.flags & PolylineTypeFlags.CloseLwPolyline) == PolylineTypeFlags.CloseLwPolyline;
            }
            set
            {
                this.flags = value ? PolylineTypeFlags.CloseLwPolyline : PolylineTypeFlags.OpenLwPolyline;
            }
        }

        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
    }
}
