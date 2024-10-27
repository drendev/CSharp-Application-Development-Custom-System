using System;

namespace MainActivity
{
    public class ChangePasswordService
    {
        string userFilePath = EnvFilePath.path;

        public async Task ChangePasswordAsync(string username, string password)
        {
            HandlePaassword Handle = new HandlePaassword();
            Header Header = new Header("Change Password");

            DateTime now = DateTime.Now;
            string dateTimeString = now.ToString("yyyy-MM-dd HH:mm");

            string oldPassword = password;
            string newPassword = string.Empty;
            string reNewPassword = string.Empty;

            string oldPasswordInput;

            // Step 1: Validate old password
            do
            {
                Console.WriteLine("Enter old password: ");
                oldPasswordInput = ReadPassword.Hash();

                if (oldPasswordInput != oldPassword)
                {
                    Console.WriteLine("Incorrect password. Please try again.");
                }
            } while (oldPasswordInput != oldPassword);

            // Step 2: Enter new password and validate it
            do
            {
                Console.WriteLine("Enter new password (at least 8 characters long): ");
                newPassword = ReadPassword.Hash();

                if (newPassword.Length < 8)
                {
                    Console.WriteLine("Password must be at least 8 characters long. Please try again.");
                    continue;
                }

                Console.WriteLine("Re-enter new password: ");
                reNewPassword = ReadPassword.Hash();

                if (newPassword != reNewPassword)
                {
                    Console.WriteLine("Passwords do not match. Please try again.");
                }

                if (string.IsNullOrWhiteSpace(reNewPassword))
                {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                }

            } while (newPassword.Length < 8 || newPassword != reNewPassword);

            try
            {
                // Hashpassword safely
                password = Handle.Hash(newPassword);

                // Save change password log
                var log = new LogsService(username, "Changed Password", dateTimeString);
                await log.SaveToFile(EnvFilePath.logs);

                var user = new ChangePassword(password, username);
                Console.Clear();
                user.PasswordChanged();
                await user.EditPassword(EnvFilePath.path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
    }
}
