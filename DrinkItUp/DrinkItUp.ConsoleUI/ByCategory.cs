using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using System.Collections.Generic;

namespace DrinkItUp.ConsoleUI
{
    public class ByCategory
    {
        private static int option = 1;
        private static string color = "✅ \u001b[32m";
        private static string color2 = "✅ \u001b[31m";

        public static void GetDrinks(int mainalcoholId, int difficultyId)
        {
            var drinks = DrinkLogic.GetListOfDrinks();
            drinks = DrinkLogic.GetByAlcohol(drinks, mainalcoholId);
            drinks = DrinkLogic.GetByDifficulty(drinks, difficultyId);

            ShowDrink(drinks, 0);
        }

        public static void ShowDrink(List<Drink> drinks, int listElement)
        {

            if (listElement > drinks.Count() - 1 || listElement < 0)
            {
                Console.WriteLine("Błąd");
                return;

            }

            Console.Clear();
            Console.WriteLine($"Znaleziono {drinks.Count()} pasujące drinki! Oto one:");
            var card = DrinkCard.GetDrinkCard(drinks.ElementAt(listElement));
            DrinkCard.ShowDrinkCard(card);

            Console.WriteLine();

            var shortMenu = ShortMenu.GetShortMenu();


            for (int i = 0; i < shortMenu.Count; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
                if (listElement == drinks.Count() - 1 && i == 0)
                {
                    Console.WriteLine($"{(option == i + 1 ? color2 : "   ")} {shortMenu.ElementAt(i).CardTitle}\u001b[0m");
                }
                else if (listElement == 0 && i == 1)
                {
                    Console.WriteLine($"{(option == i + 1 ? color2 : "   ")} {shortMenu.ElementAt(i).CardTitle}\u001b[0m");
                }
                else
                {
                    Console.WriteLine($"{(option == i + 1 ? color : "   ")} {shortMenu.ElementAt(i).CardTitle}\u001b[0m");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey(false);
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    option = (option == 3 ? 1 : option + 1);
                    break;

                case ConsoleKey.UpArrow:
                    option = (option == 1 ? 3 : option - 1);
                    break;

                case ConsoleKey.Enter:
                    if( option == 1)
                    {
                        if(listElement == drinks.Count() - 1)
                         break;
                        else
                        {
                            listElement += 1;
                            break;
                        }
                    }
                    else if(option== 2  )
                    {
                        if(listElement == 0)
                            break;
                        else
                        {
                            listElement -= 1;
                            break;
                        }
                    }
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;


            }

            ShowDrink(drinks, listElement);

        }

    }
}
