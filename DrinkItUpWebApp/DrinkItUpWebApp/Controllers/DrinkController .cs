using DrinkItUpBusinessLogic;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;

namespace DrinkItUpWebApp.Controllers
{
    public class DrinkController : Controller
    {
        private SearchByIngredients _searchByIngredients;

        public DrinkController(DrinkContext context)
        {
            var ingredientsRepository = new IngredientRepository(context);
            _searchByIngredients = new SearchByIngredients(ingredientsRepository);
            
        }

        [HttpPost]
        public JsonResult AutoComplete(string Prefix)
        {
            var words = _searchByIngredients.GetAllIngredientsMatchingNames(Prefix);

            return Json(words);
        }

        public IActionResult DrinkSearch()
        {
            return View();
        }

        public IActionResult DrinkBrowse()
        {
            return View();
        }

        public IActionResult DrinkMixer()
        {
            return View();
        }
    }
}
