using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace CustomORM.Models
{
    internal class DTOInformation
    {
        internal Type Type { get; set; }
        internal List<PropertyInfo> Properties { get; set; }
        internal List<FieldInfo> Fields { get; set; }
    }
}
