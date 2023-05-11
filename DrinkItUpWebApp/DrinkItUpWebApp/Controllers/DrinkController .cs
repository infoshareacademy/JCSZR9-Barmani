using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class DrinkController : Controller
    {
        public IActionResult DrinkSearch()
        {
            return View();
        }

        public IActionResult DrinkShow()
        {
            return View();
        }

        public IActionResult DrinkMixer()
        {
            return View();
        }
    }
}
