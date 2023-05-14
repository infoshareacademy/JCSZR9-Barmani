using DrinkItUp.BusinessLogic.Model;
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
        public static Difficulty DifficultyParse(string str)
        {
            var difficulty = DataMenager.Difficulties.FirstOrDefault(a => a.Level.Contains(str));
            return difficulty;
        }
        public static bool DifficultyTryParse(string str, out Difficulty difficulty)
        {
            difficulty = DataMenager.Difficulties.FirstOrDefault(a => a.Level.Contains(str));

            if (difficulty == null)
                return false;
            else
                return true;
        }

        public static List<string> DifficultiesNamesToString()
        {
            var list = DataMenager.Difficulties;
            var result = new List<string>();
            foreach (var difficulty in list)
            {
                result.Add(difficulty.Level);
            }

            return result;
        }

    }
}
