using DrinkItUp.BusinessLogic;
using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using System;
using System.Drawing;
using System.Text.Json;

namespace DrinkItUp.ConsoleUI
{
    internal class StartMenu
    {
        static void Main(string[] args)

        {
            bool isSelected = false;
            
            
            ConsoleKeyInfo key;
            int option = 1;
            
            
            int selectedoption = 0;
            int selectedoption2 = 0;
            
            string color = "✅ \u001b[32m";
            int CurrentYear = DateTime.Now.Year; // obecny rok

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Cześć! Podaj swój rok urodzenia"));
            int UserAgeToEnter = Convert.ToInt32(Console.ReadLine()); // zmienna wieku uzytkownika
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Podaj swoje imię"));
            string UserName = Console.ReadLine(); //Zmienna przechowująca imię użytkownika do późniejszego przywitania
            Console.ResetColor();
            bool AdultUser;

            if ((CurrentYear - UserAgeToEnter) >= 18)
            {
                AdultUser = true;
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"Cześć {UserName}!"));
                Console.WriteLine();
                Console.WriteLine("  _\r\n {_}\r\n |(|\r\n |=|\r\n/   \\\r\n|.--|\r\n||  |\r\n||  |    .    ' .\r\n|'--|  '     \\~~~/\r\n'-=-' \\~~~/   \\_/\r\n       \\_/     Y\r\n        Y     _|_\r\n       _|_");

                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Wybierz opcje z Menu"));
                Console.WriteLine();
                Console.WriteLine();
                (int left, int top) = Console.GetCursorPosition();
                while (!isSelected)
                {

                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{(option == 1 ? color : "    ")} Przeglądaj drinki według alkoholu dominującego\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "    ")} Wyświetl wszystkie drinki\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "    ")} Wyszukaj\u001b[0m");
                    Console.WriteLine($"{(option == 4 ? color : "    ")} Zaskocz mnie\u001b[0m");
                    Console.WriteLine($"{(option == 5 ? color : "    ")} Dodaj swój drink\u001b[0m");
                    Console.WriteLine($"{(option == 6 ? color : "    ")} Edytuj istniejący drink\u001b[0m");


                    key = Console.ReadKey(false);
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:

                            option = (option == 6 ? 1 : option + 1);

                            break;

                        case ConsoleKey.UpArrow:

                            option = (option == 1 ? 6 : option - 1);

                            break;

                        case ConsoleKey.Enter:
                            isSelected = true;
                            selectedoption = option;
                            Console.Clear();
                            break;

                        case ConsoleKey.Escape:
                            isSelected = true;
                            Environment.Exit(0);
                            break;

                    }

                }


                if (selectedoption == 1)
                {
                    AlkoholMenu.ShowAlkoholMenu();
                }
                else if (selectedoption == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Wyświetlam tu wszystkie drinki");
                    var drinksList = DrinkLogic.GetAllDrinks();
                    
                    DrinkCard.ShowDrinks(drinksList, 0);


                }

                else if (selectedoption == 3)
                {
                    Console.Clear();
                    Console.WriteLine("Tu będzie wyszukiwarka");
                    SearchByNameUI.SearchByName();
                    //SearchEngine.SearchByIngredientsUI(DrinkLogic.GetAllDrinks());

                }

                else if (selectedoption == 4)
                {
                    Console.Clear();
                    Console.WriteLine("Wyskoczy random drink");
                    var random = new Random();
                    var drinksList = DrinkLogic.GetAllDrinks();
                    int i = random.Next(1, drinksList.Count());
                    List<Drink> randomDrink = new();
                    randomDrink.Add(DrinkLogic.GetById(i));
                    DrinkCard.ShowDrinks(randomDrink, 0);

                }

                else if (selectedoption == 5)
                {
                    Console.Clear();
                    Console.WriteLine("Funkcjonalnosc dodawania drinka");

                }

                else if (selectedoption == 6)
                {
                    Console.Clear();
                    Console.WriteLine("Funkcjonalnosc edycji drinka");

                }

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Wróć do nas po skończeniu 18 lat {UserName}");
            }
        }






    }
}
