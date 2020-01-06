using ORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShipMVC.Models
{

    [Table("Logs")]
    public class LogInfo
    {
        [Column("LogId", "int", true)]
        public int Id { get; set; }
        [Column("Message", "nvarchar")]
        public string Message { get; set; }
        [Column("DateTime", "datetime")]
        public DateTime DateTime { get; set; }

        public LogInfo(string message, DateTime dateTime)
        {
            Id = new Random().Next();
            Message = message;
            DateTime = dateTime;
        }
    }
}