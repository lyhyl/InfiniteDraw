using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Base
{
    public partial class RefElement : IDraggableComponent
    {
        public RectangleF Region { private set; get; } = new RectangleF();

        public void Drag(Vector delta)
        {
            Region.Offset((-delta).ToPointF());
            Position += delta;
        }
    }
}
