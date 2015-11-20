using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class RefElement : IDrawable
    {
        private ElementStorage elements;
        public int Reference { set; get; }
        public Vector Position { set; get; }
        public Vector BaseTransform { set; get; }

        public string Name { set; get; }

        public RefElement(ElementStorage es, int i)
        {
            elements = es;
            Reference = i;
            Position = Vector.Zero;
            BaseTransform = Vector.YAxis;
        }

        public Bitmap Draw(int depth)
        {
            return elements[Reference].Draw(depth);
        }

        public Rectangle MeasureSize()
        {
            return elements[Reference].MeasureSize();
        }
    }
}
