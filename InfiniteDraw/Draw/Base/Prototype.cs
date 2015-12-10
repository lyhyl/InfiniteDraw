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

        public override void Draw(Graphics g, int depth, Matrix m, WorkMode workMode)
        {
            if (depth >= DepthControl(workMode))
                return;
            foreach (var e in elements)
                e.Draw(g, depth + 1, m, workMode);
        }

        public override RectangleF MeasureSize(int depth, Matrix m, WorkMode workMode)
        {
            if (depth >= DepthControl(workMode) || elements.Count == 0)
                return RectangleF.Empty;
            return NonEmptySize(depth, m, workMode);
        }

        private int DepthControl(WorkMode workMode)
        {
            switch (workMode)
            {
                case WorkMode.Edit:
                    return 2;
                case WorkMode.Render:
                    return MaxDepth;
                default:
                    return 0;
            }
        }

        private RectangleF NonEmptySize(int depth, Matrix m, WorkMode workMode)
        {
            int idx = 0;
            RectangleF size = elements[idx].MeasureSize(depth + 1, m, workMode);
            for (; size.IsEmpty && idx < elements.Count; idx++)
                size = elements[idx].MeasureSize(depth + 1, m, workMode);
            for (; idx < elements.Count; idx++)
            {
                RectangleF sz = elements[idx].MeasureSize(depth + 1, m, workMode);
                if (!sz.IsEmpty)
                    size = RectangleF.Union(size, sz);
            }
            return size;
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString() => Name + base.ToString();
    }
}
