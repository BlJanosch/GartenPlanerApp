﻿using System;
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
using System.Linq.Expressions;

namespace DashboardWetter
{
    public class Beet
    {
        public int UserID;
        public string Name;
        public int Hoehe;
        public int Breite;

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

        public Beet(int userID, string name, int hoehe, int breite) 
        {
            this.Name = name;
            this.Hoehe = hoehe;
            this.Breite = breite;
            this.plants = new Plant[hoehe * breite];
            this.buttons = new Button[hoehe * breite];
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

            for (int i = 0; i < Hoehe; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(Size);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < Breite; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(Size);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i <= Hoehe; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(Size);
                BeetGrid.RowDefinitions.Add(row);
            }

            for (int col = 0; col < Breite; col++)
            {
                for (int row = 0; row < Hoehe; row++)
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
                        Name = $"Pflanze{col}Pflanze{row}",
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
                    Grid.SetColumn(border, col);
                    Grid.SetRow(border, row);
                    Grid.SetColumn(button, col);
                    Grid.SetRow(button, row);
                    buttons[col+(row*Breite)] = button;
                    Grid.SetColumn(image, col);
                    Grid.SetRow(image, row);
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

            BeetGrid.Width = Breite * Size;
            BeetGrid.Height = Hoehe * Size;  

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
                    int indexCol = Convert.ToInt32(button.Name.Split("Pflanze")[1]);
                    int indexRow = Convert.ToInt32(button.Name.Split("Pflanze")[2]);

                    this.plants[indexCol + (indexRow * Breite)] = AllPlants.Pflanzen[(windowAddPlant.selectedIndex)];
                }
                DrawLabelsPlants();
                UpdateBeet();
            }
        }

        private void DrawLabelsPlants()
        {
            for (int col = 0; col < Breite; col++)
            {
                for (int row = 0; row < Hoehe; row++)
                {
                    if (!(plants[ToLinear(col,row)] == null))
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

                        BeetControl canvas = new BeetControl(plants[col + (row * Breite)].Name, $"/Images/plants/plant{plants[col + (row * Breite)].ID - 1}.png", GetBrushLeft(col, row), GetBrushRight(col, row), GetBrushTop(col, row), GetBrushBottom(col, row))
                        {
                            Width = 80,
                            Height = 80
                        };

                        BeetGrid.Children.Add(canvas);

                        Grid.SetColumn(canvas, col);
                        Grid.SetRow(canvas, row);
                    }
                }
            }
        }

        private int ToLinear(int col, int row)
        {
            return col + row * Breite;
        }

        public Brush GetBrushLeft(int x, int y)
        {
            try
            {
                if (plants[ToLinear(x, y)].schlechteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x - 1, y)].ID)))
                {
                    return Brushes.Red;
                }
                else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x - 1, y)].ID)))
                {
                    return Brushes.Green;
                }
            }
            catch { }
            return Brushes.Black;
        }

        public Brush GetBrushRight(int x, int y)
        {
            try
            {
                if (plants[ToLinear(x, y)].schlechteNachbarn.Contains(plants[ToLinear(x + 1, y)].ID))
                {
                    return Brushes.Red;
                }
                else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x + 1, y)].ID)))
                {
                    return Brushes.Green;
                }
            }
            catch { }
            return Brushes.Black;
        }

        public Brush GetBrushTop(int x, int y)
        {
            try
            {
                if (plants[ToLinear(x, y)].schlechteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x, y-1)].ID)))
                {
                    return Brushes.Red;
                }
                else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x, y-1)].ID)))
                {
                    return Brushes.Green;
                }
            }
            catch { }
            return Brushes.Black;
        }

        public Brush GetBrushBottom(int x, int y)
        {
            try
            {
                if (plants[ToLinear(x, y)].schlechteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x, y+1)].ID)))
                {
                    return Brushes.Red;
                }
                else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x, y+1)].ID)))
                {
                    return Brushes.Green;
                }
            }
            catch { }
            return Brushes.Black;
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
                command.CommandText = $"INSERT INTO tblBeet(UserID, Rows, Columns, Name, Plants) VALUES({UserID}, {Hoehe}, {Breite}, '{Name}', '{Plants}');";

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
