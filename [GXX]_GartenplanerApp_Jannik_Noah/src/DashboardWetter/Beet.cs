using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DashboardWetter
{
    public class Beet
    {
        public int ID;
        public int UserID;
        public string Name;
        public int Hoehe;
        public int Breite;
        private double GoodConections = 0;
        private double BadConnections = 0;
        public double Chemie;
        public int BewässerungsInterval;
        public DateTime LastTimeWatered;
        public TimeSpan TimeDifference;
        public StackPanel MainArea;

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

        public Beet(int ID, int userID, string name, int hoehe, int breite, double goodConnections, double badConnections, int bewässerungsInterval, DateTime lastTimeWatered) 
        {
            this.ID = ID;
            this.Name = name;
            this.Hoehe = hoehe;
            this.Breite = breite;
            this.plants = new Plant[hoehe * breite];
            this.buttons = new Button[hoehe * breite];
            this.UserID = userID;
            this.GoodConections = goodConnections;
            this.BadConnections = badConnections;
            this.BewässerungsInterval = bewässerungsInterval;
            this.LastTimeWatered = lastTimeWatered;
        }

        public void DrawBeet(StackPanel MainArea)
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
                if (windowAddPlant.RadioButtonEinFeld.IsChecked == true)
                {
                    if (windowAddPlant.selectedIndex != -1)
                    {
                        int indexCol = Convert.ToInt32(button.Name.Split("Pflanze")[1]);
                        int indexRow = Convert.ToInt32(button.Name.Split("Pflanze")[2]);

                        this.plants[indexCol + (indexRow * Breite)] = AllPlants.Pflanzen[(windowAddPlant.selectedIndex)];
                    }
                }
                else if (windowAddPlant.RadioButtonGanzeReihe.IsChecked == true)
                {
                    if (windowAddPlant.selectedIndex != -1)
                    {
                        int indexRow = Convert.ToInt32(button.Name.Split("Pflanze")[2]);

                        for (int i = 0; i<Breite; i++)
                        {
                            this.plants[i+(indexRow*Breite)] = AllPlants.Pflanzen[(windowAddPlant.selectedIndex)];
                        }
                    }
                }
                else if (windowAddPlant.RadioButtonGanzeSpalte.IsChecked == true)
                {
                    if (windowAddPlant.selectedIndex != -1)
                    {
                        int indexCol = Convert.ToInt32(button.Name.Split("Pflanze")[1]);
                        int indexRow = Convert.ToInt32(button.Name.Split("Pflanze")[2]);

                        for (int i = 0; i<(Hoehe); i+=1)
                        {
                            this.plants[indexCol + (Breite*i)] = AllPlants.Pflanzen[(windowAddPlant.selectedIndex)];
                        }
                    }
                }
                DrawLabelsPlants();
                UpdateBeet();
                Chemie = GetChemie();
            }
        }

        public void DeleteElement(int col, int row)
        {
            /*
            Panel parentPanel = (Panel)this.Parent;
            parentPanel.Children.Remove(this);
            this.beet.plants[col + (row * this.beet.Breite)] = null;
            this.beet.DrawLabelsPlants();
            this.beet.UpdateBeet();
            */


            
            this.plants[ToLinear(col, row)] = null;
            this.UpdateBeet();
            this.DrawBeet(MainArea);
            this.DrawLabelsPlants();
            

        }

        public void DrawLabelsPlants()
        {
            GoodConections = 0;
            BadConnections = 0;
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

                        BeetControl canvas = new BeetControl(this, col, row, plants[col + (row * Breite)].Name, $"/Images/plants/plant{plants[col + (row * Breite)].ID - 1}.png", GetBrushLeft(col, row), GetBrushRight(col, row), GetBrushTop(col, row), GetBrushBottom(col, row))
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
                if (ToLinear(x, y) % Breite != 0)
                {
                    if (plants[ToLinear(x, y)].schlechteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x - 1, y)].ID)))
                    {
                        BadConnections++;
                        return Brushes.Red;
                    }
                    else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x - 1, y)].ID)))
                    {
                        GoodConections++;
                        return Brushes.Green;
                    }
                }
            }
            catch { }
            return Brushes.Black;
        }

        public Brush GetBrushRight(int x, int y)
        {
            try
            {
                if (ToLinear(x, y) % Breite != Breite - 1)
                {
                    if (plants[ToLinear(x, y)].schlechteNachbarn.Contains(plants[ToLinear(x + 1, y)].ID))
                    {
                        BadConnections++;
                        return Brushes.Red;
                    }
                    else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x + 1, y)].ID)))
                    {
                        GoodConections++;
                        return Brushes.Green;
                    }
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
                    BadConnections++;
                    return Brushes.Red;
                }
                else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x, y-1)].ID)))
                {
                    GoodConections++;
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
                    BadConnections++;
                    return Brushes.Red;
                }
                else if (plants[ToLinear(x, y)].guteNachbarn.Contains(Convert.ToChar(plants[ToLinear(x, y+1)].ID)))
                {
                    GoodConections++;
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
                command.CommandText = $"INSERT INTO tblBeet(UserID, Rows, Columns, Name, Plants, guteVerbindungen, schlechteVerbindungen, BewässerungsInterval, LetztesMalBewässert) VALUES({UserID}, {Hoehe}, {Breite}, '{Name}', '{Plants}', {GoodConections}, {BadConnections}, {BewässerungsInterval}, '{LastTimeWatered}');";

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
                command.CommandText = $"UPDATE tblBeet SET Plants = '{Plants}', guteVerbindungen = {GoodConections}, schlechteVerbindungen = {BadConnections}, LetztesMalBewässert = '{LastTimeWatered}' WHERE Name = '{Name}' AND UserID = {UserID};";

                int tmp = command.ExecuteNonQuery();
            }
        }

        public double GetChemie()
        {
            
            double w = 0; 
            double w_max = ((((Breite * 2) - 1) * (Hoehe - 1)) + (Breite - 1)) * 2;

            for (int i= 0; i<BadConnections; i++)
            {
                w += -1;
            }
            for (int i = 0; i<GoodConections; i++)
            {
                w += 1;
            }


                
            double chemieProzent = ((w + w_max) / (2 * w_max)) * 100;
            return chemieProzent;
            // return ((((GoodConections - (BadConnections * 2) + (Max-GoodConections-BadConnections))/ Max * 100) + 400) / 500) * 100;

        }
    }
}
