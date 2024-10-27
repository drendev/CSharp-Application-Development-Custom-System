using System;

namespace MainActivity
{
    public class Menu 
    {
        public Menu(int menuOption)
        {
            switch (menuOption)
            {
                case 1:
                    Console.Clear();
                    RegisterService register = new RegisterService();
                    register.RegisterUserAsync().Wait();
                    break;
                case 2:
                    Console.Clear();
                    LoginService login = new LoginService();
                    login.LoginUserAsync().Wait();
                    break;
                case 3:
                    Console.WriteLine("Exit");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}
