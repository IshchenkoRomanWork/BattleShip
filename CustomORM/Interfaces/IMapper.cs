using ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    internal interface IMapper<Model>
    {
        internal DBObject GetDboFrom(DTObject item);
        internal DTObject GetDtoFrom(DBObject item, Type type);

    }
}
