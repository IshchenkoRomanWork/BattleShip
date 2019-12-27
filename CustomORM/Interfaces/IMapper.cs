using CustomORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Interfaces
{
    internal interface IMapper<Model>
    {
        internal DBObject GetDboFrom(DTObject item);
        internal DTObject GetDtoFrom(DBObject item, Type type);

    }
}
