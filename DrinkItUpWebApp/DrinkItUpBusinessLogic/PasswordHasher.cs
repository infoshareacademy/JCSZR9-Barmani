using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;


namespace DrinkItUpBusinessLogic
{
    public class PasswordHasher : IPasswordHasher
    {


        public PasswordHasher()
        {
            
        }

        public string Hash(string email, string password)
        {
            double length = password.Length + email.Length;

            var hash = $"{email[0]}{Math.Pow(length, 3)}{password[4]}";

            return hash;
        }

        public bool Verify(string passwordHash, string email, string password)
        {
            var hash = Hash(email, password);
            if (hash == passwordHash)
            {
                return true;
            }

            return false;
        }
    }
}
