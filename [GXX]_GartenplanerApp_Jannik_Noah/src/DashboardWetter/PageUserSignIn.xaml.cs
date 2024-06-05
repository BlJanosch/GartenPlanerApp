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
    /// Interaction logic for PageUserSignIn.xaml
    /// </summary>
    public partial class PageUserSignIn : Page
    {
        public TextBox UserNameBox;
        public TextBox UserPasswordBox;
        public TextBox UserLocationBox;
        public Button UserLoginOK;
        public Frame MainFrame;
        public User MainUser;
        public string UserDataFile = AppDomain.CurrentDomain.BaseDirectory.Split("\\bin\\")[0] + "\\UserData\\Login.csv";
        public PageUserSignIn(Frame mainFrame, User mainUser)
        {
            this.MainFrame = mainFrame;
            this.MainUser = mainUser;
            InitializeComponent();
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

            UserNameBox = new TextBox();
            UserNameBox.Style = Styles.GetTextBoxStyle();
            UserNameBox.Name = "UserNameTextBox";

            UserPasswordBox = new TextBox();
            UserPasswordBox.Style = Styles.GetTextBoxStyle();
            UserPasswordBox.Name = "UserNameTextBox";

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
            MainArea.Children.Add(UserLoginOK);
        }

        private async void UserLoginOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainUser = DataBaseManager.GetUser(UserNameBox.Text, UserPasswordBox.Text);
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
                    using (StreamWriter writer = new StreamWriter(UserDataFile))
                    {
                        writer.WriteLine("1");
                        writer.WriteLine(MainUser.SaveToDB());
                    }
                }
            }
            catch (Exception ex)
            {
                MainUser = null;
                Loggerclass.log.Information("Falsche Eingabe bei PageUserSignIn");
                MessageBox.Show("Bitte überprüfen Sie ihre Eingabe", "Falsche Eingabe", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
