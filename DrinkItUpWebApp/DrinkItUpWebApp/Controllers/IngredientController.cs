using AutoMapper;
using DrinkItUpBusinessLogic.DTOs;
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
        private readonly IUnitService _unitService;
        

            

        public IngredientController(IIngredientService ingredientService, IUnitService unit, IMapper mapper)
        {
            _mapper = mapper;
            _ingredientService = ingredientService;
            _unitService = unit;

        }
        // GET: IngredientController
        public async Task<ActionResult> Index()
        {
            

            var ingredientModel = new IngredientModel();
            ingredientModel.IngredientsWithUnits = await GetAllIngredients();
            ingredientModel.Units = await GetAllUnits();


            return View(ingredientModel);
        }

        // POST: IngredientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IngredientModel model)
        {
         
            var ingredientDto = _mapper.Map<IngredientDto>(model);
            if (await _ingredientService.IsIngredientUnique(ingredientDto))
            {
                ingredientDto = await _ingredientService.Add(ingredientDto);
            }

            if (ingredientDto.IngredientId != 0)
            {
                return RedirectToAction(nameof(Edit), new { id = ingredientDto.IngredientId });
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
           
        }

        // GET: IngredientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var ingredientModel = _mapper.Map<IngredientModel>(await _ingredientService.GetById(id));
            ingredientModel.IngredientsWithUnits = await GetAllIngredients();
            ingredientModel.Units = await GetAllUnits();

            return View(ingredientModel);
        }

        // POST: IngredientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IngredientModel model)
        {
            var ingredientDto = _mapper.Map<IngredientDto>(model);
            if (await _ingredientService.IsIngredientUnique(ingredientDto))
            {
                ingredientDto = await _ingredientService.Update(ingredientDto);
                return RedirectToAction(nameof(Edit), new { id = ingredientDto.IngredientId });
            }

            return RedirectToAction(nameof(Edit), new { id = ingredientDto.IngredientId });

        }

        [HttpGet]
        // GET: IngredientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ingredientModel = _mapper.Map<IngredientModel>(await _ingredientService.GetById(id));
            ingredientModel.IngredientsWithUnits = await GetAllIngredients();
            ingredientModel.Units = await GetAllUnits();
            return View(ingredientModel);
        }

        // POST: IngredientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IngredientModel model)
        {
            if(await _ingredientService.IngredientIsUsed(id))
            {
                return RedirectToAction(nameof(Edit), new { id = id });
            }

            if(await _ingredientService.Remove(id))
                return RedirectToAction(nameof(Index));
            else
                return RedirectToAction(nameof(Edit), new { id = id });

        }

        private async Task<List<UnitModel>> GetAllUnits()
        {
            var unitModels = new List<UnitModel>();
            var unitsDto = await _unitService.GetAll();

            foreach (var unitDto in unitsDto)
            {
                var unitModel = _mapper.Map<UnitModel>(unitDto);
                unitModels.Add(unitModel);
            }

            return unitModels;
        }

        public async Task<List<IngredientModel>> GetAllIngredients()
        {
            var ingredientsDto = await _ingredientService.GetAllIngredientsWithUnits();
            var ingredientModels = new List<IngredientModel>();

            foreach (var ingredientDto in ingredientsDto)
            {
                var ingredient = _mapper.Map<IngredientModel>(ingredientDto);
                ingredientModels.Add(ingredient);

            }

            return ingredientModels;
        }
    }
}
