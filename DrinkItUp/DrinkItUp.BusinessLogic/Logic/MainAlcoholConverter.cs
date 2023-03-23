using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public class MainAlcoholConverter
    {
        public static string MainAlcoholToString()
        {
            string mainAlcoholToString = string.Empty;

            foreach (var alcohol in DataMenager.MainAlcohols)
            {
                mainAlcoholToString += $"{alcohol.Id}. {alcohol.Alcohol} ";
            }
            return mainAlcoholToString;
        }

        public static MainAlcohol AlcoholParse(string alcohol)
        {
            var mainAlcohol = DataMenager.MainAlcohols.FirstOrDefault(a => a.Alcohol.Contains(alcohol));
            return mainAlcohol;
        }
    }
}
