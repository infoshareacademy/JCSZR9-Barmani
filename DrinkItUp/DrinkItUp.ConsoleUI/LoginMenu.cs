using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu;

namespace DrinkItUp.ConsoleUI
{
        public class LoginMenu
        {
            public static void Main(string[] args)
            {
                // utworzenie obiektu klasy LoginMenu
                UserInput user = new UserInput();
           
                // pobranie danych użytkownika
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Cześć! Podaj swój rok urodzenia"));
                user.UserAgeToEnter = Convert.ToInt32(Console.ReadLine()); // zmienna wieku uzytkownika
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Podaj swoje imię"));
                user.UserName = Console.ReadLine(); //Zmienna przechowująca imię użytkownika do późniejszego przywitania
                Console.ResetColor();
                MainMenu mainMenu = new MainMenu(user);
                mainMenu.ShowData();
            }
        }
}
