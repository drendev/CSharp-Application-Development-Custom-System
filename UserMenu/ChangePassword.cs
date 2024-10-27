using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainActivity
{
    public class ChangePassword : IChangePassword
    {
        public string NewPassword { get; set; }
        public string Username { get; set; }

        // Constructor
        public ChangePassword(string newPassword, string username)
        {
            NewPassword = newPassword;
            Username = username;
        }

        public void PasswordChanged()
        {
            Console.WriteLine("Password changed");
        }

        public async Task EditPassword(string jsonPath)
        {
            List<Register> users;

            if (File.Exists(jsonPath))
            {
                // Read and deserialize existing data
                var existingData = await File.ReadAllTextAsync(jsonPath);
                users = JsonSerializer.Deserialize<List<Register>>(existingData) ?? new List<Register>();
            }
            else
            {
                Console.WriteLine("Error: User data file not found.");
                return;
            }

            // Find the user by Username
            var user = users.Find(u => u.Username == Username);
            if (user != null)
            {
                // Update the user's password (e.g., hash it before saving)
                user.Password = NewPassword;

                // Serialize the updated list back to the JSON file
                string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(jsonPath, jsonString);

            }
            else
            {
                Console.WriteLine($"Error: User {Username} not found.");
            }
        }
    }
}
