using Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    // klasa wyświetlająca dane użytkownika i menu główne
    public class MainMenu
    {
        private UserInput user;

        public MainMenu(UserInput user)
        {
            this.user = user;
        }

        public void ShowData()
        {
            UserInput user = new UserInput();
           



            bool isSelected = false;
            bool isSelected2 = false;
            bool isSelected3 = false;
            ConsoleKeyInfo key;
            int option = 1;
            int option2 = 1;
            int option3 = 1;
            string color = "✅ \u001b[32m";
            int CurrentYear = DateTime.Now.Year; // obecny rok
            bool Adultuser;

            

            if ((CurrentYear - user.UserAgeToEnter) >= 18)
            {
                var AdultUser = true;
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", $"Cześć {user.UserName}!"));
                Console.WriteLine();
                Console.WriteLine("  _\r\n {_}\r\n |(|\r\n |=|\r\n/   \\\r\n|.--|\r\n||  |\r\n||  |    .    ' .\r\n|'--|  '     \\~~~/\r\n'-=-' \\~~~/   \\_/\r\n       \\_/     Y\r\n        Y     _|_\r\n       _|_");

                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Wybierz opcje z Menu"));
                Console.WriteLine();
                Console.WriteLine();
                (int left, int top) = Console.GetCursorPosition();
                
                while (!isSelected)
                {

                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{(option == 1 ? color : "   ")} Przeglądaj drinki według alkoholu dominującego\u001b[0m");
                    Console.WriteLine($"{(option == 2 ? color : "   ")} Wyświetl wszystkie drinki\u001b[0m");
                    Console.WriteLine($"{(option == 3 ? color : "   ")} Wyszukaj\u001b[0m");
                    Console.WriteLine($"{(option == 4 ? color : "   ")} Zaskocz mnie\u001b[0m");
                    Console.WriteLine($"{(option == 5 ? color : "   ")} Dodaj swój drink\u001b[0m");
                    Console.WriteLine($"{(option == 6 ? color : "   ")} Edytuj istniejący drink\u001b[0m");


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
                            user.selectedoption = option;
                            Console.Clear();
                            break;

                        case ConsoleKey.Escape:
                            isSelected = true;
                            Environment.Exit(0);
                            break;

                    }

                }


            }
            if (user.selectedoption == 1)
            {

                string[] args = new string[] { }; // utworzenie pustej tablicy argumentów
                LoginMenu.Main(args);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"Wróć do nas po skończeniu 18 lat {user.UserName}");
            }

        }
    }

}
