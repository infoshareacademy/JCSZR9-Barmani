
using System.ComponentModel.DataAnnotations;


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

    }
}
