namespace DrinkItUpBusinessLogic.DTOs
{
    public class AuthenticateResponse
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string UserNameToShow { get; set; }

        public string Token { get; set; }


        public AuthenticateResponse(UserDto userDto, string token)
        {
            UserId = userDto.UserId;
            Email = userDto.Email;
            UserNameToShow = userDto.UserNameToShow;
            Token = token;
        }
    }
}
