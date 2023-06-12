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
                var drinks = await _drinkService.GetAll();
                category.Results = drinks.Select(d => _mapper.Map<DrinkSearchModel>(d));

                return View("CategoryAlcohol", category);
            }
			else if(category.CategoryName == "Poziom trudności")
			{
                var drinks = await _drinkService.GetAll();
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