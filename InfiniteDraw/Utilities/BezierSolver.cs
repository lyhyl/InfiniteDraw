using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Utilities
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

        public static PointF[] Divide(Vector a, Vector b, Vector c, Vector d, int prec)
        {
            PointF[] points = new PointF[prec + 1];
            points[0] = a.ToPointF();
            for (int i = 1; i <= prec; i++)
                points[i] = Interpolate(a, b, c, d, (double)i / prec).ToPointF();
            return points;
        }
    }

    public static class VectorConvert
    {
        public static PointF ToPointF(this Vector v)
        {
            return new PointF((float)v.X, (float)v.Y);
        }
    }
}
