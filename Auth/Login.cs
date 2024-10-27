using System;

namespace MainActivity
{
    public class Login: ILogin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void LoggedIn()
        {
            Console.WriteLine("Logged in successfully");
        }
    }
}
