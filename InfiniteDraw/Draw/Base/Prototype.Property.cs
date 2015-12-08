using InfiniteDraw.Edit.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Base
{
    public partial class Prototype : IPropertyEditable
    {
        public ElementProperty[] EditableProperties
        {
            get
            {
                return new ElementProperty[]
                {
                    new ElementProperty("Name", typeof(string),
                    (v) => {
                        try
                        {
                            Name = Convert.ToString(v);
                        }
                        catch(Exception)
                        {
                            return false;
                        }
                        return true;
                    },
                    () => { return Name; }, defaultName),

                    new ElementProperty("Max Depth", typeof(int),
                    (v) => {
                        try
                        {
                            MaxDepth = Convert.ToInt32(v);
                        }
                        catch(Exception)
                        {
                            return false;
                        }
                        return true;
                    },
                    () => { return MaxDepth; }, defaultMaxDepth)
                };
            }
        }
    }
}
