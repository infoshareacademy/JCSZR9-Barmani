using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string email, string password);

        bool Verify(string passwordHash, string email, string password);
    }
}
