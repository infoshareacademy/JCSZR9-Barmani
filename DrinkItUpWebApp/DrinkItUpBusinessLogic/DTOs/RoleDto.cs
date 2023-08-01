using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpBusinessLogic.DTOs
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        public bool? IsUsed { get; set; }
    }
}
