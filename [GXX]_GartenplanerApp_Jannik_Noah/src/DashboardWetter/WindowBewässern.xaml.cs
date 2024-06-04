﻿using System;
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
    /// Interaction logic for WindowBewässern.xaml
    /// </summary>
    public partial class WindowBewässern : Window
    {
        public WindowBewässern(int bewässerungsintervall, DateTime lastTimeWatered)
        {
            InitializeComponent();
            LabelLastTimeWatered.Content = Convert.ToString(lastTimeWatered);
            LabelInterval.Content = $"Alle {bewässerungsintervall/24} Tage";
        }

        private void ButtonBewässern_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
            this.Close();
        }



    }
}
