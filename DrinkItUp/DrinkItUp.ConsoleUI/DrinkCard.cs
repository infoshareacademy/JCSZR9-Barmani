using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public static class DrinkCard
    {
        public static List<Card>? cardsToMenu = new();

        public static List<Card> GetDrinkCard(Drink drink)
        {
            
            cardsToMenu.Clear();
            cardsToMenu.Add(new Card(1, drink.Id, "Nazwa: ", drink.Name));
            cardsToMenu.Add(new Card(2, drink.Id, "Główny Alkohol: ", drink.mainAlcohol.Alcohol));
            cardsToMenu.Add(new Card(3, drink.Id, "Trudność przygotowania: ", drink.difficulty.Level));
            cardsToMenu.Add(new Card(4, drink.Id, "Składniki: ", "składniki" ));
            cardsToMenu.Add(new Card(5, drink.Id, "Opis przygotowania: ", drink.Description));
            return cardsToMenu;
        }

        public static void ShowDrinkCard(List<Card> drinkCards)
        {
            
            
            for (int i = 0; i < drinkCards.Count; i++)
            {
                Console.WriteLine();
                Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{drinkCards.ElementAt(i).CardTitle}");
                Console.ForegroundColor = ConsoleColor.White;
                if (drinkCards.ElementAt(i).CardContent.Length > 60)
                {
                    Console.WriteLine();
                    Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (drinkCards.ElementAt(i).CardContent.Length > 60)
                        {

                            int j = 45;
                            string stringhelper = drinkCards.ElementAt(i).CardContent;
                            while (true)
                            {
                                if (stringhelper[j] != ' ')
                                {
                                    j++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            stringhelper = stringhelper.Substring(0, j);
                            Console.WriteLine(stringhelper);
                            Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                            stringhelper = drinkCards.ElementAt(i).CardContent;

                            drinkCards.ElementAt(i).CardContent = stringhelper.Substring(j + 1, stringhelper.Length - (j + 1));

                        }
                        else
                        {
                            Console.Write($"{drinkCards.ElementAt(i).CardContent}");
                            break;
                        }
                    }
                }
                else if ( i == 3)
                {
                    var list = IngredientLogic.IngredientsToList(DrinkLogic.GetById(drinkCards.ElementAt(3).ItemId).Ingredients);

                    for (int j = 0; j < list.Count; j++)
                    {
                        Console.WriteLine();
                        Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);

                        Console.Write(list.ElementAt(j));
                    }

                }
                else
                {
                    Console.Write($"{drinkCards.ElementAt(i).CardContent}");

                }
                Console.ForegroundColor = ConsoleColor.Gray;

            }


            Console.WriteLine();
        }
        /// <summary>
        /// Zawsze używamy do wyświetlenia drinków. Jak masz tylko jeden wynik to zawsze włóż go do "List<Drink>"
        /// </summary>
        /// <param name="drinks"> Zawsze podajemy Listę! Nawet przy jednym drinku!</param>
        /// <param name="listElement">Zaczynaj od zera!</param>
        public static void ShowDrinks(List<Drink> drinks, int listElement)
        {

            if (listElement > drinks.Count() - 1 || listElement < 0)
            {
                Console.WriteLine("Błąd");
                return;

            }

            Console.Clear();
            Console.WriteLine($"Drinki do wyświetlenia: {drinks.Count()}. W przypadku więcej niż jednego drinka, użyj 'Następny' bądź 'Poprzedni':");
            var card = DrinkCard.GetDrinkCard(drinks.ElementAt(listElement));
            DrinkCard.ShowDrinkCard(card);

            Console.WriteLine();

            if (drinks.Count() > 1)
            {
                var shortMenu = ShortMenu.GetShortMenu();
                ShortMenu.ShowShortMenu(drinks, shortMenu, listElement);
            }
            else
            {
                ShortMenu.ShowShortMenuOneResult();
            }
        }



    }
}
