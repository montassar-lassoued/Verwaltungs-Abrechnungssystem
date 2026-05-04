using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControls
{
    public class Anmeldung
    {
        private static long id;
        private static string vorname;
        private static string nachname;
        private static string anmeldename;

        public static long Id { get => id; set => id = value; }
        public static string Vorname { get => vorname; set => vorname = value; }
        public static string Nachname { get => nachname; set => nachname = value; }
        public static string Anmeldename { get => anmeldename; set => anmeldename = value; }
    }
}
