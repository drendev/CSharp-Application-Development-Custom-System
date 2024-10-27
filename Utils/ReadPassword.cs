using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainActivity
{
    public static class ReadPassword
    {
        public static string Hash()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b"); // Removes the last asterisk from the console
                    password.Length--; // Removes the last character from the password
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); // Move to the next line after password input
            return password.ToString();
        }
    }
}
