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
                var stringHelper = $"{(ingredient.Quantity == 0 ? "" : ingredient.Quantity + " ")}{(Unit)ingredient.Unit} {ingredient.NameOfIngredient}";
                listString.Add(stringHelper);
            }

            return listString;
        }

        public static HashSet<string> GetAllIngredientsNames(List<Drink> listDrinks)
        {
            var hashIngredientsNames = new HashSet<string>();
            var listString = new List<string>();

            foreach (Drink drink in listDrinks)
            {
                foreach (Ingredient ingredient in drink.Ingredients)
                {
                    listString.Add(ingredient.NameSingular);
                }
                hashIngredientsNames.UnionWith(listString);
            }
            return hashIngredientsNames;
        }


        public static string UnitsToString()
        {
            var stringUnits = string.Empty;
            var enumCount = Enum.GetNames(typeof(Unit)).Length;

            for (int i = 0; i < enumCount; i++)
            {
                if (i % 7 == 6)
                stringUnits += "\n";

                stringUnits += $"| \u001b[32m{i}\u001b[0m: {(Unit)i} |";


            }
            return stringUnits;
        }

        public static bool IngredientTryParse(string str, out Ingredient ingredient)
        {
            var strTable = str.Split(',');
            var enumCount = Enum.GetNames(typeof(Unit)).Length;
            ingredient = null;
            if (strTable.Count() != 4)
                return false;
            if (!Decimal.TryParse(strTable[0], out decimal quantity))
                return false;
            if (Int32.Parse(strTable[1]) > enumCount || Int32.Parse(strTable[1]) < 0)
                return false;
            ingredient = new Ingredient(quantity, (Unit)Int32.Parse(strTable[1]), strTable[2], strTable[3]);
            return true;
        }


    }
}
