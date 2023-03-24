using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public static class AddNewDrinkUI
    {
        public static void AddNewDrink()
        {
            string nameOfDrink, alcohol, description, difficulty;
            Console.Clear();

            Console.WriteLine("Witaj we wprowadzaniu drinka. Każdy wybór trzeba wprowadzić wg. instrukcji.");
            Console.WriteLine("Możesz w każdej chwili przerwać wprowadzanie wpisując 'q'");
            Console.WriteLine();

            do
            {
                Console.Write("Podaj nazwę drinka: ");
                nameOfDrink = Console.ReadLine();

                if (nameOfDrink == "q")
                {
                    MainMenu.ShowMainMenu();
                }
                else if (string.IsNullOrWhiteSpace(nameOfDrink))
                {
                    ErrMsg("Panie... no wpisz coś! Naciśnij dowolny klawisz i wpisz ponownie!");
                }
                else
                {
                    break;
                }
            } while (true);

            do
            { 
            Console.WriteLine("Wpisz alkohol słownie zachowując wielkość liter.");
            Console.WriteLine("Dostępne główne alkohole:");
            Console.WriteLine(MainAlcoholConverter.MainAlcoholToString());
            Console.Write("Podaj alkohol dominujący w drinku: ");
            alcohol = Console.ReadLine();

                if (alcohol == "q")
                {
                    MainMenu.ShowMainMenu();
                }
                else if (string.IsNullOrWhiteSpace(alcohol))
                {
                    ErrMsg("Panie... no wpisz coś! Naciśnij dowolny klawisz i wpisz ponownie!");
                }
                else if (!MainAlcoholConverter.AlcoholTryParse(alcohol, out MainAlcohol mainAlcohol))
                {
                    ErrMsg("Panie... Wpisze ten alkohol tak jak masz pokazane! Naciśnij dowolny klawisz i wpisz ponownie!");
                }
                else
                {
                    break;
                }
            } while (true);

            do
            {
                Console.WriteLine("Wpisz stopień trudności słownie zachowując wielkość liter.");
                Console.WriteLine("Dostępne stopnie trudności:");
                Console.WriteLine(DifficultyConverter.DifficultyString());
                Console.Write("Podaj stopień trudnośći: ");
                difficulty = Console.ReadLine();

                if (difficulty == "q")
                {
                    MainMenu.ShowMainMenu();
                }
                else if (string.IsNullOrWhiteSpace(difficulty))
                {
                    ErrMsg("Panie... no wpisz coś! Naciśnij dowolny klawisz i wpisz ponownie!");
                }
                else if (!DifficultyConverter.DifficultyTryParse(difficulty, out Difficulty newDifficulty))
                {
                    ErrMsg("Panie... Wpisze ten stopień trudności tak jak masz pokazane! Naciśnij dowolny klawisz i wpisz ponownie!");
                }
                else
                {
                    break;
                }
            } while (true);

            var ingredients = GiveIngredients();

            do
            {
                Console.Write("Podaj opis przygotowania: ");
                description = Console.ReadLine();

                if (description == "q")
                {
                    MainMenu.ShowMainMenu();
                }
                else if (string.IsNullOrWhiteSpace(description))
                {
                    ErrMsg("Panie... no wpisz coś! Naciśnij dowolny klawisz i wpisz ponownie!");
                }
                else
                {
                    break;
                }
            } while (true);

            AddNewDrinkLogic.AddNewDrink(
                nameOfDrink,
                MainAlcoholConverter.AlcoholParse(alcohol),
                DifficultyConverter.DifficultyParse(difficulty),
                ingredients,
                description
                );

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Pomyślnie dodano drinka, Naciśnij dowolny klawisz aby wyjść do głównego menu.");
            Console.ForegroundColor = ConsoleColor.Gray; 

            Console.ReadKey();
            MainMenu.ShowMainMenu();
        }

        public static List<Ingredient> GiveIngredients()
        {
            Console.WriteLine("Będziesz teraz podawać składniki drinka. Musisz zachować odpowiednią formułę podawania.");
            Console.WriteLine("Dla przykładu: 200 ml białego rumu, zapis byłby '200,0,białego rumu,Rum'.");
            Console.WriteLine("Zielone numery opisane niżej oznaczają odpowiednią jednostkę. Ostatni wpis po ',' to zapis do wyszukiwarki.");
            Console.WriteLine("Możesz w każdej chwili przerwać wprowadzanie wpisując 'q'.");
            Console.WriteLine("Wpisz i zatwierdź 'end' gdy zapiszesz ostatni składnik.");
            Console.WriteLine(IngredientLogic.UnitsToString());
            var ingredients = new List<Ingredient>();
            do
            {
                var consoleInput = Console.ReadLine();
                if (consoleInput == "q")
                {
                    MainMenu.ShowMainMenu();
                }
                else if (consoleInput == "end")
                {
                    break;
                }
                else if (string.IsNullOrWhiteSpace(consoleInput))
                {
                    ErrMsg("Panie... no wpisz tu coś noooooo! Naciśnij dowolny klawisz i lecimy jeszcze raz!");
                }
                else if (!IngredientLogic.IngredientTryParse(consoleInput, out Ingredient ingredient))
                {
                    ErrMsg("Panie... no wpisz ten składnik według instrukcji! Naciśnij dowolny klawisz i lecimy jeszcze raz!");
                }
                else
                {
                    ingredients.Add(ingredient);
                }
            } while (true);
            return ingredients;
        }

        public static void ErrMsg(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();
        }


    }
}
