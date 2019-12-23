using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models
{
    internal class DTObject
    {
        internal object InnerObject { get; set; }
        internal DTOInformation Information {get; set; }
    }
}
