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

namespace DashboardWetter
{
    public class Beet
    {
        public string Name;
        public int Breite;
        public int Laenge;

        // Diese Array wird mit Länge mal Breite Feldern erstellt. Die erste Pflanze im Array ist
        // links oben. Die zweite Pflanze im Array ist im zweiten Feld von oben links. Es geht
        // also immer eine Reihe sozusagen durch und dann geht es in die nächste Reihe.
        public string[] plants;

        public Beet(string name, int breite, int laenge) 
        {
            this.Name = name;
            this.Breite = breite;
            this.Laenge = laenge;
            this.plants = new string[breite * laenge];
        }

        public void DrawBeet(StackPanel MainArea, Frame MainFrame)
        {
            MainArea.Children.Clear();
            double Size = 80;

            Grid BeetGrid = new Grid()
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

            for (int x = 0; x < Laenge; x++)
            {
                for (int y = 0; y < Breite; y++)
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
                        Name = $"Pflanze{x+(y*Breite)}",
                    };

                    button.Click += Button_Click;
                    Image image = new Image();
                    if (plants[x*y] == null)
                    {
                        image = new Image()
                        {
                            Source = new BitmapImage(new Uri("Images/Plus.png", UriKind.Relative)),
                            Height = 50,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,

                        };
                    }
                    Grid.SetColumn(border, x);
                    Grid.SetRow(border, y);
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
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
            
            MainArea.Children.Add(BeetGrid);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            
            WindowAddPlant windowAddPlant = new WindowAddPlant(button.Name);
            windowAddPlant.ShowDialog();
        }

    }
}
