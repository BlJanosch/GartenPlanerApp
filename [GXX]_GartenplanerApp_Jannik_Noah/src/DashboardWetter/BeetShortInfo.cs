using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Syncfusion.UI.Xaml.ProgressBar;
using System.Windows.Threading;

namespace DashboardWetter
{
    public class BeetShortInfo
    {
        public Beet beet;
        public StackPanel MainArea;
        public Frame MainFrame;
        private string CurrentMode;
        private Grid grid;
        public SfCircularProgressBar circularWater;
        public DispatcherTimer timer_Uhr = new DispatcherTimer();
        public BeetShortInfo(Beet beet, StackPanel mainArea, Frame frame)
        {
            this.beet = beet;
            this.MainArea = mainArea;
            this.MainFrame = frame;
        }
        public Border GetShortInfo()
        {
            CurrentMode = "Beet";
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

            grid = new Grid()
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

            Button changeButton = new Button()
            {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10, 5, 0, 0),
                Opacity = 0.000001
            };
            changeButton.Click += ChangeButton_Click;

            Image infoImage = new Image()
            {
                Source = new BitmapImage(new Uri("Images/Info_Icon.png", UriKind.Relative)),
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 5, 0, 0)
            };

            Image changeImage = new Image()
            {
                Source = new BitmapImage(new Uri("Images/Change.png", UriKind.Relative)),
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10, 5, 0, 0)
            };


            grid.Children.Add(nameLabel);
            grid.Children.Add(infoImage);
            grid.Children.Add(infoButton);
            grid.Children.Add(changeImage);
            grid.Children.Add(changeButton);


            grid.Children.Add(DrawBeet());


            border.Child = grid;

            timer_Uhr.Interval = TimeSpan.FromMilliseconds(10);
            timer_Uhr.Tick += Timer_Uhr_Tick; ;
            timer_Uhr.Start();

            return border;
        }

        private void Timer_Uhr_Tick(object? sender, EventArgs e)
        {
            try
            {
                beet.TimeDifference = beet.LastTimeWatered.AddHours(beet.BewässerungsInterval) - DateTime.Now;
                circularWater.Progress = beet.TimeDifference.TotalHours / beet.BewässerungsInterval * 100;
            }
            catch { }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMode == "Beet")
            {
                grid.Children.RemoveAt(grid.Children.Count - 1);
                grid.Children.Add(DrawChemie());
                CurrentMode = "Chemie";
            }
            else if (CurrentMode == "Chemie")
            {
                grid.Children.RemoveAt(grid.Children.Count - 1);
                grid.Children.Add(DrawWater());
                CurrentMode = "Water";
            }
            else if (CurrentMode == "Water")
            {
                grid.Children.RemoveAt(grid.Children.Count - 1);
                grid.Children.Add(DrawBeet());
                CurrentMode = "Beet";
            }
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            beet.DrawBeet(MainArea, MainFrame);
        }

        private Grid DrawBeet()
        {
            Grid beetViewGrid = new Grid()
            {
                Height = 100,
                Width = 180,
                Background = Brushes.Brown,
                VerticalAlignment = VerticalAlignment.Center,
            };

            for (int i = 0; i < beet.Breite; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                beetViewGrid.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < beet.Hoehe; i++)
            {
                RowDefinition row = new RowDefinition();
                beetViewGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < beet.Breite; i++)
            {
                for (int j = 0; j < beet.Hoehe; j++)
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
            return beetViewGrid;
        }

        private SfCircularProgressBar DrawChemie()
        {
            SfCircularProgressBar circular = new SfCircularProgressBar();
            circular.Progress = beet.Chemie;
            circular.Width = 180;
            circular.Maximum = 100;
            circular.ProgressColor = Brushes.White;
            circular.TrackColor = Brushes.DarkGray;
            circular.FontFamily = new FontFamily("Ahorni");
            circular.FontWeight = FontWeights.Bold;
            circular.HorizontalAlignment = HorizontalAlignment.Center;
            circular.VerticalAlignment = VerticalAlignment.Center;
            circular.Foreground = Brushes.White;
            circular.FontSize = 20;
            RangeColorCollection rangeColors = new RangeColorCollection();
            rangeColors.Add(new RangeColor() { IsGradient = true, Color = Colors.Red, Start = 0, End = 25 });
            rangeColors.Add(new RangeColor() { IsGradient = true, Color = Colors.Orange, Start = 25, End = 50 });
            rangeColors.Add(new RangeColor() { IsGradient = true, Color = Colors.LightGreen, Start = 50, End = 75 });
            rangeColors.Add(new RangeColor() { Color = Colors.DarkGreen, Start = 75, End = 100 });
            circular.RangeColors = rangeColors;
            return circular;
        }
        private SfCircularProgressBar DrawWater()
        {
            circularWater = new SfCircularProgressBar();
            circularWater.Progress = beet.TimeDifference.TotalHours / beet.BewässerungsInterval * 100;
            circularWater.Width = 180;
            circularWater.Maximum = 100;
            circularWater.ProgressColor = Brushes.White;
            circularWater.TrackColor = Brushes.DarkGray;
            circularWater.FontFamily = new FontFamily("Ahorni");
            circularWater.FontWeight = FontWeights.Bold;
            circularWater.HorizontalAlignment = HorizontalAlignment.Center;
            circularWater.VerticalAlignment = VerticalAlignment.Center;
            circularWater.Foreground = Brushes.White;
            circularWater.FontSize = 20;
            RangeColorCollection rangeColors = new RangeColorCollection();
            rangeColors.Add(new RangeColor() { IsGradient = true, Color = Colors.LightSkyBlue, Start = 0, End = 25 });
            rangeColors.Add(new RangeColor() { IsGradient = true, Color = Colors.LightBlue, Start = 25, End = 50 });
            rangeColors.Add(new RangeColor() { IsGradient = true, Color = Colors.Blue, Start = 50, End = 75 });
            rangeColors.Add(new RangeColor() { Color = Colors.DarkBlue, Start = 75, End = 100 });
            circularWater.RangeColors = rangeColors;
            return circularWater;
        }
    }
}
