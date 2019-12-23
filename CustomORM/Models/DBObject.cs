using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models
{
    class DBObject
    {
        internal List<object> RowValues { get; set; }
        internal DBOInformation Information {get; set;}
    }
}
