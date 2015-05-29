using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebObjectMapping.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class MapsToAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string propName;

        // This is a positional argument
        public MapsToAttribute(string pName)
        {
            this.propName = pName;
        }

        public string PropertyName
        {
            get { return propName; }
        }

        // This is a named argument
        public int NamedInt { get; set; }
    }
}
