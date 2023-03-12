using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Model
{
    public class Drink
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MainAlcohol mainAlcohol { get; set; }

        public Difficulty difficulty { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public string Description { get; set; }

        public Drink(int id, string name, MainAlcohol mainalcohol, Difficulty difficulty, List<Ingredient> ingredients, string description)
        {
            Id = id;
            Name= name;
            mainAlcohol = mainalcohol;
            this.difficulty = difficulty;
            Ingredients= ingredients;
            Description = description;
        }

    }
}
