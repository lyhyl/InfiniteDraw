using InfiniteDraw.Edit.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteDraw.Utilities;
using System.Drawing;

namespace InfiniteDraw.Draw.Bezier
{
    public partial class Bezier3
    {
        private partial class ControlPoint : IDraggableComponent
        {
            private int index;

            public RectangleF Region
            {
                get
                {
                    return RectangleF.Empty;
                }
            }

            public ControlPoint(int idx)
            {
                index = idx;
            }

            public void Drag(Vector delta)
            {
            }
        }
    }
}
