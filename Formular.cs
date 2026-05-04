namespace FCC_Verwaltungssystem
{
    public class Formular
    {
        private long user_id;
        private string name;
        private string kopfttext;
        private string fusstext;
        private string zahlungszieltext;
        private string zusatztext;
        private string emailBody;
        private bool bMwst;
        private string speicherOrdner;
        public Formular(string formular_name)
        {
            Name = formular_name;
        }

        public string Name { get => name; set => name = value; }
        public string Kopfttext { get => kopfttext; set => kopfttext = value; }
        public string Fusstext { get => fusstext; set => fusstext = value; }
        public string Zusatztext { get => zusatztext; set => zusatztext = value; }
        public string Zahlungszieltext { get => zahlungszieltext; set => zahlungszieltext = value; }
        public string EmailBody { get => emailBody; set => emailBody = value; }
        public bool BMwst { get => bMwst; set => bMwst = value; }
        public string SpeicherOrdner { get => speicherOrdner; set => speicherOrdner = value; }
        public long User_id { get => user_id; set => user_id = value; }
    }
}