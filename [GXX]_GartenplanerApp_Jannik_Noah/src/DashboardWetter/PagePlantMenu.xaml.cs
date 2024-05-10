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
    /// Interaction logic for PagePlantMenu.xaml
    /// </summary>
    public partial class PagePlantMenu : Page
    {
        PlantManager plantManager;
        public Frame MainFrame;
        public List<string> names = DataBaseManager.GetAllNames();
        public PagePlantMenu(PlantManager plantManager, Frame MainFrame)
        {
            InitializeComponent();
            this.plantManager = plantManager;
            this.MainFrame = MainFrame;
        }

        public void DrawPlantMenu()
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
            label.Content = "Pflanzen";
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

            ListBox listBox = new ListBox();

            MainArea.Children.Add(label);
            MainArea.Children.Add(wrapPanelBeet);
            foreach (string plantName in this.names)
            {
                listBox.Items.Add(plantName);   
            }
            wrapPanelBeet.Children.Add(border);
            border.Child = grid;
            grid.Children.Add(imagePlus);
            grid.Children.Add(buttonAddBeet);
            grid.Children.Add(listBox);
        }

        private void ButtonAddBeet_Click(object sender, RoutedEventArgs e)
        {
            // Auf Plant Manager umschreiben !!!
            /* 
            WindowAddBeet windowAddBeet = new WindowAddBeet();
            windowAddBeet.ShowDialog();

            if (windowAddBeet.DialogResult == true)
            {
                Beet neuesBeet = new Beet((windowAddBeet.TBName.Text != "") ? windowAddBeet.TBName.Text : $"Beet {beeteManager.Beete.Count + 1}", Convert.ToInt32(windowAddBeet.TBBreite.Text), Convert.ToInt32(windowAddBeet.TBLänge.Text));
                beeteManager.AddBeet(neuesBeet);
                PageBeeteMenu newPage = new PageBeeteMenu(beeteManager, MainFrame);
                MainFrame.Content = newPage;
                newPage.DrawBeeteMenu();
            }
            */
        }
    }
}
