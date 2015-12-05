using InfiniteDraw.Edit.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfiniteDraw.Utilities;

namespace InfiniteDraw.Draw.Base
{
    public partial class Prototype : IEditable
    {
        public IEnumerable<IDraggableComponent> Components
        {
            get
            {
                return elements;
            }
        }

        public EditableMenuItem[] EditMenu
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public EditState CreateComponentMouseDown(Vector position)
        {
            throw new NotImplementedException();
        }

        public EditState CreateComponentMouseMove(Vector position)
        {
            throw new NotImplementedException();
        }

        public EditState CreateComponentMouseUp(Vector position)
        {
            throw new NotImplementedException();
        }

        public void DeleteComponent(int index)
        {
            throw new NotImplementedException();
        }

        public void ModifiedComponent(int index)
        {
            throw new NotImplementedException();
        }
    }
}
