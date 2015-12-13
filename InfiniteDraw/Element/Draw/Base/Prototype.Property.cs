using InfiniteDraw.Edit.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Element.Draw.Base
{
    public partial class Prototype : IPropertyEditable
    {
        private bool elementPropertyCreated = false;

        private ElementProperty nameElementProperty;
        private bool NameSetter(object v)
        {
            try
            {
                Name = Convert.ToString(v);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private ElementProperty maxDepthElementProperty;
        private bool MaxDepthSetter(object v)
        {
            try
            {
                MaxDepth = Convert.ToInt32(v);
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
                    nameElementProperty,
                    maxDepthElementProperty
                };
            }
        }

        private void CreateElementProperty()
        {
            elementPropertyCreated = true;
            nameElementProperty = new ElementProperty("Name", typeof(string), NameSetter, () => Name, defaultName);
            maxDepthElementProperty = new ElementProperty("Max Depth", typeof(int), MaxDepthSetter, () => MaxDepth, defaultMaxDepth);
        }
    }
}
