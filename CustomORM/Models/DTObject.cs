using CustomORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CustomORM.Models
{
    internal class DTObject
    {
        internal object InnerObject { get; set; }
        internal List<IPropertyFieldInfo> PropertiesFields { get; set; }

        public DTObject()
        {

        }

    }
}
