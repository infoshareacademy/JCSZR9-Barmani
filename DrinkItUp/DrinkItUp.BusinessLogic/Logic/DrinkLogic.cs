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


        public static List<Drink> GetAllDrinks()
        {
            return _drinks;
        }

        // Metoda która pobiera Obiekt Drink z listy po Id tego drinka. 
        public static Drink GetById(int id)
        {
            try
            {
                // Zwracamy obiekt pierwszy znaleziony w liście po Id. To jest Linq czyli metody wbudowane w Visual Studio, na pewno będzie na zajęciach.
                return _drinks.FirstOrDefault(c => c.Id == id);
            }
            catch(Exception ex)
            {
                // Zwracamy wódkę ze szklanką w razie wyjątku, to zawsze dobra odpowiedź!! :D Taki żarcik ;P
                Console.WriteLine("Nie znaleziono drinka po Id");
                return _drinks.ElementAt(0);
            }

        }

        public static List<Drink> GetByDifficulty(List<Drink> drinks, int id)
        {
            try
            {
                
                return (List<Drink>)drinks.Where(c => c.difficulty.Id == id).ToList();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Nie znaleziono drinka po Id");
                return new List<Drink>();
            }

        }

        public static List<Drink> GetByAlcohol(List<Drink> drinks, int id)
        {
            try
            {
                
                return (List<Drink>)drinks.Where(c => c.mainAlcohol.Id == id).ToList();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Nie znaleziono drinka po Id");
                return new List<Drink>();
            }

        }



    }

}

