using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI.Menu
{
    public static class LoginMenu
    {

        public static int UserAgeToEnter;
        public static string UserName;

        public static void ShowLoginMenu()
        {
            string color = "\u001b[32m";
            int CurrentYear = DateTime.Now.Year; // obecny rok

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("{0," + Console.WindowWidth / 2 + "}", "Cześć! Podaj swój rok urodzenia"));
            Console.SetCursorPosition(Console.WindowWidth / 3, Console.CursorTop);
            UserAgeToEnter = Convert.ToInt32(Console.ReadLine()); // zmienna wieku uzytkownika

            Console.WriteLine(string.Format("{0," + (Console.WindowWidth / 3 + 10) + "}", "Podaj swoje imię"));
            Console.SetCursorPosition(Console.WindowWidth / 3, Console.CursorTop);
            UserName = Console.ReadLine(); //Zmienna przechowująca imię użytkownika do późniejszego przywitania
            Console.ResetColor();
            bool AdultUser;
            AdultUser = true;

            MainMenu.ShowMainMenu();
        }
    }
}
