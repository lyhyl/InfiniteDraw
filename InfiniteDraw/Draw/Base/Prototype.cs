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

        public override void Draw(Graphics g, int depth, Matrix m, WorkMode editMode)
        {
            if (depth >= DepthControl(editMode))
                return;
            foreach (var e in elements)
                e.Draw(g, depth + 1, m, editMode);
            if (depth == 0)
                DrawAxes(g, m, editMode);
        }

        public override RectangleF MeasureSize(int depth, Matrix m, WorkMode editMode)
        {
            if (depth >= DepthControl(editMode) || elements.Count == 0)
                return RectangleF.Empty;
            return NonEmptySize(depth, m, editMode);
        }

        private int DepthControl(WorkMode editMode)
        {
            switch (editMode)
            {
                case WorkMode.Edit:
                    return 2;
                case WorkMode.Render:
                    return MaxDepth;
                default:
                    return 0;
            }
        }

        private RectangleF NonEmptySize(int depth, Matrix m, WorkMode editMode)
        {
            int idx = 0;
            RectangleF size = elements[idx].MeasureSize(depth + 1, m, editMode);
            for (; size.IsEmpty && idx < elements.Count; idx++)
                size = elements[idx].MeasureSize(depth + 1, m, editMode);
            for (; idx < elements.Count; idx++)
            {
                RectangleF sz = elements[idx].MeasureSize(depth + 1, m, editMode);
                if (!sz.IsEmpty)
                    size = RectangleF.Union(size, sz);
            }
            return size;
        }
        
        private void DrawAxes(Graphics g, Matrix m, WorkMode editMode)
        {
            g.DrawLine(Pens.Red, new Point(0, 0), new Point(0, 100));
            g.DrawLine(Pens.Green, new Point(0, 0), new Point(100, 0));
        }

        public void AddElement(RefElement re)
        {
            elements.Add(re);
        }

        public override string ToString() => Name + base.ToString();
    }
}
