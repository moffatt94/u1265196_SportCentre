﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HuddersfieldSportCentre.Models
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}