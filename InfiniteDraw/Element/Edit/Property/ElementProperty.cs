using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Edit.Property
{
    public delegate object Getter();
    public delegate bool Setter(object value);
    public class ElementProperty
    {
        public Setter Setter { set; get; }
        public Getter Getter { set; get; }
        public string Name { set; get; }
        public Type Type { get; }
        public object Default { get; }
        public ElementProperty(string name, Type valueType, Setter setter, Getter getter, object defaultValue)
        {
            Name = name;
            Type = valueType;
            Setter = setter;
            Getter = getter;
            Default = defaultValue;
        }
    }
}
