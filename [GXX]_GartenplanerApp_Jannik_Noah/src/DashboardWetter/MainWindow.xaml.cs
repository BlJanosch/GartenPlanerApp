

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
            PageHome pageHome = new PageHome(MainUser);
            MainFrame.Content = pageHome;
            pageHome.DrawHome();
            HomeButton.IsEnabled = true;
            BeeteButton.IsEnabled = true;
            PflanzeButton.IsEnabled = true;
            UserButton.IsEnabled = true;
        }

        public void DrawBeeteMenu()
        {
            PageBeeteMenu pageBeeteMenu = new PageBeeteMenu(beeteManager);
            MainFrame.Content = pageBeeteMenu;
            pageBeeteMenu.DrawBeeteMenu();
        }



        public void DrawUserMenu()
        {
            PageUserMenu pageUserMenu = new PageUserMenu(MainUser);
            MainFrame.Content = pageUserMenu;
            pageUserMenu.DrawUserMenu();
        }

       

        /*
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(UserDataFile, false))
            {
                writer.WriteLine("0");
            }
            MainArea.Children.Clear();
            DrawUserLogin();
        }
        */

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
            PageUserLogin pageUserLogin = new PageUserLogin();
            MainFrame.Content = pageUserLogin;
            pageUserLogin.DrawUserLogin();
        }

        private void BeeteButton_Click(object sender, RoutedEventArgs e)
        {
            DrawBeeteMenu();
        }
    }
}
