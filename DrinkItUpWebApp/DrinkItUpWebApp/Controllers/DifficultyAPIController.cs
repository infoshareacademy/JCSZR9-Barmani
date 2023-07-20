using AutoMapper;
using DrinkItUpBusinessLogic;
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
    }
}
