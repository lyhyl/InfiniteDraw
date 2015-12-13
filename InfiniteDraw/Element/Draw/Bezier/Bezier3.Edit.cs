using InfiniteDraw.Edit.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteDraw.Utilities;
using System.Windows.Forms;

namespace InfiniteDraw.Element.Draw.Bezier
{
    public partial class Bezier3 : IEditable
    {
        private enum Bezier3EditingState { Locating, Controlling, None }

        private bool toolItemsCreated = false;
        private EditableToolItem extendToolItem;
        private Bezier3EditingState editingState = Bezier3EditingState.None;

        public IEnumerable<IDraggableComponent> Components => componentProxies;

        public EditableToolItem[] EditMenu
        {
            get
            {
                if (!toolItemsCreated)
                    CreateToolItems();
                return new EditableToolItem[] { extendToolItem };
            }
        }
        
        public EditState EditComponentMouseDown(Vector position, MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    editingState = Bezier3EditingState.None;
                    return EditState.Ended;
                case MouseButtons.Middle:
                default:
                    break;
            }
            switch (editingState)
            {
                case Bezier3EditingState.Locating:
                    for (int i = 0; i < 3; i++) // Really... 3 points
                        AddControlPoint(position);
                    editingState = Bezier3EditingState.Controlling;
                    return EditState.Editing;
                default:
                    editingState = Bezier3EditingState.None;
                    return EditState.Ended;
            }
        }

        public EditState EditComponentMouseMove(Vector position)
        {
            switch (editingState)
            {
                case Bezier3EditingState.Controlling:
                    componentProxies[componentProxies.Count - 1].DragTo(position);
                    return EditState.Editing;
                default:
                    editingState = Bezier3EditingState.None;
                    return EditState.Ended;
            }
        }

        public EditState EditComponentMouseUp(Vector position, MouseButtons button)
        {
            switch (editingState)
            {
                case Bezier3EditingState.Locating:
                    return EditState.Editing;
                case Bezier3EditingState.Controlling:
                    editingState = Bezier3EditingState.Locating;
                    return EditState.Editing;
                default:
                    editingState = Bezier3EditingState.None;
                    return EditState.Ended;
            }
        }

        private void CreateToolItems()
        {
            toolItemsCreated = true;
            extendToolItem = new EditableToolItem("Extend Curve", () => editingState = Bezier3EditingState.Locating);
        }
    }
}
