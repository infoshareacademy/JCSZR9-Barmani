﻿using DrinkItUpBusinessLogic;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using DrinkItUpWebApp.DAL.Repositories.Interfaces;
using DrinkItUpWebApp.Models;
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

		public IActionResult DrinkMixerPrep()
		{
			var ingredientsNames = _searchByIngredients.GetAllNames();

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

		//[HttpPost]
		//[ValidateAntiForgeryToken]
  //      [Route("[controller]/mix1/{allNames}&{searchNames}&{name}")]
		//public ActionResult DrinkMixerAdd(string allNames,string searchNames, string name)
		//{
  //          var searchNamesList = new List<string>();

		//	var allNamesList = _searchByIngredients.GetAllNamesFromSumbit(allNames);
  //          if(searchNames != null)
  //          {
  //              searchNamesList = _searchByIngredients.GetAllNamesFromSumbit(searchNames);
  //          }

			

		//	searchNamesList.Add(name);
		//	allNamesList.Remove(name);

		//	var ingredientsSearch = new IngredientsSearchModel{ IngredientsNames = allNamesList, IngredientsToSearch = searchNamesList }; 

		//	return RedirectToAction(nameof(DrinkMixer),ingredientsSearch);
		//}

        [HttpGet]
        [Route("mixer")]
		public IActionResult DrinkMixer(IngredientsSearchModel model)
        {

            return View(model);
        }
    }
}
