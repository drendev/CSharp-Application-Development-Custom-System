using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainActivity
{
    public class Main : Login
    {
        public Main(string username, string password) : base(username, password)
        {
            int menuOption = 0;

            do
            {
                Header header = new Header("User Menu");
                Console.WriteLine("Logged in");
                Console.WriteLine();
                Console.WriteLine($"Welcome {username}!");
                Console.WriteLine();
                Console.WriteLine("1. Change Password");
                Console.WriteLine("2. Change Email");
                Console.WriteLine("3. View My Logs");
                Console.WriteLine("4. Logout");
                Console.Write("Choose an option: ");

                if (int.TryParse(Console.ReadLine(), out menuOption))
                {
                    if (menuOption >= 1 && menuOption <= 4)
                    {
                        UserMenu userMenu = new UserMenu(menuOption, username, password);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid option. Please try again.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (menuOption != 4);



        }
    }
}
