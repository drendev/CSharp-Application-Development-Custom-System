using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainActivity
{
    public class UserMenu
    {
        public UserMenu(int menuOption, string username, string password)
        {
            switch (menuOption)
            {
                case 1:
                    Console.Clear();
                    ChangePasswordService changePassword = new ChangePasswordService();
                    changePassword.ChangePasswordAsync(username, password).Wait();
                    break;
                case 2:
                    Console.Clear();
                    ChangeEmailService changeEmail = new ChangeEmailService();
                    changeEmail.ChangeEmailAsync(username).Wait();
                    break;
                case 3:
                    Console.Clear();
                    ViewMyLogs viewMyLogs = new ViewMyLogs();
                    viewMyLogs.LogsAsync(username).Wait();
                    break;
                case 4:
                    Console.Clear();
                    Header header = new Header("Main Menu");
                    Console.WriteLine("Logged out");
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}
