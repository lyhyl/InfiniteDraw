using InfiniteDraw.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class RefFactor : Drawable
    {
        private FactorStorage factors;
        private int index;
        public Vector Position { set; get; }
        public Vector BaseTransform { set; get; }

        public RefFactor(FactorStorage fs, int i)
        {
            factors = fs;
            index = i;
            Position = Vector.Zero;
            BaseTransform = Vector.YAxis;
        }

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
