﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleShipMVC.Models
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}