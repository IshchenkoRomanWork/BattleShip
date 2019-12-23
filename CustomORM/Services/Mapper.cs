using CustomORM.Interfaces;
using CustomORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Services
{
    class Mapper<Model> : IMapper<Model>
    {
        DBObject IMapper<Model>.GetDboFrom(DTObject item)
        {
            throw new NotImplementedException();
        }

        DTObject IMapper<Model>.GetDtoFrom(DBObject item)
        {
            throw new NotImplementedException();
        }
    }
}
