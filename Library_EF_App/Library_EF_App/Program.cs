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
    // Ny kommentar till Macke:
    // Se metoden nedan från rad 184 ("SubAdd"). Metoden är ett utkast och lite klottrig. Tanken är att jag ska kunna skapa ett lån (order) av en bok eller flera böcker (book).
    // Det finns en many-to-many-relation mellan klasserna order och book. Ett lån ska kunna ha flera böcker, och samtidigt ska man 
    // kunna låna en bok flera gånger. I databasen finns en kopplingstabell "OrdersBooks" med OrderId och BookId.  
    
    // Hur lägger jag till ett lån till mitt context? Jag skulle vilja skicka in en lista med BookId:n, så att kopplingen skapas i OrderBooks,
    // men fattar inte hur jag ska göra det. Jag vill ju inte heller skapa massa böcker i metoden, för de ska redan finnas i databasen, och
    // och vill man lägga till böcker så får man göra det via en annan metod.


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
                                Console.Clear();
                                SubFind(key, unitOfWork);
                                Console.ReadLine();
                                break;
                            case "2":
                                // Add
                                Console.Clear();
                                SubAdd(key, unitOfWork);
                                Console.ReadLine();
                                break;
                            case "3":
                                // Update
                                Console.Clear();
                                SubUpdate(key, unitOfWork);
                                Console.ReadLine();
                                break;
                            case "4":
                                // Remove
                                Console.Clear();
                                SubRemove(key, unitOfWork);
                                Console.ReadLine();
                                break;
                            default:
                                Console.WriteLine("Invalid command");
                                Console.ReadLine();
                                break;
                        }
                    } while (key2 != "9");

                } while (key != "9"); // TODO - Exit-funktionen fungerar inte ordentligt. Lägg till case "9" : break;
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

         public static void SubFind(string key, UnitOfWork unitOfWork)
         {
            // User
            if (key == "1")
            {
                var users = unitOfWork.Users.GetAll();
                Console.WriteLine("Users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"Id: {user.Id}, Firstname: {user.Firstname}, Lastname: {user.Lastname}");
                }
            }

            // Loan (order)
            if (key == "2")
            {
                var loans = unitOfWork.Orders.GetAll();
                Console.WriteLine("Loans");
                foreach (var loan in loans)
                {
                    var loanWithUser = unitOfWork.Orders.GetOrderWithUser(loan.Id);
                    Console.WriteLine($"Loan Id: {loan.Id}, Checkout date: {loan.CheckOutDate}, " +
                        $"Return date: {loan.ReturnDate}, User Id: {loanWithUser.User.Id}, " +
                        $"User name: {loan.User.Firstname} {loan.User.Lastname}");
                }
            }

            // Author
            if (key == "3")
            {
                var authors = unitOfWork.Authors.GetAll();
                Console.WriteLine("Authors:");
                foreach (var author in authors)
                {
                    Console.WriteLine($"Id: {author.Id}, Firstname: {author.Firstname}, Lastname: {author.Lastname}");
                }
            }

            //Book
            if (key == "4") // TODO - Snygga till denna vid tillfälle
            {
                var books = unitOfWork.Books.GetAll();
                Console.WriteLine("Books:");

                foreach (var book in books)
                {
                    var bookWithAuthor = unitOfWork.Books.GetBookWithAuthor(book.Id);
                    Console.WriteLine($"Id: {book.Id}, Book: {book.Name}, Author: {bookWithAuthor.Author.Firstname} {bookWithAuthor.Author.Lastname}");
                }
            }
         }

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

            //Loan
            if (key == "2")
            {
                Console.WriteLine("Add loan");
                Console.WriteLine();
                Console.WriteLine("Id of user loaning the book: ");
                string userId = Console.ReadLine();
                int choice = Convert.ToInt32(userId);

                if (unitOfWork.Orders.Any(o => o.UserId == choice))
                {
                    Order loan = new Order(DateTime.Now, DateTime.Now.AddDays(30), Convert.ToInt32(userId));
                    while (true)
                    {
                        Console.WriteLine("Enter Id of book to be loaned. Press \"d\" when done.");
                        string key2 = Console.ReadLine();
                        if (key2 == "d" || key2 == "D")
                            break;
                        else if (Int32.TryParse(key2, out int numValue) && unitOfWork.Books.Any(b => b.Id == numValue))
                        {
                            Book book = unitOfWork.Books.Get(Convert.ToInt32(key2));
                            loan.Books.Add(book);
                        }
                    }
                    unitOfWork.Orders.Add(loan);
                    unitOfWork.Complete();
                }
                else
                {
                    Console.WriteLine($"User with Id {userId} does not exist.");
                }
            }

            // Author
            if (key == "3")
            {
                Console.WriteLine("Add book");
                Console.WriteLine();
                Console.WriteLine("Firstname: ");
                var firstName = Console.ReadLine();
                Console.WriteLine("Lastname: ");
                var lastName = Console.ReadLine();

                var author = new Author(firstName, lastName);
                unitOfWork.Authors.Add(author);
                unitOfWork.Complete();
            }

            // Book
            if (key == "4")
            {
                Console.WriteLine("Add book");
                Console.WriteLine();
                Console.WriteLine("Name of book:");
                string name = Console.ReadLine();

                var book = new Book();

                while (true)
                {
                    Console.WriteLine("Id of author");
                    string authorId = Console.ReadLine();

                    if (Int32.TryParse(authorId, out int numValue2) && unitOfWork.Authors.Any(b => b.Id == numValue2))
                    {
                        book.Name = name;
                        book.AuthorId = Convert.ToInt32(authorId);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Author with Id {authorId} does not exist.");
                    }
                }
                unitOfWork.Books.Add(book);
                unitOfWork.Complete();
            }
         }

        public static void SubUpdate(string key, UnitOfWork unitOfWork)
        {
            // User
            if (key == "1")
            {
                Console.WriteLine("Update user");
                Console.WriteLine();
                Console.WriteLine("Id of user to be updated:");
                string userId = Console.ReadLine();
                int choice = Convert.ToInt32(userId);

                if (unitOfWork.Users.Any(u => u.Id == choice))
                {
                    var user = unitOfWork.Users.Get(choice);
                    Console.WriteLine($"{user.Firstname} {user.Lastname}");

                    Console.WriteLine("New firstname:");
                    string newFirstname = Console.ReadLine();
                    Console.WriteLine("New lastname:");
                    string newLastname = Console.ReadLine();

                    user.Firstname = newFirstname;
                    user.Lastname = newLastname;

                    unitOfWork.Complete();
                }
                else
                {
                    Console.WriteLine($"User with Id {userId} does not exist.");
                }
            }

            // Loan
            if (key == "2")
            {
                Console.WriteLine("Method not yet implemented.");
            }

            // Author
            if (key == "3")
            {
                Console.WriteLine("Update author");
                Console.WriteLine();
                Console.WriteLine("Id of author to be updated:");
                string authorId = Console.ReadLine();
                int choice = Convert.ToInt32(authorId);

                if (unitOfWork.Authors.Any(a => a.Id == choice))
                {
                    var author = unitOfWork.Authors.Get(choice);
                    Console.WriteLine($"{author.Firstname} {author.Lastname}");

                    Console.WriteLine("New firstname:");
                    string newFirstname = Console.ReadLine();
                    Console.WriteLine("New lastname:");
                    string newLastname = Console.ReadLine();

                    author.Firstname = newFirstname;
                    author.Lastname = newLastname;

                    unitOfWork.Complete();

                }
                else
                {
                    Console.WriteLine($"Author with Id {choice} does not exist.");
                }
            }

            // Book
            if (key == "4")
            {
                Console.WriteLine("Update book");
                Console.WriteLine();
                Console.WriteLine("Id of book to be updated:");
                string bookId = Console.ReadLine();
                int choice = Convert.ToInt32(bookId);

                if (unitOfWork.Books.Any(a => a.Id == choice))
                {
                    var book = unitOfWork.Books.Get(choice);
                    Console.WriteLine($"Author Id: {book.AuthorId}, Book name: {book.Name}");

                    Console.WriteLine("New name of book:");
                    string newBookName = Console.ReadLine();
                    Console.WriteLine("New Author Id:");
                    string newAuthorId = Console.ReadLine();

                    book.Name = newBookName;
                    book.AuthorId = Convert.ToInt32(newAuthorId);

                    unitOfWork.Complete();
                }
                else
                {
                    Console.WriteLine($"Book with Id {choice} does not exist.");
                }
            }
        }

        public static void SubRemove(string key, UnitOfWork unitOfWork)
        {
            // User
            if (key == "1")
            {
                Console.WriteLine("Remove user");
                Console.WriteLine();
                Console.WriteLine("Id of user to be removed:");
                string userId = Console.ReadLine();
                int choice = Convert.ToInt32(userId);

                if (unitOfWork.Users.Any(a => a.Id == choice))
                {
                    var user = unitOfWork.Users.Get(choice);
                    unitOfWork.Users.Remove(user);
                    unitOfWork.Complete();
                    Console.WriteLine($"User with Id {choice} has been removed.");
                }
                else
                {
                    Console.WriteLine($"User with Id {choice} does not exist.");
                }
            }

            // Loan
            // Author
            // Book
        }
    }
}
