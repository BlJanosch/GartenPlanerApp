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

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for WindowAddBeet.xaml
    /// </summary>
    public partial class WindowAddBeet : Window
    {
        public WindowAddBeet()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            int zahl;
            try
            {
                zahl = Convert.ToInt32(TBLänge.Text);
                if (zahl <= 0) 
                {
                    throw new Exception("Bitte keine negativen Zahlen eingeben!");
                }
                else if (zahl >= 11) 
                {
                    throw new Exception("Achtung! Höchstens 10 Spalten erlaubt!");
                }
                zahl = Convert.ToInt32(TBBreite.Text);
                if (zahl <= 0)
                {
                    throw new Exception("Bitte keine negativen Zahlen eingeben!");
                }
                else if (zahl >= 5)
                {
                    throw new Exception("Achtung! Höchstens 4 Reihen erlaubt!");
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("ACHTUNG Nur Zahlen erlaubt!!!", "Eingabe überprüfen", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eingabe überprüfen", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
