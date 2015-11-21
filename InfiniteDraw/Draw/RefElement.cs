using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
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

            Rectangle size = MeasureSize(depth);
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            
            float scale = (float)BaseTransform.Length;
            int w = re.Width / 2, h = re.Height / 2;
            g.TranslateTransform((float)(Position.X + size.Width / 2), (float)(Position.Y + size.Height / 2));
            g.RotateTransform((float)(Math.Atan2(BaseTransform.Y, BaseTransform.X) / Math.PI * -180.0));
            g.ScaleTransform(scale, scale);
            g.DrawImage(re, new Point[] { new Point(-w, -h), new Point(w, -h), new Point(-w, h) });

            return bmp;
        }

        public override Rectangle MeasureSize(int depth)
        {
            Drawable reference = elements[Reference];
            if (reference == null)
                throw new Exception("Invalid GID");

            Rectangle re = reference.MeasureSize(depth);
            double sqlen = re.Width * re.Width + re.Height * re.Height;
            int ext = (int)Math.Ceiling(Math.Sqrt(sqlen)) / 2;
            int w = (int)Math.Abs(Position.X) + ext;
            int h = (int)Math.Abs(Position.Y) + ext;
            return new Rectangle(-w, -h, w * 2, h * 2);
        }
    }
}
