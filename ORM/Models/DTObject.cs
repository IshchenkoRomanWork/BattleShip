using ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Models
{
    internal class DTObject
    {
        internal object InnerObject { get; set; }
        internal Type BaseType { get; set; }
        internal List<IPropertyFieldInfo> PropertiesFields { get; set; }

        public DTObject()
        {

        }

    }
}
