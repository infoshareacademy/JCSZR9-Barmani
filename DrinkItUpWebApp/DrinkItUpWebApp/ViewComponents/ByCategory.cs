using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.ViewComponents
{
	public class ByCategory : ViewComponent
	{
		private readonly IByCategoryService _byCategoryService;

		public ByCategory(IByCategoryService byCategoryService)
		{
			_byCategoryService = byCategoryService;
		}
		public async Task<IViewComponentResult> InvokeAsyncAll(CategoryModel category)
		{

			return View();
		}

		public async Task<IViewComponentResult> InvokeAsyncMainAlcohol(CategoryModel category)
		{
			

			return View();
		}

		public async Task<IViewComponentResult> InvokeAsyncDifficulty(CategoryModel category)
		{
			

			return View();
		}
	}
}