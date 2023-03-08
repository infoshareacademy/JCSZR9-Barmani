using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public class ShortMenu
    {
        public static List<Card>? cardsToMenu = new();

        public static List<Card> GetShortMenu()
        {

            cardsToMenu.Clear();
            cardsToMenu.Add(new Card(1, 1 , "Następny", ""));
            cardsToMenu.Add(new Card(2, 2 , "Poprzedni", ""));
            cardsToMenu.Add(new Card(3, 3,  "Wyjście", ""));
            return cardsToMenu;
        }



    }
}
