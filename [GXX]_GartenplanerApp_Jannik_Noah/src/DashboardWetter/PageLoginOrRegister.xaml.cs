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
    /// Interaction logic for PageLoginOrRegister.xaml
    /// </summary>
    public partial class PageLoginOrRegister : Page
    {
        public Button RegisterButton;
        public Button LoginButton;
        public Frame MainFrame;
        public User MainUser;
        public PageLoginOrRegister(Frame mainFrame, User mainUser)
        {
            this.MainFrame = mainFrame;
            this.MainUser = mainUser;
            InitializeComponent();
        }

        public void DrawUserLogin()
        {
            MainArea.Background = Brushes.Transparent;
            MainArea.VerticalAlignment = VerticalAlignment.Center;

            RegisterButton = new Button();
            RegisterButton.Content = "Registrieren";
            RegisterButton.Style = Styles.GetUserLoginButtonStyle();
            RegisterButton.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
            RegisterButton.Margin = new Thickness(0, 20, 0, 20);

            LoginButton = new Button();
            LoginButton.Content = "Anmelden";
            LoginButton.Style = Styles.GetUserLoginButtonStyle();
            LoginButton.Background = new SolidColorBrush(Color.FromRgb(38, 38, 38));
            LoginButton.Margin = new Thickness(0, 20, 0, 20);

            MainArea.Children.Add(RegisterButton);
            MainArea.Children.Add(LoginButton);
            Loggerclass.log.Information($"UserLogin wurde erfolgreich gezeichnet.");

        }

    }
}
