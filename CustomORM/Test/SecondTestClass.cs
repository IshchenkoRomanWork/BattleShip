using ORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Test
{
    [Table("SecondTestTable")]
    public class SecondTestClass
    {
        [Column("STID", "int", true)]
        public int Id { get; set; }
        [Column("boolVal", "bit")]
        public bool boolValue { get; set; }
    }
}
