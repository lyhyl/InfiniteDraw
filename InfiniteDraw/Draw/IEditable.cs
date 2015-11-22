using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    interface IEditable
    {
        RectangleF[] GetEditRegions();
        void Modified(int index);
        void Create();
        void Delete(int index);
    }
}
