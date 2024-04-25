﻿using OpenMeteo;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaktionslogik für Window_ChangeNameLocation.xaml
    /// </summary>
    public partial class Window_ChangeNameLocation : Window
    {
        public User MainUser;
        public string ChangeWhat;
        public string UserDataFile;
        public Window_ChangeNameLocation(User mainUser, string changeWhat, string userDataFile)
        {
            InitializeComponent();
            MainUser = mainUser;
            ChangeWhat = changeWhat;
            ChangeLabel.Content = changeWhat;
            UserDataFile = userDataFile;
        }

        private async void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChangeWhat == "Name")
                {
                    MainUser.Name = TextBoxNew.Text;
                }
                else if (ChangeWhat == "Location")
                {
                    OpenMeteo.OpenMeteoClient client = new OpenMeteo.OpenMeteoClient();
                    WeatherForecast forecast = await client.QueryAsync(TextBoxNew.Text);
                    if (forecast == null)
                    {
                        throw new Exception($"{TextBoxNew.Text} konnte nicht gefunden werden!");
                    }
                    MainUser.Location = TextBoxNew.Text;
                    using (StreamWriter writer = new StreamWriter(UserDataFile, false))
                    {
                        writer.WriteLine("1");
                        writer.WriteLine(MainUser.Searlized());
                    }
                }
                else
                {
                    throw new Exception("Falscher Veränderungstyp wurde mitgegeben!");
                }
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}