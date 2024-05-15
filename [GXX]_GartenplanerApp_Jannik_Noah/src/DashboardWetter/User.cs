﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;

namespace DashboardWetter
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } 
        public string Location { get; set; }

        public User(string name, string password, string location)
        {
            Name = name;
            Password = password;
            Location = location;
        }

        public User() { }

        public string SaveToDB()
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

        public void SaveUser()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO tblUser(Name, Password, Location) VALUES('{Name}', '{Password}', '{Location}');";

                int tmp = command.ExecuteNonQuery();
                ID = GetUserID();
            }
        }
        public void UpdateUser()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"UPDATE tblUser SET Name = {Name}, Password = {Password}, Location = {Location} WHERE id = {ID};";

                int tmp = command.ExecuteNonQuery();
            }
        }

        public int GetUserID()
        {
            int id = 0;
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"SELECT id FROM tblUser where Name = '{Name}'";

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }
                }
            }
            return id;
        }
    }
}
