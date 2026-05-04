namespace FCC_Verwaltungssystem
{
    public class EMailData
    {
        private string von;
        private string passwort;
        private string an;
        private string cc;
        private string bcc;
        private string betreff;
        private string body;
        private string anhang1;
        private string anhang1_Name;
        private string anhang2;
        private string anhang2_Name;
        private string anhang3;
        private string anhang3_Name;
        private string anhang4;
        private string anhang4_Name;
        private string anhang5;
        private string anhang5_Name;

        private string host;
        private int port;
        private bool ssl;
        private bool standard_info_verwenden;

        public string Von { get => von; set => von = value; }
        public string An { get => an; set => an = value; }
        public string Cc { get => cc; set => cc = value; }
        public string Bcc { get => bcc; set => bcc = value; }
        public string Betreff { get => betreff; set => betreff = value; }
        public string Body { get => body; set => body = value; }
        public string Anhang1 { get => anhang1; set => anhang1 = value; }
        public string Anhang1_Name { get => anhang1_Name; set => anhang1_Name = value; }
        public string Anhang2 { get => anhang2; set => anhang2 = value; }
        public string Anhang2_Name { get => anhang2_Name; set => anhang2_Name = value; }
        public string Anhang3 { get => anhang3; set => anhang3 = value; }
        public string Anhang3_Name { get => anhang3_Name; set => anhang3_Name = value; }
        public string Anhang4 { get => anhang4; set => anhang4 = value; }
        public string Anhang4_Name { get => anhang4_Name; set => anhang4_Name = value; }
        public string Anhang5 { get => anhang5; set => anhang5 = value; }
        public string Anhang5_Name { get => anhang5_Name; set => anhang5_Name = value; }
        public string Passwort { get => passwort; set => passwort = value; }
        public string Host { get => host; set => host = value; }
        public int Port { get => port; set => port = value; }
        public bool Ssl { get => ssl; set => ssl = value; }
        public bool Standard_info_verwenden { get => standard_info_verwenden; set => standard_info_verwenden = value; }
    }
}