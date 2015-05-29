using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebObjectMapping.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class HateoasRouteAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string mapTo;

        // This is a positional argument
        public HateoasRouteAttribute(string routeName)
        {
            this.mapTo = routeName;
        }

        public string RouteName
        {
            get { return mapTo; }
        }

        // This is a named argument
        public int NamedInt { get; set; }
    }
}
