using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly IUserRepository _userRepository;

        public PasswordHasher(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Hash(string email, string password)
        {
            double length = password.Length + email.Length;

            var hash = $"{email[0]}{Math.Pow(length, 3)}{password[4]}";

            return hash;
        }

        public async Task<bool> Verify(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
            {
                return false;
            }

            var hash = Hash(email, password);
            if(hash == user.PasswordHash)
            {
                return true;
            }

            return false;

        }
    }
}
