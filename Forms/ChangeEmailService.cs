using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace MainActivity
{
    public class ChangeEmailService
    {
        string userFilePath = EnvFilePath.path;

        public async Task ChangeEmailAsync(string username)
        {
            Header Header = new Header("Change Email");

            DateTime now = DateTime.Now;
            string dateTimeString = now.ToString("yyyy-MM-dd HH:mm");

            string newEmail;

            // Validate Email
            do
            {
                Console.WriteLine("Enter new Email");
                newEmail = Console.ReadLine();

                if (!IsValidEmail(newEmail))
                {
                    Console.WriteLine("Email not valid.");
                }
                else if (string.IsNullOrEmpty(newEmail))
                {
                    Console.WriteLine("Email is empty.");
                }

            } while (!IsValidEmail(newEmail));

            try
            {
                // Save change email log
                var log = new LogsService(username, $"Changed email to {newEmail}", dateTimeString);
                await log.SaveToFile(EnvFilePath.logs);

                var user = new ChangeEmail(newEmail, username);
                Console.Clear();
                user.EmailChanged();
                await user.EditEmail(EnvFilePath.path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
