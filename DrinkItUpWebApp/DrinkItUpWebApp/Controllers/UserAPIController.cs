using AutoMapper;
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

        public UserAPIController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

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

            return Ok(addedUserDto);
        }
    }
}
