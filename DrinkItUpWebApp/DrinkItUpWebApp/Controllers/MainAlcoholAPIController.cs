using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Middleware.Authorization;
using DrinkItUpWebApp.Models;
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
        private readonly ILogger<MainAlcoholAPIController> _logger;

        public MainAlcoholAPIController(IMainAlcoholService mainAlcoholService, IMapper mapper, ILogger<MainAlcoholAPIController> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _mainAlcoholService = mainAlcoholService;
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var mainAlcoholsDtos = await _mainAlcoholService.GetAll();
            foreach(var main in mainAlcoholsDtos)
            {
                main.IsUsed = await _mainAlcoholService.IsMainAlcoholUsed(main.MainAlcoholId);
            }
            return Ok(mainAlcoholsDtos);
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mainAlcoholDto = await _mainAlcoholService.GetById(id);
            mainAlcoholDto.IsUsed = await _mainAlcoholService.IsMainAlcoholUsed(mainAlcoholDto.MainAlcoholId);
            return Ok(mainAlcoholDto);
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] MainAlcoholDto mainAlcoholDto)
        {
            if (!await _mainAlcoholService.IsMainAlcoholUnique(mainAlcoholDto.Name))
                return BadRequest("Name is already used");

            var addedMainAlcoholDto = await _mainAlcoholService.AddMainAlcohol(mainAlcoholDto);
            _logger.LogInformation($"{DateTime.Now}: New Main Alcohol {addedMainAlcoholDto.Name} has been added.");
            return Ok(addedMainAlcoholDto);
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]

        public async Task<IActionResult> Update([FromBody] MainAlcoholDto mainAlcoholDto)
        {
            if (!await _mainAlcoholService.IsMainAlcoholUnique(mainAlcoholDto.Name))
            {
                return BadRequest("Name is already used");
            }

            await _mainAlcoholService.Update(mainAlcoholDto);
            _logger.LogInformation($"{DateTime.Now}: Main Alcohol {mainAlcoholDto.Name} has been updated.");
            return Ok(await _mainAlcoholService.GetById(mainAlcoholDto.MainAlcoholId));
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _mainAlcoholService.IsMainAlcoholUsed(id))
            {
                return BadRequest("Main Alcohol is in use, cannot delete");
            }

            if (await _mainAlcoholService.Remove(id))
            {
                _logger.LogInformation($"{DateTime.Now}: Main Alcohol id: {id} has been removed.");
                return AcceptedAtAction("Deleted");
            }
            else
                return StatusCode(500);
        }
    }
}
