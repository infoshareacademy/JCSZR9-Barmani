using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class MainAlcohol
    {
        public int MainAlcoholId { get; set; }
        [StringLength(25)]
        public string Name { get; set; } = null!;

        public Drink? Drink { get; set; }

    }
}
