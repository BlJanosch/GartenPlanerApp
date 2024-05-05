using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

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
            //BeetGrid.Background = Brushes.Transparent;

            for (int x = 0; x < Laenge; x++)
            {
                for (int y = 0; y < Breite; y++)
                {
                    Border border = new Border()
                    {
                        BorderThickness = new Thickness(2),
                        BorderBrush = Brushes.Black
                    };
                    Grid.SetColumn(border, x);
                    Grid.SetRow(border, y);
                    BeetGrid.Children.Add(border);
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
    }
}
