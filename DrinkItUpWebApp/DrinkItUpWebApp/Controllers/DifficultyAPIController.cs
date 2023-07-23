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

        public DifficultyAPIController(IDifficultyService difficultyService, IMapper mapper)
        {
            _mapper = mapper;
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

            return Ok(await _difficultyService.GetById(difficultyDto.DifficultyId));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(int id, [FromBody] DifficultyDto difficultyDto)
        {
            if (await _mainAlcoholService.IsMainAlcoholUsed(id))
            {
                return BadRequest("Main Alcohol is in use, cannot delete");
            }


            if (await _mainAlcoholService.Remove(id))
                return Ok("Deleted");
            else
                return StatusCode(500);
        }
    }
}
