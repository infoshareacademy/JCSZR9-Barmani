using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DrinkItUpWebApp.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            if ("id" != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register(Login id)
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register()
        {

            return View();


        }

        //GET: Login
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {

            return View();
        }


        //Logout
        public ActionResult Logout()
        {

            return RedirectToAction("Login");
        }







        //// GET: LoginController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: LoginController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: LoginController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: LoginController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LoginController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LoginController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LoginController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
