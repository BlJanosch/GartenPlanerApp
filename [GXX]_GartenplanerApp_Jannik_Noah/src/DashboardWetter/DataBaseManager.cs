using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;
using System.Xml.Linq;
using System.Data;

namespace DashboardWetter
{
    public class DataBaseManager
    {
        static public PlantManager GetAllPlants()
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    @"SELECT * FROM tblPflanze";

                PlantManager plantManager = new PlantManager();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Plant plant = new Plant(reader.GetInt16(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11), reader.GetString(12), reader.GetString(13));
                        plantManager.Pflanzen.Add(plant);
                    }
                }
                Loggerclass.log.Information($"Alle Pflanzen konnten erfolgreich abgerufen werden.");
                return plantManager;
            }
        }

        static public List<Beet> GetAllBeete(User user)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM tblBeet";

                List<Beet> Beete = new List<Beet>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (user.ID == reader.GetInt32(1))
                        {
                            string[] Plants = reader.GetString(5).Split(",");
                            PlantManager plantManager = GetAllPlants();

                            Beet beet = new Beet(reader.GetInt16(0), reader.GetInt16(1), reader.GetString(4), reader.GetInt32(2), reader.GetInt32(3), reader.GetDouble(6), reader.GetDouble(7), reader.GetInt16(8), Convert.ToDateTime(reader.GetString(9)));
                            beet.Chemie = beet.GetChemie();
                            int counter = 0;
                            foreach (string element in Plants)
                            {
                                if (element != "x")
                                {
                                    beet.plants[counter] = GetRigthPlant(element);
                                }
                                counter++;
                            }
                            Beete.Add(beet);
                        }
                    }
                }
                return Beete;
            }
        }

        static private Plant GetRigthPlant(string PlantID)
        {
            PlantManager AllPlants = GetAllPlants();
            foreach (Plant plant in AllPlants.Pflanzen)
            {
                if (PlantID == Convert.ToString(plant.ID))
                {
                    return plant;
                }
            }
            Loggerclass.log.Information("Pflanze konnte nicht in der DB gefunden werden!");
            throw new Exception("Diese Pflanze konnt nicht gefunden werden!");
        }

        static public User GetUser(string name, string password)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM tblUser";

                User user = new User();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(1) == name && reader.GetString(2) == User.PasswordToHash(password))
                        {
                            user = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            user.GetUserID();
                            return user;
                        }
                    }
                }
                Loggerclass.log.Information("Benutzer konnte in der DB nicht gefunden werden!");
                throw new Exception("Benutzer konnte in der DB nicht gefunden werden!");
            }
        }

        static public List<User> GetAllUser()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = @"SELECT * FROM tblUser";

                List<User> Users = new List<User>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        user.GetUserID();
                        Users.Add(user);
                    }
                }
                Loggerclass.log.Information("Alle Benutzer konnten abgerufen werden.");
                return Users;
            }
        }
    }
}