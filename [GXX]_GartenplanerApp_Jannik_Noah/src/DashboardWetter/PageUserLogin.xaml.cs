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
        public PasswordBox UserPasswordBox;
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
                Style = Styles.GetFontStyle(20),
                Content = "NAME",
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Label UserPassword = new Label()
            {
                Style = Styles.GetFontStyle(20),
                Content = "PASSWORT",
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Label UserLocation = new Label()
            {
                Style = Styles.GetFontStyle(20),
                Content = "STANDORT",
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            UserNameBox = new TextBox();
            UserNameBox.Style = Styles.GetTextBoxStyle();

            UserPasswordBox = new PasswordBox();
            UserPasswordBox.Style = Styles.GetPasswordTextBoxStyle();

            UserLocationBox = new TextBox();
            UserLocationBox.Style = Styles.GetTextBoxStyle();

            UserLoginOK = new Button();
            UserLoginOK.Content = "OK";
            UserLoginOK.Style = Styles.GetUserLoginButtonStyle();
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
                var Users = DataBaseManager.GetAllUser();
                foreach (User user in Users)
                {
                    if (user.Name == UserNameBox.Text)
                    {
                        MainUser = null;
                        Loggerclass.log.Error($"Benutzer {user.Name} existiert bereits.");
                        throw new NullReferenceException();
                    }
                }

                MainUser = new User(UserNameBox.Text, User.PasswordToHash(UserPasswordBox.Password), UserLocationBox.Text);
                MainUser.SaveUser();
                OpenMeteo.OpenMeteoClient client = new OpenMeteo.OpenMeteoClient();
                WeatherForecast forecast = await client.QueryAsync(MainUser.Location);
                if (forecast == null)
                {
                    Loggerclass.log.Information($"Standort {MainUser.Location} konnte nicht gefunden werden.");
                    throw new Exception($"Standort {MainUser.Location} konnte nicht gefunden werden.");
                }

                if (File.Exists(UserDataFile))
                {
                    using (StreamWriter writer = new StreamWriter(UserDataFile, false))
                    {
                        writer.WriteLine("1");
                        writer.WriteLine(MainUser.SaveToDB());
                    }
                }
                else
                {
                    Loggerclass.log.Error("User Data File ist nicht verfügbar!");
                    using (StreamWriter writer = new StreamWriter(UserDataFile))
                    {
                        writer.WriteLine("1");
                        writer.WriteLine(MainUser.SaveToDB());
                    }
                }
                PageHome pageHome = new PageHome(MainUser);
                MainFrame.Content = pageHome;
                pageHome.DrawHome();

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Dieser Name ist bereits vergeben!", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Didn't find Location {UserLocationBox.Text}! Change it in the User Menu.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
