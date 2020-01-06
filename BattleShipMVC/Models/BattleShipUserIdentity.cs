using Microsoft.AspNet.Identity;
using ORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShipMVC.Models
{
    [Table("Users")]
    public class BattleShipUserIdentity : IUser<int>
    {
        [Column("UserId", "int", true)]
        public int Id { get; set; }
        [Column("UserName", "nvarchar")]
        public string UserName { get; set; }
        [Column("PasswordHash", "nvarchar")]
        public string PasswordHash { get; set; }

        public BattleShipUserIdentity()
        {

        }
        public BattleShipUserIdentity(string userName)
        {
            Id = new Random().Next();
            UserName = userName;
        }
    }
}