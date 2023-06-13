﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        [MaxLength(25)]
        public string Name { get; set; } = null!;
    }
}
