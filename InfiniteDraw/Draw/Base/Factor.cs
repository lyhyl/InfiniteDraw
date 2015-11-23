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
        private const string defaultName = nameof(Factor);
        private const int defaultMaxDepth = 8;
        private List<RefElement> elements = new List<RefElement>();

        public int MaxDepth { set; get; } = defaultMaxDepth;
        public string Name { set; get; } = defaultName;

        public Factor()
        {
        }

        public override void Draw(int depth, Graphics g)
        {
            if (depth >= MaxDepth)
                return;
            foreach (var e in elements)
                e.Draw(depth + 1, g);
        }

        public override RectangleF MeasureSize(int depth, Matrix m)
        {
            if (depth >= MaxDepth || elements.Count == 0)
                return RectangleF.Empty;
            RectangleF size = elements[0].MeasureSize(depth + 1, m);
            for (int i = 1; i < elements.Count; i++)
                size = RectangleF.Union(size, elements[i].MeasureSize(depth + 1, m));
            return size;
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString() => Name + base.ToString();

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
                        return v is string;
                    },
                    () => { return Name; }, defaultName),

                    new ElementProperty("Max Depth", typeof(int),
                    (v) => {
                        if (v is int)
                            MaxDepth = (int)v;
                        return v is int;
                    },
                    () => { return MaxDepth; }, defaultMaxDepth)
                };
            }
        }
    }
}
