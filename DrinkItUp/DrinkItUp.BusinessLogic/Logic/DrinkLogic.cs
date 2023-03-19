using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public static class DrinkLogic
    {
        // Pobieramy sobie listę z DataMenagera jako prywatne pole wewnątrz klasy, żeby za każdym razem nie odczytywać pliku.
        // Można by było zawsze się odwoływać do DataMenager.Drinks , ale to zła praktyka, przy większym projekcie masakra
        private static List<Drink>? _drinks = DataMenager.Drinks;

        public static List<Drink> GetAllDrinks()
        {
            return _drinks;
        }


        /// <summary>
        /// Metoda która pobiera Obiekt Drink z listy po Id tego drinka. 
        /// </summary>
        /// <param name="id">Id jest odpowiedzialne za ID drinka</param>
        /// <returns></returns>
        public static Drink GetById(int id)
        {
            try
            {
                // Zwracamy obiekt pierwszy znaleziony w liście po Id. To jest Linq czyli metody wbudowane w Visual Studio, na pewno będzie na zajęciach.
                return _drinks.First(c => c.Id == id);
            }
            catch(Exception ex)
            {
                // Zwracamy wódkę ze szklanką w razie wyjątku, to zawsze dobra odpowiedź!! :D Taki żarcik ;P
                Console.WriteLine("Nie znaleziono drinka po ID! Podaję jedyny prawdziwy! Naciśnij dowolny klawisz!");
                Console.ReadKey();
                return GetById(1);
            }
        }

        public static List<Drink> GetByDifficulty(List<Drink> drinks, int id)
        {                
                return (List<Drink>)drinks.Where(c => c.difficulty.Id == id).ToList();
        }

        public static List<Drink> GetByAlcohol(List<Drink> drinks, int id)
        {                
                return (List<Drink>)drinks.Where(c => c.mainAlcohol.Id == id).ToList();        
        }

        public static List<Drink> GetDrinksByCategory(int mainalcoholId, int difficultyId)
        {
            var drinks = _drinks;
            drinks = DrinkLogic.GetByAlcohol(drinks, mainalcoholId);
            drinks = DrinkLogic.GetByDifficulty(drinks, difficultyId);
            return drinks;
        }

        public static List<Drink> GetByName(string userInput)
        {
            return _drinks.Where(c => c.Name.Contains(userInput)).ToList();

        }

        public static List<Drink> SearchByIngredientsLogic(Dictionary<string, List<Drink>> dictionary)
        {
            var results = new List<Drink>();
            var listDrinks = DrinkLogic.GetAllDrinks();

            //dodajemy drinki po składnikach wpisanych przez użytkownika do słownika
            foreach (var key in dictionary.Keys)
            {
                string sPattern = "([^a-z]|^)([A-Z]|[a-z])*";
                sPattern = sPattern.Insert(10, key);
                var regex = new Regex(sPattern, RegexOptions.IgnoreCase);

                foreach (var drink in listDrinks)
                {
                    bool check = false;

                    foreach (var ingredient in drink.Ingredients)
                    {
                        if (regex.IsMatch(ingredient.NameSingular))
                            check = true;

                    }

                    if (check)
                        dictionary[key].Add(drink);

                }

            }

            // Linq bierze wszystkie drinki ze słownika, sortuje po ilości powtórzeń i wpisuje juz tylko pojedyncze drinki do listy.
            results = dictionary
                .SelectMany(l => dictionary.Values)
                .SelectMany(d => d) // spłaszczamy słownik do listy drinków samych
                .GroupBy(d => d.Id)
                .OrderByDescending(k => k.Count())
                .Select(d => DrinkLogic.GetById(d.Key))
                .ToList();

            //for (int counter = dictionary.Count(); counter > 0; counter--)
            //{
            //    foreach (var key1 in dictionary.Keys)
            //    {
            //        foreach (var drink in dictionary[key1])
            //        {
            //            int i = 1;
            //            foreach (var key2 in dictionary.Keys)
            //            {
            //                if (key2 != key1)
            //                {
            //                    if (dictionary[key2].Contains(drink))
            //                        i++;
            //                }
            //            }

            //            if(i == counter)
            //            {
            //                if(!results.Contains(drink))
            //                    results.Add(drink);
            //            }
            //        }
            //    }
            //}

            return results;
        }
    }

}

