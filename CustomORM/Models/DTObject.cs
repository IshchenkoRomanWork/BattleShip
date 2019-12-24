using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CustomORM.Models
{
    internal class DTObject
    {
        internal object InnerObject { get; set; }
        internal List<PropertyInfo> Properties { get; set; }
        internal List<FieldInfo> Fields { get; set; }

        public DTObject()
        {

        }
    }
}
