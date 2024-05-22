using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Data.Sqlite;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace DashboardWetter
{
    public class Beet
    {
        public int UserID;
        public string Name;
        public int Breite;
        public int Laenge;

        public static PlantManager AllPlants = DataBaseManager.GetAllPlants();

        // Diese Array wird mit Länge mal Breite Feldern erstellt. Die erste Pflanze im Array ist
        // links oben. Die zweite Pflanze im Array ist im zweiten Feld von oben links. Es geht
        // also immer eine Reihe sozusagen durch und dann geht es in die nächste Reihe.
        public Plant[] plants;
        public Button[] buttons;

        Grid BeetGrid = new Grid()
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Background = Brushes.Transparent,
            Margin = new Thickness(0, 10, 0, 10),
        };

        public Beet(int userID, string name, int breite, int laenge) 
        {
            this.Name = name;
            this.Breite = breite;
            this.Laenge = laenge;
            this.plants = new Plant[breite * laenge];
            this.buttons = new Button[breite * laenge];
            this.UserID = userID;
        }

        public void DrawBeet(StackPanel MainArea, Frame MainFrame)
        {
            MainArea.Children.Clear();
            double Size = 80;

            BeetGrid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Transparent,
                Margin = new Thickness(0, 10, 0, 10),
            };

            for (int i = 0; i < Breite; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(Size);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < Laenge; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(Size);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i <= Breite; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(Size);
                BeetGrid.RowDefinitions.Add(row);
            }

            for (int y = 0; y < Laenge; y++)
            {
                for (int x = 0; x < Breite; x++)
                {
                    Border border = new Border()
                    {
                        BorderThickness = new Thickness(2),
                        BorderBrush = Brushes.Black,
                        Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d251e")),
                    };
                    Button button = new Button()
                    {
                        Opacity = 0.000001,
                        Name = $"Pflanze{x}Pflanze{y}",
                    };

                    button.Click += Button_Click;
                    Image image = new Image();

                    image = new Image()
                    {
                        Source = new BitmapImage(new Uri("Images/Plus.png", UriKind.Relative)),
                        Height = 50,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,

                    };
                    Grid.SetColumn(border, y);
                    Grid.SetRow(border, x);
                    Grid.SetColumn(button, y);
                    Grid.SetRow(button, x);
                    buttons[x+(y*Breite)] = button;
                    Grid.SetColumn(image, y);
                    Grid.SetRow(image, x);
                    BeetGrid.Children.Add(border);
                    BeetGrid.Children.Add(image);
                    BeetGrid.Children.Add(button);
                }
            }
            

            Label nameLabel = new Label()
            {
                Content = Name,
                FontSize = 40,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 55,
                Margin = new Thickness(0, 10, 0, 0),
            };

            BeetGrid.Width = Laenge * Size;
            BeetGrid.Height = Breite * Size;  

            MainArea.Children.Add(nameLabel);
            if ((MainArea.Children.Contains(BeetGrid) == false))
            { 
                MainArea.Children.Add(BeetGrid);
            }
            DrawLabelsPlants();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            
            WindowAddPlant windowAddPlant = new WindowAddPlant(button.Name);
            windowAddPlant.ShowDialog();
            if (windowAddPlant.DialogResult == true)
            {
                if (windowAddPlant.selectedIndex != -1)
                {
                    int indexX = Convert.ToInt32(button.Name.Split("Pflanze")[1]);
                    int indexY = Convert.ToInt32(button.Name.Split("Pflanze")[2]);

                    this.plants[indexX + (indexY * Breite)] = AllPlants.Pflanzen[(windowAddPlant.selectedIndex)];
                }
                DrawLabelsPlants();
                UpdateBeet();
            }
        }

        private void DrawLabelsPlants()
        {
            for (int x = 0; x < Breite; x++)
            {
                for (int y = 0; y < Laenge; y++)
                {
                    if (!(plants[x+(y*Breite)] == null))
                    {
                        /*Canvas canvas = new Canvas()
                        {
                            Height = 80,
                            Width = 80,
                            Background = Brushes.White,
                        };
                        Label labelName = new Label()
                        {
                            Content = plants[x+(y*Breite)].Name,
                        };
                        Image image = new Image()
                        {

                            Height = 80,
                            Width = 80,
                            
                        };

                        image.Source = new BitmapImage(new Uri($"/Images/plants/plant{plants[x + (y * Breite)].ID-1}.png", UriKind.Relative));

                        Canvas.SetTop(image, 10);
                        Canvas.SetLeft(image, 0);

                        canvas.Children.Add(image);
                        canvas.Children.Add(labelName);
                        
                        BeetGrid.Children.Add(canvas);*/

                        BeetControl canvas = new BeetControl(plants[x + (y * Breite)].Name, $"/Images/plants/plant{plants[x + (y * Breite)].ID - 1}.jpg")
                        {
                            Width = 80,
                            Height = 80
                        };

                        BeetGrid.Children.Add(canvas);

                        Grid.SetColumn(canvas, y);
                        Grid.SetRow(canvas, x);
                    }
                }
            }
        }


        public void SaveBeet()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {
                connection.Open();
                // Planzen richt Laden --> Problem

                SqliteCommand command = connection.CreateCommand();

                string Plants = "";
                int counter = 0;
                foreach (Plant plant in this.plants)
                {
                    if (counter == 0)
                    {
                        Plants += "x";

                    }
                    else
                    {
                        Plants += ",x";
                    }
                    counter++;
                }
                command.CommandText = $"INSERT INTO tblBeet(UserID, Rows, Columns, Name, Plants) VALUES({UserID}, {Breite}, {Laenge}, '{Name}', '{Plants}');";

                int tmp = command.ExecuteNonQuery();
            }
        }

        public void UpdateBeet()
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                string Plants = "";
                int counter = 0;
                foreach (Plant plant in this.plants)
                {
                    if (counter == 0)
                    {
                        if (plant != null)
                        {
                            Plants += plant.ID;
                        }
                        else
                        {
                            Plants += "x";
                        }
                    }
                    else
                    {
                        if (plant != null)
                        {
                            Plants += $",{plant.ID}";
                        }
                        else { Plants += ",x"; }
                    }
                    counter++;
                }
                command.CommandText = $"UPDATE tblBeet SET Plants = '{Plants}' WHERE Name = '{Name}' AND UserID = {UserID};";

                int tmp = command.ExecuteNonQuery();
            }
        }
    }
}
