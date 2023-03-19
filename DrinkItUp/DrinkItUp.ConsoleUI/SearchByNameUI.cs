using DrinkItUp.BusinessLogic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public static class SearchByNameUI
    {
        public static void SearchByName()
        {
            Console.Clear();
            Console.WriteLine("Wpisz nazwę wyszukiwanego drinka");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Prosze o wprowadzenie nazwy wyszukiwanego drinka");
            }

            userInput = userInput.Trim();

            var drinkUserInput = DrinkLogic.GetByName(userInput);

            DrinkCard.ShowDrinks(drinkUserInput, 0);
        }
    }
}
