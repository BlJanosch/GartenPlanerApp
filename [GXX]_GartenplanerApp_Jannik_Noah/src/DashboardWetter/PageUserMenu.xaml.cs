﻿using System;
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
    /// Interaction logic for PageUserMenu.xaml
    /// </summary>
    public partial class PageUserMenu : Page
    {
        private User MainUser;
        public string UserDataFile = AppDomain.CurrentDomain.BaseDirectory.Split("\\bin\\")[0] + "\\UserData\\Login.csv";
        public Frame MainFrame;
        public PageUserLogin pageUserLogin;
        public PageUserMenu(User MainUser, Frame mainFrame)
        {
            InitializeComponent();
            this.MainUser = MainUser;
            MainFrame = mainFrame;
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
            imageBrush.ImageSource = new BitmapImage(UriHelper.GetRessourceUri("Images/userPicture2.png"));
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

        // WICHTIG!!!
        // DrawUserLogin noch implementieren!!!

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(UserDataFile, false))
            {
                writer.WriteLine("0");
            }
            MainArea.Children.Clear();
            pageUserLogin = new PageUserLogin(MainFrame, MainUser, UserDataFile);
            MainFrame.Content = pageUserLogin;
            pageUserLogin.DrawUserLogin();
        }
    }
}
