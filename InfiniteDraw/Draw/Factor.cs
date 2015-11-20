using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class Factor : IDrawable
    {
        private List<RefElement> elements = new List<RefElement>();

        public int MaxDepth { set; get; }
        public string Name { set; get; }

        private static int cc = 1;

        public Factor()
        {
            MaxDepth = 8;
            Name = "New Factor " + cc++;
        }

        public Bitmap Draw(int depth)
        {
            if (depth > MaxDepth)
                return null;
            Rectangle size = MeasureSize();
            if (size.Width == 0)
                size.Width = 100;
            if (size.Height == 0)
                size.Height = 100;
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.DrawLine(Pens.Black, Point.Empty, new Point(size.Size));
            g.DrawLine(Pens.Black, new Point(size.Width, 0), new Point(0, size.Height));
            foreach (var e in elements)
            {
                Bitmap sbmp = e.Draw(depth + 1);
                if (sbmp != null)
                    g.DrawImage(sbmp, Point.Empty);
            }
            return bmp;
        }

        public Rectangle MeasureSize()
        {
            Rectangle size = new Rectangle(0, 0, 0, 0);
            foreach (var d in elements)
                size = Rectangle.Union(size, d.MeasureSize());
            return size;
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
