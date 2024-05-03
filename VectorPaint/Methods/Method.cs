using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Methods
{
    public class Method
    {
        public static double LineAngle(Vector3 start, Vector3 end)
        {
            double angle = Math.Atan2((end.Y - start.Y), (end.X - start.X)) * 180.0 / Math.PI;
            if (angle < 0.0)
                angle += 360.0;
            return angle;
        }

        public static Entities.Ellipse GetEllipse(Vector3 center, Vector3 firstPoint, Vector3 secondPoint)
        {
            double major = center.DistanceFrom(firstPoint);
            double minor = center.DistanceFrom(secondPoint);
            double angle = LineAngle(center, firstPoint);
            Entities.Ellipse elp = new Entities.Ellipse(center, major, minor);
            elp.Rotation = angle;
            return elp;
        }
    }
}
