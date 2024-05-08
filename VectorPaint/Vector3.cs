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

        public static bool operator ==(Vector3 v1, Vector3 v2)
        {
            return Equals(v1, v2);
        }

        public static bool operator !=(Vector3 v1, Vector3 v2)
        {
            return !Equals(v1, v2);
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator *(Vector3 v, double d)
        {
            return new Vector3(v.X * d, v.Y * d, v.Z * d);
        }

        public static Vector3 operator *(double d, Vector3 v)
        {
            return new Vector3(v.X * d, v.Y * d, v.Z * d);
        }

        public static Vector3 operator /(Vector3 v, double d)
        {
            double inv = 1 / d;
            return v * inv;
        }

        public static Vector3 operator /(double d, Vector3 v)
        {
            return new Vector3(d / v.X, d / v.Y, d / v.Z);
        }

        public void Normalize()
        {
            double m = Modulus();
            if (Methods.Method.IsZero(m, Methods.Method.Epsilon))
                throw new ArithmeticException("Cannot normalize a zero vector.");
            double m_inv = 1 / m;
            this.x *= m_inv;
            this.y *= m_inv; 
            this.z *= m_inv;

        }

        public double Modulus()
        {
            return Math.Sqrt(DotProduct(this, this));
        }

        public double AngleWith(Vector3 v)
        {
            double angle = Math.Atan2((v.Y - this.y), (v.X - this.x)) * 180.0 / Math.PI;
            if (angle < 0)
                angle += 360.0;
            return angle;
        }

        public bool Equals(Vector3 v, double threshold)
        {
            return (Methods.Method.IsEqual(v.X, this.x, threshold) && 
                (Methods.Method.IsEqual(v.Y, this.y, threshold) && 
                (Methods.Method.IsEqual(v.Z, this.z, threshold))));
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
                return this.Equals((Vector3)obj, Methods.Method.Epsilon);
            return false;
        }

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

        public Vector3 CopyOrMove(Vector3 fromPoint, Vector3 toPoint)
        {
            double dx = toPoint.X - fromPoint.X;
            double dy = toPoint.Y - fromPoint.Y;
            double dz = toPoint.Z - fromPoint.Z;

            return new Vector3(this.x + dx, this.y + dy, this.z + dz);
        }

        public Vector3 Rotate2D(Vector3 basePoint, Vector3 targetPoint)
        {
            double angle = basePoint.AngleWith(targetPoint);
            double length = basePoint.DistanceFrom(this);
            angle += basePoint.AngleWith(this);

            double sin = Math.Sin(angle * Math.PI / 180.0);
            double cos = Math.Cos(angle * Math.PI / 180.0);

            double x = length * cos + basePoint.X;
            double y = length * sin + basePoint.Y;

            return new Vector3(x, y);
        }

        public Vector3 Scale(Vector3 basePoint, double value)
        {
            double length = this.DistanceFrom(basePoint) * value;
            double angle = basePoint.AngleWith(this) * Math.PI / 180.0;
            double x = length * Math.Cos(angle) + basePoint.X;
            double y = length * Math.Sin(angle) + basePoint.Y;

            return new Vector3(x, y);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", X, Y, Z);
        }
    }
}
