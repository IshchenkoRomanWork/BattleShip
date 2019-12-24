using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Test
{
    [Table("SecondTestTable")]
    public class SecondTestClass
    {
        [Column("STID", "int")]
        public int Id { get; set; }
        [Column("boolVal", "bool")]
        public bool boolValue { get; set; }
    }
}
