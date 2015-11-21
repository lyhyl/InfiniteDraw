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
    public class Factor : Drawable
    {
        private List<RefElement> elements = new List<RefElement>();

        public int MaxDepth { set; get; }

        public Factor()
        {
            MaxDepth = 3;
        }

        public override Bitmap Draw(int depth)
        {
            if (depth > MaxDepth)
                return null;
            Rectangle size = MeasureSize(depth);
            if (size.Width == 0 || size.Height == 0)
                return null;
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var e in elements)
            {
                Bitmap sbmp = e.Draw(depth + 1);
                if (sbmp != null)
                {
                    Rectangle es = e.MeasureSize(depth + 1);
                    g.DrawImage(sbmp, new PointF(size.Width / 2 - es.Width / 2, size.Height / 2 - es.Height / 2));
                }
            }
            return bmp;
        }

        public override Rectangle MeasureSize(int depth)
        {
            if (depth > MaxDepth)
                return new Rectangle(0, 0, 0, 0);
            Rectangle size = new Rectangle(0, 0, 0, 0);
            foreach (var d in elements)
                size = Rectangle.Union(size, d.MeasureSize(depth + 1));
            return size;
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString()
        {
            return "Factor" + base.ToString();
        }
    }
}
