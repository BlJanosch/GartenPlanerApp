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
    /// Interaction logic for WindowAddPlant.xaml
    /// </summary>
    public partial class WindowAddPlant : Window
    {
        public string Titel;
        public WindowAddPlant(string Titel)
        {
            InitializeComponent();
            this.Titel = Titel; 
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Title = Titel;
        }
    }
}
