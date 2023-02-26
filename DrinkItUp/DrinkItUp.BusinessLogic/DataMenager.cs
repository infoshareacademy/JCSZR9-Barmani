using DrinkItUp.BusinessLogic.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic
{
    internal class DataMenager
    {
        public static List<Drink> Drinks { get; set; } = GetList<Drink>("Drink.json");
        public static List<MainAlcohol> MainAlcohols { get; set; } = GetList<MainAlcohol>("MainAlcohol.json");
        public static List<Difficulty> Difficulties { get; set; } = GetList<Difficulty>("Difficulty.json");

        public static List<T> GetList<T>(string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            string itemsSerialized = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<T>>(itemsSerialized) ?? new List<T>();
        }
    }
}
