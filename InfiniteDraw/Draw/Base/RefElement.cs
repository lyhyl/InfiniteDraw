using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Base
{
    public class RefElement : Drawable
    {
        private ElementStorage elements;

        public int Reference { set; get; }
        public Vector Position { set; get; }
        public Vector BaseTransform { set; get; }

        public RefElement(ElementStorage es, int r)
        {
            elements = es;
            Reference = r;
            Position = Vector.Zero;
            BaseTransform = Vector.XAxis;
        }

        public override Bitmap Draw(int depth)
        {
            Drawable reference = elements[Reference];
            if (reference == null)
                throw new Exception("Invalid GID");
            Bitmap re = reference.Draw(depth);
            if (re == null)
                return null;

            RectangleF size = MeasureSize(depth);
            if (size == RectangleF.Empty)
                return null;

            Bitmap bmp = new Bitmap(
                (int)(Math.Ceiling(size.Right) - Math.Floor(size.Left)),
                (int)(Math.Ceiling(size.Bottom) - Math.Floor(size.Top)),
                PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            
            float scale = (float)BaseTransform.Length;
            g.TranslateTransform(
                (float)((size.Left + size.Right) / 2 - Math.Floor(size.Left)),
                (float)((size.Top + size.Bottom) / 2 - Math.Floor(size.Top)));
            g.RotateTransform((float)(Math.Atan2(BaseTransform.Y, BaseTransform.X) / Math.PI * 180.0));
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(-re.Width / 2, -re.Height / 2);
            g.DrawImage(re, new Point[] { new Point(0, 0), new Point(re.Width, 0), new Point(0, re.Height) });

            return bmp;
        }

        public override RectangleF MeasureSize(int depth)
        {
            Drawable reference = elements[Reference];
            if (reference == null)
                throw new Exception("Invalid GID");

            RectangleF rs = reference.MeasureSize(depth);
            if (rs == RectangleF.Empty)
                return RectangleF.Empty;

            Vector[] vtrs = new Vector[] {
                new Vector(rs.Left, rs.Top),
                new Vector(rs.Right, rs.Top),
                new Vector(rs.Left, rs.Bottom),
                new Vector(rs.Right, rs.Bottom) };

            double s = BaseTransform.Length;
            double a = Math.Atan2(BaseTransform.Y, BaseTransform.X);
            double cosa = Math.Cos(a), sina = Math.Sin(a);
            for (int i = 0; i < vtrs.Length; i++)
                vtrs[i] = new Vector(
                    cosa * vtrs[i].X * s - sina * vtrs[i].Y * s,
                    sina * vtrs[i].X * s + cosa * vtrs[i].Y * s);

            double l = Math.Floor(Math.Min(Math.Min(vtrs[0].X, vtrs[1].X), Math.Min(vtrs[2].X, vtrs[3].X)));
            double r = Math.Ceiling(Math.Max(Math.Max(vtrs[0].X, vtrs[1].X), Math.Max(vtrs[2].X, vtrs[3].X)));
            double t = Math.Floor(Math.Min(Math.Min(vtrs[0].Y, vtrs[1].Y), Math.Min(vtrs[2].Y, vtrs[3].Y)));
            double b = Math.Ceiling(Math.Max(Math.Max(vtrs[0].Y, vtrs[1].Y), Math.Max(vtrs[2].Y, vtrs[3].Y)));

            return new RectangleF((float)(l + Position.X), (float)(t + Position.Y), (float)(r - l), (float)(b - t));
        }
    }
}
