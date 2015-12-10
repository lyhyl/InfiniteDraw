using InfiniteDraw.Draw.Base;
using InfiniteDraw.Draw.Bezier;
using System;
using System.Collections.Generic;

namespace InfiniteDraw.Draw
{
    public class ElementStorage
    {
        private static ElementStorage instance = null;
        private Dictionary<int, Drawable> elements = new Dictionary<int, Drawable>();

        public event ElementEvent ElementCreated;
        public event ElementEvent ElementDeleted;
        public event ElementEvent ElementModified;
        public event ElementEvent ElementSelected;

        public event ElementEvent RequestEditElement;

        public static ElementStorage Instance
        {
            get
            {
                if (instance == null)
                    instance = new ElementStorage();
                return instance;
            }
        }

        public static ElementStorage Open(string path)
        {
            if (instance != null)
                throw new InvalidOperationException("Close storage first");
            instance = new ElementStorage();
            // TODO: Open file & initialize storage
            return instance;
        }

        public static bool Save()
        {
            // TODO: Save to file
            return true;
        }

        public static void Close()
        {
            instance = null;
        }

        private ElementStorage() { }

        public Drawable Create(string type)
        {
            throw new NotImplementedException();
        }

        public int CreateFactor()
        {
            Drawable d = new Prototype();
            Add(d);
            return d.GID;
        }

        public int CreateRefElement(int gid)
        {
            Drawable d = new RefElement(gid);
            Add(d);
            return d.GID;
        }

        public int CreateBezier()
        {
            Drawable d = new Bezier3();
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
            ElementCreated?.Invoke(this, new ElementEventArgs(d));
        }

        public void Delete(Drawable d)
        {
            elements.Remove(d.GID);
            ElementDeleted?.Invoke(this, new ElementEventArgs(d));
        }

        public void Modified(Drawable d) => ElementModified?.Invoke(this, new ElementEventArgs(d));

        public void Selected(Drawable d) => ElementSelected?.Invoke(this, new ElementEventArgs(d));

        public void RequestEdit(Drawable d) => RequestEditElement?.Invoke(this, new ElementEventArgs(d));

        public Drawable this[int gid] => elements[gid];
    }

    public delegate void ElementEvent(object sender, ElementEventArgs e);
    public class ElementEventArgs : EventArgs
    {
        public Drawable Drawable { get; }
        public ElementEventArgs(Drawable d)
        {
            Drawable = d;
        }
    }
}
