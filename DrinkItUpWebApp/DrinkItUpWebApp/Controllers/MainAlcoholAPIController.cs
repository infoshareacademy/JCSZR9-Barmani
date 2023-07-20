using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainAlcoholAPIController : ControllerBase
    {
        private readonly IMainAlcoholService _mainAlcoholService;
        private readonly IMapper _mapper;

        public MainAlcoholAPIController(IMainAlcoholService mainAlcoholService, IMapper mapper)
        {
            _mapper = mapper;
            _mainAlcoholService = mainAlcoholService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var mainAlcoholsDtos = await _mainAlcoholService.GetAll();
            return Ok(mainAlcoholsDtos);
        }
    }
}
