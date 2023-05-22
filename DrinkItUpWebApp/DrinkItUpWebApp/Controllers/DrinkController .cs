using DrinkItUpBusinessLogic;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;

namespace DrinkItUpWebApp.Controllers
{
    public class DrinkController : Controller
    {
        private ISearchByIngredients _searchByIngredients;

        public DrinkController(ISearchByIngredients searchByIngredients)
        {

            _searchByIngredients = searchByIngredients;

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
