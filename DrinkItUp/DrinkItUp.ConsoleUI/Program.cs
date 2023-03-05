
using DrinkItUp.BusinessLogic;
using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;

namespace DrinkItUp.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SearchEngine.SearchByIngredients(DataMenager.Drinks);


        }
    }
}