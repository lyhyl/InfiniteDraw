using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class ElementStorage
    {
        private Dictionary<int, Drawable> elements = new Dictionary<int, Drawable>();

        public event ElementEvent ElementCreated;
        public event ElementEvent ElementDeleted;
        public event ElementEvent ElementModified;

        public event ElementEvent RequestEditElement;

        public ElementStorage()
        {
        }

        public Drawable Create(string type)
        {
            throw new NotImplementedException();
        }

        public int CreateFactor()
        {
            Drawable d = new Factor();
            Add(d);
            return d.GID;
        }

        public RefElement CreateRefElement(int gid)
        {
            return new RefElement(this, gid);
        }

        public int CreateBezier()
        {
            Drawable d = new Bezier();
            Add(d);
            return d.GID;
        }

        public void Reset()
        {
            elements.Clear();
        }

        public void Add(Drawable d)
        {
            elements[d.GID] = d;

            if (ElementCreated != null)
                ElementCreated(this, new ElementEventArgs(d));
        }

        public void Delete(Drawable d)
        {
            elements.Remove(d.GID);

            if (ElementDeleted != null)
                ElementDeleted(this, new ElementEventArgs(d));
        }

        public void Modified(Drawable d)
        {
            if (ElementModified != null)
                ElementModified(this, new ElementEventArgs(d));
        }

        public void RequestEdit(Drawable d)
        {
            if (RequestEditElement != null)
                RequestEditElement(this, new ElementEventArgs(d));
        }

        public Drawable this[int gid]
        {
            get { return elements[gid]; }
        }
    }

    public delegate void ElementEvent(object sender, ElementEventArgs e);
    public class ElementEventArgs : EventArgs
    {
        public Drawable Drawable { set; get; }
        public ElementEventArgs(Drawable d)
        {
            Drawable = d;
        }
    }
}
