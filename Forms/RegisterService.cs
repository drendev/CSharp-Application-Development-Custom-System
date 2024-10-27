using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MainActivity
{
    public class RegisterService
    {
        string UsersFilePath = EnvFilePath.path;

        public async Task RegisterUserAsync()
        {
            Header Header = new Header("Register");
            HandlePaassword Handle = new HandlePaassword();
            DateTime now = DateTime.Now;
            string dateTimeString = now.ToString("yyyy-MM-dd HH:mm");

            var existingUsernames = await LoadExistingUsernamesAsync();

            string username;
            // Validates username length, empty input and uniqueness
            do
            {
                Console.WriteLine("Enter a new username:");
                username = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                }
                else if (username.Length < 5)
                {
                    Console.WriteLine("Username must be at least 5 characters long. Please try again.");
                }
                else if (existingUsernames.Contains(username))
                {
                    Console.WriteLine("Username already exists. Please try again.");
                }
            } while(string.IsNullOrWhiteSpace(username) || username.Length < 5 || existingUsernames.Contains(username));

            string password;
            // Validate password length and empty input
            do
            {
                Console.WriteLine("Enter a new password:");
                password = ReadPassword.Hash();

                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                }
                else if (password.Length < 8)
                {
                    Console.WriteLine("Password must be at least 8 characters long. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(password) || password.Length < 8);

            string confirmPassword;
            // Validates confirm password matching
            do
            {
                Console.WriteLine("Confirm password:");
                confirmPassword = ReadPassword.Hash();

                if (password != confirmPassword)
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                }

            } while (password != confirmPassword);

            
            string email;
            // Validates email format
            do
            {
                Console.WriteLine("Enter your email:");
                email = Console.ReadLine();

                if (!IsValidEmail(email))
                {
                    Console.WriteLine("Invalid email format. Please try again.");
                }
            } while (!IsValidEmail(email));

            try
            {
                // Hashpassword safely
                password = Handle.Hash(password);

                // Save registered log
                var log = new LogsService(username, "Registered", dateTimeString);
                await log.SaveToFile(EnvFilePath.logs);

                var user = new Register(username, password, email);
                
                Console.Clear();
                user.Registered();
                await user.SaveToFile(EnvFilePath.path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        // Load existing usernames from the json file
        private async Task<HashSet<string>> LoadExistingUsernamesAsync()
        {
            if (!File.Exists(UsersFilePath))
                return new HashSet<string>();

            var usersData = await File.ReadAllTextAsync(UsersFilePath);
            var users = JsonSerializer.Deserialize<List<Register>>(usersData) ?? new List<Register>();

            return users.Select(u => u.Username).ToHashSet();
        }

        // Validate email
        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
