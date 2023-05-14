using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    [PrimaryKey("DrinkId","IngredientId")]
    public class DrinkIngredient
    {
        public decimal Quantity { get; set; }
        
        public int DrinkId { get; set; }

        public Drink Drink { get; set; }
        
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
