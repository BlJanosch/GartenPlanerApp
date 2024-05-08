

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
        public PageUserMenu pageUserMenu;

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
            this.Loaded += Window_Loaded;
            MainFrame.ContentRendered += CheckPageHome;
        }

        private void CheckPageHome(object? sender, EventArgs e)
        {
            string PageName = MainFrame.Content.GetType().Name;
            if (PageName == "PageHome")
            {
                if (pageUserMenu != null && pageUserMenu.pageUserLogin != null)
                {
                    MainUser = pageUserMenu.pageUserLogin.MainUser;
                }
                HomeButton.IsEnabled = true;
                BeeteButton.IsEnabled = true;
                PflanzeButton.IsEnabled = true;
                UserButton.IsEnabled = true;
            }
            else if (PageName == "PageUserLogin")
            {
                HomeButton.IsEnabled = false;
                BeeteButton.IsEnabled = false;
                PflanzeButton.IsEnabled = false;
                UserButton.IsEnabled = false;
            }
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
            PageBeeteMenu pageBeeteMenu = new PageBeeteMenu(beeteManager, MainFrame);
            MainFrame.Content = pageBeeteMenu;
            pageBeeteMenu.DrawBeeteMenu();
        }



        public void DrawUserMenu()
        {
            pageUserMenu = new PageUserMenu(MainUser, MainFrame);
            MainFrame.Content = pageUserMenu;
            pageUserMenu.DrawUserMenu();
        }

        public void DrawUserLogin()
        {
            PageUserLogin pageUserLogin = new PageUserLogin(MainFrame, MainUser, UserDataFile);
            MainFrame.Content = pageUserLogin;
            pageUserLogin.DrawUserLogin();
        }

        private void BeeteButton_Click(object sender, RoutedEventArgs e)
        {
            DrawBeeteMenu();
        }
    }
}
