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

        public ScrollViewer DrawBeet(StackPanel MainArea)
        {
            /* Zoom Funktion einbauen
             */
            MainArea.Children.Clear();

            ScrollViewer scrollViewer = new ScrollViewer();

            Grid BeetGrid = new Grid();
            BeetGrid.Width = MainArea.ActualWidth;
            BeetGrid.Background = Brushes.White;

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
            EditGrid.Width = MainArea.ActualWidth;
            EditGrid.Background = Brushes.Black;
            BeetGrid.Height = MainArea.ActualHeight - nameLabel.Height - EditGrid.Height;

            MainArea.Children.Add(nameLabel);
            scrollViewer.Content = BeetGrid;
            MainArea.Children.Add(scrollViewer);
            MainArea.Children.Add(EditGrid);
            return scrollViewer;
        } 
    }
}
