using System;
using System.Collections.Generic;

namespace FCC_Verwaltungssystem
{
    public class RechnungHelper
    {
        private int id;
        private string rechnungsnummer;
        private int kunden_id;
        private string rehnungsbezeichnung;
        private DateTime? rErstellungsdatum;
        private DateTime? leistungsBeginn;
        private DateTime? leistungsEnde;
        private DateTime? rechnungsdatum;
        private DateTime? rDruckdatum;
        private Zahlungsstatus? zahlungsstatus;
        private DateTime? zahlungsdatum;
        private DateTime? zahlungsziel;
        private Prozessstatus? prozessstatus;
        private string notizen;
        private List<Leistung> rLeistungen;
        private string zahlungsart;
        private List<StornoRechnung> stornos;
        private string waehrung;

        public int Id { get => id; set => id = value; }
        public int Kunden_id { get => kunden_id; set => kunden_id = value; }
        public string Rechnungsbezeichnung { get => rehnungsbezeichnung; set => rehnungsbezeichnung = value; }
        public Prozessstatus? Prozessstatus { get => prozessstatus; set => prozessstatus = value; }
        public string Notizen { get => notizen; set => notizen = value; }
        public List<Leistung> Leistungen { get => rLeistungen; set => rLeistungen = value; }
        public string Rechnungsnummer { get => rechnungsnummer; set => rechnungsnummer = value; }
        public Zahlungsstatus? Zahlungsstatus { get => zahlungsstatus; set => zahlungsstatus = value; }
        public DateTime? Zahlungsdatum { get => zahlungsdatum; set => zahlungsdatum = value; }
        public DateTime? Zahlungsziel { get => zahlungsziel; set => zahlungsziel = value; }
        public DateTime? Erstellungsdatum { get => rErstellungsdatum; set => rErstellungsdatum = value; }
        public DateTime? LeistungsBeginn { get => leistungsBeginn; set => leistungsBeginn = value; }
        public DateTime? LeistungsEnde { get => leistungsEnde; set => leistungsEnde = value; }
        public DateTime? Rechnungsdatum { get => rechnungsdatum; set => rechnungsdatum = value; }
        public DateTime? Druckdatum { get => rDruckdatum; set => rDruckdatum = value; }
        public List<StornoRechnung> Stornos { get => stornos; set => stornos = value; }
        public string Zahlungsart { get => zahlungsart; set => zahlungsart = value; }
        public string Waehrung { get => waehrung; set => waehrung = value; }
    }
}
