
using DrinkItUp.BusinessLogic.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public class DataMigration
    {
        private readonly DrinkContext drinkContext;
        private enum unitsEnum 
        {
            ml,
            l,
            cała,
            cały,
            całe,
            kostki,
            kostka,
            kostek,
            plasterek,
            plasterki,
            szklanka,
            listki,
            listek,
            kruszony,
            kruszona,
            listków,
            szczypta,
            plasterków,
            sztuk,
            laska,
            ćwiartka,
            ćwiartki

        };

        public DataMigration(DrinkContext drinkContext)
        {
            this.drinkContext = drinkContext;
            

        }
        public void DIUToSQL()
        {
            UnitsToSQL();
            AlcoholsToSQL();
            DifficultiesToSQL();
            DrinksToSQL();
            IngredientsToSQL();
            DrinkIngredientsToSQL();
        }
        private void UnitsToSQL()
        {
            var list = new List<Unit>();

            var enumCount = Enum.GetNames(typeof(unitsEnum)).Length;
            for(int i = 0 ; i < enumCount; i++)
            {
                var unit = new Unit { Name = $"{(unitsEnum)i}"};
                list.Add(unit);
            }

            drinkContext.Units.AddRange(list);
            drinkContext.SaveChanges();
        }

        private void AlcoholsToSQL()
        {
            var list = MainAlcoholConverter.MainAlcoholNamesToString();
            foreach (var alcohol in list)
            {
                var mainAlcohol = new MainAlcohol { Name = alcohol};
                drinkContext.MainAlcohols.Add(mainAlcohol);
            }

            drinkContext.SaveChanges();
        }

        private void DifficultiesToSQL()
        {
            var list = DifficultyConverter.DifficultiesNamesToString();
            foreach (var diff in list)
            {
                var difficulty = new Difficulty { Name = diff};
                drinkContext.Difficulties.Add(difficulty);
            }

            drinkContext.SaveChanges();
        }

        private void DrinksToSQL()
        {
            var list = DataMenager.Drinks;
            foreach (var drink in list)
            {
                var drinkToSQL = new Drink { Name = drink.Name, MainAlcoholId = drink.mainAlcohol.Id, DifficultyId = drink.difficulty.Id, Description = drink.Description };
                drinkContext.Drinks.Add(drinkToSQL);
            }

            drinkContext.SaveChanges();
        }

        private void IngredientsToSQL()
        {
            
            var listDrinks = DataMenager.Drinks;
            foreach (var drink in listDrinks)
            {
                foreach (var ingredient in drink.Ingredients)
                {
                    var ingredientSQL = drinkContext.Ingredients.FirstOrDefault(i => i.Name == ingredient.NameSingular && i.UnitId == (int)ingredient.Unit + 1);
                    if (ingredientSQL != null && ingredientSQL.Name == ingredient.NameSingular && ingredientSQL.UnitId == (int)ingredient.Unit + 1)
                        continue;

                    var ingredientToSQL = new Ingredient { Name = ingredient.NameSingular, UnitId = (int)ingredient.Unit + 1 };
                    drinkContext.Ingredients.Add(ingredientToSQL);
                    drinkContext.SaveChanges();

                }
            }

            
        }
        private void DrinkIngredientsToSQL()
        { 
            var listDrinks = DataMenager.Drinks;
            foreach (var drink in listDrinks)
            {
                foreach (var ingredient in drink.Ingredients)
                {
                    var ingredientId = drinkContext.Ingredients.FirstOrDefault(i => i.Name == ingredient.NameSingular && i.UnitId == (int)ingredient.Unit + 1).IngredientId;

                    var drinkIngredient = new DrinkIngredient { DrinkId = drink.Id, Quantity = ingredient.Quantity, IngredientId = ingredientId };
                    Console.WriteLine($"{drinkIngredient.DrinkId} + {drinkIngredient.IngredientId}");
                    drinkContext.DrinkIngredients.Add(drinkIngredient);
                }
            }

            drinkContext.SaveChanges();
        }
    }
}
