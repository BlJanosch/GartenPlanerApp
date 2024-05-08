using OpenMeteo;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
            GutenTag.FontSize = 40;
            GutenTag.FontFamily = new System.Windows.Media.FontFamily("Aharoni");
            GutenTag.FontWeight = FontWeights.Bold;
            GutenTag.Foreground = System.Windows.Media.Brushes.White;
            GutenTag.HorizontalAlignment = HorizontalAlignment.Center;
            GutenTag.Margin = new Thickness(0, 20, 0, 0);

            Label UserName = new Label();
            UserName.Content = MainUser.Name;
            UserName.FontSize = 20;
            UserName.FontFamily = new System.Windows.Media.FontFamily("Aharoni");
            UserName.FontWeight = FontWeights.Bold;
            UserName.Foreground = System.Windows.Media.Brushes.White;
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

            Grid subGrid = new Grid();
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

            Label Zurzeit = new Label() { Content = "Zurzeit", FontSize = 15, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center };
            Label Ganztaegig = new Label() { Content = "Standort", FontSize = 15, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetColumn(Ganztaegig, 1);

            TemperaturNow = new Label() { Name = "TemperaturNowLabel", FontSize = 12, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, Content = "Temperatur", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(TemperaturNow, 1);
            RegenNow = new Label() { Name = "RegenNowLabel", FontSize = 12, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, Content = "Regen", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(RegenNow, 2);
            SchneeNow = new Label() { Name = "SchneeNowLabel", FontSize = 12, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, Content = "Schnee", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(SchneeNow, 3);
            WolkenNow = new Label() { Name = "WolkenNowLabel", FontSize = 12, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, Content = "Wolken", HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(WolkenNow, 4);

            CurrentLocation = new Label() { Name = "TemperaturDailyLabel", FontSize = 12, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, Content = MainUser.Location, HorizontalAlignment = HorizontalAlignment.Center };
            Grid.SetRow(CurrentLocation, 1);
            Grid.SetColumn(CurrentLocation, 1);


            subGrid.Children.Add(Zurzeit);
            subGrid.Children.Add(Ganztaegig);
            subGrid.Children.Add(TemperaturNow);
            subGrid.Children.Add(RegenNow);
            subGrid.Children.Add(SchneeNow);
            subGrid.Children.Add(WolkenNow);
            subGrid.Children.Add(CurrentLocation);

            innerGrid.Children.Add(subGrid);

            UhrDashBoard = new Label() { Name = "UhrDashBoard", FontSize = 15, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Right, Content = "00:00", VerticalAlignment = VerticalAlignment.Center };
            Label WetterDaten = new Label() { FontSize = 20, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, Content = "Wetter Daten" };
            WetterDashBoard = new Label() { Name = "WetterDashBoard", VerticalContentAlignment = VerticalAlignment.Center, FontSize = 15, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = "Wetter" };

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

            Label label1 = new Label() { Content = "Chemie", FontSize = 20, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            Ellipse ellipse1 = new Ellipse() { Height = 120, Width = 120, StrokeThickness = 2, Stroke = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Ellipse ellipse2 = new Ellipse() { Height = 100, Width = 100, StrokeThickness = 2, Stroke = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };

            Label label2 = new Label() { Name = "ChemieLabel", Content = "N/A", FontSize = 20, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };

            innerGrid1.Children.Add(label1);
            innerGrid1.Children.Add(ellipse1);
            innerGrid1.Children.Add(ellipse2);
            innerGrid1.Children.Add(label2);

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

            Label label3 = new Label() { Content = "Wasser", FontSize = 20, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            Ellipse ellipse3 = new Ellipse() { Height = 120, Width = 120, StrokeThickness = 2, Stroke = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Ellipse ellipse4 = new Ellipse() { Height = 100, Width = 100, StrokeThickness = 2, Stroke = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };

            Label label4 = new Label() { Name = "WasserLabel", Content = "N/A", FontSize = 20, FontFamily = new FontFamily("Aharoni"), FontWeight = FontWeights.Bold, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };

            innerGrid2.Children.Add(label3);
            innerGrid2.Children.Add(ellipse3);
            innerGrid2.Children.Add(ellipse4);
            innerGrid2.Children.Add(label4);

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
        }

        private async void getWeather()
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
    }
}
