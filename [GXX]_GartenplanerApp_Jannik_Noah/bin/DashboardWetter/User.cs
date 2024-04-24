using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DashboardWetter
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; private set; } 
        public string Location { get; set; }

        public User(string name, string password, string location)
        {
            Name = name;
            Password = password;
            Location = location;
        }

        public User() { }

        public string Searlized()
        {
            return $"{Name};{Password};{Location}";
        }

        public static string PasswordToHash(string Password)
        {

            byte[] inputBytes = Encoding.UTF8.GetBytes(Password);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                // Gib den Hash-Wert aus
                string hashString = builder.ToString();
                return hashString;
            }
        }
    }
}
