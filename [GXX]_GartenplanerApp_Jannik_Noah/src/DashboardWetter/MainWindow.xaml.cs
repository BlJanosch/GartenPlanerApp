

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
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
using OpenMeteo;

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Wetter;
        public DispatcherTimer timer_Uhr = new DispatcherTimer();
        public Label UhrDashBoard;
        public Label TemperaturNow;
        public Label RegenNow;
        public Label SchneeNow;
        public Label WolkenNow;
        public Label CurrentLocation;
        public Label RegenDaily;
        public Label SchneeDaily;
        public Label WolkenDaily;
        public Label WetterDashBoard;
        public string UserDataFile = AppDomain.CurrentDomain.BaseDirectory.Split("\\bin\\")[0] + "\\UserData\\Login.csv";
        public User MainUser;
        public TextBox UserNameBox;
        public TextBox UserPasswordBox;
        public TextBox UserLocationBox;
        public Button UserLoginOK;
        public WrapPanel wrapPanelBeet;
        public BeeteManager beeteManager;

        public MainWindow()
        {
            InitializeComponent();
            UhrDashBoard = new Label();
            TemperaturNow = new Label();
            RegenNow = new Label();
            SchneeNow = new Label();
            WolkenNow = new Label();
            CurrentLocation = new Label();
            RegenDaily = new Label();
            SchneeDaily = new Label();
            WolkenDaily = new Label();
            WetterDashBoard = new Label();
            MainUser = new User();
            UserLoginOK = new Button();
            wrapPanelBeet = new WrapPanel();
            beeteManager = new BeeteManager();
            UserLoginOK.Click += UserLogin_Click;
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawUserLogin();
            bool SignedIn = false;
            string[] UserData = new string[3];
            if (File.Exists(UserDataFile))
            {
                using (StreamReader reader = new StreamReader(UserDataFile))
                {
                    for (int x = 0; x < 2; x++)
                    {
                        if (x == 0)
                        {
                            if (reader.ReadLine() == "1")
                            {
                                SignedIn = true;
                            }
                            else
                            {
                                MainArea.Children.Clear();
                                DrawUserLogin();
                                SignedIn = true;
                                break;
                            }
                        }
                        else
                        {
                            UserData = reader.ReadLine().Split(";");
                            if (SignedIn)
                            {
                                MainUser = new User(UserData[0], UserData[1], UserData[2]);
                                DrawHome();
                            }
                        }
                    }
                    
                }
            }
            
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

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            DrawUserMenu();    
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            DrawHome();
        }

        public void DrawHome()
        {
            HomeButton.IsEnabled = true;
            BeeteButton.IsEnabled = true;
            PflanzeButton.IsEnabled = true;
            UserButton.IsEnabled = true;
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

        public void DrawBeeteMenu()
        {
            MainArea.Children.Clear();
            MainArea.Background = Brushes.Transparent;

            wrapPanelBeet.Children.Clear();
            wrapPanelBeet.HorizontalAlignment = HorizontalAlignment.Center;

            Label Header = new Label()
            {
                Content = "BEETE",
                FontSize = 40,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };


            Border border = new Border()
            {
                Background = Brushes.Black,
                Opacity = 0.6,
                Width = 225,
                Height = 195,
                CornerRadius = new CornerRadius(20, 20, 20, 20),
                Margin = new Thickness(5, 5, 5, 5),
                
            };
            border.BorderThickness = new Thickness(1);
            Grid grid = new Grid();
            grid.Margin = new Thickness(10);
            Image imagePlus = new Image();
            imagePlus.Source = new BitmapImage(new Uri("Images/Plus.png", UriKind.Relative));
            imagePlus.Height = 80;
            imagePlus.VerticalAlignment = VerticalAlignment.Center;
            imagePlus.HorizontalAlignment = HorizontalAlignment.Center;

            Button buttonAddBeet = new Button()
            {
                Width = 80,
                Height = 80,
                Opacity = 0.0001,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            buttonAddBeet.Click += ButtonAddBeet_Click;

            MainArea.Children.Add(Header);
            MainArea.Children.Add(wrapPanelBeet);
            foreach (Beet beet in beeteManager.Beete)
            {
                BeetShortInfo info = new BeetShortInfo(beet);
                wrapPanelBeet.Children.Add(info.GetShortInfo());
            }
            wrapPanelBeet.Children.Add(border);
            border.Child = grid;
            grid.Children.Add(imagePlus);
            grid.Children.Add(buttonAddBeet);
        }

        private void ButtonAddBeet_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBeet windowAddBeet = new WindowAddBeet();
            windowAddBeet.ShowDialog();

            if (windowAddBeet.DialogResult == true)
            {
                Beet neuesBeet = new Beet(windowAddBeet.TBName.Text, Convert.ToInt32(windowAddBeet.TBBreite.Text), Convert.ToInt32(windowAddBeet.TBLänge.Text));
                beeteManager.AddBeet(neuesBeet);
                DrawBeeteMenu();
            }
        }

        public void DrawUserMenu()
        {
            MainArea.Children.Clear();
            MainArea.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));

            Ellipse Profilbuch = new Ellipse()
            {
                Height = 150,
                Width = 150,
            };
            Profilbuch.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            Profilbuch.StrokeThickness = 2;
            Profilbuch.Margin = new Thickness(0, 30, 0, 0);
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(UriHelper.GetRessourceUri("Images/userPicture2.png"));
            imageBrush.Stretch = Stretch.UniformToFill;
            imageBrush.TileMode = TileMode.None;
            Profilbuch.Fill = imageBrush;


            Label UserName = new Label()
            {
                FontSize = 20,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Content = MainUser.Name,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Button ChangeUserName = new Button()
            {
                Width = 200,
                Height = 30,
                Content = "Change User Name",
                FontSize = 15,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(38, 80, 38)),
                Margin = new Thickness(0, 20, 0, 0),
            };

            Button ChangeLocation = new Button()
            {
                Width = 200,
                Height = 30,
                Content = "Change Location",
                FontSize = 15,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(38, 80, 38)),
                Margin = new Thickness(0, 20, 0, 0),
            };

            Button ChangePassword = new Button()
            {
                Width = 200,
                Height = 30,
                Content = "Change Password",
                FontSize = 15,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(38, 80, 38)),
                Margin = new Thickness(0, 20, 0, 0),
            };

            Button Logout = new Button()
            {
                Width = 200,
                Height = 30,
                Content = "Logout",
                FontSize = 15,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(38, 80, 38)),
                Margin = new Thickness(0, 20, 0, 20),
            };

            ChangePassword.Click += ChangePassword_Click;
            ChangeUserName.Click += ChangeUserName_Click;
            ChangeLocation.Click += ChangeLocation_Click;
            Logout.Click += Logout_Click;
            MainArea.Children.Add(Profilbuch);
            MainArea.Children.Add(UserName);
            MainArea.Children.Add(ChangeUserName);
            MainArea.Children.Add(ChangePassword);
            MainArea.Children.Add(ChangeLocation);
            MainArea.Children.Add(Logout);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            Window_ChangePassword window_ChangePassword = new Window_ChangePassword(MainUser, UserDataFile);
            window_ChangePassword.ShowDialog();
            if (window_ChangePassword.DialogResult == true)
            {
                MainUser = window_ChangePassword.MainUser;
                window_ChangePassword.Close();
            }
        }

        private void ChangeUserName_Click(object sender, RoutedEventArgs e)
        {
            Window_ChangeNameLocation window_ChangeName = new Window_ChangeNameLocation(MainUser, "Name", UserDataFile);
            window_ChangeName.ShowDialog();
            if (window_ChangeName.DialogResult == true)
            {
                MainUser = window_ChangeName.MainUser;
                window_ChangeName.Close();
            }
            DrawUserMenu();
        }

        private void ChangeLocation_Click(object sender, RoutedEventArgs e)
        {
            Window_ChangeNameLocation window_ChangeLocation = new Window_ChangeNameLocation(MainUser, "Location", UserDataFile);
            window_ChangeLocation.ShowDialog();
            if (window_ChangeLocation.DialogResult == true)
            {
                MainUser = window_ChangeLocation.MainUser;
                window_ChangeLocation.Close();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(UserDataFile, false))
            {
                writer.WriteLine("0");
            }
            MainArea.Children.Clear();
            DrawUserLogin();
        }

        private async void UserLogin_Click(object sender, RoutedEventArgs e)
        {
            // Check Input
            try
            {
                MainUser = new User(UserNameBox.Text, User.PasswordToHash(UserPasswordBox.Text), UserLocationBox.Text);
                OpenMeteo.OpenMeteoClient client = new OpenMeteo.OpenMeteoClient();
                WeatherForecast forecast = await client.QueryAsync(MainUser.Location);
                
                DrawHome();
                if (File.Exists(UserDataFile))
                {
                    using (StreamWriter writer = new StreamWriter(UserDataFile, false))
                    {
                        writer.WriteLine("1");
                        writer.WriteLine(MainUser.Searlized());
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(UserDataFile))
                    {
                        writer.WriteLine("1");
                        writer.WriteLine(MainUser.Searlized());
                    }
                }
            }
            catch
            {
                MessageBox.Show($"Didn't find Location '{UserLocationBox.Text}'", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            } 
        }

        public void DrawUserLogin()
        {
            MainArea.Background = Brushes.Transparent;
            Ellipse Profilbuch = new Ellipse()
            {
                Height = 150,
                Width = 150,
            };
            Profilbuch.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#262626"));
            Profilbuch.StrokeThickness = 2;
            Profilbuch.Margin = new Thickness(0, 30, 0, 0);
            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = new BitmapImage(UriHelper.GetRessourceUri("Images/userPicture2.png"));
           
            imageBrush.Stretch = Stretch.UniformToFill;
            imageBrush.TileMode = TileMode.None;

            Profilbuch.Fill = imageBrush;

            Label UserName = new Label()
            {
                FontSize = 20,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Content = "NAME",
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Label UserPassword = new Label()
            {
                FontSize = 20,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Content = "PASSWORT",
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Label UserLocation = new Label()
            {
                FontSize = 20,
                FontFamily = new FontFamily("Aharoni"),
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.White,
                Content = "STANDORT",
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            UserNameBox = new TextBox();
            UserNameBox.Width = 200;
            UserNameBox.Height = 30;
            UserNameBox.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
            UserNameBox.FontSize = 15;
            UserNameBox.FontFamily = new FontFamily("Aharoni");
            UserNameBox.FontWeight = FontWeights.Bold;
            UserNameBox.Foreground = Brushes.White;
            UserNameBox.TextAlignment = TextAlignment.Center;
            UserNameBox.VerticalContentAlignment = VerticalAlignment.Center;
            UserNameBox.Name = "UserNameTextBox";

            UserPasswordBox = new TextBox();
            UserPasswordBox.Width = 200;
            UserPasswordBox.Height = 30;
            UserPasswordBox.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
            UserPasswordBox.FontSize = 15;
            UserPasswordBox.FontFamily = new FontFamily("Aharoni");
            UserPasswordBox.FontWeight = FontWeights.Bold;
            UserPasswordBox.Foreground = Brushes.White;
            UserPasswordBox.TextAlignment = TextAlignment.Center;
            UserPasswordBox.VerticalContentAlignment = VerticalAlignment.Center;
            UserPasswordBox.Name = "UserNameTextBox";

            UserLocationBox = new TextBox();
            UserLocationBox.Width = 200;
            UserLocationBox.Height = 30;
            UserLocationBox.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
            UserLocationBox.FontSize = 15;
            UserLocationBox.FontFamily = new FontFamily("Aharoni");
            UserLocationBox.FontWeight = FontWeights.Bold;
            UserLocationBox.Foreground = Brushes.White;
            UserLocationBox.TextAlignment = TextAlignment.Center;
            UserLocationBox.VerticalContentAlignment = VerticalAlignment.Center;
            UserLocationBox.Name = "UserNameTextBox";

            UserLoginOK.Width = 200;
            UserLoginOK.Height = 30;
            UserLoginOK.Content = "OK";
            UserLoginOK.FontSize = 15;
            UserLoginOK.FontFamily = new FontFamily("Aharoni");
            UserLoginOK.FontWeight = FontWeights.Bold;
            UserLoginOK.Foreground = Brushes.White;
            UserLoginOK.VerticalContentAlignment = VerticalAlignment.Center;
            UserLoginOK.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
            UserLoginOK.Margin = new Thickness(0, 20, 0, 20);

            MainArea.Children.Add(Profilbuch);
            MainArea.Children.Add(UserName);
            MainArea.Children.Add(UserNameBox);
            MainArea.Children.Add(UserPassword);
            MainArea.Children.Add(UserPasswordBox);
            MainArea.Children.Add(UserLocation);
            MainArea.Children.Add(UserLocationBox);
            MainArea.Children.Add(UserLoginOK);
        }

        private void BeeteButton_Click(object sender, RoutedEventArgs e)
        {
            DrawBeeteMenu();
        }
    }
}
