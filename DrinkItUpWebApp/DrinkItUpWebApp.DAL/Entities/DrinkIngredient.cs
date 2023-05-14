using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    [Keyless]
    public class DrinkIngredient
    {
        public decimal Quantity { get; set; }

        public int DrinkId { get; set; }

        public Drink Drink { get; set; }

        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
