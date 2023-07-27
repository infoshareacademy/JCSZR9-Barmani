using AutoMapper;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.Middleware.Authorization;
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
            foreach(var difficulty in difficultiesDtos)
            {
                difficulty.IsUsed = await _difficultyService.IsDifficultyUsed(difficulty.DifficultyId);
            }
            return Ok(difficultiesDtos);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var difficultyDto = await _difficultyService.GetById(id);
            difficultyDto.IsUsed = await _difficultyService.IsDifficultyUsed(difficultyDto.DifficultyId);
            return Ok(difficultyDto);
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] DifficultyDto difficultyDto)
        {
            if (!await _difficultyService.IsDifficultyUnique(difficultyDto.Name))
                return BadRequest("Name is already used");

            var addedDififcultyDto = await _difficultyService.AddDifficulty(difficultyDto);
            return Ok(addedDififcultyDto);
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]

        public async Task<IActionResult> Update([FromBody] DifficultyDto difficultyDto)
        {
            if (!await _difficultyService.IsDifficultyUnique(difficultyDto.Name))
            {
                return BadRequest("Name is already used");
            }

            await _difficultyService.Update(difficultyDto);
            return Ok(await _difficultyService.GetById(difficultyDto.DifficultyId));
        }


        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _difficultyService.IsDifficultyUsed(id))
            {
                return BadRequest("Difficulty is in use, cannot delete");
            }


            if (await _difficultyService.Remove(id))
                return AcceptedAtAction("Deleted");
            else
                return StatusCode(500);
        }
    }
}
