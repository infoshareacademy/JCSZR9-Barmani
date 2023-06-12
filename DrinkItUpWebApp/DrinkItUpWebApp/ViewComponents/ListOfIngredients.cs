using DrinkItUpWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinkItUpWebApp.ViewComponents
{
	public class ListOfIngredients : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(List<string> ingredientsNames, List<string> ingredientsToSearch)
		{
			var ingredientsNamesModel = new IngredientsSearchModel { IngredientsNames = ingredientsNames, IngredientsToSearch = ingredientsToSearch };
			
			return View(ingredientsNamesModel);
		}
	}
}
