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

            ScrollViewer scrollViewer = new ScrollViewer();

            Grid BeetGrid = new Grid();

            for (int i = 0; i < Laenge; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(50);
                BeetGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < Breite; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(50);
                BeetGrid.RowDefinitions.Add(row);
            }
            BeetGrid.Width = MainFrame.ActualWidth;
            BeetGrid.Background = Brushes.White;

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
            };

            Grid EditGrid = new Grid();
            EditGrid.Height = MainArea.ActualHeight * 0.1;
            EditGrid.Width = MainFrame.ActualWidth;
            EditGrid.Background = Brushes.Black;
            BeetGrid.Height = MainArea.ActualHeight - nameLabel.Height - EditGrid.Height;

            MainArea.Children.Add(nameLabel);
            scrollViewer.Content = BeetGrid;
            MainArea.Children.Add(scrollViewer);
            MainArea.Children.Add(EditGrid);
        } 
    }
}
