using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Drink
    {
        public int DrinkId { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public int MainAlcoholId { get; set; }

        public MainAlcohol MainAlcohol { get; set; } = null!;

        public int DifficultyId { get; set; }

        public Difficulty Difficulty { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();

    }
}
