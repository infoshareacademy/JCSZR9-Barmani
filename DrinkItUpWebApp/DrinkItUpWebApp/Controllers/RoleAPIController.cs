using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using DrinkItUpWebApp.Middleware.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAPIController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RoleAPIController(IRoleRepository roleRepository, IUserService userService,IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleRepository.GetAll().ToListAsync();
            var rolesDto = new List<RoleDto>();
            var users = await _userService.GetAll();
            foreach (var role in roles)
            {
                var roleDto = _mapper.Map<RoleDto>(role);
                roleDto.IsUsed = users.Select(u => u.Role.RoleId).Contains(role.RoleId);
                rolesDto.Add(roleDto);
            }
            return Ok(rolesDto);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var roleDto = _mapper.Map<RoleDto>(await _roleRepository.GetById(id));
            return Ok(roleDto);
        }


        [HttpPost]
        [Authorize]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] RoleDto roleDto)
        {

            var role = _mapper.Map<Role>(roleDto);

            var addedRoleDto = _mapper.Map<RoleDto>(await _roleRepository.Add(role));
            await _roleRepository.Save();
            return Ok(addedRoleDto);
        }

        [HttpPost]
        [Authorize]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);

            var updatedRoleDto = _mapper.Map<RoleDto>(await _roleRepository.Add(role));
            await _roleRepository.Save();
            return Ok(updatedRoleDto);
        }


        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleRepository.GetById(id);
            _roleRepository.Delete(role);
            await _roleRepository.Save();
            return Ok();

        }
    }
}
