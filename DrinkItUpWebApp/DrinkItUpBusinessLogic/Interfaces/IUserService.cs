using DrinkItUpBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Register(UserDto userDto);

        Task<IEnumerable<UserDto>> GetAll();

        Task<UserDto> GetById(int id);

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    }
}
