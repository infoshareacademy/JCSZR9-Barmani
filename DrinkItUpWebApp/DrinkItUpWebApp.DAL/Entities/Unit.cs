using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Unit
    {
        public int UnitId { get; set; }
        [StringLength(25)]
        public string Name { get; set; } = null!;

        public Ingredient? Ingredient { get; set; }
    }
}
