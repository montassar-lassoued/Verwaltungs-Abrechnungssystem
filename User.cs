using System.Data;

namespace FCC_Verwaltungssystem
{
    public class User
    {
        private static long id;
        private static string vorname;
        private static string nachname;
        private static string anmeldename;
        private static string passwort;
        private static int rolle;
        private static Rechte rechte;

        public User(long _id, string _anmeldename, string _passwort, int _rolle, string _vorname, string _nachname)
        {
            ID = _id;
            anmeldename = _anmeldename;
            passwort = _passwort;
            rolle = _rolle;
            Vorname = _vorname;
            Nachname = _nachname;
        }
        //public static string Name => name;

        public static string Passwort => passwort;

        public static int Rolle => rolle;

        public static string Anmeldename => anmeldename;

        public static long ID { get => id; set => id = value; }
        public static string Vorname { get => vorname; set => vorname = value; }
        public static string Nachname { get => nachname; set => nachname = value; }
        public static Rechte Rechte { get => rechte; set => rechte = value; }

        public static void addRechte(DataTable dataTable)
        {
            Rechte = new Rechte();
            Rechte.initialize(dataTable, Rolle);
        }
    }
}