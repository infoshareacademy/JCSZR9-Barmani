using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Model
{
    public class Ingredient
    {
        public decimal Quantity { get; set; }

        public Unit Unit { get; set; }

        public string NameOfIngredient { get; set; }

        public string NameSingular { get; set; }

        [JsonConstructor]
        public Ingredient(decimal quantity, Unit unit, string nameOfIngredient)
        {
            Quantity= quantity;
            Unit= unit;
            NameOfIngredient= nameOfIngredient;
        }

        public Ingredient(decimal quantity, Unit unit, string nameOfIngredient, string nameSingular)
        {
            Quantity = quantity;
            Unit = unit;
            NameOfIngredient = nameOfIngredient;
            NameSingular = nameSingular;
        }
    }


    public enum Unit
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

    }
}

