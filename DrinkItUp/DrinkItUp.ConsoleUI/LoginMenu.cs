using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public static class LoginMenu
    {
        
        
        private static int _userAgeToEnter;

        public static int UserAgeToEnter
        {
            get { return _userAgeToEnter; }
            private set { _userAgeToEnter = value; }
        }

        private static string _userName;
        public static string UserName
        {
            get { return _userName; }
            private set { _userName = value; }
        }

        public static void ShowLoginMenu()
        {
            string color = "✅ \u001b[32m";
            int CurrentYear = DateTime.Now.Year; // obecny rok
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Cześć! Podaj swój rok urodzenia"));
            UserAgeToEnter = Convert.ToInt32(Console.ReadLine()); // zmienna wieku uzytkownika
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Podaj swoje imię"));
            string UserName = Console.ReadLine(); //Zmienna przechowująca imię użytkownika do późniejszego przywitania
            Console.ResetColor();
            bool AdultUser;
            AdultUser = true;

            MainMenu.ShowMainMenu();
        }
    }
}
