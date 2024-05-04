using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPaint.Entities
{
    [Flags]
    public enum PolylineTypeFlags
    {
        OpenLwPolyline = 0,
        CloseLwPolyline = 1
    }
}
