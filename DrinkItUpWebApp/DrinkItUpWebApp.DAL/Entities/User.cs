using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(70)]
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        [MaxLength(70)]
        public string UserNameToShow { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        public int RoleId { get; set; } 

        public Role Role { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();




    }
}
