using DrinkItUp.BusinessLogic.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUp.BusinessLogic
{
    public class DataMenager
    {
        //Postaram sie Wam to wytłumaczyć. Poniżej macie 3 właściwości (Property po angielsku) które na wstępie przyjmują wartość z metody GetList<T>.
        //    List jeszcze nie mieliśmy na zajęciach, ale najprościej to kolekcje. Tak jak naprzykład tablice. List<Drink> to lista obiektów typu drink. Można się odwołać do każdej właściwości takiego
        //    obiektu przechowywanego w liście. Na końcu "Drink.json" to nazwa pliku, tak jak jest w metodzie na dole (string filename)
        public static List<Drink> Drinks { get; set; } = GetList<Drink>("Drink.json");
        public static List<MainAlcohol> MainAlcohols { get; set; } = GetList<MainAlcohol>("MainAlcohol.json");
        public static List<Difficulty> Difficulties { get; set; } = GetList<Difficulty>("Difficulty.json");


        // Metoda która pobiera dane z pliku
        public static List<T> GetList<T>(string fileName)
        {

            // Na początku tworzy zmienną typu string i zapisuje ścieżke pliku. Tutaj użyłem wbudowanych klas w VisualStudio, żeby zawsze pobierało ściężke naszego projektu na początek
            // To jest to Path.Combine(AppDomain.CurrentDomain.BaseDirectory 
            // Później podana jest nazwa folderu. Tutaj "Data" i fileName, który podajemy wyżej.
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

            // Tutaj tworzy zmienną string do której zostanie wczytany cały plik json. itemSerialized bo taka jest konwencja przy używania jsonów. 
            //File.ReadAllText to jest metoda wbudowana w VisualStudio która zczytuje cały plik do stringa. 
            string itemsSerialized = File.ReadAllText(filePath, Encoding.UTF8);
            Console.WriteLine(itemsSerialized);
            //zwracamy Listę. JsonConvert to metoda wbudowana w paczkę Newtonsoft Json, którą musiałem ściągnąć do naszego projektu. 
            //DeserializeObject bierzę nasz tekst i przekształca go na tę listę.
            // ?? new List<T> zwraca nam pustą listę jeśli nic by nie było w naszym pliku. Ale spoko, dodałem już rekordy
            return JsonConvert.DeserializeObject<List<T>>(itemsSerialized) ?? new List<T>();
        }
    }
}
