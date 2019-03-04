using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_EF_App
{
    // Till Mack10izzle. Löste problemet som jag hade i UI:n, men du får gärna kolla ändå.
    // Har använt mycket exempel från kursen i EF för att bygga allt som har med databasen att göra,
    // samt repositories osv. Så har inte järnkoll på allt i projektet.


    // Dock, nytt problem: hur använder jag mig av UnitOfWork och minimerar koden på bästa sätt? Tanken är att jag vill kunna 
    // använda metoderna i mina repositories för att sen spara till databasen, men jag vill inte göra en switch/case för varje
    // repository. Dvs:
    // if (key == 1)
    //      switch/case med metoder för UserRepository
    // if (key == 2)
    //      switch/case med metoder för OrderRepository
    // osv....
    //
    // Tänkte lösa det här med att använda mig av en submeny (DisplaySubmenu()) och sen försöka göra en generell switch/case,
    // men vet inte hur jag ska lösa det.

    public class Program
    {
        public static void Main(string[] args)
        {
            string key;

            do
            {
                Console.Clear();
                DisplayMenu();
                key = Console.ReadLine();

                string key2;
                do
                {
                    Console.Clear();
                    DisplaySubmenu(key);
                    key2 = Console.ReadLine();

                    switch (key2)
                    {
                        case "1":
                            // Metod
                            break;
                        case "2":
                            // Metod
                            break;
                        case "3":
                            // Metod
                            break;
                        case "4":
                            // Metod
                            break;
                        default:
                            Console.WriteLine("Invalid command");
                            Console.ReadLine();
                            break;
                    }
                } while (key2 != "9");

            } while (key != "9");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("The Library App");
            Console.WriteLine();
            Console.WriteLine("Manage:");
            Console.WriteLine("\t1. Users");
            Console.WriteLine("\t2. Loans");
            Console.WriteLine("\t3. Authors");
            Console.WriteLine("\t4. Books");
            Console.WriteLine("\t9. Exit");
        }

        public static Dictionary<int, string> choices = new Dictionary<int, string>()
        {
            {1, "user"},
            {2, "loan"},
            {3, "author"},
            {4, "book"}
        };

        public static void DisplaySubmenu(string key)
        {
            Console.WriteLine($"Manage {choices[Convert.ToInt32(key)]}s");
            Console.WriteLine();
            Console.WriteLine($"\t1. Find {choices[Convert.ToInt32(key)]}");
            Console.WriteLine($"\t2. Add {choices[Convert.ToInt32(key)]}");
            Console.WriteLine($"\t3. Update {choices[Convert.ToInt32(key)]}");
            Console.WriteLine($"\t4. Remove {choices[Convert.ToInt32(key)]}");
            Console.WriteLine("\t9. Exit");
        }


    }
}
