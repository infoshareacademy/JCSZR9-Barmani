using AutoMapper;

using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Middleware.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitAPIController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitAPIController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var unitDtos = await _unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                unitDto.IsUsed = await _unitService.IsUnitUsed(unitDto.UnitId);
            }
            return Ok(unitDtos);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var unitDto = await _unitService.GetById(id);
            return Ok(unitDto);
        }


        [HttpPost]
        [Authorize]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] UnitDto unitDto)
        {

            if (!await _unitService.IsUnitUnique(unitDto.Name))
                return BadRequest("Name is already used");

            var addedUnitDto = await _unitService.AddUnit(unitDto);

            return Ok(addedUnitDto);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] UnitDto unitDto)
        {
            if (!await _unitService.IsUnitUnique(unitDto.Name))
            {
                return BadRequest("Name is already used");
            }
            await _unitService.Update(unitDto);
            return Ok(await _unitService.GetById(unitDto.UnitId));
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(int id, [FromBody] UnitDto unitDto)
        {
            if (await _unitService.IsUnitUsed(id))
            {
                return BadRequest("Unit is in use, cannot delete");
            }

            if (await _unitService.Remove(id))
                return Ok("Deleted");
            else
                return StatusCode(500);
        }

    }
}
