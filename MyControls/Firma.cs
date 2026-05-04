namespace MyControls
{
    public class Firma
    {
        private static string name;
        private static string adresse;
        private static string g_Fuehrer;
        private static string webseite;
        private static string mail;
        private static string tel;
        private static string umsatzsteuerID;
        private static string bankbezeichnung;
        private static string kontoinhaber;
        private static string iban;
        private static string bic;

        public static string Name { get => name; set => name = value; }
        public static string Adresse { get => adresse; set => adresse = value; }
        public static string G_Fuehrer { get => g_Fuehrer; set => g_Fuehrer = value; }
        public static string Webseite { get => webseite; set => webseite = value; }
        public static string Mail { get => mail; set => mail = value; }
        public static string Tel { get => tel; set => tel = value; }
        public static string UmsatzsteuerID { get => umsatzsteuerID; set => umsatzsteuerID = value; }
        public static string Bankbezeichnung { get => bankbezeichnung; set => bankbezeichnung = value; }
        public static string Kontoinhaber { get => kontoinhaber; set => kontoinhaber = value; }
        public static string Iban { get => iban; set => iban = value; }
        public static string Bic { get => bic; set => bic = value; }
    }
}
