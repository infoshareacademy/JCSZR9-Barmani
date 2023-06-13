using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DrinkItUpWebApp.Models
{
    public class UserModel
    {
        [Required]
        [MaxLength(70)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [MaxLength(70)]
        public string UserNameToShow { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        

    }
}
