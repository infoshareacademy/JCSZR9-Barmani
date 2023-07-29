using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Middleware.Authorization;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientAPIController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;
        private readonly ISearchByIngredients _searchByIngredients;
        private readonly ILogger<IngredientAPIController> _logger;
        private readonly IUnitService _unitService;

        public IngredientAPIController(IIngredientService ingredientService, IUnitService unit, IMapper mapper, ISearchByIngredients searchByIngredients, ILogger<IngredientAPIController> logger)
        {
            _mapper = mapper;
            _searchByIngredients = searchByIngredients;
            _logger = logger;
            _ingredientService = ingredientService;
            _unitService = unit;
        }

        [HttpGet]
        [Route("GetAllNames")]
        public async Task<ActionResult> GetAllNames()
        {
            var ingredientsDtosNames = await _searchByIngredients.GetAllNamesDistinct();

            return Ok(ingredientsDtosNames);
        }
        [HttpGet]
        [Route("GetAllUnits/{ingredient}")]
        public async Task<ActionResult> GetAllUnits(string ingredient)
        {
            var unitDtos = await _ingredientService.GetAllUnitsForIngredient(ingredient);

            return Ok(unitDtos);
        }
        [HttpGet]
        [Route("GetByNameAndUnit/{ingredient}/{unitId}")]
        public async Task<ActionResult> GetByNameAndUnit(string ingredient, int unitId)
        {
            var ingredientDto = await _ingredientService.GetByNameAndUnit(ingredient,unitId);

            return Ok(ingredientDto);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var ingredientsDtos = await _ingredientService.GetAllIngredientsWithUnits();

            return Ok(ingredientsDtos);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ingredientDto = await _ingredientService.GetById(id);
            ingredientDto.Unit = await _unitService.GetById(ingredientDto.UnitId);

            return Ok(ingredientDto);
        }

        [HttpPost]
        [Authorize]
        [Route("Add")]
        public async Task<IActionResult> Create([FromBody]IngredientDto ingredientDto)
        {
            var ingredientToAdd = new IngredientDto { Name = ingredientDto.Name , UnitId = ingredientDto.UnitId };
            if (!await _ingredientService.IsIngredientUnique(ingredientToAdd))
            {
                return BadRequest("Ingredient is in database");
            }
            var ingredientAdded = await _ingredientService.Add(ingredientToAdd);

            _logger.LogInformation($"{DateTime.Now}: New Ingredient {ingredientAdded.Name} has been added.");
            return Ok(ingredientAdded);
        }



        [Authorize]
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody]IngredientDto ingredientDto)
        {

            if (!await _ingredientService.IsIngredientUnique(ingredientDto))
            {
                return BadRequest("Ingredient is in database");
            }
            ingredientDto = await _ingredientService.Update(ingredientDto);

            _logger.LogInformation($"{DateTime.Now}: New Ingredient {ingredientDto.Name} has been updated.");
            return Ok(await _ingredientService.GetById(ingredientDto.IngredientId));
        }



        // Delete: IngredientController/Delete/5
        [Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _ingredientService.IngredientIsUsed(id))
            {
                return BadRequest("Ingredient is used in drink");
            }

            if (await _ingredientService.Remove(id))
            {
                _logger.LogInformation($"{DateTime.Now}: Ingredient id:{id} has been removed.");
                return AcceptedAtAction("Deleted");
            }
            else
                return StatusCode(500);
        }
    }
}
