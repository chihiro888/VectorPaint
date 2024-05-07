using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    public class LwPolyline: EntityObject
    {
        private List<LwPolylineVertex> vertexes;
        private PolylineTypeFlags flags;
        private double thickness;

        public LwPolyline(): this(new List<LwPolylineVertex>(), false)
        {
        }

        public LwPolyline(List<LwPolylineVertex> vertexes, bool IsClosed): base(EntityType.LwPolyline)
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

        /*
        public List<EntityObject> Explode()
        {
            List<EntityObject> entities = new List<EntityObject>();
            int index = 0;
            foreach(LwPolylineVertex vertex in this.vertexes)
            {
                double bulge = vertex.Bulge;
                Vector2 p1, p2;

                if (index == this.vertexes.Count - 1)
                {
                    if(!this.IsClosed)
                    {
                        break;
                    }
                    p1 = new Vector2(vertex.Position.X, vertex.Position.Y);
                    p2 = new Vector2(this.vertexes[0].Position.X, this.vertexes[0].Position.Y);
                }
                else
                {
                    p1 = new Vector2(vertex.Position.X, vertex.Position.Y);
                    p2 = new Vector2(this.vertexes[index + 1].Position.X, this.vertexes[index + 1].Position.Y);

                }

                if (Methods.Method.IsZero(bulge))
                {
                    entities.Add(new Line
                    {
                        StartPoint = new Vector3(p1.X, p1.Y),
                        EndPoint = new Vector3(p2.X, p2.Y),
                        Thickness = this.thickness,
                    });
                } else
                {
                    double theta = 4 * Math.Atan(Math.Abs(bulge));
                    double c = p1.DistanceForm(p2);
                    double r = c / 2 / Math.Sin(theta / 2);

                    if (Methods.Method.IsZero(r))
                    {
                        entities.Add(new Line
                        {
                            StartPoint = new Vector3(p1.X, p1.Y),
                            EndPoint = new Vector3(p2.X, p2.Y),
                            Thickness = this.thickness,
                        });
                    } else
                    {
                        double gama = (Math.PI - theta) / 2;
                        double phi = p1.AngleWith(p2) + Math.Sign(bulge) * gama;
                        Vector3 center = new Vector2(p1.X + r * Math.Cos(phi), p1.Y + r * Math.Sin(phi));
                        double startAngle, endAngle; 

                        if (bulge > 0 )
                        {
                            startAngle = Vector2.Angle(p1 - center) * Methods.Method.DegToRad;
                            endAngle = startAngle + theta * Methods.Method.DegToRad;
                        } 
                        else
                        {
                            endAngle = Vector2.Angle(p1 - center) * Methods.Method.DegToRad;
                            startAngle = endAngle - theta * Methods.Method.DegToRad;
                        }

                        entities.Add(new Arc
                        {
                            Center = new Vector3(center.X, center.Y),
                            Radius = r,
                            StartAngle = startAngle,
                            EndAngle = endAngle,
                            Thickness = this.Thickness
                        });
                    }
                }
                index++;
            }
            return entities;
        }
        */

        public override object Clone()
        {
            List<LwPolylineVertex> vertexs_copy = new List<LwPolylineVertex>();
            foreach(LwPolylineVertex vertex in this.vertexes)
            {
                vertexs_copy.Add((LwPolylineVertex)vertex.Clone());
            }
            return new LwPolyline
            {
                Vertexes = vertexs_copy,
                Flags = this.flags,
                Thickness = this.thickness,

                // EntityObject properties
                IsVisible = this.IsVisible,
            };
        }
    }
}
