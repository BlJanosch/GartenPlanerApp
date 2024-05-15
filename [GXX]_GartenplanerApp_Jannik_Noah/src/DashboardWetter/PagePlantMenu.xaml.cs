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
        public PlantManager Plants = DataBaseManager.GetAllPlants();
        public PagePlantMenu(PlantManager plantManager, Frame MainFrame)
        {
            InitializeComponent();
            this.plantManager = plantManager;
            this.MainFrame = MainFrame;

            ListBoxPlants.SelectionChanged += ListBox_SelectionChanged;
        }

        public void DrawPlantMenu()
        {



            ListBox listBox = ListBoxPlants;
            
            foreach (Plant Plant in Plants.Pflanzen)
            {
                listBox.Items.Add(Plant.Name);
                
            }

            

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxPlants.SelectedIndex != -1)
            {
                PagePlantInfo pagePlantInfo = new PagePlantInfo(ListBoxPlants.SelectedIndex);
                FramePlantInfo.Content = pagePlantInfo;
                pagePlantInfo.DrawPlantInfo();
            }
        }


    }
}
