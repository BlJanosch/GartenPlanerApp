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
using System.Windows.Shapes;
using Microsoft.Win32;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for WindowBestellen.xaml
    /// </summary>
    public partial class WindowBestellen : Window
    {
        public WindowBestellen(string Beetname, Plant[] plants)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            InitializeComponent();
            LabelBeetname.Content = Beetname;
            ListBoxBefüllen(plants);
        }

        private void ButtonBestellen_Click(object sender, RoutedEventArgs e)
        {


            string Botschaft = "";
            for (int i = 0; i<ListBoxEinkaufsliste.Items.Count; i++)
            {
                Botschaft = Botschaft + ListBoxEinkaufsliste.Items[i] + "\n";
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                string Filename = saveFileDialog1.FileName;
                if (Filename != "")
                {
                    Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Size(PageSizes.A5);
                            page.Margin(2, Unit.Centimetre);
                            page.Header().Text($"Einkaufsliste {LabelBeetname.Content}").SemiBold().FontSize(28).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                            page.Content().PaddingVertical(1, Unit.Centimetre).Column(x =>
                            {
                                x.Item().Text(Botschaft).FontSize(17).FontColor(QuestPDF.Helpers.Colors.Black);

                            });
                            page.Footer().AlignCenter().Text("TerraScape\nDeine App für den Garten").SemiBold().FontSize(10).FontColor(QuestPDF.Helpers.Colors.Green.Accent4);

                        });
                    }).GeneratePdf(Filename);
                    MessageBox.Show("Erfolgreich gespeichert!", "Glückwunsch", MessageBoxButton.OK, MessageBoxImage.Information);
                    Loggerclass.log.Information("Erfolgreich gespeichert");
                    this.Close();
                }
                else
                {
                    Loggerclass.log.Error("Datei konnte nicht gespeichert werden");
                    MessageBox.Show("Datei konnte nicht gespeichert werden... kein Speicherort ausgewählt.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }



        }

        private void ListBoxBefüllen(Plant[] plants)
        {
            List<string> plantsCombined = new List<string>();
            foreach (Plant plant in plants)
            {
                try
                {
                    if (plantsCombined.Contains(plant.Name) == false)
                    {
                        plantsCombined.Add(plant.Name);
                    }
                }
                catch
                {

                }
            }

            int[] counts = new int[plantsCombined.Count];
            int index = 0;
            foreach (string plantName in plantsCombined)
            {
                
                try
                {
                    foreach (Plant plant in plants)
                    {
                        try
                        {
                            if (plantName == plant.Name)
                            {
                                counts[index]++;
                            }
                            
                        }
                        catch { }
                    }
                    
                }
                catch
                {

                }
                index++;

            }

            for (int i = 0; i < counts.Length; i++)
            {
                ListBoxEinkaufsliste.Items.Add($"{plantsCombined[i]}: {counts[i]}");
            }
        }
    }
}
