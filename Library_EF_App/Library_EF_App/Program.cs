using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_EF_App.Persistence;
using Library_EF_App.Persistence.Repositories;
using Library_EF_App.Core.Domain;

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


    // Uppdatering: Kanske kommit på ett sätt att lösa ovan. Se början på metod nedan som heter subFind, där jag tänker mig skapa
    // lämplig repository baserat på vilket val man gör i första menyn (dvs vad värdet på "key" är). Skapar nya problem dock.


    /*
     Yoooooooooooo
     Snyggt jobbat. Klockrent att du använt EF. Det börjar likna något nu :D

    UnitOfWork och EF
    Snyggt att du gjort en egen abstraction över EFs egen UnitOfWork, dvs ditt LibraryContext.
    Kolla vad folk säger om att abstrahera bort EFs egen UnitOfWork med sitt eget. Det finns både för och nackdelar.
    Efs DbContext klass är en UoW och implementerar redan repository-pattern. 
    Det du har absolut inte gjort fel genom att lägga ett eget repository-pattern över EFs eget men läs lite om det för att få mer förståelse.
    En anledning att inte göra det är att det tar väldigt mkt tid att skapa klasser som wrappar EF istället för att bara använda dem rakt av.

    Sen, iom att du har abstraherat in EF i din egna Uow, då ska EF stanna där nere. dvs, inget förutom
    din egen UoW ska veta om att EF finns och all kommunikation med EF går via din UoW. Inkapsling av beroenden.

    Sen när du väl ska använda din UoW, 
    låt säga att du ska visa böcker

    public static void ShowBooks(UnitOfWork uow)
    {
        var books = uow.Books.GetAll();
        ...
    }

    Alla dina repon går via din UnitOfWork. 
         
     */

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork())
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
                                // Find
                                break;
                            case "2":
                                // Add
                                Console.Clear();
                                SubAdd(key, unitOfWork);
                                Console.ReadLine();
                                break;
                            case "3":
                                // Update
                                break;
                            case "4":
                                // Remove
                                break;
                            default:
                                Console.WriteLine("Invalid command");
                                Console.ReadLine();
                                break;
                        }
                    } while (key2 != "9");

                } while (key != "9"); // TODO - Exit-funktionen fungerar inte ordentligt
            }
        }

        public static Dictionary<int, string> choices = new Dictionary<int, string>()
        {
            {1, "user"},
            {2, "loan"},
            {3, "author"},
            {4, "book"}
        };

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

        //public static void SubFind(string key, UnitOfWork unitOfWork)
        //{
        //    //använd ditt UoW direkt.. dvs unitOfWork.Books.GetAll(); etc

        //    // User
        //    if (key == "1")
        //        var findUsers = unitOfWork.Users.Find();
        //        // var repository = new UserRepository(unitOfWork); 
        //    // Ska mina repositories ta en UnitOfWork i sin konstruktor istället för en LibraryContext?
        //    // Här tänker du fel. UnitOfWorken innehåller ju ditt repo, inte tvärtom. ditt repo använder EfContext men du kapslar in den i din UoW.


        //    // Loan
        //    if (key == "2")
        //        var repository = new OrderRepository(unitOfWork);
        //    // Author

        //    // Book

        //}

        public static void SubAdd(string key, UnitOfWork unitOfWork)
        {
            // User
            if (key == "1")
            {
                Console.WriteLine("Add user");
                Console.WriteLine();
                Console.WriteLine("Firstname: ");
                var firstName = Console.ReadLine();
                Console.WriteLine("Lastname: ");
                var lastName = Console.ReadLine();

                var user = new User(firstName, lastName);

                unitOfWork.Users.Add(user);
                unitOfWork.Complete();
            }

            // Loan
            if (key == "2")
            {
                Console.WriteLine("Add loan");
                Console.WriteLine();
                Console.WriteLine("Id of user loaning the book: ");
                string userId = Console.ReadLine();

                if (unitOfWork.Orders.Any(Int32.TryParse(userId, out int id))) ;

            }


            // Author
            // Book
        }

    }
}
