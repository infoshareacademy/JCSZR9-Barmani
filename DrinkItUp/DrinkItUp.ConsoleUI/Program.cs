
using DrinkItUp.BusinessLogic.Logic;

namespace DrinkItUp.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Console.WriteLine(DrinkLogic.GetById(3).Name);
        }
    }
}