using ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    internal interface IMapper<Model>
    {
        DBObject GetDboFrom(DTObject item);
        DTObject GetDtoFrom(DBObject item, Type type);

    }
}
