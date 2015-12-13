using System;

namespace InfiniteDraw.Element
{
    public interface IElement
    {
        event EventHandler Modified;
        event EventHandler Actived;

        void Active();
    }
}
