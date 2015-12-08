using System;
using System.Drawing;

namespace InfiniteDraw.Utilities
{
    public struct Vector
    {
        public static Vector Zero => new Vector(0, 0);
        public static Vector XAxis => new Vector(1, 0);
        public static Vector YAxis => new Vector(0, 1);

        public double X { set; get; }
        public double Y { set; get; }

        public double LengthSq => X * X + Y* Y;
        public double Length => Math.Sqrt(LengthSq);
        public Vector Normal => new Vector(-Y, X);

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Normalize()
        {
            double l = Length;
            X /= l;
            Y /= l;
        }

        public static Vector operator -(Vector v) => new Vector(-v.X, v.Y);
        public static Vector operator +(Vector a, Vector b) => new Vector(a.X + b.X, a.Y + b.Y);
        public static Vector operator -(Vector a, Vector b) => new Vector(a.X - b.X, a.Y - b.Y);
        public static Vector operator *(Vector v, double f) => new Vector(v.X * f, v.Y * f);
        public static Vector operator *(double f, Vector v) => new Vector(v.X * f, v.Y * f);
        public static double operator *(Vector a, Vector b) => a.X * b.X + a.Y * b.Y;
        public static Vector operator /(Vector v, double f) => new Vector(v.X / f, v.Y / f);

        public override string ToString() => $"<{X},{Y}>";
    }

    public static class VectorConvert
    {
        public static PointF ToPointF(this Vector v) => new PointF((float)v.X, (float)v.Y);
    }
}
