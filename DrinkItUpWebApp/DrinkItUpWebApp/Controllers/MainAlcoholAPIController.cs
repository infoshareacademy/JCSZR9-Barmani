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
            m
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

            return Ok(await _mainAlcoholService.GetById(mainAlcoholDto.MainAlcoholId));
        }

        [Authorize]
        [HttpPost]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] MainAlcoholDto mainAlcoholDto)
        {
            if (await _mainAlcoholService.IsMainAlcoholUsed(id))
            {
                return BadRequest("Main Alcohol is in use, cannot delete");
            }


            if (await _mainAlcoholService.Remove(id))
                return AcceptedAtAction("Deleted");
            else
                return StatusCode(500);
        }
    }
}
