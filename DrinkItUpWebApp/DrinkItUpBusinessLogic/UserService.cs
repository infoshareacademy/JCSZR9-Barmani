using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DrinkItUpBusinessLogic
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        private readonly AppSettings _appSettings;

        
    

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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

        public async Task<UserDto> GetById(int id)
        {
            var userDto = _mapper.Map<UserDto>( await _userRepository.GetById(id) ?? new User());
            return userDto;
        }

        public async Task<UserDto> Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user.RoleId = 3;
            user.PasswordHash = _passwordHasher.Hash(user.Email, userDto.Password);

            await _userRepository.Add(user);
            await _userRepository.Save();

            return _mapper.Map<UserDto>(user);

        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var users = await _userRepository.GetAllWithRoles();
            var user = users.FirstOrDefault(x => x.Email == request.Email && _passwordHasher.Verify(x.PasswordHash, request.Email, request.Password));
            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            var userDto = _mapper.Map<UserDto>(user);

            return new AuthenticateResponse(userDto, token);
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
