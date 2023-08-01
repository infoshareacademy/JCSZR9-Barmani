using AutoMapper;
using Azure;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Middleware.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserAPIController> _logger;

        public UserAPIController(IUserService userService, IMapper mapper, ILogger<UserAPIController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            _logger.LogInformation($"{DateTime.Now}: User id: {response.UserId} has been authenticated.");
            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var usersDtos = await _userService.GetAll();
            return Ok(usersDtos);
        }


        [HttpGet]
        [Route("GetAllCount")]
        public async Task<IActionResult> GetAllCount()
        {
            var usersDtos = await _userService.GetAll();
            return Ok(usersDtos.Count());
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            var addedUserDto = await _userService.Register(userDto);

            _logger.LogInformation($"{DateTime.Now}: User {addedUserDto.Email} has been registered.");
            return Ok(addedUserDto);
        }
    }
}
