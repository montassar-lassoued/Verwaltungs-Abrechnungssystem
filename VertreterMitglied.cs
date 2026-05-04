using System;

namespace FCC_Verwaltungssystem
{
    public class VertreterMitglied
    {
        private int id;
        private string vorname;
        private string nachname;
        private DateTime? geburtsdatum;
        private string strasse;
        private string plz;
        private string ort;
        private string handy;

        public int Id { get => id; set => id = value; }
        public string Vorname { get => vorname; set => vorname = value; }
        public string Nachname { get => nachname; set => nachname = value; }
        public DateTime? Geburtsdatum { get => geburtsdatum; set => geburtsdatum = value; }
        public string Strasse { get => strasse; set => strasse = value; }
        public string Plz { get => plz; set => plz = value; }
        public string Ort { get => ort; set => ort = value; }
        public string Handy { get => handy; set => handy = value; }
    }
}