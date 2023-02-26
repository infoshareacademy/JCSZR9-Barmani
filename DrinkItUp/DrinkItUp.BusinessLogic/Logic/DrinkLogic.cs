using DrinkItUp.BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic.Logic
{
    public class DrinkLogic
    {
        // Pobieramy sobie listę z DataMenagera jak prywatne pole wewnątrz klasy, żeby za każdym razem nie odczytywać pliku.
        // Można by było zawsze się odwoływać do DataMenager.Drinks , ale to zła praktyka, przy większym projekcie masakra
        private static List<Drink>? _drinks = DataMenager.Drinks;


        
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
                Console.WriteLine($"{GetById(1).Name}");
                return GetById(1);
            }

        }


    }

}

