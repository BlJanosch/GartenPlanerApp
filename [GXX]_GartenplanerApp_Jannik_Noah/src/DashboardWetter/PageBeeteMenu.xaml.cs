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

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for PageBeeteMenu.xaml
    /// </summary>
    public partial class PageBeeteMenu : Page
    {

        BeeteManager beeteManager;
        public Frame MainFrame;
        public PageBeeteMenu(BeeteManager beeteManager, Frame MainFrame)
        {
            InitializeComponent();
            this.beeteManager = beeteManager;
            this.MainFrame = MainFrame;
        }

        public void DrawBeeteMenu()
        {
            MainArea.Children.Clear();
            MainArea.HorizontalAlignment = HorizontalAlignment.Center;
            // MainArea.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2a6d43"));

            WrapPanel wrapPanelBeet = new WrapPanel();
            wrapPanelBeet.Margin = new Thickness(5);
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
            label.FontSize = 40;
            label.FontFamily = new System.Windows.Media.FontFamily("Aharoni");
            label.FontWeight = FontWeights.Bold;
            label.Foreground = System.Windows.Media.Brushes.White;
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
                BeetShortInfo shortInfo = new BeetShortInfo(beet);
                wrapPanelBeet.Children.Add(shortInfo.GetShortInfo());
            }
            wrapPanelBeet.Children.Add(border);
            border.Child = grid;
            grid.Children.Add(imagePlus);
            grid.Children.Add(buttonAddBeet);
        }

        private void ButtonAddBeet_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBeet windowAddBeet = new WindowAddBeet();
            windowAddBeet.ShowDialog();

            if (windowAddBeet.DialogResult == true)
            {
                Beet neuesBeet = new Beet(windowAddBeet.TBName.Text, Convert.ToInt32(windowAddBeet.TBBreite.Text), Convert.ToInt32(windowAddBeet.TBLänge.Text));
                beeteManager.AddBeet(neuesBeet);
                PageBeeteMenu newPage = new PageBeeteMenu(beeteManager, MainFrame);
                MainFrame.Content = newPage;
                newPage.DrawBeeteMenu();
            }
        }
    }
}
