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
		// Dla wyszukiwania po składnikach wyszukiwarka, podpowiedzi.
        [HttpPost]
        public async Task<JsonResult> AutoCompleteIngredients(string Prefix, string Idefix)
        {
            var words = await _searchByIngredients.GetAllIngredientsMatchingNames(Prefix, Idefix);
            return Json(words);
        }

		// Szukaj dla Głównej wyszukiwarki
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
				return Json(drinksModel.Take(3));
			}
			return Json(null);
		}

		[HttpPost]
		public async Task<IActionResult> DrinkSearchBar(string input)
		{
			string msg = "Wyniki wyszukiwania:";
            var drinks = await _searchByNameOrOneIngredient.SearchByName(input);
            var drinksModel = new Dictionary<string, List<DrinkSearchModel>>();
            if (drinks != null)
            {
                foreach (var drink in drinks)
                {
                    var drinkModel = _mapper.Map<DrinkSearchModel>(drink);
					if (drinksModel.ContainsKey(msg))
						drinksModel[msg].Add(drinkModel);
                    else
						drinksModel.Add("Wyniki wyszukiwania:",new List<DrinkSearchModel> { drinkModel });
                }
				var drinkSearch = new DrinkSearchModel();
				drinkSearch.Results = drinksModel;
                return View("DrinkSearch", drinkSearch);
            }
            return RedirectToAction(nameof(DrinkSearch), drinksModel);
        }

		[HttpGet]
		public IActionResult DrinkSearch(DrinkSearchModel drinkSearch)
        {
            return View(drinkSearch);
        }


        public IActionResult DrinkBrowse(CategoryModel category)
        {
            return View(category);
        }

		//Zaskocz mnie
        public async Task<IActionResult> DrinkSuprise()
        {
			var random = new Random();
			var drinkIds = await _getDrinkDetails.GetAllDrinksId();
            var randomId = random.Next(0,drinkIds.Count());

            return RedirectToAction(nameof(DrinkDetails), new { id = drinkIds.ElementAt(randomId)});
        }

		//Widok Szczegółowy
        public async Task<IActionResult> DrinkDetails(int id)
        {
			var drinkWithDetails = await _getDrinkDetails.GetDrinkToDetailView(id);

			var drinkWithDetailsModel = _mapper.Map<DrinkWithDetailsModel>(drinkWithDetails);

            return View(drinkWithDetailsModel);
        }

		// Wyszukiwanie po składnikach
        public async Task<IActionResult> DrinkMixerPrep()
		{
			var ingredientsNames = await _searchByIngredients.GetAllNamesDistinct();

			var ingredientsNamesModel = new IngredientsSearchModel();

			ingredientsNamesModel.IngredientsNames = ingredientsNames;
            ingredientsNamesModel.IngredientsToSearch = new List<string>();

			return RedirectToAction(nameof(DrinkMixer),ingredientsNamesModel);
		}


		
		public async Task<IActionResult> DrinkMixerAdd(string searchnames, string name)
		{
			var searchNamesList = new List<string>();

			
			if (searchnames != null)
			{
				searchNamesList = _searchByIngredients.GetAllNamesFromSumbit(searchnames);
			}

			var allNamesList = await _searchByIngredients.GetAllNamesDistinct();
			allNamesList = allNamesList.Except(searchNamesList).ToList();


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

			var drinks = new Dictionary<string, List<DrinkSearchModel>>();
			foreach(var drinkKey in drinksDtos.Keys)
			{
				foreach (var drinkDto in drinksDtos[drinkKey])
				{
					var drinkModel = _mapper.Map<DrinkSearchModel>(drinkDto);

					if (drinks.ContainsKey(drinkKey))
						drinks[drinkKey].Add(drinkModel);
					else
						drinks.Add(drinkKey, new List<DrinkSearchModel> { drinkModel });
				}
			}

			var drinkSearch = new DrinkSearchModel();
			drinkSearch.Results = drinks;
			return View("DrinkSearch",drinkSearch);

		}



		[HttpGet]
		public IActionResult DrinkMixer(IngredientsSearchModel model)
        {

            return View(model);
        }
    }
}
