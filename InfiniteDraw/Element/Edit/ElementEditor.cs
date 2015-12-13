using InfiniteDraw.Edit.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Edit
{
    public class ElementEditor
    {
        public IEditable Editable { set; get; }

        public ElementEditor(IEditable editable)
        {
            Editable = editable;
            if (Editable == null)
                throw new ArgumentNullException(nameof(editable));
        }
    }
}
