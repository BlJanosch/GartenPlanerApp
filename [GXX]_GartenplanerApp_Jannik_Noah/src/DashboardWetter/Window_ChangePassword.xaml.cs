using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for Window_ChangePassword.xaml
    /// </summary>
    public partial class Window_ChangePassword : Window
    {
        public User MainUser;
        public string UserDataFile;
        public Window_ChangePassword(User mainUser, string userDataFile)
        {
            InitializeComponent();
            this.MainUser = mainUser;
            ButtonConfirm.IsEnabled = true;
            UserDataFile = userDataFile;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (User.PasswordToHash(TBold.Text) == MainUser.Password)
            {
                MainUser.Password = User.PasswordToHash(TBnew.Text);
                using (StreamWriter writer = new StreamWriter(UserDataFile, false))
                {
                    writer.WriteLine("1");
                    writer.WriteLine(MainUser.Searlized());
                }
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Passwort stimmt nicht mit vorherigem überein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
