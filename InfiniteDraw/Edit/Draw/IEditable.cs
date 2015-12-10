using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfiniteDraw.Edit.Draw
{
    public interface IEditable
    {
        IEnumerable<IDraggableComponent> Components { get; }
        EditableToolItem[] EditMenu { get; }
        EditState EditComponentMouseDown(Vector position, MouseButtons button);
        EditState EditComponentMouseMove(Vector position);
        EditState EditComponentMouseUp(Vector position, MouseButtons button);
    }
    public class EditableToolItem
    {
        public string Name { set; get; }
        public Action Callback { set; get; }
        public EditableToolItem(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }
    }
    public enum EditState { Editing, Ended }
}
