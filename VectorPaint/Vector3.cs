using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint
{
    // #002- vector and draw a point
    public class Vector3
    {
        private double x;
        private double y;
        private double z;

        public Vector3(double x, double y )
        {
            this.X = x;
            this.Y = y;
            this.Z = 0.0;
        }

        public Vector3(double x, double y, double z): this(x,y)
        {
            this.Z = z;
        }

        public double X
        {
            get { return x;  }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        // #002- vector and draw a point
        public System.Drawing.PointF ToPointF
        {
            get
            {
                return new System.Drawing.PointF((float)X, (float)Y);
            }
        }

        // #002- vector and draw a point
        public static Vector3 Zero
        {
            get { return new Vector3(0.0, 0.0, 0.0); }
        }

        public static Vector3 UnitX
        {
            get { return new Vector3(1, 0, 0); }
        }

        public static Vector3 UnitY
        {
            get { return new Vector3(0, 1, 0); }
        }

        public static Vector3 UnitZ
        {
            get { return new Vector3(0, 0, 1); }
        }

        public static Vector3 NaN
        {
            get { return new Vector3(double.NaN, double.NaN, double.NaN); }
        }

        public double this[int index]
        {
            get
            {
                switch(index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch(index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        public static double DotProduct(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            double vx = v1.Y * v2.Z - v1.Z * v1.Y;
            double vy = v1.Z * v2.X - v1.X * v2.Z;
            double vz = v1.X * v2.Y - v1.Y * v2.X;
            return new Vector3(vx, vy, vz);
        }

        public Vector3 Round(int numDigits)
        {
            return new Vector3(
                Math.Round(this.x, numDigits),
                Math.Round(this.y, numDigits),
                Math.Round(this.z, numDigits)
            );
        }

        /*
        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return Equals(v1, v2);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !Equals(v1, v2);
        }
        */

        public void Normalize()
        {
            // TODO #032 - methods and operators in Vector3
        }

        public double AngleWith(Vector3 v)
        {
            double angle = Math.Atan2((v.Y - this.y), (v.X - this.x)) * 180.0 / Math.PI;
            if (angle < 0)
                angle += 360.0;
            return angle;
        }

        /*
        public bool Equals(Vector3 v, double threshold)
        {
            // TODO #032 - methods and operators in Vector3
            return true;
        }
        */

        public double DistanceFrom(Vector3 v)
        {
            double dx = v.X - X;
            double dy = v.Y - Y;
            double dz = v.Z - Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public Vector2 ToVector2
        {
            get { return new Vector2(X, Y); }
        }
    }
}
