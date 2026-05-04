using System;

namespace FCC_Verwaltungssystem
{
    public class Kunde
    {
        private int id;
        private string anrede;
        private string name;
        private string ansprechpartner;
        private DateTime? geburtsdatum;
        private string strasse;
        private string plz;
        private string ort;
        private string land;
        private string handy;
        private string email;
        private string beschreibung;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Ansprechpartner { get => ansprechpartner; set => ansprechpartner = value; }
        public DateTime? Geburtsdatum { get => geburtsdatum; set => geburtsdatum = value; }
        public string Strasse { get => strasse; set => strasse = value; }
        public string Plz { get => plz; set => plz = value; }
        public string Ort { get => ort; set => ort = value; }
        public string Land { get => land; set => land = value; }
        public string Handy { get => handy; set => handy = value; }
        public string Email { get => email; set => email = value; }
        public string Beschreibung { get => beschreibung; set => beschreibung = value; }
        public string Anrede { get => anrede; set => anrede = value; }
    }
}