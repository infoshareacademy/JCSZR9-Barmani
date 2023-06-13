using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;


namespace DrinkItUpBusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.RoleId = 3;
            user.PasswordHash = _passwordHasher.Hash(user.Email, userDto.Password);

            await _userRepository.Add(user);
            await _userRepository.Save();
            
        }
    }
}
