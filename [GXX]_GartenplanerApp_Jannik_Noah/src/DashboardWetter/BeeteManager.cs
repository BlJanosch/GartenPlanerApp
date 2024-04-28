using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardWetter
{
    public class BeeteManager
    {
        public List<Beet> Beete = new List<Beet>();

        public BeeteManager() 
        { 
        }

        public void AddBeet(Beet beet)
        {
            Beete.Add(beet);
        }
    }
}
