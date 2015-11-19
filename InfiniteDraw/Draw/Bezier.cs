using InfiniteDraw.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class Bezier : Drawable
    {
        private Vector[] ctrlps = null;

        protected override Bitmap Draw(int depth)
        {
            throw new NotImplementedException();
        }

        public override Rectangle MeasureSize()
        {
            throw new NotImplementedException();
        }
    }
}
