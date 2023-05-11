namespace DrinkItUpWebApp.Models
{
    public class Login
    {

        public int Id { get; set; }

        public string EMail { get; set; }

        public string Password { get; set; }

        

        public List<Login> login = new List<Login>();

    }
}
