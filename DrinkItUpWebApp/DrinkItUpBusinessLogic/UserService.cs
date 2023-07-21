using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using System.Data.Entity;

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

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var usersDtos = new List<UserDto>();

            var users = await _userRepository.GetAll().ToListAsync();

            if(users == null)
                return usersDtos;

            foreach( var user in users)
            {
                var userDto = _mapper.Map<UserDto>(user);
                usersDtos.Add(userDto);
            }

            return usersDtos;
        }

        public async Task<UserDto> Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.RoleId = 1;
            user.PasswordHash = _passwordHasher.Hash(user.Email, userDto.Password);

            await _userRepository.Add(user);
            await _userRepository.Save();

            return _mapper.Map<UserDto>(user);

        }

    }
}
