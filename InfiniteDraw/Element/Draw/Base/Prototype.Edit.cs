using InfiniteDraw.Edit.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteDraw.Utilities;
using System.Windows.Forms;

namespace InfiniteDraw.Element.Draw.Base
{
    public partial class Prototype : IEditable
    {
        private bool addRef = false;

        private bool toolItemsCreated = false;
        private EditableToolItem addRefToolItem;

        public IEnumerable<IDraggableComponent> Components => elements;

        public EditableToolItem[] EditMenu
        {
            get
            {
                if(!toolItemsCreated)
                    CreateToolItems();
                return new EditableToolItem[] { addRefToolItem };
            }
        }

        public EditState EditComponentMouseDown(Vector position, MouseButtons button)
        {
            if(addRef)
            {
                addRef = false;
                int reid = ElementStorage.Instance.CreateRefElement(GID);
                RefElement re = ElementStorage.Instance[reid] as RefElement;
                re.Position = position;
                AddElement(re);
                return EditState.Ended;
            }
            return EditState.Ended;
        }

        public EditState EditComponentMouseMove(Vector position)
        {
            return EditState.Ended;
        }

        public EditState EditComponentMouseUp(Vector position, MouseButtons button)
        {
            return EditState.Ended;
        }

        private void CreateToolItems()
        {
            toolItemsCreated = true;
            addRefToolItem = new EditableToolItem("Add Ref", () => { addRef = true; });
        }
    }
}
