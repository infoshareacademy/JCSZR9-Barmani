using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public class ShortMenu
    {
        private static int option = 1;
        private static string color = "✅ \u001b[32m";
        private static string color2 = "✅ \u001b[31m";



        

        public static List<Card> GetShortMenu()
        {
            List<Card>? cardsToMenu = new();

            cardsToMenu.Clear();
            cardsToMenu.Add(new Card(1, 1, "Następny", ""));
            cardsToMenu.Add(new Card(2, 2, "Poprzedni", ""));
            cardsToMenu.Add(new Card(3, 3, "Wyjście", ""));
            return cardsToMenu;
        }

        

        /// <summary>
        /// Używamy zawsze najpierw ShowDrinks z DrinkCards.
        /// </summary>
        /// <param name="drinks"></param>
        /// <param name="shortMenu"></param>
        /// <param name="listElement"></param>
        public static void ShowShortMenu(List<Drink> drinks, List<Card> shortMenu, int listElement)
        {

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
                    if (option == 1)
                    {
                        if (listElement == drinks.Count() - 1)
                            break;
                        else
                        {
                            listElement += 1;
                            break;
                        }
                    }
                    else if (option == 2)
                    {
                        if (listElement == 0)
                            break;
                        else
                        {
                            listElement -= 1;
                            break;
                        }
                    }
                    else if (option == 3)
                    {
                        MainMenu.ShowMainMenu();
                    
                    }

                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;


            }

            DrinkCard.ShowDrinks(drinks, listElement);

        }

        public static void ShowShortMenuOneResult()
        {
            Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
            Console.WriteLine($"{color} Wyjście\u001b[0m");
            ConsoleKeyInfo key = Console.ReadKey(false);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    MainMenu.ShowMainMenu();
                    break;

                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;


            }
        }


    }
}
