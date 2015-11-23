using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Base
{
    public class RefElement : Drawable
    {
        private ElementStorage elements;

        public int Reference { set; get; }
        public Vector Position { set; get; } = Vector.Zero;
        public Vector BaseTransform { set; get; } = Vector.XAxis;

        public RefElement(ElementStorage es, int r)
        {
            elements = es;
            Reference = r;
        }

        public override void Draw(int depth, Graphics g)
        {
            Drawable reference = elements[Reference];
            if (reference == null)
                throw new Exception("Invalid GID");

            Matrix memo = g.Transform;

            float scale = (float)BaseTransform.Length;
            g.TranslateTransform((float)Position.X, (float)Position.Y);
            g.RotateTransform((float)(Math.Atan2(BaseTransform.Y, BaseTransform.X) * 180.0 / Math.PI));
            g.ScaleTransform(scale, scale);
            reference.Draw(depth, g);

            g.Transform = memo;
        }

        public override RectangleF MeasureSize(int depth, Matrix m)
        {
            Drawable reference = elements[Reference];
            if (reference == null)
                throw new Exception("Invalid GID");

            m = m.Clone();

            float scale = (float)BaseTransform.Length;
            m.Translate((float)Position.X, (float)Position.Y);
            m.Rotate((float)(Math.Atan2(BaseTransform.Y, BaseTransform.X) * 180.0 / Math.PI));
            m.Scale(scale, scale);

            return reference.MeasureSize(depth, m);
        }
    }
}
