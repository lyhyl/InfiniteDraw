using System;

namespace InfiniteDraw.Utils
{
    public struct Vector
    {
        public static Vector Zero { get { return new Vector(0, 0); } }
        public static Vector XAxis { get { return new Vector(1, 0); } }
        public static Vector YAxis { get { return new Vector(0, 1); } }

        public double X { set; get; }
        public double Y { set; get; }
        public double LengthSq { get { return X * X + Y * Y; } }
        public double Length { get { return Math.Sqrt(LengthSq); } }
        public Vector Normal { get { return new Vector(-Y, X); } }

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

        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, v.Y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator *(Vector v, double f)
        {
            return new Vector(v.X * f, v.Y * f);
        }

        public static Vector operator *(double f, Vector v)
        {
            return new Vector(v.X * f, v.Y * f);
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static Vector operator /(Vector v, double f)
        {
            return new Vector(v.X / f, v.Y / f);
        }
    }
}
