using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for PageBeeteMenu.xaml
    /// </summary>
    public partial class PageBeeteMenu : Page
    {

        BeeteManager beeteManager;
        public Frame MainFrame;
        public User CurrentUser;
        public PageBeeteMenu(BeeteManager beeteManager, Frame MainFrame, User user)
        {
            InitializeComponent();
            this.beeteManager = beeteManager;
            this.MainFrame = MainFrame;
            CurrentUser = user;
        }

        public void DrawBeeteMenu()
        {
            MainArea.Children.Clear();
            MainArea.HorizontalAlignment = HorizontalAlignment.Center;

            WrapPanel wrapPanelBeet = new WrapPanel();
            wrapPanelBeet.Margin = new Thickness(0);
            Border border = new Border()
            {
                Background = Brushes.Black,
                Opacity = 0.6,
                Width = 225,
                Height = 195,
                CornerRadius = new CornerRadius(20, 20, 20, 20)

            };
            Label label = new Label();
            label.Content = "Beete";
            label.Style = Styles.GetFontStyle(40);
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Margin = new Thickness(0, 20, 0, 0);

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

            MainArea.Children.Add(label);
            MainArea.Children.Add(wrapPanelBeet);
            foreach (Beet beet in beeteManager.Beete)
            {
                BeetShortInfo shortInfo = new BeetShortInfo(beet, MainArea, MainFrame, beeteManager, this);
                wrapPanelBeet.Children.Add(shortInfo.GetShortInfo());
            }
            wrapPanelBeet.Children.Add(border);
            border.Child = grid;
            grid.Children.Add(imagePlus);
            grid.Children.Add(buttonAddBeet);

        }


        private void ButtonAddBeet_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBeet windowAddBeet = new WindowAddBeet(beeteManager);
            windowAddBeet.ShowDialog();

            if (windowAddBeet.DialogResult == true)
            {
                int bewässerung;
                if (windowAddBeet.BewässerungBox.SelectedIndex == 6)
                {
                    bewässerung = 168;
                }
                else if (windowAddBeet.BewässerungBox.SelectedIndex == 7)
                {
                    bewässerung = 336;
                }
                else
                {
                    bewässerung = (windowAddBeet.BewässerungBox.SelectedIndex + 1) * 24;
                }
                Beet neuesBeet = new Beet(beeteManager.Beete.Count, CurrentUser.ID, (windowAddBeet.TBName.Text != "") ? windowAddBeet.TBName.Text : $"Beet {beeteManager.Beete.Count + 1}", Convert.ToInt32(windowAddBeet.TBBreite.Text), Convert.ToInt32(windowAddBeet.TBLänge.Text), 0, 0, bewässerung, DateTime.Now); ; ;
                neuesBeet.SaveBeet();
                beeteManager.AddBeet(neuesBeet);
                PageBeeteMenu newPage = new PageBeeteMenu(beeteManager, MainFrame, CurrentUser);
                MainFrame.Content = newPage;
                newPage.DrawBeeteMenu();
                Loggerclass.log.Information($"Beet {neuesBeet.Name} wurde hinzugefügt.");
            }
        }
    }
}
