using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic
{
    
    public class SearchEngine
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        } 

        private static List<string> _listAllIngredientsNames = new List<string>();

        private static void GetAllIngredients(List<Drink> listDrinks)
        {
            var listIngredientsNames = new List<string>();

            foreach(Drink drink in listDrinks)
            {
                foreach(Ingredient ingredient in drink.Ingredients)
                {
                    listIngredientsNames.Add(ingredient.NameOfIngredient);
                }    
            }

            _listAllIngredientsNames = listIngredientsNames;
        }
        public static async Task UserInput()
        {
            
            await Task.Run(() =>
            {
                string sPattern = "([^a-z]|^)([A-Z]|[a-z])*";
                int i = 0;
                int counter = 1;
                while (true)
                {
                    
                    ConsoleKeyInfo key = Console.ReadKey();
                    Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop + -2);
                    ClearCurrentConsoleLine();
                    Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else if(key.Key == ConsoleKey.Spacebar)
                    {
                        sPattern = "([^a-z]|^)([A-Z]|[a-z])*";
                        i = 10;
                        
                    }
                    else
                    {
                        sPattern = sPattern.Insert(i, key.KeyChar.ToString());
                        var regex = new Regex(sPattern, RegexOptions.IgnoreCase);
                        var list = _listAllIngredientsNames.Where(s => regex.IsMatch(s)).Take(5).ToList();
                        // StartsWith() wykluczało np. curacao! REGEX THE BEST!
                        Console.Write(String.Join(", ", list));
                        i++;
                    }
                    Console.SetCursorPosition(((Console.WindowWidth - 100) / 2) + counter, Console.CursorTop + 2);
                    counter++;
                }
            });
        }

        public static async void SearchByIngredients(List<Drink> drinkList)
        {
            Console.Clear();
            GetAllIngredients(drinkList);
            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);
            Console.WriteLine("Napisz co masz w domu! Cytryny? Gin? Tonik? Wpisz w konsole te składniki oddzielając spacją.");
            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);
            Console.WriteLine("Dla przykładu wyżej byłoby to 'cytryny gin tonik' !! Podpowiedzi wyświetlają się u góry!");
            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop + 4);
            
            var task = UserInput();
            Task.WaitAll(task);

            string userInput = Console.ReadLine();


           
        }
    }
}
