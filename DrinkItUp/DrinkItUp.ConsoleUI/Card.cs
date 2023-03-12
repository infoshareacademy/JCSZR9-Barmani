using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.ConsoleUI
{
    public class Card
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public string CardTitle { get; set; }

        public string CardContent { get; set; }


        public Card(int id, int itemid, string cardtitle, string cardcontent)
        {
            Id = id;
            ItemId = itemid;
            CardTitle = cardtitle;
            CardContent = cardcontent;
        }

        public Card(int id, string cardtitle) 
        { 
            Id = id;
            CardTitle = cardtitle;

        }
    }
}
