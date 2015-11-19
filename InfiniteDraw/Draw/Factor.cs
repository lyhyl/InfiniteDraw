using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class Factor : Drawable
    {
        private List<Drawable> elements = new List<Drawable>();

        public int MaxDepth { set; get; }
        public string Name { set; get; }

        private static int cc = 1;

        public Factor()
        {
            MaxDepth = 8;
            Name = "New Factor " + cc++;
        }

        protected override Bitmap Draw(int depth)
        {
            Rectangle size = MeasureSize();
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            return bmp;
        }

        public override Rectangle MeasureSize()
        {
            Rectangle size = new Rectangle(0, 0, 0, 0);
            foreach (var d in elements)
                size = Rectangle.Union(size, d.MeasureSize());
            return size;
        }

        public void AddElement(Drawable e)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
