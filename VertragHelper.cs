namespace FCC_Verwaltungssystem
{
    public class VertragHelper
    {
        private int id;
        private string vorname;
        private string nachname;
        private string geburtsdatum;
        private string strasse;
        private string plz;
        private string ort;
        private string handy;
        private string geschlecht;
        private bool miderjaehrige;
        private string vertragsbeginn;
        private string status;
        private string kursname;
        private string betrag;
        private string anmerkung;
        private KategorieVertrag kategorie;
        private bool suche_nach_Vertreter_activ;

        public int Id { get => id; set => id = value; }
        public string Vorname { get => vorname; set => vorname = value; }
        public string Nachname { get => nachname; set => nachname = value; }
        public string Geburtsdatum { get => geburtsdatum; set => geburtsdatum = value; }
        public string Strasse { get => strasse; set => strasse = value; }
        public string Plz { get => plz; set => plz = value; }
        public string Ort { get => ort; set => ort = value; }
        public string Handy { get => handy; set => handy = value; }
        public string Geschlecht { get => geschlecht; set => geschlecht = value; }
        public bool Miderjaehrige { get => miderjaehrige; set => miderjaehrige = value; }
        public string Vertragsbeginn { get => vertragsbeginn; set => vertragsbeginn = value; }
        public string Status { get => status; set => status = value; }
        public string Kursname { get => kursname; set => kursname = value; }
        public string Betrag { get => betrag; set => betrag = value; }
        public string Anmerkung { get => anmerkung; set => anmerkung = value; }
        public bool Suche_nach_Vertreter_activ { get => suche_nach_Vertreter_activ; set => suche_nach_Vertreter_activ = value; }
        public KategorieVertrag Kategorie { get => kategorie; set => kategorie = value; }
    }
}