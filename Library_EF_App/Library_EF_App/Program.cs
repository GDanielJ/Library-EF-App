using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_EF_App
{
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
            Console.WriteLine($"Manage choices[Convert.ToInt32(key)]s");
        }


    }
}
