using DrinkItUp.BusinessLogic.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public static class AlkoholMenu
    {

        public static void ShowAlkoholMenu()
        {
            bool isSelected = false;
            int option = 1;
            string color = "\u263A \u001b[32m";
            int selectedoption = 0;
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Wybierz opcje z Menu"));
            Console.WriteLine();
            Console.WriteLine();
            (int left, int top) = Console.GetCursorPosition();
            while (!isSelected)
            {

                Console.SetCursorPosition(left, top);
                MainMenu.CursorPosition();
                Console.WriteLine($"{(option == 1 ? color : "   ")} Wódka\u001b[0m");
                MainMenu.CursorPosition();
                Console.WriteLine($"{(option == 2 ? color : "   ")} Whisky\u001b[0m");
                MainMenu.CursorPosition();
                Console.WriteLine($"{(option == 3 ? color : "   ")} Rum\u001b[0m");
                MainMenu.CursorPosition();
                Console.WriteLine($"{(option == 4 ? color : "   ")} Gin\u001b[0m");
                MainMenu.CursorPosition();
                Console.WriteLine($"{(option == 5 ? color : "   ")} Likier\u001b[0m");
                MainMenu.CursorPosition();
                Console.WriteLine($"{(option == 6 ? color : "   ")} Powrót do Menu Głównego\u001b[0m");



                ConsoleKeyInfo key = Console.ReadKey(false);
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
                        Environment.Exit(0);
                        break;

                }

            }
            if (selectedoption == 1 || selectedoption == 2 || selectedoption == 3 || selectedoption == 4 || selectedoption == 5)
            {
                DiffMenu.ShowDiffMenu(selectedoption);
            }
            else
            {
                MainMenu.ShowMainMenu();
            }
        }

    }
}
