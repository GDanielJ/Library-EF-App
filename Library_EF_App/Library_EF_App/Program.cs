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

            } while (key != "9");
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("The Library App");
            Console.WriteLine();
            Console.WriteLine("Manage:");
            Console.WriteLine("1. users");
            Console.WriteLine("2. loans");
            Console.WriteLine("3. authors");
            Console.WriteLine("4. books");
        }
    }
}
