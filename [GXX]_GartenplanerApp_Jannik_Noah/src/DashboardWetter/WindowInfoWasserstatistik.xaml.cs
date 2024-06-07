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
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace DashboardWetter
{
    /// <summary>
    /// Interaction logic for WindowInfoWasserstatistik.xaml
    /// </summary>
    public partial class WindowInfoWasserstatistik : Window
    {
        public WindowInfoWasserstatistik()
        {
            InitializeComponent();
            LabelInfo.Content = "leicht: Niederschlagshöhe in 60 Minuten < 2,5 mm, in 10 Minuten < 0,5 mm\nmäßig: Niederschlagshöhe in 60 Minuten ≥ 2,5 mm bis< 10,0 mm, in 10 Minuten ≥ 0,5 mm bis< 1,7 mm\nstark: Niederschlagshöhe in 60 Minuten ≥ 10,0 mm, in 10 Minuten ≥ 1,7 mm\nsehr stark: Niederschlagshöhe in 60 Minuten ≥ 50,0 mm, in 10 Minuten ≥ 8,3";

        }
    }
}
