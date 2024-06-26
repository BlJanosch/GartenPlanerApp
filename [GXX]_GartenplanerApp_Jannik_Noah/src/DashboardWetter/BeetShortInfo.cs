﻿using System;
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
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using System.Numerics;

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
        private BeeteManager beeteManager;
        private PageBeeteMenu pageBeeteMenu;

        private Button BeetBestellenButton;
        private Canvas canvas;
        private Image BestellButtonImage;

        public void CreateItems()
        {
            BeetBestellenButton = new Button()
            {
                Height = 20,
                Width = 20,
                Background = Brushes.Transparent,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(10, 0, 0, 5),
                Opacity = 0.00001,
            };
            BeetBestellenButton.Click += BeetBestellenButton_Click;

            canvas = new Canvas()
            {
                Height = 20,
                Width = 20,
                Background = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(10, 0, 0, 5),
                Opacity = 1
            };

            BestellButtonImage = new Image()
            {
                Source = new BitmapImage(new Uri("Images/wagen.png", UriKind.Relative)),
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(10, 0, 0, 5)
            };
            Loggerclass.log.Information("Items wurde erstellt in CreatItems() --> BeetShortInfo.cs");
        }


        public BeetShortInfo(Beet beet, StackPanel mainArea, Frame frame, BeeteManager beeteManager, PageBeeteMenu pageBeeteMenu)
        {
            this.pageBeeteMenu = pageBeeteMenu;
            this.beet = beet;
            this.MainArea = mainArea;
            this.MainFrame = frame;
            this.beeteManager = beeteManager;
            CreateItems();
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

            Button DeleteButton = new Button()
            {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 10, 5),
                Opacity = 0.000001
            };
            DeleteButton.Click += DeleteButton_Click;

            Button BewässerungButton = new Button()
            {
                Height = 20,
                Width = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(10, 0, 0, 5),
                Opacity = 0.000001
            };
            BewässerungButton.Click += BewässerungButton_Click;

            

            Image BewässerungImage = new Image()
            {
                Source = new BitmapImage(new Uri("Images/WaterDrop.png", UriKind.Relative)),
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(10, 0, 0, 5)
            };

            Image DeleteImage = new Image()
            {
                Source = new BitmapImage(new Uri("Images/Delete.png", UriKind.Relative)),
                Height = 20,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 10, 5)
            };

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
            grid.Children.Add(DeleteImage);
            grid.Children.Add(DeleteButton);
            grid.Children.Add(BewässerungImage);
            grid.Children.Add(BewässerungButton);
            grid.Children.Add(canvas);
            grid.Children.Add(BestellButtonImage);
            grid.Children.Add(BeetBestellenButton);




            grid.Children.Add(DrawBeet());


            border.Child = grid;

            timer_Uhr.Interval = TimeSpan.FromMilliseconds(10);
            timer_Uhr.Tick += Timer_Uhr_Tick; ;
            timer_Uhr.Start();

            Loggerclass.log.Information("GetShortInfo() in BeetShortInfo wurde erfolgreich ausgeführt.");
            return border;
        }

        private void BeetBestellenButton_Click(object sender, RoutedEventArgs e)
        {
            WindowBestellen windowBestellen = new WindowBestellen(beet.Name, beet.plants);
            windowBestellen.ShowDialog();
        }

        private void BewässerungButton_Click(object sender, RoutedEventArgs e)
        {
            WindowBewässern windowBewässern = new WindowBewässern(beet.BewässerungsInterval, beet.LastTimeWatered);
            windowBewässern.ShowDialog();

            if (windowBewässern.DialogResult == true)
            { 
                using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
                {
                    connection.Open();

                    SqliteCommand command = connection.CreateCommand();

                    beet.LastTimeWatered = DateTime.Now;

                    command.CommandText = $"UPDATE tblBeet SET LetztesMalBewässert = '{beet.LastTimeWatered}' WHERE Name = '{beet.Name}' AND UserID = {beet.UserID};";

                    int tmp = command.ExecuteNonQuery();
                }
                Loggerclass.log.Information($"Beet {beet.Name} wurde erfolgreich bewässert und die DB aktualisiert");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            foreach (Beet beet in this.beeteManager.Beete)
            {
                if (beet.Name == this.beet.Name && beet.UserID == this.beet.UserID)
                {
                    this.beeteManager.Beete.Remove(beet);
                    break;
                }
            }
            

            using (SqliteConnection connection = new SqliteConnection("Data Source=Assets/GartenPlaner.db"))
            {
                connection.Open();

                SqliteCommand command = connection.CreateCommand();

                command.CommandText = $"DELETE FROM tblBeet WHERE Name = '{this.beet.Name}' AND UserID = {this.beet.UserID};";

                int tmp = command.ExecuteNonQuery();
            }

            Loggerclass.log.Information($"Beet {this.beet.Name} wurde erfolgreich gelöscht.");

            pageBeeteMenu.DrawBeeteMenu();

        }

        private void Timer_Uhr_Tick(object? sender, EventArgs e)
        {
            try
            {
                beet.TimeDifference = beet.LastTimeWatered.AddHours(beet.BewässerungsInterval) - DateTime.Now;
                double WaterResult = beet.TimeDifference.TotalHours / beet.BewässerungsInterval * 100;
                if (WaterResult <= 0)
                {
                    circularWater.Progress = 0;
                }
                else
                {
                    circularWater.Progress = WaterResult;
                }
            }
            catch { }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMode == "Beet")
            {
                grid.Children.Remove(BeetBestellenButton);
                grid.Children.Remove(BestellButtonImage);
                grid.Children.Remove(canvas);
                grid.Children.RemoveAt(grid.Children.Count - 1);
                grid.Children.Add(DrawChemie());
                CurrentMode = "Chemie";
                Loggerclass.log.Information($"Ansicht von Beet wurde auf folgende Ansicht geändert: {CurrentMode}");

            }
            else if (CurrentMode == "Chemie")
            {
                grid.Children.RemoveAt(grid.Children.Count - 1);
                grid.Children.Add(DrawWater());
                CurrentMode = "Water";
                Loggerclass.log.Information($"Ansicht von Beet wurde auf folgende Ansicht geändert: {CurrentMode}");
            }
            else if (CurrentMode == "Water")
            {
                grid.Children.RemoveAt(grid.Children.Count - 1);
                grid.Children.Add(DrawBeet());
                CurrentMode = "Beet";
                grid.Children.Add(canvas);
                grid.Children.Add(BestellButtonImage);
                grid.Children.Add(BeetBestellenButton);
                Loggerclass.log.Information($"Ansicht von Beet wurde auf folgende Ansicht geändert: {CurrentMode}");
            }
        }

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            beet.DrawBeet(MainArea);
            beet.MainArea = MainArea;
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
            Loggerclass.log.Information($"Detailierte Ansicht von Beet {beet.Name} wurde gezeichnet");
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
            double WaterResult = beet.TimeDifference.TotalHours / beet.BewässerungsInterval * 100;
            if (WaterResult <= 0)
            {
                circularWater.Progress = 0;
            }
            else
            {
                circularWater.Progress = WaterResult;
            }
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
