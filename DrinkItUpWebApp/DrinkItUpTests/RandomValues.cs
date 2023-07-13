using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpTests
{
    internal static class RandomValues
    {
        internal static string RandomNameGenerator(int maxLength)
        {
            var name = "";
            var random = new Random();
            var alphabet = "abcdefghijklmnopqrstuwxyz";
            var nameLength = random.Next(1, maxLength);

            for(int i = 0; i < nameLength;i++)
            {
                var randomChar = alphabet[random.Next(alphabet.Length)];
                name += randomChar.ToString();
            }

            return name;
        }

        internal static int RandomIdGenerator(int maxId)
        {
            var random = new Random();

            return random.Next(1, maxId); 
        }
    }
}
