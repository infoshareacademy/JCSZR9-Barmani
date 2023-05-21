using System.ComponentModel.DataAnnotations;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Difficulty
    {

        public int DifficultyId { get; set; }
        [StringLength(7)]
        public string Name { get; set; } = null!;

    }
}
