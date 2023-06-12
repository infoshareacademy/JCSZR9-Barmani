using AutoMapper;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.ViewComponents
{
	public class ByCategory : ViewComponent
	{
		private readonly IByCategoryService _byCategoryService;
		private readonly IDrinkService _drinkService;
		private readonly IMainAlcoholService _mainAlcoholService;
		private readonly IDifficultyService _difficultyService;
		private readonly IMapper _mapper;

		public ByCategory(IByCategoryService byCategoryService,
			IDrinkService drinkService,
			IMainAlcoholService mainAlcoholService,
			IDifficultyService difficultyService,
			IMapper mapper)
		{
			_byCategoryService = byCategoryService;
			_drinkService = drinkService;
			_mainAlcoholService = mainAlcoholService;
			_difficultyService = difficultyService;
			_mapper = mapper;
		}
		public async Task<IViewComponentResult> InvokeAsync(CategoryModel category)
		{
			if(category.CategoryName == "Alkohol dominujący")
			{
				var mainAlcoholDtos = await _mainAlcoholService.GetAll();
				category.Values = mainAlcoholDtos.Select(m => m.Name).ToList();
				if(category.Value == null)
					category.Value = category.Values.FirstOrDefault();

				var mainAlcohol = await _mainAlcoholService.GetByName(category.Value);
				var drinks = await _byCategoryService.GetDrinksByMainAlcoholId(mainAlcohol.MainAlcoholId);
                category.Results = drinks.Select(d => _mapper.Map<DrinkSearchModel>(d));

                return View("CategoryAlcohol", category);
            }
			else if(category.CategoryName == "Poziom trudności")
			{
				var difficultyDtos = await _difficultyService.GetAll();
				category.Values = difficultyDtos.Select(d => d.Name).ToList();

                if (category.Value == null)
                    category.Value = category.Values.FirstOrDefault();

				var difficulty = await _difficultyService.GetByName(category.Value);
				var drinks = await _byCategoryService.GetDrinksByDifficultyId(difficulty.DifficultyId);
                category.Results = drinks.Select(d => _mapper.Map<DrinkSearchModel>(d));

                return View("CategoryDifficulty", category);
            }
			else
			{
                var drinks = await _drinkService.GetAll();
                category.Results = drinks.Select(d => _mapper.Map<DrinkSearchModel>(d));

                return View("CategoryAll", category);
            }
			
		}

	}
}