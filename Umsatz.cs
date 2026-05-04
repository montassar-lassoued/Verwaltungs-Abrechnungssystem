namespace FCC_Verwaltungssystem
{
    public class Umsatz
    {
        private bool typAlle;
        private bool typRechnung;
        private bool typStorno;
        private bool statusAlle;
        private bool statusBezahlt;
        private bool statusOffen;
        private bool inklKurse;
        private bool inklVertraege;
        private string datumVon;
        private string datumBis;

        public bool TypAlle { get => typAlle; set => typAlle = value; }
        public bool TypRechnung { get => typRechnung; set => typRechnung = value; }
        public bool TypStorno { get => typStorno; set => typStorno = value; }
        public bool StatusAlle { get => statusAlle; set => statusAlle = value; }
        public bool StatusBezahlt { get => statusBezahlt; set => statusBezahlt = value; }
        public bool StatusOffen { get => statusOffen; set => statusOffen = value; }
        public bool InklKurse { get => inklKurse; set => inklKurse = value; }
        public bool InklVertraege { get => inklVertraege; set => inklVertraege = value; }
        public string DatumVon { get => datumVon; set => datumVon = value; }
        public string DatumBis { get => datumBis; set => datumBis = value; }
    }
}