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
            /* Zoom Funktion einbauen
             */
            MainArea.Children.Clear();
            int margin = 0;
            if (Laenge % 2 == 0)
            {
                margin = 10;
            }
            else
            {
                margin = 35;
            }

            Grid BeetGrid = new Grid()
            {
                
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            for (int i = 0; i < (14-Laenge)/2; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(50);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < Laenge; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(50);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(50);
            BeetGrid.RowDefinitions.Add(row1);
            for (int i = 0; i < Breite; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(50);
                BeetGrid.RowDefinitions.Add(row);
            }
            BeetGrid.Width = MainFrame.ActualWidth - margin * 2;
            BeetGrid.Background = Brushes.White;

            for (int x = ((14 - Laenge) / 2); x < (Laenge+((14 - Laenge) / 2)); x++)
            {
                for (int y = 1; y < Breite+1; y++)
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
            };


            BeetGrid.Height = MainArea.ActualHeight - nameLabel.Height;

            MainArea.Children.Add(nameLabel);
            
            MainArea.Children.Add(BeetGrid);
            
        } 
    }
}
