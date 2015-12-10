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
        private bool elementPropertyCreated = false;

        private ElementProperty xElementProperty;
        private ElementProperty yElementProperty;
        private bool XSetter(object v)
        {
            try
            {
                Position = new Vector(Convert.ToDouble(v), Position.Y);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private bool YSetter(object v)
        {
            try
            {
                Position = new Vector(Position.X, Convert.ToDouble(v));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private ElementProperty referenceElementProperty;
        private bool ReferenceSetter(object v)
        {
            try
            {
                Reference = Convert.ToInt32(v);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public ElementProperty[] EditableProperties
        {
            get
            {
                if (!elementPropertyCreated)
                    CreateElementProperty();
                return new ElementProperty[]
                {
                    referenceElementProperty,
                    xElementProperty,
                    yElementProperty
                };
            }
        }

        private void CreateElementProperty()
        {
            elementPropertyCreated = true;
            xElementProperty = new ElementProperty("Pos X", typeof(double), XSetter, () => Position.X, 0);
            yElementProperty = new ElementProperty("Pos Y", typeof(double), YSetter, () => Position.Y, 0);
            referenceElementProperty = new ElementProperty("Ref", typeof(int), ReferenceSetter, () => Reference, 0);
        }
    }
}
