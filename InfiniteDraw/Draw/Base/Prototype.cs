using InfiniteDraw.Edit.Property;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw.Base
{
    public partial class Prototype : Drawable
    {
        private const string defaultName = nameof(Prototype);
        private const int defaultMaxDepth = 8;
        private List<RefElement> elements = new List<RefElement>();

        public int MaxDepth { set; get; } = defaultMaxDepth;
        public string Name { set; get; } = defaultName;

        public Prototype()
        {
        }

        public override void Draw(int depth, Graphics g)
        {
            if (depth < MaxDepth)
                foreach (var e in elements)
                    e.Draw(depth + 1, g);
        }

        public override void EditDraw(int depth, Graphics g)
        {
            if (depth <= 1)
                foreach (var e in elements)
                    e.EditDraw(depth + 1, g);
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

        public override RectangleF MeasureEditSize(int depth, Matrix m)
        {
            if (depth >= 1 || elements.Count == 0)
                return RectangleF.Empty;
            RectangleF size = elements[0].MeasureEditSize(depth + 1, m);
            for (int i = 1; i < elements.Count; i++)
                size = RectangleF.Union(size, elements[i].MeasureEditSize(depth + 1, m));
            return size;
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString() => Name + base.ToString();
    }
}
