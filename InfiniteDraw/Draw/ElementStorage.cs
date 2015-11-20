using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class ElementStorage
    {
        private List<IDrawable> elements = new List<IDrawable>();

        public event ElementEvent ElementAdded;
        public event ElementEvent ElementRemoved;
        public event ElementEvent ElementModified;

        public event ElementEvent RequestEditElement;

        public ElementStorage()
        {
        }

        public IDrawable Create(string type)
        {
            throw new NotImplementedException();
        }

        public int CreateFactor()
        {
            elements.Add(new Factor());
            return elements.Count - 1;
        }

        public int CreateBezier()
        {
            elements.Add(new Bezier());
            return elements.Count - 1;
        }

        public void Reset()
        {
            elements.Clear();
        }

        public void Add(IDrawable d)
        {
            elements.Add(d);

            if (ElementAdded != null)
                ElementAdded(this, new ElementEventArgs(d));
        }

        public void Remove(IDrawable d)
        {
            elements.Remove(d);

            if (ElementRemoved != null)
                ElementRemoved(this, new ElementEventArgs(d));
        }

        public void Modified(IDrawable d)
        {
            if (ElementModified != null)
                ElementModified(this, new ElementEventArgs(d));
        }

        public void RequestEdit(IDrawable d)
        {
            if (RequestEditElement != null)
                RequestEditElement(this, new ElementEventArgs(d));
        }

        public IDrawable this[int index]
        {
            get { return elements[index]; }
        }
    }

    public delegate void ElementEvent(object sender, ElementEventArgs e);
    public class ElementEventArgs : EventArgs
    {
        public IDrawable Drawable { set; get; }
        public ElementEventArgs(IDrawable d)
        {
            Drawable = d;
        }
    }
}
