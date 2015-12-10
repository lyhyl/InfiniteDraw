using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System.Drawing;

namespace InfiniteDraw.Draw.Bezier
{
    public partial class Bezier3
    {
        private partial class ControlPoint : IDraggableComponent
        {
            public int Index { set; get; }
            public Bezier3 Curve { set; get; }

            public RectangleF Region
            {
                get
                {
                    const float size = 6, hsize = size / 2;
                    Vector pos = Curve.controlPoints[Index];
                    return new RectangleF((float)(pos.X - hsize), (float)(pos.Y - hsize), size, size);
                }
            }

            public ControlPoint(Bezier3 curve, int index)
            {
                Curve = curve;
                Index = index;
            }

            public void Drag(Vector delta) => AdjustRelativePoints(delta);

            internal void DragTo(Vector position) => Drag(position - Curve.controlPoints[Index]);

            private void AdjustRelativePoints(Vector delta)
            {
                switch (Index % 3)
                {
                    case 0:
                        Curve.controlPoints[Index] += delta;
                        Curve.controlPoints[Index + 2] -= delta;
                        break;
                    case 1:
                        for (int i = -1; i <= 1; i++)
                            Curve.controlPoints[Index + i] += delta;
                        break;
                    case 2:
                        Curve.controlPoints[Index] += delta;
                        Curve.controlPoints[Index - 2] -= delta;
                        break;
                }
            }
        }
    }
}
