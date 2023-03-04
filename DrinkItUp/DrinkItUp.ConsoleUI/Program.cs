
using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;

namespace DrinkItUp.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var list = DrinkCard.GetDrinkCard(DrinkLogic.GetById(3));
           DrinkCard.ShowDrinkCard(list);
        }
    }
}