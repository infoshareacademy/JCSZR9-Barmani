using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
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
            return Ok(mainAlcoholsDtos);
        }


        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mainAlcoholDtos = await _mainAlcoholService.GetById(id);
            return Ok(mainAlcoholDtos);
        }


        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] MainAlcoholDto mainAlcoholDto)
        {
            if (!await _mainAlcoholService.IsMainAlcoholUnique(mainAlcoholDto.Name))
                return BadRequest("Name is already used");

            var addedMainAlcoholDto = await _mainAlcoholService.AddMainAlcohol(mainAlcoholDto);
            return Ok(addedMainAlcoholDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("update")]

        public async Task<ActionResult> Update([FromBody] MainAlcoholDto mainAlcoholDto)
        {
            if (!await _mainAlcoholService.IsMainAlcoholUnique(mainAlcoholDto.Name))
            {
                return BadRequest("Name is already used");
            }

            return Ok(await _mainAlcoholService.GetById(mainAlcoholDto.MainAlcoholId));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<ActionResult> Delete(int id, [FromBody] MainAlcoholDto mainAlcoholDto)
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
