using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Test
{
    [Table("NonExistentTable")]
    public class NonExistentDbClass
    {
    }
}
