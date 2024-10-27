using System;
using BCrypt.Net;

namespace MainActivity
{
    public class HandlePaassword
    {
        // Hash password using BCrypt
        public string Hash(string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            return hash;
        }
    }
}
