using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpWebApp.DAL.Entities
{
    public class Like
    {
        public int LikeId { get; set; }

        public int UserId { get; set; }

        public int DrinkId { get; set; }
    }
}
