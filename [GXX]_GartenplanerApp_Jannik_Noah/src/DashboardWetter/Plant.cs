using Microsoft.Data.Sqlite;
using OpenMeteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardWetter
{
    public class Plant
    {
        public int ID;
        public string Name;
        public string Saatverfahren;
        public string Pflanzenabstand;
        public string Saattiefe;
        public string Saatzeit;
        public string Temperatur;
        public string Nährstoffbedarf;
        public string Wasserbedarf;
        public string Pflege;
        public string Krankheiten;
        public string Ernte;
        public int[] guteNachbarn;
        public int[] schlechteNachbarn;
        public Plant(int id, string name, string saatverfahren, string pflanzenabstand, string saattiefe, string saatzeit, string temperatur, string nährstoffbedarf, string wasserbedarf, string pflege, string krankheiten, string ernte, string guteNachbarn, string schlechteNachbarn)
        {
            ID = id;
            Name = name;
            Saatverfahren = saatverfahren;
            Pflanzenabstand = pflanzenabstand;
            Saattiefe = saattiefe;
            Saatzeit = saatzeit;
            Temperatur = temperatur;
            Nährstoffbedarf = nährstoffbedarf;
            Wasserbedarf = wasserbedarf;
            Pflege = pflege;
            Krankheiten = krankheiten;
            Ernte = ernte;

            if (guteNachbarn != "")
            {
                string[] g_Nachbarn = guteNachbarn.Split("#");
                int[] gu_Nachbarn = new int[g_Nachbarn.Length];
                for (int i = 0; i < g_Nachbarn.Length; i++)
                {
                    gu_Nachbarn[i] = Convert.ToInt32(g_Nachbarn[i]);
                }
                this.guteNachbarn = gu_Nachbarn;
                Loggerclass.log.Information($"GuteNachbarn wurden gesetzt.");
            }
            else
            {
                this.guteNachbarn = new int[0];
            }

            if (schlechteNachbarn != "")
            {
                string[] s_Nachbarn = schlechteNachbarn.Split("#");
                int[] sc_Nachbarn = new int[s_Nachbarn.Length];
                for (int i = 0; i < s_Nachbarn.Length; i++)
                {
                    sc_Nachbarn[i] = Convert.ToInt32(s_Nachbarn[i]);
                }
                this.schlechteNachbarn = sc_Nachbarn;
                Loggerclass.log.Information($"SchlechteNachbarn wurden gesetzt.");
            }
            else
            {
                this.schlechteNachbarn = new int[0];
            }
        }
    }
}
