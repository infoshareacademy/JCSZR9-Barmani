using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var drinks = DrinkLogic.GetAllDrinks();
            drinks = DrinkLogic.GetByAlcohol(drinks, mainalcoholId);
            drinks = DrinkLogic.GetByDifficulty(drinks, difficultyId);
            return drinks;
        }

        public static List<Drink> GetByName(string userInput)
        {
            return _drinks.Where(c => c.Name.Contains(userInput)).ToList();

        }

    }

}

