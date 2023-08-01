using System.ComponentModel.DataAnnotations;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        [MaxLength(25)]
        public string Name { get; set; } = null!;

    }
}
