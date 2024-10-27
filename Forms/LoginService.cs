using System;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Linq;

namespace MainActivity
{
    public class LoginService
    {
        string UsersFilePath = EnvFilePath.path;

        public async Task LoginUserAsync()
        {
            Header Header = new Header("Log in");
            DateTime now = DateTime.Now;
            string dateTimeString = now.ToString("yyyy-MM-dd HH:mm");

            if (!File.Exists(UsersFilePath))
            {
                Console.WriteLine("User data file not found.");
            }

            var usersData = await File.ReadAllTextAsync(UsersFilePath);
            var users = JsonSerializer.Deserialize<List<Register>>(usersData) ?? new List<Register>();

            bool isAuthenticated = false;

            // Main loop for login attempts
            while (!isAuthenticated)
            {

                Console.WriteLine("Enter your username:");
                string username = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                string password = ReadPassword.Hash();

                // Check if username and password match an existing user
                var user = users.FirstOrDefault(u => u.Username == username);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    // Save login log
                    var log = new LogsService(username, "Logged in", dateTimeString);
                    await log.SaveToFile(EnvFilePath.logs);

                    Console.Clear();
                    isAuthenticated = true;
                    Main userMenu = new Main(username, password);
                }
                else
                {
                    Console.WriteLine("Invalid username or password. Please try again.");
                }
            }

        }
    }
}
