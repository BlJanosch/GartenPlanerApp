using System;
using System.Collections.Generic;
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
        public string newPassword = "";
        public string oldPW;
        public Window_ChangePassword(string oldPW)
        {
            InitializeComponent();
            this.oldPW = oldPW;
            ButtonConfirm.IsEnabled = true;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (User.PasswordToHash(TBold.Text) == oldPW)
            {
                newPassword = User.PasswordToHash(TBnew.Text);
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Passwort stimmt nicht mit vorherigem überein", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
