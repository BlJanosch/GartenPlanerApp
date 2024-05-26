using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
    /// Interaction logic for PageUserMenu.xaml
    /// </summary>
    public partial class PageUserMenu : Page
    {
        public User MainUser;
        public string UserDataFile = AppDomain.CurrentDomain.BaseDirectory.Split("\\bin\\")[0] + "\\UserData\\Login.csv";
        public Frame MainFrame;
        public PageLoginOrRegister pageLoginOrRegister;
        public PageUserSignIn pageUserSignIn;
        public PageUserLogin pageUserLogin;
        public BeeteManager beeteManager = new BeeteManager();
        public bool UserLogout = false;

        // Test-Event
        public delegate void FinishedHandler(object sender, EventArgs e);
        public event FinishedHandler Finished;

        private SoundPlayer soundPlayer;
        private Button buttonPlay;
        private Button buttonMute;

        public PageUserMenu(User MainUser, Frame mainFrame, SoundPlayer soundPlayer)
        {
            InitializeComponent();
            this.MainUser = MainUser;
            MainFrame = mainFrame;
            this.soundPlayer = soundPlayer;
        }

        public PageUserMenu()
        {
            InitializeComponent();

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
            imageBrush.ImageSource = new BitmapImage(UriHelper.GetRessourceUri("Images/TerraScape.png"));
            imageBrush.Stretch = Stretch.UniformToFill;
            imageBrush.TileMode = TileMode.None;
            Profilbuch.Fill = imageBrush;


            Label UserName = new Label()
            {
                Style = Styles.GetFontStyle(20),
                Content = MainUser.Name,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Button ChangeUserName = new Button()
            {
                Content = "Change User Name",
                Style = Styles.GetUserLoginButtonStyle(),
            };

            Button ChangeLocation = new Button()
            {
                Content = "Change Location",
                Style = Styles.GetUserLoginButtonStyle(),
            };

            Button ChangePassword = new Button()
            {
                Content = "Change Password",
                Style = Styles.GetUserLoginButtonStyle(),
            };

            Button Logout = new Button()
            {
                Content = "Logout",
                Style = Styles.GetUserLoginButtonStyle(),
                Margin = new Thickness(0, 20, 0, 20),
            };

            Button buttonMute = new Button()
            {
                Content = "Mute",
                Style = Styles.GetUserLoginButtonStyle(),

            };

            buttonMute.Click += ButtonMute_Click;
            this.buttonMute = buttonMute;

            Button buttonPlay = new Button()
            {
                Content = "Play",
                Style = Styles.GetUserLoginButtonStyle(),
                
            };

            buttonPlay.Click += ButtonPlay_Click;
            this.buttonPlay = buttonPlay;

            ChangePassword.Click += ChangePassword_Click;
            ChangeUserName.Click += ChangeUserName_Click;
            ChangeLocation.Click += ChangeLocation_Click;
            Logout.Click += Logout_Click;
            MainArea.Children.Add(Profilbuch);
            MainArea.Children.Add(UserName);

            MainArea.Children.Add(buttonPlay);
            MainArea.Children.Add(buttonMute);

            MainArea.Children.Add(ChangeUserName);
            MainArea.Children.Add(ChangePassword);
            MainArea.Children.Add(ChangeLocation);
            MainArea.Children.Add(Logout);

        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            //this.buttonPlay.IsEnabled = false;
            //this.buttonMute.IsEnabled = true;
            soundPlayer.PlayLooping();
            
        }

        private void ButtonMute_Click(object sender, RoutedEventArgs e)
        {
            //this.buttonMute.IsEnabled = false;
            //this.buttonPlay.IsEnabled = true;
            soundPlayer.Stop();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            Window_ChangePassword window_ChangePassword = new Window_ChangePassword(MainUser, UserDataFile);
            window_ChangePassword.ShowDialog();
            if (window_ChangePassword.DialogResult == true)
            {
                MainUser = window_ChangePassword.MainUser;
                MainUser.UpdateUser();
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
                MainUser.UpdateUser();
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
                MainUser.UpdateUser();
                window_ChangeLocation.Close();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            UserLogout = true;
            using (StreamWriter writer = new StreamWriter(UserDataFile, false))
            {
                writer.WriteLine("0");
            }
            MainArea.Children.Clear();
            pageLoginOrRegister = new PageLoginOrRegister(MainFrame, MainUser);
            MainFrame.Content = pageLoginOrRegister;
            pageLoginOrRegister.DrawUserLogin();
            pageLoginOrRegister.LoginButton.Click += LoginButton2_Click;
            pageLoginOrRegister.RegisterButton.Click += RegisterButton2_Click;
        }

        private void RegisterButton2_Click(object sender, RoutedEventArgs e)
        {
            pageUserLogin = new PageUserLogin(MainFrame, MainUser, UserDataFile);
            MainFrame.Content = pageUserLogin;
            pageUserLogin.DrawUserLogin();
            pageUserLogin.UserLoginOK.Click += UserLoginOK_Click1;
        }

        private void UserLoginOK_Click1(object sender, RoutedEventArgs e)
        {
            try
            {
                MainUser = pageUserLogin.MainUser;
                beeteManager.Beete = DataBaseManager.GetAllBeete(MainUser);
                Finished?.Invoke(this, EventArgs.Empty);
            }
            catch
            {
                pageUserLogin = new PageUserLogin(MainFrame, MainUser, UserDataFile);
                MainFrame.Content = pageUserLogin;
                pageUserLogin.DrawUserLogin();
                pageUserLogin.UserLoginOK.Click += UserLoginOK_Click1;
            }
        }

        private void LoginButton2_Click(object sender, RoutedEventArgs e)
        {
            pageUserSignIn = new PageUserSignIn(MainFrame, MainUser);
            MainFrame.Content = pageUserSignIn;
            pageUserSignIn.DrawUserLogin();
            pageUserSignIn.UserLoginOK.Click += UserLoginOK_Click;
        }

        private void UserLoginOK_Click(object sender, RoutedEventArgs e)
        {
            MainUser = pageUserSignIn.MainUser;
            if (MainUser != null)
            {
                beeteManager.Beete = DataBaseManager.GetAllBeete(MainUser);
                Finished?.Invoke(this, EventArgs.Empty);
            } 
        }
    }
}