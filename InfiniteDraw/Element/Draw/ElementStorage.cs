using InfiniteDraw.Element.Draw.Base;
using InfiniteDraw.Element.Draw.Bezier;
using System;
using System.Collections.Generic;

namespace InfiniteDraw.Element.Draw
{
    public class ElementStorage
    {
        private static ElementStorage instance = null;

        private int MaxID = 0;
        private Dictionary<int, IDrawable> elements = new Dictionary<int, IDrawable>();

        public event ElementStorageEventHandler ElementAdded;
        public event ElementStorageEventHandler ElementRemoved;
        public event ElementStorageEventHandler RequestEditElement;

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

        public int CreateFactor()
        {
            IDrawable d = new Prototype();
            Add(d);
            return d.GID;
        }

        public int CreateRefElement(int gid)
        {
            IDrawable d = new RefElement(gid);
            Add(d);
            return d.GID;
        }

        public int CreateBezier()
        {
            IDrawable d = new Bezier3();
            Add(d);
            return d.GID;
        }

        public void Clear()
        {
            elements.Clear();
        }

        public void Add(IDrawable d)
        {
            d.GID = MaxID++;
            elements[d.GID] = d;
            OnElementAdded(d);
        }

        public void Remove(IDrawable d)
        {
            elements.Remove(d.GID);
            OnElementRemoved(d);
        }

        public void RequestEdit(IDrawable d) => RequestEditElement?.Invoke(this, new ElementStorageEventArgs(d));

        public IDrawable this[int gid] => elements[gid];

        protected void OnElementAdded(IDrawable d)
        {
            ElementAdded?.Invoke(this, new ElementStorageEventArgs(d));
        }

        protected void OnElementRemoved(IDrawable d)
        {
            ElementRemoved?.Invoke(this, new ElementStorageEventArgs(d));
        }
    }

    public delegate void ElementStorageEventHandler(object sender, ElementStorageEventArgs e);
    public class ElementStorageEventArgs : EventArgs
    {
        public IDrawable Element { get; }
        public ElementStorageEventArgs(IDrawable d)
        {
            Element = d;
        }
    }
}
