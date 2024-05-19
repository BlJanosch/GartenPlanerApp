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
    /// Interaction logic for PagePlantInfo.xaml
    /// </summary>
    public partial class PagePlantInfo : Page
    {
        public int plantIndex;
        PlantManager plantManager = DataBaseManager.GetAllPlants();

        public PagePlantInfo(int plantIndex)
        {
            InitializeComponent();
            this.plantIndex = plantIndex;
            
        }

        public void DrawPlantInfo()
        {
            LabelPlantName.Content = plantManager.Pflanzen[plantIndex].Name;

           
            TextBlockInfos.Text = $"\nSAAT\nSaatzeit: {plantManager.Pflanzen[plantIndex].Saatzeit}\nSaatverfahren: {plantManager.Pflanzen[plantIndex].Saatverfahren}\nSaattiefe: {plantManager.Pflanzen[plantIndex].Saattiefe}\nPflanzabstand: {plantManager.Pflanzen[plantIndex].Pflanzenabstand}\n\nPFLEGE\n{plantManager.Pflanzen[plantIndex].Pflege}\n\nTemperatur: {plantManager.Pflanzen[plantIndex].Temperatur}\nWasserbedarf: {plantManager.Pflanzen[plantIndex].Wasserbedarf}\nNährstoffbedarf: {plantManager.Pflanzen[plantIndex].Nährstoffbedarf}\nErnte: {plantManager.Pflanzen[plantIndex].Ernte}\nHäufige Krankheiten: {plantManager.Pflanzen[plantIndex].Krankheiten}\n";
            TextBlockInfos.FontFamily = new FontFamily("Ahorni");
            TextBlockInfos.FontSize = 13;
            TextBlockInfos.Foreground = Brushes.White;  
            TextBlockInfos.FontWeight = FontWeights.Bold;
            TextBlockInfos.Margin = new Thickness(10, 0, 0, 0);
            Image.Source = new BitmapImage(new Uri($"/Images/plants/plant{plantIndex}.jpg", UriKind.Relative));

        }
    }
}
