using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public class DifficultyConverter
    {
        public static string DifficultyString()
        {
            string difficultyString = string.Empty;

            foreach (var difficulty in DataMenager.Difficulties)
            {
                difficultyString += $"{difficulty.Id}. {difficulty.Level} ";
            }
            return difficultyString;
        }

    }
}
