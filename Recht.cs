namespace FCC_Verwaltungssystem
{
    public class Recht
    {
        private int id;
        private bool aktiv;
        private string beschreibung;

        public Recht(int _id, string recht, bool _aktiv)
        {
            Id = _id;
            Beschreibung = recht;
            Aktiv = _aktiv;
        }
        public bool Aktiv { get => aktiv; set => aktiv = value; }
        public string Beschreibung { get => beschreibung; set => beschreibung = value; }
        public int Id { get => id; set => id = value; }
    }
}