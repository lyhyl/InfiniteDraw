using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Edit.Property
{
    public interface IPropertyEditable
    {
        ElementProperty[] EditableProperties { get; }
    }
}
