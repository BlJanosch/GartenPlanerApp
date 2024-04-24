using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DashboardWetter
{
    public static class UriHelper
    {
        public static Uri GetRessourceUri(string relativePath)
        {
            return new Uri(@"pack://application:,,,/"
                            + Assembly.GetExecutingAssembly().GetName().Name
                            + ";component/" +
                            relativePath, UriKind.Absolute);
        }

        public static BitmapImage GetBitmapImage(string relativePath)
        {
            return new BitmapImage(GetRessourceUri(relativePath));  
        }
    }
}
