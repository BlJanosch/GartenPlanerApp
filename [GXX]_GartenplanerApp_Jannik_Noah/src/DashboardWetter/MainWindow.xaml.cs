

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
using System.Media;

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public string Wetter;
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
        public PlantManager plantManager;
        public PageUserMenu pageUserMenu;
        public PageUserLogin pageUserLogin;
        public SoundPlayer soundPlayer;
        public PageUserSignIn pageUserSignIn;
        public bool AutomaticWatering = true;

        public MainWindow()
        {
            InitializeComponent();
            this.soundPlayer = new SoundPlayer("Assets/song.wav");
            //this.soundPlayer.PlayLooping();
            
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
            plantManager = new PlantManager();
            this.Loaded += Window_Loaded;
            MainFrame.ContentRendered += CheckPageHome;
        }

        private void CheckPageHome(object? sender, EventArgs e)
        {
            string PageName = MainFrame.Content.GetType().Name;
            if (PageName == "PageHome")
            {
                HomeButton.IsEnabled = true;
                BeeteButton.IsEnabled = true;
                PflanzeButton.IsEnabled = true;
                UserButton.IsEnabled = true;
                beeteManager.Beete = DataBaseManager.GetAllBeete(MainUser);
                Loggerclass.log.Information($"Menü Buttons wurden freigegeben");
            }
            else if (PageName == "PageUserLogin" || PageName == "PageLoginOrRegister")
            {
                HomeButton.IsEnabled = false;
                BeeteButton.IsEnabled = false;
                PflanzeButton.IsEnabled = false;
                UserButton.IsEnabled = false;
                Loggerclass.log.Information($"Menü Buttons wurden gesperrt");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawUserLoginOrRegister();
            bool SignedIn = false;
            string[] UserData = new string[4];
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

                                DrawUserLoginOrRegister();
                                Loggerclass.log.Information($"Benutzer {MainUser.Name} wurde erfolgreich regestriert oder angemeldet.");
                                SignedIn = true;
                                break;
                            }
                        }
                        else
                        {
                            UserData = reader.ReadLine().Split(";");
                            if (SignedIn)
                            {
                                MainUser = new User(UserData[1], UserData[2], UserData[3]);
                                MainUser.GetUserID();
                                DrawHome();
                                beeteManager.Beete = DataBaseManager.GetAllBeete(MainUser);
                                Loggerclass.log.Information($"Benutzer {MainUser.Name} wurde erfolgreich automatisch eingeloggt.");
                            }
                        }
                    }

                }
            }
            else
            {
                Loggerclass.log.Error("Warning User Data File is not available!");
            }
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
            pageHome.AutomaticWatering = AutomaticWatering;
            HomeButton.IsEnabled = true;
            BeeteButton.IsEnabled = true;
            PflanzeButton.IsEnabled = true;
            UserButton.IsEnabled = true;
            Loggerclass.log.Information($"Home Menü wurde erfolgreich gezeichnet");
        }

        public void DrawBeeteMenu()
        {

            try
            {
                if (!pageUserMenu.UserLogout)
                {
                    throw new Exception();
                }
                PageBeeteMenu pageBeeteMenu = new PageBeeteMenu(pageUserMenu.beeteManager, MainFrame, pageUserMenu.MainUser);
                MainFrame.Content = pageBeeteMenu;
                pageBeeteMenu.DrawBeeteMenu();
            }
            catch
            {
                PageBeeteMenu pageBeeteMenu = new PageBeeteMenu(beeteManager, MainFrame, MainUser);
                MainFrame.Content = pageBeeteMenu;
                pageBeeteMenu.DrawBeeteMenu();
            }
            Loggerclass.log.Information($"Beete Menü wurde erfolgreich gezeichnet.");
        }



        public void DrawUserMenu()
        {
            pageUserMenu = new PageUserMenu(MainUser, MainFrame, soundPlayer);
            MainFrame.Content = pageUserMenu;
            pageUserMenu.DrawUserMenu();
            pageUserMenu.AutomaticWateringButton.Click += AutomaticWateringButton_Click;
            pageUserMenu.Finished += PageUserMenu_Finished;
            Loggerclass.log.Information($"User Menü wurde erfolgreich gezeichnet.");
        }

        private void AutomaticWateringButton_Click(object sender, RoutedEventArgs e)
        {
            AutomaticWatering = pageUserMenu.AutomaticWatering;
            if (!AutomaticWatering)
            {
                pageUserMenu.AutomaticWateringButton.Content = "AB aktivieren";
            }
            else
            {
                pageUserMenu.AutomaticWateringButton.Content = "AB deaktivieren";
            }
        }

        private void PageUserMenu_Finished(object sender, EventArgs e)
        {
            MainUser = pageUserMenu.MainUser;
            beeteManager.Beete = DataBaseManager.GetAllBeete(MainUser);
            DrawHome();
        }

        public void DrawUserLoginOrRegister()
        {
            PageLoginOrRegister pageLoginOrRegister = new PageLoginOrRegister(MainFrame, MainUser);
            MainFrame.Content = pageLoginOrRegister;
            pageLoginOrRegister.DrawUserLogin();
            pageLoginOrRegister.RegisterButton.Click += RegisterButton_Click;
            pageLoginOrRegister.LoginButton.Click += LoginButton_Click;
            Loggerclass.log.Information($"UserLoginOrRegister wurde erfolgreich gezeichnet.");
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            pageUserSignIn = new PageUserSignIn(MainFrame, MainUser);
            MainFrame.Content = pageUserSignIn;
            pageUserSignIn.DrawUserLogin();
            pageUserSignIn.UserLoginOK.Click += UserSignInOK;
        }

        private void UserSignInOK(object sender, RoutedEventArgs e)
        {
            MainUser = pageUserSignIn.MainUser;
            if (MainUser != null)
            {
                beeteManager.Beete = DataBaseManager.GetAllBeete(MainUser);
                DrawHome();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            pageUserLogin = new PageUserLogin(MainFrame, MainUser, UserDataFile);
            MainFrame.Content = pageUserLogin;
            pageUserLogin.DrawUserLogin();
            pageUserLogin.UserLoginOK.Click += UserLoginOK_Click;
        }

        private void UserLoginOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainUser = pageUserLogin.MainUser;
                MainUser.GetUserID();
                Loggerclass.log.Information($"Benutzer {MainUser.Name} wurde erfolgreich eingeloggt.");
            }
            catch
            {
                Loggerclass.log.Error($"Fehler beim Einloggen! Wird erneut versucht...");
                pageUserLogin = new PageUserLogin(MainFrame, MainUser, UserDataFile);
                MainFrame.Content = pageUserLogin;
                pageUserLogin.DrawUserLogin();
                pageUserLogin.UserLoginOK.Click += UserLoginOK_Click;
            }
        }

        private void BeeteButton_Click(object sender, RoutedEventArgs e)
        {
            DrawBeeteMenu();
        }

        private void PflanzeButton_Click(object sender, RoutedEventArgs e)
        {
            PagePlantMenu pagePlantMenu = new PagePlantMenu(plantManager, MainFrame);
            MainFrame.Content = pagePlantMenu;
            pagePlantMenu.DrawPlantMenu();
        }
    }
}
