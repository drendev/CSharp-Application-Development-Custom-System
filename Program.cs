using System;

namespace MainActivity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Header header = new Header("Main Menu");
            int menuOption = 0;

            do
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                if (int.TryParse(Console.ReadLine(), out menuOption))
                {
                    if (menuOption >= 1 && menuOption <= 3)
                    {
                        Menu menu = new Menu(menuOption);
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

            } while (menuOption != 3);
        }
    }
}
