﻿using DrinkItUp.BusinessLogic.Logic;
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

        public static void ShowDrinks(List<Drink> drinks, int listElement)
        {

            if (listElement > drinks.Count() - 1 || listElement < 0)
            {
                Console.WriteLine("Błąd");
                return;

            }

            Console.Clear();
            Console.WriteLine($"Znaleziono {drinks.Count()} pasujące drinki! Oto one:");
            var card = DrinkCard.GetDrinkCard(drinks.ElementAt(listElement));
            DrinkCard.ShowDrinkCard(card);

            Console.WriteLine();

            var shortMenu = ShortMenu.GetShortMenu();
            ShortMenu.ShowShortMenu(drinks, shortMenu, listElement);
        }



    }
}
