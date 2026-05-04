namespace FCC_Verwaltungssystem
{
    public class Kostenzeitraum
    {
        private string datum_von;
        private string datum_bis;
        private float betrag;

        public string Datum_von { get => datum_von; set => datum_von = value; }
        public string Datum_bis { get => datum_bis; set => datum_bis = value; }
        public float Betrag { get => betrag; set => betrag = value; }
    }
}