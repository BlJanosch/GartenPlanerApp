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
        public BeeteManager BeeteManager;
        public WindowAddBeet(BeeteManager beete)
        {
            InitializeComponent();
            BeeteManager = beete;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            int zahl;
            try
            {
                zahl = Convert.ToInt32(TBLänge.Text);
                if (zahl <= 0) 
                {
                    Loggerclass.log.Information("Negative Zahl im Beet-Ersteller eingegeben.");
                    throw new Exception("Bitte keine negativen Zahlen eingeben!");
                }
                else if (zahl > 8) 
                {
                    Loggerclass.log.Information("Zu viele Spalten eingegeben im Beet-Ersteller.");
                    throw new Exception("Achtung! Höchstens 8 Spalten erlaubt!");
                }
                zahl = Convert.ToInt32(TBBreite.Text);
                if (zahl <= 0)
                {
                    Loggerclass.log.Information("Negative Zahl im Beet-Ersteller eingegeben.");
                    throw new Exception("Bitte keine negativen Zahlen eingeben!");
                }
                else if (zahl > 5)
                {
                    Loggerclass.log.Information("Zu viele Reihen eingegeben im Beet-Ersteller.");
                    throw new Exception("Achtung! Höchstens 5 Reihen erlaubt!");
                }

                foreach (Beet beet in BeeteManager.Beete)
                {
                    if (TBName.Text == beet.Name)
                    {
                        Loggerclass.log.Information("Beetname wurde bereits vergeben.");
                        throw new Exception("Dieser Name ist bereits vergeben!");
                    }
                }
                this.DialogResult = true;
                this.Close();
            }
            catch (FormatException)
            {
                Loggerclass.log.Error("Falsches Eingabe-Format");
                MessageBox.Show("ACHTUNG Nur Zahlen erlaubt!!!", "Eingabe überprüfen", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Loggerclass.log.Error($"{ex}");
                MessageBox.Show(ex.Message, "Eingabe überprüfen", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
