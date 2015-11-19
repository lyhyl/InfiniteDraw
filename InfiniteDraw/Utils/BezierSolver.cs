using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Utils
{
    public class BezierSolver
    {
        public static Vector Interpolate(Vector a, Vector b, Vector c, Vector d, double t)
        {
            double it = 1 - t;
            return it * it * it * a + 3 * it * it * t * b + 3 * it * t * t * c + t * t * t * d;
        }

        public static Vector Tanget(Vector a, Vector b, Vector c, Vector d, double t)
        {
            double it = 1 - t;
            return 3 * it * it * (b - a) + 6 * it * t * (c - b) + 3 * t * t * (d - c);
        }
    }
}
