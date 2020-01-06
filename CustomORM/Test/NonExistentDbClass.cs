using ORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Test
{
    [Table("NonExistentTable")]
    public class NonExistentDbClass
    {
    }
}
