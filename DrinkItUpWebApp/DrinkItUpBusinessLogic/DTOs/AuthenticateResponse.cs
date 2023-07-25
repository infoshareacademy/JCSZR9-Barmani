namespace DrinkItUpBusinessLogic.DTOs
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string UserNameToShow { get; set; }

        public RoleDto Role { get; set; }

        public string Token { get; set; }


        public AuthenticateResponse(UserDto userDto, string token)
        {
            UserId = userDto.UserId;
            Email = userDto.Email;
            UserNameToShow = userDto.UserNameToShow;
            Role = userDto.Role;
            Token = token;
        }
    }
}
