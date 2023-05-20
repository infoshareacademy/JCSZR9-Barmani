using DrinkItUpBusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class DrinkController : Controller
    {
        

        public DrinkController()
        {
            
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
