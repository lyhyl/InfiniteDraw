using InfiniteDraw.Edit;
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
    public class Factor : Drawable, IPropertyEditable
    {
        private List<RefElement> elements = new List<RefElement>();

        public int MaxDepth { set; get; }
        public string Name { set; get; }

        public Factor()
        {
            MaxDepth = 8;
            Name = "Factor";
        }

        public override Bitmap Draw(int depth)
        {
            if (depth >= MaxDepth)
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
            foreach (var e in elements)
            {
                Bitmap sbmp = e.Draw(depth + 1);
                if (sbmp != null)
                {
                    RectangleF es = e.MeasureSize(depth + 1);
                    g.DrawImage(sbmp, new PointF(size.Width / 2 - es.Width / 2, size.Height / 2 - es.Height / 2));
                }
            }
            return bmp;
        }

        public override RectangleF MeasureSize(int depth)
        {
            if (depth >= MaxDepth || elements.Count == 0)
                return RectangleF.Empty;
            RectangleF size = elements[0].MeasureSize(depth + 1);
            for (int i = 1; i < elements.Count; i++)
                size = RectangleF.Union(size, elements[i].MeasureSize(depth + 1));
            return size;
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString()
        {
            return Name + base.ToString();
        }

        public ElementProperty[] EditableProperties
        {
            get
            {
                return new ElementProperty[]
                {
                    new ElementProperty("Name", typeof(string),
                    (v) => {
                        if (v is string)
                            Name = v as string;
                        return !(v is string);
                    },
                    () => { return Name; }, "Factor"),

                    new ElementProperty("Max Depth", typeof(int),
                    (v) => {
                        if (v is int)
                            MaxDepth = (int)v;
                        return !(v is string);
                    },
                    () => { return MaxDepth; }, 8)
                };
            }
        }
    }
}
