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

namespace DashboardWetter
{
    public class DataBaseManager
    {
        static public List<Plant> GetAllPlants()
        {
            using (SqliteConnection connection =
                new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {

                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText =
                    @"SELECT name FROM tblPflanze";

                List<string> names = new List<string>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        names.Add(name);
                    }
                }

                return names;
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
                        Beet beet = new Beet(reader.GetString(4), reader.GetInt32(2), reader.GetInt32(3));
                        Beete.Add(beet);
                    }
                }
                return Beete;

                CREATE TABLE tblBeet(id integer primary key autoincrement not null, UserID int, Rows int, Columns int, Name text, Plants text);
            }
        }
    }
}
