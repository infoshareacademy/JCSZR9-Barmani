using System.ComponentModel.DataAnnotations;


namespace DrinkItUpBusinessLogic.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        [MaxLength(70)]
        public string Email { get; set; } = null!;
        [MinLength(6)]
        public string Password { get; set; } = null!;
        [MaxLength(70)]
        public string UserNameToShow { get; set; } = null!;
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int? RoleId { get; set; }
    }
}
