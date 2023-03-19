using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using DrinkItUp.ConsoleUI;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic
{
    
    public class SearchByIngredients
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        } 
        /// <summary>
        /// Podajemy listę wszystkch drinków! Reszta dzieję się sama!
        /// </summary>
        /// <param name="drinkList"></param>
        public static void SearchByIngredientsUI(List<Drink> drinkList)
        {
            Console.Clear();

            var listAllIngredientsNames = IngredientLogic.GetAllIngredientsNames(drinkList);
            var dictionaryUserInput= new Dictionary<string, List<Drink>>();

            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);
            Console.WriteLine("Napisz co masz w domu! Cytryny? Gin? Tonik? Wpisz w konsole te składniki oddzielając przecinkiem.");
            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);
            Console.WriteLine("Dla przykładu wyżej byłoby to 'cytryny,gin,tonik,' !! Podpowiedzi wyświetlają się u góry!");
            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);
            Console.WriteLine("Naciśnij TAB żeby dodać pierwszą podpowiedź! Ostatni składnik zatwierdź po przecinku naciskając Enter! BENG!");
            Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop + 4);

            string sPattern = "([^a-z]|^)([A-Z]|[a-z])*";
            string stringHelper = string.Empty;
            int i = 10;
            int counter = 1;
            List<string> list = new List<string>();

            while (true)
            {

                ConsoleKeyInfo key = Console.ReadKey();
                Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop + -2);
                ClearCurrentConsoleLine();
                Console.SetCursorPosition((Console.WindowWidth - 100) / 2, Console.CursorTop);

                if (key.Key == ConsoleKey.Enter)
                {
                    var results = DrinkLogic.SearchByIngredientsLogic(dictionaryUserInput);
                    if(results != null)
                    {
                        DrinkCard.ShowDrinks(results, 0);
                    }
                    break;
                }
                else if (key.Key == ConsoleKey.OemComma)
                {
                    dictionaryUserInput.Add(stringHelper, new List<Drink>());
                    stringHelper = string.Empty;
                    sPattern = "([^a-z]|^)([A-Z]|[a-z])*";
                    i = 10;

                }
                else if (key.Key == ConsoleKey.Tab)
                {
                    Console.SetCursorPosition(((Console.WindowWidth - 100) / 2) + counter - (i - 9), Console.CursorTop + 2);
                    Console.Write(list.ElementAt(0) + ",");
                    Console.SetCursorPosition(((Console.WindowWidth - 100) / 2) + counter, Console.CursorTop - 2);
                    counter += list.ElementAt(0).Length - (i - 9) + 1;
                    stringHelper = list.ElementAt(0).ToString();
                    dictionaryUserInput.Add(stringHelper, new List<Drink>());
                    stringHelper = string.Empty;
                    sPattern = "([^a-z]|^)([A-Z]|[a-z])*";
                    i = 10;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (i > 10)
                    {
                        i--;
                        stringHelper = stringHelper.Remove(i - 10);
                        sPattern = sPattern.Remove(i);
                        var regex = new Regex(sPattern, RegexOptions.IgnoreCase);
                        list = listAllIngredientsNames.Where(s => regex.IsMatch(s)).Take(5).ToList();
                        // StartsWith() wykluczało np. curacao! REGEX THE BEST!
                        Console.Write(String.Join(", ", list));
                        counter -= 2;
                        Console.SetCursorPosition(((Console.WindowWidth - 100) / 2) + counter, Console.CursorTop + 2);
                        Console.Write(" ");
                        Console.SetCursorPosition(((Console.WindowWidth - 100) / 2) + counter, Console.CursorTop - 2);

                    }
                    else
                    {
                        counter--;

                    }

                }
                else
                {
                    stringHelper = stringHelper.Insert(i-10, key.KeyChar.ToString());
                    sPattern = sPattern.Insert(i, key.KeyChar.ToString());
                    var regex = new Regex(sPattern, RegexOptions.IgnoreCase);
                    list = listAllIngredientsNames.Where(s => regex.IsMatch(s)).Take(5).ToList();
                    // StartsWith() wykluczało np. curacao! REGEX THE BEST!
                    Console.Write(String.Join(", ", list));
                    i++;
                    
                }
                Console.SetCursorPosition(((Console.WindowWidth - 100) / 2) + counter, Console.CursorTop + 2);
                counter++;

            }


        }

        

    }
}
