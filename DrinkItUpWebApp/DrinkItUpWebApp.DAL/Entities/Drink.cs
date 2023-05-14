using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Drink
    {
        public int DrinkId { get; set; }

        public string Name { get; set; }

        public int MainAlcoholId { get; set; }
        public MainAlcohol MainAlcohol { get; set; }
        public int DifficultyId { get; set; }
        public Difficulty Difficulty { get; set; }

        public string Description { get; set; }


    }
}
