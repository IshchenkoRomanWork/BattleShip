using ORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Test
{
    [Table("TestTable")]
    public class TestClass
    {
        [Column("TID", "int", true)]
        public int Id { get; set; }
        [Column("IntVal", "int")]
        public int IntValue { get; set; }
        [Column("StringVal", "nvarchar(100)")]
        public string stringValue { get; set; }
        [IsForeignKey]
        [Column("TID", "int")]
        public List<SecondTestClass> SecondList { get; set; }

    }
}
