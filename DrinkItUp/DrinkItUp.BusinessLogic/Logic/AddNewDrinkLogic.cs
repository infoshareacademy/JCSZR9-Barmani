using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public static class AddNewDrinkLogic


    {
        private static List<Drink>? _drinks = DataMenager.Drinks;
        


        public static List<Drink> AddNewDrink(string name, MainAlcohol mainalcohol, Difficulty difficulty, List<Ingredient> ingredients, string description)
        {
            var drinks = DrinkLogic.GetAllDrinks();
            drinks.Name = name;
            //List<Drink> drinks.Name = name;
            //List<Drink> MainAlcohol = mainalcohol;
            //List<Drink> Difficulty difficulty, 
            //List<Drink> List< Ingredient > ingredients,
            //List<Drink> string description
            //drinks = DrinkLogic.GetByAlcohol(drinks, mainalcoholId);
            return _drinks;



        }



        // 1. potrzebuje metody do sprawdzenia jaki jest ostatni Id drinka i nadać następny
        // 2. nazwa alkoholu - dodać do listy
        // 3. alkohol główny dodawać z ręki czy z listy wyboru (jak w shortmenu)?
        // 4. difficulty dodawać z ręki czy z listy wyboru (jak w shortmenu)?
        // 5. ingredients - czy użytkownik musi najpierw wskazać liczbę składników, a nastęnie je wpisywać? 

    }
}
