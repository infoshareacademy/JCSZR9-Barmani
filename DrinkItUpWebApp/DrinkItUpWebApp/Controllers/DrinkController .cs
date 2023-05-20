using DrinkItUpBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class DrinkController : Controller
    {
        private IRepositoryDataIngredient _dataIngredient;

        public DrinkController(IRepositoryDataIngredient dataIngredient)
        {
            _dataIngredient = dataIngredient;
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
