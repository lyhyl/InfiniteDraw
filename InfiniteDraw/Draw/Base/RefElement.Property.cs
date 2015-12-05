using InfiniteDraw.Edit.Property;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Base
{
    public partial class RefElement : IPropertyEditable
    {
        public ElementProperty[] EditableProperties
        {
            get
            {
                return new ElementProperty[]
                {
                    new ElementProperty("Pos X", typeof(double),
                    (v) => {
                        try
                        {
                            Position = new Vector(Convert.ToDouble(v), Position.Y);
                        }
                        catch(Exception e)
                        {
                            return false;
                        }
                        return true;
                    },
                    () => { return Position.X; }, 0),
                    new ElementProperty("Pos Y", typeof(double),
                    (v) => {
                        try
                        {
                            Position = new Vector(Position.X, Convert.ToDouble(v));
                        }
                        catch(Exception e)
                        {
                            return false;
                        }
                        return true;
                    },
                    () => { return Position.Y; }, 0)
                };
            }
        }
    }
}
