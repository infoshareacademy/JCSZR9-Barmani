using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;
        private List<IngredientModel> _ingredientModels { get; set; }

        public IngredientController(IIngredientService ingredientService, IMapper mapper)
        {
            _mapper = mapper;
            _ingredientService = ingredientService;
        }
        // GET: IngredientController
        public async Task<ActionResult> Index()
        {
            var ingredientsDto = await _ingredientService.GetAllIngredientsWithUnits();
            var ingredientModels = new List<IngredientModel>();
            foreach(var ingredientDto in ingredientsDto)
            {
                var ingredient = _mapper.Map<IngredientModel>(ingredientDto);
                ingredientModels.Add(ingredient);
                _ingredientModels = ingredientModels;
            }    

            var ingredientModel = new IngredientModel();
            ingredientModel.IngredientsWithUnits = _ingredientModels;

            return View(ingredientModel);
        }

        // POST: IngredientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
