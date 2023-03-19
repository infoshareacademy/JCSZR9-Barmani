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

            var drinksList = DrinkLogic.GetAllDrinks();
            drinksList.Add(drink);
            DataMenager.SaveListDrinks(drinksList);

        }



        // 1. potrzebuje metody do sprawdzenia jaki jest ostatni Id drinka i nadać następny
        // 2. nazwa alkoholu - dodać do listy
        // 3. alkohol główny dodawać z ręki czy z listy wyboru (jak w shortmenu)?
        // 4. difficulty dodawać z ręki czy z listy wyboru (jak w shortmenu)?
        // 5. ingredients - czy użytkownik musi najpierw wskazać liczbę składników, a nastęnie je wpisywać? 

    }
}
