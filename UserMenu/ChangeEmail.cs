using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainActivity
{
    public class ChangeEmail: IChangeEmail
    {
        public string NewEmail { get; set; }
        public string Username { get; set; }

        public ChangeEmail(string newEmail, string username)
        {
            NewEmail = newEmail;
            Username = username;
        }

        public void EmailChanged()
        {
            Console.WriteLine("Email has been changed to: " + NewEmail);
        }

        public async Task EditEmail(string jsonPath)
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
                user.Email = NewEmail;

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
