using AutoMapper;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyAPIController : ControllerBase
    {
        private readonly IDifficultyService _difficultyService;
        private readonly IMapper _mapper;
        private readonly ILogger<DifficultyAPIController> _logger;

        public DifficultyAPIController(IDifficultyService difficultyService, IMapper mapper, ILogger<DifficultyAPIController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _difficultyService = difficultyService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var difficultiesDtos = await _difficultyService.GetAll();
            return Ok(difficultiesDtos);
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var difficultyDto = await _difficultyService.GetById(id);
            return Ok(difficultyDto);
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] DifficultyDto difficultyDto)
        {
            if (!await _difficultyService.IsDifficultyUnique(difficultyDto.Name))
                return BadRequest("Name is already used");

            var addedDififcultyDto = await _difficultyService.AddDifficulty(difficultyDto);
            _logger.LogInformation($"{DateTime.Now}: New Difficulty {addedDififcultyDto.Name} has been added.");
            return Ok(addedDififcultyDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("update")]

        public async Task<ActionResult> Update([FromBody] DifficultyDto difficultyDto)
        {
            if (!await _difficultyService.IsDifficultyUnique(difficultyDto.Name))
            {
                return BadRequest("Name is already used");
            }

            _logger.LogInformation($"{DateTime.Now}: Difficulty {difficultyDto.Name} has been updated.");
            return Ok(await _difficultyService.GetById(difficultyDto.DifficultyId));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(int id, [FromBody] DifficultyDto difficultyDto)
        {
            if (await _difficultyService.IsDifficultyUsed(id))
            {
                return BadRequest("Difficulty is in use, cannot delete");
            }


            if (await _difficultyService.Remove(id))
                return Ok("Deleted");
            else
                return StatusCode(500);
        }
    }
}
