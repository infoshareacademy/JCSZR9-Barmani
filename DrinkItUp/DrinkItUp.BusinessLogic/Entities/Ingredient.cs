using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Entities
{
    public class Ingredient
    {
        
        public int IngredientId { get; set; }
       
        public string Name { get; set; }

        public int UnitId { get; set; }

        public Unit Unit { get; set; }

    }
}
