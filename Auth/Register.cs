using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using BCrypt.Net;

namespace MainActivity
{
    public class Register : IRegister
    {
        // Properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        // Constructor
        public Register(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        // Registered notification
        public void Registered()
        {
            Console.WriteLine($"User {Username} registered successfully!");
        }

        // Save Information to json file
        public async Task SaveToFile(string jsonPath)
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
                users = new List<Register>();
            }

            // Add the new user and serialize the list
            users.Add(this);
            string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(jsonPath, jsonString);
        }
    }
}
