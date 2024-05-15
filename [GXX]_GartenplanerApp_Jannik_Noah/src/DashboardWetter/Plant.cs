using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardWetter
{
    public class Plant
    {

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
        public string guteNachbarn;
        public string schlechteNachbarn;
        public Plant(string name, string saatverfahren, string pflanzenabstand, string saattiefe, string saatzeit, string temperatur, string nährstoffbedarf, string wasserbedarf, string pflege, string krankheiten, string ernte, string guteNachbarn, string schlechteNachbarn)
        {
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
            this.guteNachbarn = guteNachbarn;
            this.schlechteNachbarn = schlechteNachbarn;
        }
    }
}
