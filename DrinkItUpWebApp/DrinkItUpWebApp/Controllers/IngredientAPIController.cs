using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientAPIController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;
        private readonly ISearchByIngredients _searchByIngredients;
        private readonly IUnitService _unitService;

        public IngredientAPIController(IIngredientService ingredientService, IUnitService unit, IMapper mapper, ISearchByIngredients searchByIngredients)
        {
            _mapper = mapper;
            _searchByIngredients = searchByIngredients;
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
        [Route("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var ingredientsDtos = await _ingredientService.GetAllIngredientsWithUnits();

            return Ok(ingredientsDtos);
        }

    }
}
