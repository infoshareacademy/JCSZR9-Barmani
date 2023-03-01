using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    internal static class DrinkCard
    {
        public static List<Card>? cardsToMenu = new();

        public static List<Card> GetDrinkCard(Drink drink)
        {
            cardsToMenu.Clear();
            cardsToMenu.Add(new Card(1, drink.Id, "Nazwa: ", drink.Name));
            cardsToMenu.Add(new Card(2, drink.Id, "Główny Alkohol: ", drink.mainAlcohol.Alcohol));
            cardsToMenu.Add(new Card(3, drink.Id, "Trudność przygotowania: ", drink.difficulty.Level));
            cardsToMenu.Add(new Card(4, drink.Id, "Składniki: ", drink.Ingredients));
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
                    Console.WriteLine();
                    Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                    int m = 0;
                    string stringhelper = drinkCards.ElementAt(i).CardContent;
                    for (int n = 0; n < stringhelper.Length - 1; n++)
                    {

                        if (stringhelper[n] == ' ')
                        {
                            m++;
                        }

                        if (m == 2)
                        {
                            m = 0;
                            
                            stringhelper = stringhelper.Substring(0, n);
                            Console.WriteLine(stringhelper);
                            Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                            stringhelper = drinkCards.ElementAt(i).CardContent;
                            drinkCards.ElementAt(i).CardContent = stringhelper.Substring(n + 1, stringhelper.Length - (n + 1));
                            stringhelper = drinkCards.ElementAt(i).CardContent;
                            n = 0;
                        }
                        
                    }
                    Console.Write(drinkCards.ElementAt(i).CardContent);

                }
                else
                {
                    Console.Write($"{drinkCards.ElementAt(i).CardContent}");

                }
                Console.ForegroundColor = ConsoleColor.Gray;

            }



        }

    }
}
