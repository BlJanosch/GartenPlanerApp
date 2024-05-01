using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace DashboardWetter
{
    public class BeetShortInfo
    {
        public Beet beet;
        public StackPanel MainArea;
        public BeetShortInfo(Beet beet, StackPanel mainArea)
        {
            this.beet = beet;
            MainArea = mainArea;
        }
        public Border GetShortInfo()
        {
            Border border = new Border()
            {
                Background = Brushes.Black,
                Opacity = 0.6,
                Width = 225,
                Height = 195,
                CornerRadius = new CornerRadius(20),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(5, 5, 5, 5),
            };

            Grid grid = new Grid()
            {
                Margin = new Thickness(10)
            };

            Label nameLabel = new Label()
            {
                Content = beet.Name,
                FontSize = 20,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            Button infoButton = new Button()
            {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 5, 10, 0),
                Opacity = 0.000001
            };
            infoButton.Click += infoButton_Click;

            Image infoImage = new Image()
            {
                Source = new BitmapImage(new Uri("Images/Info_Icon.png", UriKind.Relative)),
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 5, 0, 0)
            };

            Grid beetViewGrid = new Grid()
            {
                Height = 100,
                Width = 180,
                Background = Brushes.Brown,
                VerticalAlignment= VerticalAlignment.Center,
            };

            for (int i = 0; i < beet.Laenge; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                beetViewGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < beet.Breite; i++)
            {
                RowDefinition row = new RowDefinition();
                beetViewGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < beet.Laenge; i++)
            {
                for (int j = 0; j < beet.Breite; j++)
                {
                    Border Border = new Border()
                    {
                        BorderThickness = new Thickness(1),
                        BorderBrush = Brushes.Black,
                    };
                    Grid.SetColumn(Border, i);
                    Grid.SetRow(Border, j); 
                    beetViewGrid.Children.Add(Border);
                }
            }

            grid.Children.Add(nameLabel);
            grid.Children.Add(infoImage);
            grid.Children.Add(infoButton);
            grid.Children.Add(beetViewGrid);

            border.Child = grid;

            return border;
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            beet.DrawBeet(MainArea);
        }
    }
}
