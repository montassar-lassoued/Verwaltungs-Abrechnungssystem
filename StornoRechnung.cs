using System;
using System.Collections.Generic;

namespace FCC_Verwaltungssystem
{
    public class StornoRechnung
    {
        private string stornoNummer;
        private string kurzbez;
        private DateTime? stornoDatum;
        private DateTime? erstellungsdatum;
        private string bezugRechnungsnummer;
        private string korrektur;
        private List<Leistung> leistungen;

        public string StornoNummer { get => stornoNummer; set => stornoNummer = value; }
        public string Kurzbez { get => kurzbez; set => kurzbez = value; }
        public DateTime? StornoDatum { get => stornoDatum; set => stornoDatum = value; }
        public DateTime? Erstellungsdatum { get => erstellungsdatum; set => erstellungsdatum = value; }
        public string BezugRechnungsnummer { get => bezugRechnungsnummer; set => bezugRechnungsnummer = value; }
        public string Korrektur { get => korrektur; set => korrektur = value; }
        public List<Leistung> Leistungen { get => leistungen; set => leistungen = value; }
    }
}