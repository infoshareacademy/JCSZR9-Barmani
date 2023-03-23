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
            Console.Clear();
            Console.WriteLine("Witaj we wprowadzaniu drinka. Każdy wybór trzeba wprowadzić wg. instrukcji.");
            Console.WriteLine("Możesz w każdej chwili przerwać wprowadzanie wpisując 'q'");
            Console.WriteLine();
            Console.Write("Podaj nazwę drinka: ");
            var nameOfDrink = Console.ReadLine();
            Console.WriteLine("Wpisz alkohol słownie zachowując wielkość liter.");
            Console.WriteLine("Dostępne główne alkohole:");
            Console.WriteLine(MainAlcoholConverter.MainAlcoholToString());

            Console.Write("Podaj alkohol dominujący w drinku: ");
            var alcohol = Console.ReadLine();

            Console.WriteLine("Wpisz stopień trudności słownie zachowując wielkość liter.");
            Console.WriteLine("Dostępne stopnie trudności:");
            Console.WriteLine(DifficultyConverter.DifficultyString());
            Console.Write("Podaj stopień trudnośći: ");
            var difficulty = Console.ReadLine();

            var ingredients = GiveIngredients();

            Console.Write("Podaj opis przygotowania: ");
            var description = Console.ReadLine();

            AddNewDrinkLogic.AddNewDrink(
                nameOfDrink, 
                MainAlcoholConverter.AlcoholParse(alcohol),
                DifficultyConverter.DifficultyParse(difficulty),
                ingredients,
                description
                );
            MainMenu.ShowMainMenu();
        }

        public static List<Ingredient> GiveIngredients()
        {
            Console.WriteLine("Będziesz teraz podawać składniki drinka. Musisz zachować odpowiednią formułę podawania.");
            Console.WriteLine("Dla przykładu: 200 ml białego rumu, zapis byłby '200,0,białego rumu,Rum'.");
            Console.WriteLine("Zielone numery opisane niżej oznaczają odpowiednią jednostkę. Ostatni wpis po ',' to zapis do wyszukiwarki po składnikach.");
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
                else
                {
                    IngredientLogic.IngredientTryParse(consoleInput, out Ingredient ingredient);
                    ingredients.Add(ingredient);
                }
            }while (true);
            return ingredients;
        }




    }
}
