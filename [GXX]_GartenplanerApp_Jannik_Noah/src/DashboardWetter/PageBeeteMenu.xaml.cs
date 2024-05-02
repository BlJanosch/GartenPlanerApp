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
        public PageBeeteMenu(BeeteManager beeteManager)
        {
            InitializeComponent();
            this.beeteManager = beeteManager;
        }

        public void DrawBeeteMenu()
        {
            MainArea.Children.Clear();
            // MainArea.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2a6d43"));

            WrapPanel wrapPanelBeet = new WrapPanel();
            wrapPanelBeet.Margin = new Thickness(30);
            Border border = new Border()
            {
                Background = Brushes.Black,
                Opacity = 0.6,
                Width = 225,
                Height = 195,
                CornerRadius = new CornerRadius(20, 20, 20, 20)

            };
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

            MainArea.Children.Add(wrapPanelBeet);
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
                DrawBeeteMenu();
            }
        }
    }
}
