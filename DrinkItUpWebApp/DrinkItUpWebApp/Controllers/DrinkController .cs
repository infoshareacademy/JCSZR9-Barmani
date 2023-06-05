using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace DrinkItUpWebApp.Controllers
{
    public class DrinkController : Controller
    {
        private ISearchByIngredients _searchByIngredients;
        private readonly IGetDrinkDetails _getDrinkDetails;
        private readonly ISearchByNameOrOneIngredient _searchByNameOrOneIngredient;
        private readonly IMapper _mapper;

        public DrinkController(ISearchByIngredients searchByIngredients, IGetDrinkDetails getDrinkDetails, ISearchByNameOrOneIngredient searchByNameOrOneIngredient, IMapper mapper)
        {
			
			_mapper = mapper;
            _searchByIngredients = searchByIngredients;
            _getDrinkDetails = getDrinkDetails;
            _searchByNameOrOneIngredient = searchByNameOrOneIngredient;
        }

        [HttpPost]
        public JsonResult AutoCompleteIngredients(string Prefix)
        {
            var words = _searchByIngredients.GetAllIngredientsMatchingNames(Prefix);
            return Json(words);
        }

		[HttpPost]
		public async Task<JsonResult> AutoCompleteMain(string Prefix)
		{
			var drinks = await _searchByNameOrOneIngredient.SearchByName(Prefix);
			var drinksModel = new List<DrinkSearchModel>();
			if(drinks != null)
			{
				foreach(var drink in drinks)
				{
					var drinkModel = _mapper.Map<DrinkSearchModel>(drink);
					drinksModel.Add(drinkModel);
				}
				return Json(drinksModel);
			}
			return Json(null);
		}

		public async Task<IActionResult> DrinkSearchBar(string input)
		{
            var drinks = await _searchByNameOrOneIngredient.SearchByName(input);
            var drinksModel = new List<DrinkSearchModel>();
            if (drinks != null)
            {
                foreach (var drink in drinks)
                {
                    var drinkModel = _mapper.Map<DrinkSearchModel>(drink);
                    drinksModel.Add(drinkModel);
                }
				return View("DrinkSearch", drinksModel);
            }
            return View("DrinkSearch", drinksModel);
        }

		[HttpGet]
		public IActionResult DrinkSearch(IEnumerable<DrinkSearchModel> drinks)
        {
            return View(drinks);
        }

        public IActionResult DrinkBrowse()
        {

            return View();
        }

        public async Task<IActionResult> DrinkSuprise()
        {
			var random = new Random();
			var drinkIds = await _getDrinkDetails.GetAllDrinksId();
            var randomId = random.Next(0,drinkIds.Count());

            return RedirectToAction(nameof(DrinkDetails), new { id = drinkIds.ElementAt(randomId)});
        }

        public async Task<IActionResult> DrinkDetails(int id)
        {
			var drinkWithDetails = await _getDrinkDetails.GetDrinkToDetailView(id);

			var drinkWithDetailsModel = _mapper.Map<DrinkWithDetailsModel>(drinkWithDetails);
            return View(drinkWithDetailsModel);
        }

        public async Task<IActionResult> DrinkMixerPrep()
		{
			var ingredientsNames = await _searchByIngredients.GetAllNamesDistinct();

			var ingredientsNamesModel = new IngredientsSearchModel();

			ingredientsNamesModel.IngredientsNames = ingredientsNames;
            ingredientsNamesModel.IngredientsToSearch = new List<string>();

			return RedirectToAction(nameof(DrinkMixer),ingredientsNamesModel);
		}


		
		public IActionResult DrinkMixerAdd(string allnames, string searchnames, string name)
		{
			var searchNamesList = new List<string>();

			var allNamesList = _searchByIngredients.GetAllNamesFromSumbit(allnames);
			if (searchnames != null)
			{
				searchNamesList = _searchByIngredients.GetAllNamesFromSumbit(searchnames);
			}



			searchNamesList.Add(name);
			allNamesList.Remove(name);

			var ingredientsSearch = new IngredientsSearchModel { IngredientsNames = allNamesList, IngredientsToSearch = searchNamesList };

			return RedirectToAction(nameof(DrinkMixer), ingredientsSearch);
		}

		public IActionResult DrinkMixerRemove(string allnames, string searchnames, string name)
		{
			var allNamesList = new List<string>();

			var searchNamesList = _searchByIngredients.GetAllNamesFromSumbit(searchnames);
			if (allnames != null)
			{
				allNamesList = _searchByIngredients.GetAllNamesFromSumbit(allnames);
			}



			searchNamesList.Remove(name);
			allNamesList.Add(name);

			var ingredientsSearch = new IngredientsSearchModel { IngredientsNames = allNamesList, IngredientsToSearch = searchNamesList };

			return RedirectToAction(nameof(DrinkMixer), ingredientsSearch);
		}

		public async Task<IActionResult> DrinkMixerResults(string searchnames)
		{

			if (searchnames == null)
			{
				return RedirectToAction(nameof(DrinkMixerPrep));
			}

			var drinksDtos = await _searchByIngredients.GetMatchingDrinksToIngredients(searchnames);

			var drinks = new List<DrinkSearchModel>();
			foreach(var drink in drinksDtos)
			{
				drinks.Add(_mapper.Map<DrinkSearchModel>(drink));
			}


			return View("DrinkSearch",drinks);

		}



		[HttpGet]
        [Route("mixer")]
		public IActionResult DrinkMixer(IngredientsSearchModel model)
        {

            return View(model);
        }
    }
}
