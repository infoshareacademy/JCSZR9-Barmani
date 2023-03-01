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

        public static void GetDrinkCard(Drink drink)
        {
            cardsToMenu.Clear();
            cardsToMenu.Add(new Card(1, drink.Id, "Nazwa: ", drink.Name));
            cardsToMenu.Add(new Card(2, drink.Id, "Główny Alkohol: ", drink.mainAlcohol.Alcohol));
            cardsToMenu.Add(new Card(3, drink.Id, "Trudność przygotowania: ", drink.difficulty.Level));
            cardsToMenu.Add(new Card(4, drink.Id, "Składniki: ", drink.Ingredients));
            cardsToMenu.Add(new Card(5, drink.Id, "Opis przygotowania: ", drink.Description));

        }



    }
}
