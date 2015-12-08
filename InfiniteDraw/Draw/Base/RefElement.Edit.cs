using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Base
{
    public partial class RefElement : IDraggableComponent
    {
        public RectangleF Region => MeasureSize(1, new Matrix(), WorkMode.Edit);

        public void Drag(Vector delta)
        {
            Position += delta;
        }
    }
}
