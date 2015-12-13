using InfiniteDraw.Utilities;
using System.Collections.Generic;
using System.Drawing;

namespace InfiniteDraw.Element.Draw.Bezier
{
    public class BezierSolver
    {
        public static Vector Interpolate(IList<Vector> v, int offest, double t)
        {
            double s = 1 - t;
            Vector t0 = s * s * s * v[0 + offest];
            Vector t1 = s * s * t * v[1 + offest];
            Vector t2 = s * t * t * v[2 + offest];
            Vector t3 = t * t * t * v[3 + offest];
            return t0 + 3 * t1 + 3 * t2 + t3;
        }

        public static Vector Tanget(IList<Vector> v, int offest, double t)
        {
            double s = 1 - t;
            Vector t0 = s * s * (v[1 + offest] - v[0 + offest]);
            Vector t1 = s * t * (v[2 + offest] - v[1 + offest]);
            Vector t2 = t * t * (v[3 + offest] - v[2 + offest]);
            return 3 * t0 + 6 * t1 + 3 * t2;
        }

        public static PointF[] Divide(IList<Vector> v, int offset, int seg)
        {
            PointF[] points = new PointF[seg + 1];
            for (int i = 0; i <= seg; i++)
            {
                Vector tv = Interpolate(v, offset, (double)i / seg);
                points[i] = tv.ToPointF();
            }
            return points;
        }
    }
}
