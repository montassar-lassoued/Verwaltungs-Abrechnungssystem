namespace FCC_Verwaltungssystem
{
    public class Kurs
    {
        private int id;
        private string name;
        private float? betrag;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public float? Betrag { get => betrag; set => betrag = value; }
    }
}