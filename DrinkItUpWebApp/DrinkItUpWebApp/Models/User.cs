namespace DrinkItUpWebApp.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        

        public List<User> login = new List<User>();

    }
}
