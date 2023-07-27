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
        private readonly ILogger<UnitAPIController> _logger;

        public UnitAPIController(IUnitService unitService, IMapper mapper, ILogger<UnitAPIController> logger)
        {
            _unitService = unitService;
            _mapper = mapper;
            _logger = logger;
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
            unitDto.IsUsed = await _unitService.IsUnitUsed(id);
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

            _logger.LogInformation($"{DateTime.Now}: New Unit {addedUnitDto.Name} has been added.");
            return Ok(addedUnitDto);
        }

        [HttpPost]
        [Authorize]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UnitDto unitDto)
        {
            if (!await _unitService.IsUnitUnique(unitDto.Name))
            {
                return BadRequest("Name is already used");
            }
            await _unitService.Update(unitDto);

            _logger.LogInformation($"{DateTime.Now}: Unit {unitDto.Name} has been updated.");
            return Ok(await _unitService.GetById(unitDto.UnitId));
        }


        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _unitService.IsUnitUsed(id))
            {
                return BadRequest("Unit is in use, cannot delete");
            }

            if (await _unitService.Remove(id))
            {
                _logger.LogInformation($"{DateTime.Now}: Unit id: {id} has been removed.");
                return AcceptedAtAction("Deleted");
            }
            else
                return StatusCode(500);
        }
    }
}
