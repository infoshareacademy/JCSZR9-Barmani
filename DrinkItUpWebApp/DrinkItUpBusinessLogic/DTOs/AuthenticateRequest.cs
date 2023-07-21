using System.ComponentModel.DataAnnotations;

namespace DrinkItUpBusinessLogic.DTOs;

public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
