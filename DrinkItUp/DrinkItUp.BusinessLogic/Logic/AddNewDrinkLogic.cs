using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public static class AddNewDrinkLogic


    {
        private static int _id = DataMenager.Drinks.Max(d => d.Id);

        private static int GetNextId()
        { 
            return _id++;
        }


        public static void AddNewDrink (string name, MainAlcohol mainalcohol, Difficulty difficulty, List<Ingredient> ingredients, string description)
        {
            int id = GetNextId();

            var drink = new Drink(id, name, mainalcohol,difficulty, ingredients, description);

            DataMenager.Drinks.Add(drink);

            DataMenager.SaveListDrinks(DataMenager.Drinks);

        }
    }
}
