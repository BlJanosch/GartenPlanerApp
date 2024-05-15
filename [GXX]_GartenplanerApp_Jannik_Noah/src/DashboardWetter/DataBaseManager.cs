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

                return plantManager;
            }
        }

        static public List<Beet> GetAllBeete()
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    @"SELECT * FROM tblBeet";

                List<Beet> Beete = new List<Beet>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] Plants = reader.GetString(5).Split(",");
                        Beet beet = new Beet(reader.GetString(4), reader.GetInt32(2), reader.GetInt32(3));
                        Beete.Add(beet);
                    }
                }
                return Beete;

                
            }
        }
    }
}
