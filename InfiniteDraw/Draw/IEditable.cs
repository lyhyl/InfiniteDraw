using InfiniteDraw.Utilities;
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
        EditState Create(Vector position);
        void Delete(int index);
    }
    public enum EditState { Editing, Ended, Quest }
}
