using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DrinkItUpWebApp.Middleware.Authorization;

namespace DrinkItUpWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkAPIController : ControllerBase
    {
        private readonly IDrinkService _drinkService;
        private readonly IByCategoryService _categoryService;
        private ISearchByIngredients _searchByIngredients;
        private readonly IGetDrinkDetails _getDrinkDetails;
        private readonly ISearchByNameOrOneIngredient _searchByNameOrOneIngredient;
        private readonly IMapper _mapper;
        private readonly ILogger<DrinkAPIController> _logger;

        public DrinkAPIController(IDrinkService drinkService, 
            IByCategoryService categoryService,
            ISearchByIngredients searchByIngredients, 
            IGetDrinkDetails getDrinkDetails, 
            ISearchByNameOrOneIngredient searchByNameOrOneIngredient, 
            IMapper mapper,
            ILogger<DrinkAPIController> logger)
        {

            _mapper = mapper;
            _logger = logger;
            _drinkService = drinkService;
            _categoryService = categoryService;
            _searchByIngredients = searchByIngredients;
            _getDrinkDetails = getDrinkDetails;
            _searchByNameOrOneIngredient = searchByNameOrOneIngredient;
        }

        [HttpGet]
        [Route("autocompletemain/{input}")]
        public async Task<IActionResult> AutoCompleteMain(string input)
        {
            var drinks = await _searchByNameOrOneIngredient.SearchByName(input);
            var drinksModel = new List<DrinkSearchModel>();
            if (drinks != null)
            {
                return Ok(drinks.Take(3));
            }
            return Ok(null);
        }

        [HttpGet]
        [Route("search/{input}")]
        public async Task<IActionResult> SearchByNameOrOneIngredient(string input)
        {
            var drinks = await _searchByNameOrOneIngredient.SearchByName(input);
            var drinksModel = new List<DrinkSearchModel>();
            if (drinks != null)
            {
                return Ok(drinks);
            }
            return Ok(null);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> DrinkDetails(int id)
        {
            var drinkWithDetails = await _getDrinkDetails.GetDrinkToDetailView(id);

            return Ok(drinkWithDetails);
        }

        [HttpGet]
        [Route("mixer/autocompleteingredient/{input}/{chosen}")]
        public async Task<IActionResult> AutoCompleteIngredients(string input, string? chosen)
        {
            if(input == "nothing")
                return Ok(null);

            var ingredientsNames = await _searchByIngredients.GetAllIngredientsMatchingNames(input, chosen);
            return Ok(ingredientsNames);
        }

        [HttpGet]
        [Route("mixer/{searchNames}")]
        public async Task<IActionResult> DrinkMixerResults(string searchNames)
        {
            var drinksDtos = await _searchByIngredients.GetMatchingDrinksToIngredients(searchNames);
           
            return Ok(drinksDtos);

        }

        [HttpGet]
        [Route("byCategory/alcohol/{id}")]
        public async Task<IActionResult> GetByMainAlcohol(int id)
        {
            var drinksDtos = await _categoryService.GetDrinksByMainAlcoholId(id);

            return Ok(drinksDtos);

        }

        [HttpGet]
        [Route("byCategory/difficulty/{id}")]
        public async Task<IActionResult> GetByDifficulty(int id)
        {
            var drinksDtos = await _categoryService.GetDrinksByDifficultyId(id);

            return Ok(drinksDtos);

        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var drinksDtos = await _drinkService.GetAll();

            return Ok(drinksDtos);

        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] DrinkWithDetailsDto drinkWithDetailsDto)
        {
            if (!await _drinkService.IsDrinkUnique(drinkWithDetailsDto))
                return BadRequest("The same drink is added in database");

            var addedDrink = await _drinkService.AddDrink(drinkWithDetailsDto);
            _logger.LogInformation($"{DateTime.Now}: New Drink {addedDrink.Name} has been added.");
            return Ok(addedDrink);
        }

        [Authorize]
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] DrinkWithDetailsDto drinkWithDetailsDto)
        {
            if (!await _drinkService.IsDrinkUnique(drinkWithDetailsDto))
                return BadRequest("The same drink is added in database");

            var updatedDrink = await _drinkService.UpdateDrink(drinkWithDetailsDto);
            _logger.LogInformation($"{DateTime.Now}: Drink {updatedDrink.Name} has been updated.");
            return Ok(updatedDrink);
        }


        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (await _drinkService.RemoveDrink(id))
            {
                _logger.LogInformation($"{DateTime.Now}: Drink id:{id} has been removed.");
                return AcceptedAtAction("Deleted");
            }
            else
                return StatusCode(500);
        }


    }
}
