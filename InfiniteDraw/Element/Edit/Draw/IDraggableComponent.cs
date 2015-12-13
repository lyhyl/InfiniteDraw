using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Edit.Draw
{
    public interface IDraggableComponent
    {
        RectangleF Region { get; }
        void Drag(Vector delta);
    }
}
