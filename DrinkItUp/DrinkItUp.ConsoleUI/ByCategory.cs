using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using System.Collections.Generic;

namespace DrinkItUp.ConsoleUI
{
    public class ByCategory
    {
        

        public static void GetDrinks(int mainalcoholId, int difficultyId)
        {
            var drinks = DrinkLogic.GetListOfDrinks();
            drinks = DrinkLogic.GetByAlcohol(drinks, mainalcoholId);
            drinks = DrinkLogic.GetByDifficulty(drinks, difficultyId);

            DrinkCard.ShowDrinks(drinks, 0);
        }

        

    }
}
