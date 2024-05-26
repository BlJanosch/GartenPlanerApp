using OpenMeteo;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.UI.Xaml.ProgressBar;
using System.Windows.Threading;

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for PageHome.xaml
    /// </summary>
    public partial class PageHome : Page
    {

        public DispatcherTimer timer_Uhr = new DispatcherTimer();
        public Label TemperaturNow;
        public Label RegenNow;
        public Label SchneeNow;
        public Label WolkenNow;
        public Label CurrentLocation;
        public User MainUser;
        public Label WetterDashBoard;
        public Label UhrDashBoard;
        public string Wetter;
        public SfCircularProgressBar circularWater;
        public SfCircularProgressBar circularChemie;
        public Grid subGrid;
        public bool WaterWarningSet = false;
        public bool ChemieWarningSet = false;

        public PageHome(User MainUser)
        {
            InitializeComponent();
            this.MainUser = MainUser;
        }

        public PageHome()
        {
            InitializeComponent();
        }

        public void DrawHome()
        {

            MainArea.Children.Clear();
            MainArea.Background = Brushes.Transparent;

            Label GutenTag = new Label();
            GutenTag.Content = "GUTEN TAG";
            GutenTag.Style = Styles.GetFontStyle(40);
            GutenTag.HorizontalAlignment = HorizontalAlignment.Center;
            GutenTag.Margin = new Thickness(0, 20, 0, 0);

            Label UserName = new Label();
            UserName.Content = MainUser.Name;
            UserName.Style = Styles.GetFontStyle(20);
            UserName.HorizontalAlignment = HorizontalAlignment.Center;

            Image WeatherImage = new Image();
            WeatherImage.Source = new BitmapImage(new Uri("Images/Wetter.png", UriKind.Relative));
            WeatherImage.Width = 100;
            WeatherImage.Height = 100;

            MainArea.Children.Add(GutenTag);
            MainArea.Children.Add(UserName);
            MainArea.Children.Add(WeatherImage);

            Border border = new Border();
            border.CornerRadius = new CornerRadius(10);
            border.BorderThickness = new Thickness(2, 1, 2, 2);
            border.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            border.Height = 165;
            border.Width = 600;
            border.Opacity = 0.8;
            border.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            border.Margin = new Thickness(0, 10, 0, 0);

            Grid innerGrid = new Grid();

            innerGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            innerGrid.RowDefinitions.Add(new RowDefinition());

            subGrid = new Grid();
            Grid.SetRow(subGrid, 1);

            subGrid.ColumnDefinitions.Add(new ColumnDefinition());
            subGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(1.4, GridUnitType.Star);
                    subGrid.RowDefinitions.Add(row);
                }
                RowDefinition row2 = new RowDefinition();
                row2.Height = GridLength.Auto;
                subGrid.RowDefinitions.Add(row2);
            }

            Label Zurzeit = new Label() { Content = "Zurzeit", Style = Styles.GetFontStyle(15), HorizontalAlignment = HorizontalAlignment.Center };
            Label Ganztaegig = new Label() { Content = "Standort", Style = Styles.GetFontStyle(15), HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetColumn(Ganztaegig, 1);
            Label Warnings = new Label() { Content = "Warnungen", Style = Styles.GetFontStyle(15), HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(Warnings, 2);
            Grid.SetColumn(Warnings, 1);

            TemperaturNow = new Label() { Name = "TemperaturNowLabel", Style = Styles.GetFontStyle(12), Content = "Temperatur", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(TemperaturNow, 1);
            RegenNow = new Label() { Name = "RegenNowLabel", Style = Styles.GetFontStyle(12), Content = "Regen", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(RegenNow, 2);
            SchneeNow = new Label() { Name = "SchneeNowLabel", Style = Styles.GetFontStyle(12), Content = "Schnee", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(SchneeNow, 3);
            WolkenNow = new Label() { Name = "WolkenNowLabel", Style = Styles.GetFontStyle(12), Content = "Wolken", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(WolkenNow, 4);

            CurrentLocation = new Label() { Name = "TemperaturDailyLabel", Style = Styles.GetFontStyle(12), Content = MainUser.Location, HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(CurrentLocation, 1);
            Grid.SetColumn(CurrentLocation, 1);


            subGrid.Children.Add(Zurzeit);
            subGrid.Children.Add(Ganztaegig);
            subGrid.Children.Add(TemperaturNow);
            subGrid.Children.Add(RegenNow);
            subGrid.Children.Add(SchneeNow);
            subGrid.Children.Add(WolkenNow);
            subGrid.Children.Add(CurrentLocation);
            subGrid.Children.Add(Warnings);

            innerGrid.Children.Add(subGrid);

            UhrDashBoard = new Label() { Name = "UhrDashBoard", Style = Styles.GetFontStyle(15), HorizontalAlignment = HorizontalAlignment.Right, Content = "00:00", VerticalAlignment = VerticalAlignment.Center };
            Label WetterDaten = new Label() { Style = Styles.GetFontStyle(20), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Content = "Wetter Daten" };
            WetterDashBoard = new Label() { Name = "WetterDashBoard", VerticalContentAlignment = VerticalAlignment.Center, Style = Styles.GetFontStyle(15), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = "Wetter" };

            innerGrid.Children.Add(UhrDashBoard);
            innerGrid.Children.Add(WetterDaten);
            innerGrid.Children.Add(WetterDashBoard);

            border.Child = innerGrid;

            MainArea.Children.Add(border);

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.HorizontalAlignment = HorizontalAlignment.Center;

            Border border1 = new Border();
            border1.CornerRadius = new CornerRadius(10);
            border1.BorderThickness = new Thickness(2, 1, 2, 2);
            border1.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            border1.Height = 150;
            border1.Width = 290;
            border1.Opacity = 0.8;
            border1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            border1.Margin = new Thickness(0, 10, 10, 0);

            Grid innerGrid1 = new Grid();

            Label label1 = new Label() { Content = "Chemie", Style = Styles.GetFontStyle(20), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            circularChemie = new SfCircularProgressBar();
            circularChemie.Progress = CalculateChemie();
            circularChemie.Width = 347;
            circularChemie.ProgressColor = Brushes.White;
            circularChemie.TrackColor = Brushes.DarkGray;
            circularChemie.FontFamily = new FontFamily("Ahorni");
            circularChemie.FontWeight = FontWeights.Bold;
            circularChemie.HorizontalAlignment = HorizontalAlignment.Center;
            circularChemie.VerticalAlignment = VerticalAlignment.Center;
            circularChemie.Foreground = Brushes.White;
            circularChemie.FontSize = 20;
            RangeColorCollection rangeColorsChemie = new RangeColorCollection();
            rangeColorsChemie.Add(new RangeColor() { IsGradient = true, Color = Colors.Red, Start = 0, End = 25 });
            rangeColorsChemie.Add(new RangeColor() { IsGradient = true, Color = Colors.Orange, Start = 25, End = 50 });
            rangeColorsChemie.Add(new RangeColor() { IsGradient = true, Color = Colors.LightGreen, Start = 50, End = 75 });
            rangeColorsChemie.Add(new RangeColor() { Color = Colors.DarkGreen, Start = 75, End = 100 });
            circularChemie.RangeColors = rangeColorsChemie;

            innerGrid1.Children.Add(label1);
            innerGrid1.Children.Add(circularChemie);

            border1.Child = innerGrid1;

            stackPanel.Children.Add(border1);

            Border border2 = new Border();
            border2.CornerRadius = new CornerRadius(10);
            border2.BorderThickness = new Thickness(2, 1, 2, 2);
            border2.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            border2.Height = 150;
            border2.Width = 290;
            border2.Opacity = 0.8;
            border2.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            border2.Margin = new Thickness(10, 10, 0, 0);

            Grid innerGrid2 = new Grid();

            Label label3 = new Label() { Content = "Wasser", Style = Styles.GetFontStyle(20), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            circularWater = new SfCircularProgressBar();
            circularWater.Progress = CalculateWater();
            circularWater.Width = 347;
            circularWater.ProgressColor = Brushes.White;
            circularWater.TrackColor = Brushes.DarkGray;
            circularWater.FontFamily = new FontFamily("Ahorni");
            circularWater.FontWeight = FontWeights.Bold;
            circularWater.HorizontalAlignment = HorizontalAlignment.Center;
            circularWater.VerticalAlignment = VerticalAlignment.Center;
            circularWater.Foreground = Brushes.White;
            circularWater.FontSize = 20;
            RangeColorCollection rangeColorsWater = new RangeColorCollection();
            rangeColorsWater.Add(new RangeColor() { IsGradient = true, Color = Colors.LightSkyBlue, Start = 0, End = 25 });
            rangeColorsWater.Add(new RangeColor() { IsGradient = true, Color = Colors.LightBlue, Start = 25, End = 50 });
            rangeColorsWater.Add(new RangeColor() { IsGradient = true, Color = Colors.Blue, Start = 50, End = 75 });
            rangeColorsWater.Add(new RangeColor() { Color = Colors.DarkBlue, Start = 75, End = 100 });
            circularWater.RangeColors = rangeColorsWater;

            innerGrid2.Children.Add(label3);
            innerGrid2.Children.Add(circularWater);


            border2.Child = innerGrid2;

            stackPanel.Children.Add(border2);

            MainArea.Children.Add(stackPanel);

            timer_Uhr.Stop();
            timer_Uhr.Interval = TimeSpan.FromMilliseconds(10);
            timer_Uhr.Tick += timer_Uhr_Tick;
            timer_Uhr.Start();
            Wetter = "Location needed";
            WetterDashBoard.Content = Wetter;
            getWeather();
        }
        private void timer_Uhr_Tick(object? sender, EventArgs e)
        {
            UhrDashBoard.Content = DateTime.Now.ToString("hh:mm:ss");
            circularWater.Progress = CalculateWater();
            circularChemie.Progress = CalculateChemie();
            if (CalculateChemie() < 50 && !ChemieWarningSet)
            {
                Label Warning = new Label() { Style = Styles.GetFontStyle(12), Content = "Chemie Stand unter 50% gefallen!", HorizontalAlignment = HorizontalAlignment.Center };
                Grid.SetRow(Warning, subGrid.Children.Count - 5);
                Grid.SetColumn(Warning, 1);
                subGrid.Children.Add(Warning);
                ChemieWarningSet = true;
            }
            else if (CalculateWater() < 50 && !WaterWarningSet)
            {
                Label Warning = new Label() { Style = Styles.GetFontStyle(12), Content = "Wasser Stand unter 50% gefallen!", HorizontalAlignment = HorizontalAlignment.Center };
                Grid.SetRow(Warning, subGrid.Children.Count - 5);
                Grid.SetColumn(Warning, 1);
                subGrid.Children.Add(Warning);
                WaterWarningSet = true;
            }
            else if (CalculateChemie() > 50 && ChemieWarningSet)
            {
                subGrid.Children.RemoveAt(subGrid.Children.Count - 1);
                subGrid.Children.RemoveAt(subGrid.Children.Count - 2);
                ChemieWarningSet = false;
                WaterWarningSet = false;
            }
            else if (CalculateWater() > 50 && WaterWarningSet)
            {
                subGrid.Children.RemoveAt(subGrid.Children.Count - 1);
                subGrid.Children.RemoveAt(subGrid.Children.Count - 2);
                ChemieWarningSet = false;
                WaterWarningSet = false;
            }
        }

        private async void getWeather()
        {
            try
            {
                OpenMeteo.OpenMeteoClient client = new OpenMeteo.OpenMeteoClient();
                WeatherForecast forecast = await client.QueryAsync(MainUser.Location);

                int id = Convert.ToInt32(forecast.Current.Weathercode);
                WetterDashBoard.Content = client.WeathercodeToString(id);
                TemperaturNow.Content = $"Temperatur {forecast.Current.Temperature} C°";
                RegenNow.Content = $"Regen {forecast.Current.Rain} %";
                SchneeNow.Content = $"Schnee {forecast.Current.Snowfall} %";
                WolkenNow.Content = $"Wolken {forecast.Current.Cloudcover} %";
            }
            catch
            {
                WetterDashBoard.Content = "N/A";
                TemperaturNow.Content = "N/A";
                RegenNow.Content = "N/A";
                SchneeNow.Content = "N/A";
                WolkenNow.Content = "N/A";
            }
        }

        private double CalculateWater()
        {
            double sum = 0;
            List<Beet> Beete = DataBaseManager.GetAllBeete(MainUser);
            foreach (Beet beet in Beete)
            {
                beet.TimeDifference = beet.LastTimeWatered.AddHours(beet.BewässerungsInterval) - DateTime.Now; ;
                sum += beet.TimeDifference.TotalHours / beet.BewässerungsInterval * 100;
            }
            return sum/Beete.Count;
        }

        private double CalculateChemie()
        {
            double sum = 0;
            List<Beet> Beete = DataBaseManager.GetAllBeete(MainUser);
            foreach (Beet beet in Beete)
            {
                sum += beet.Chemie;
            }
            return sum / Beete.Count;
        }
    }
}
