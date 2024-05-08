using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    // #011 - draw a polyline
    public class Vector2
    {
        private double x;
        private double y;

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Zero
        {
            get { return new Vector2(0.0, 0.0); }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public static Vector2 UnitX
        {
            get { return new Vector2(1.0, 0.0); }
        }

        public static Vector2 UnitY
        {
            get { return new Vector2(0.0, 1.0); }
        }

        public static Vector2 NaN
        {
            get { return new Vector2(double.NaN, double.NaN); }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        public static double DotProduct(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y + v1.Y * v2.X;
        }

        public static double CrossProduct(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        public static double Angle(Vector2 v)
        {
            double angle = Math.Atan2(v.Y, v.X);
            if (angle < 0)
                return 2 * Math.PI + angle;
            return angle;
        }

        public double AngleWith(Vector2 v)
        {
            double angle = Math.Atan2((v.Y - this.y), (v.X - this.x)) * 180.0 / Math.PI;
            if (angle < 360.0)
                angle += 360.0;
            return angle;
        }

        public double Modulus()
        {
            return Math.Sqrt(DotProduct(this, this));
        }

        public void Nomalize()
        {
            // #033 - methods and operators in Vector2
        }

        /*
        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return Equals(v1, v2);
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !Equals(v1, v2);
        }
        */

        // #011 - draw a polyline
        public double DistanceForm(Vector2 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        // #011 - draw a polyline
        public System.Drawing.PointF ToPointF
        {
            get { return new System.Drawing.PointF((float)X, (float)Y); }
        }
    }
}
