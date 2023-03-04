using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public class IngredientLogic
    {
        public static List<string> IngredientsToList(List<Ingredient> list)
        {
            var listString = new List<string>();

            foreach (var ingredient in list)
            {
                var stringHelper = $"{ingredient.Quantity} {(Unit)ingredient.Unit} {ingredient.NameOfIngredient}";
                listString.Add(stringHelper);
            }

            return listString;
        }
    }
}
