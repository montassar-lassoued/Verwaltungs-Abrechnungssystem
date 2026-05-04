namespace FCC_Verwaltungssystem
{
    public class Leistung
    {
        private int id;
        private int position;
        private string bezeichnung;
        private string einheit;
        private int menge;
        private int steuersatz;
        private float brutto;
        private float steuer;
        private float netto;
        private string beschreibung;

        public int Id { get => id; set => id = value; }
        public string Einheit { get => einheit; set => einheit = value; }
        public int Menge { get => menge; set => menge = value; }
        public int Steuersatz { get => steuersatz; set => steuersatz = value; }
        public float Brutto { get => brutto; set => brutto = value; }
        public float Steuer { get => steuer; set => steuer = value; }
        public float Netto { get => netto; set => netto = value; }
        public string Beschreibung { get => beschreibung; set => beschreibung = value; }
        public string Bezeichnung { get => bezeichnung; set => bezeichnung = value; }
        public int Pos { get => position; set => position = value; }
    }
}