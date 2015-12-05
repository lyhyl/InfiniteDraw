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
        EditableMenuItem[] EditMenu { get; }
        void ModifiedComponent(int index);
        void DeleteComponent(int index);
        EditState CreateComponentMouseDown(Vector position);
        EditState CreateComponentMouseMove(Vector position);
        EditState CreateComponentMouseUp(Vector position);
    }
    public class EditableMenuItem
    {
        public string Name { set; get; }
        public Action Callback;
    }
    public enum EditState { Editing, Ended, Quest }
}
