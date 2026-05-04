using System;
using System.Collections.Generic;

namespace FCC_Verwaltungssystem
{
    public class Vertrag
    {
        private int id;
        private DateTime? beginn;
        private StatusVertrag? status;
        private DateTime? status_geaendert_am;
        private List<Kurs> kurs;
        private string anmerkung;
        private KategorieVertrag? kategorie;
        private Mitglied mitglied;

        public int Id { get => id; set => id = value; }
        public DateTime? Beginn { get => beginn; set => beginn = value; }
        public StatusVertrag? Status { get => status; set => status = value; }
        public DateTime? Status_geaendert_am { get => status_geaendert_am; set => status_geaendert_am = value; }
        public List<Kurs> Kurse { get => kurs; set => kurs = value; }
        public string Anmerkung { get => anmerkung; set => anmerkung = value; }
        public Mitglied Mitglied { get => mitglied; set => mitglied = value; }
        public KategorieVertrag? Kategorie { get => kategorie; set => kategorie = value; }
    }
}