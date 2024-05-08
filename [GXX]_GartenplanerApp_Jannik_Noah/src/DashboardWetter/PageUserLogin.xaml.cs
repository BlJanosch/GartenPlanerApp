using OpenMeteo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for PageUserLogin.xaml
    /// </summary>
    public partial class PageUserLogin : Page
    {

        public TextBox UserNameBox;
        public TextBox UserPasswordBox;
        public TextBox UserLocationBox;
        public Button UserLoginOK;
        public Frame MainFrame;
        public User MainUser;
        public string UserDataFile;
        public PageUserLogin(Frame mainFrame, User mainUser, string userDataFile)
        {
            this.MainFrame = mainFrame;
            this.MainUser = mainUser;
            InitializeComponent();
            UserDataFile = userDataFile;
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

            UserLoginOK = new Button();
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
            UserLoginOK.Click += UserLoginOK_Click;

            MainArea.Children.Add(Profilbuch);
            MainArea.Children.Add(UserName);
            MainArea.Children.Add(UserNameBox);
            MainArea.Children.Add(UserPassword);
            MainArea.Children.Add(UserPasswordBox);
            MainArea.Children.Add(UserLocation);
            MainArea.Children.Add(UserLocationBox);
            MainArea.Children.Add(UserLoginOK);
        }

        private async void UserLoginOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainUser = new User(UserNameBox.Text, User.PasswordToHash(UserPasswordBox.Text), UserLocationBox.Text);
                OpenMeteo.OpenMeteoClient client = new OpenMeteo.OpenMeteoClient();
                WeatherForecast forecast = await client.QueryAsync(MainUser.Location);
                if (forecast == null)
                {
                    throw new Exception("Didn't find Location");
                }

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
                PageHome pageHome = new PageHome(MainUser);
                MainFrame.Content = pageHome;
                pageHome.DrawHome();
            }
            catch
            {
                MessageBox.Show($"Didn't find Location '{UserLocationBox.Text}'", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
