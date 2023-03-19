using DrinkItUp.BusinessLogic.Logic;

namespace DrinkItUp.ConsoleUI
{
    public static class DiffMenu
    {

        public static void ShowDiffMenu(int selectedAlkohol)
        {
            bool isSelected = false;
            int option = 1;
            string color = "✅ \u001b[32m";
            int selectedoption = 0;
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "Wybierz opcje z Menu"));
            Console.WriteLine();
            Console.WriteLine();
            (int left, int top) = Console.GetCursorPosition();
            while (!isSelected)
            {

                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{(option == 1 ? color : "   ")} Łatwy\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "   ")} Średni\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "   ")} Trudny\u001b[0m");
                Console.WriteLine($"{(option == 4 ? color : "   ")} Wszystkie\u001b[0m");
                Console.WriteLine($"{(option == 5 ? color : "   ")} Wstecz\u001b[0m");
                Console.WriteLine($"{(option == 6 ? color : "   ")} Wróć do Menu Głównego\u001b[0m");



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
            if (selectedoption == 1 || selectedoption == 2 || selectedoption == 3)
            {
                var drinks = DrinkLogic.GetDrinksByCategory(selectedAlkohol, selectedoption);
                DrinkCard.ShowDrinks(drinks, 0);
            }
            else if (selectedoption == 4)
            {
                var drinks = DrinkLogic.GetAllDrinks();
                drinks = DrinkLogic.GetByAlcohol(drinks, selectedAlkohol);
                DrinkCard.ShowDrinks(drinks, 0);
            }
            else if (selectedoption == 5) 
            {
                AlkoholMenu.ShowAlkoholMenu();
            }
            else if (selectedoption == 6)
            {
                MainMenu.ShowMainMenu();
            }
        }

    }
}
