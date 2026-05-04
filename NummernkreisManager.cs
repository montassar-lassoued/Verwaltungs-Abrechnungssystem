using Serilog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class NummernkreisManager
    {
        public static string GetAktuelleRechnungsnummer()
        {
            // Rechnungsnummer generieren
            DataTable dataTable_rechnung = DataAccessLayer.Get_AktRechnungNummer();
            if (dataTable_rechnung == null || dataTable_rechnung.Rows.Count < 1)
            {
                MessageBox.Show("Fehler bei der Initialisierung.\n\nDie Rechnungsnummer konnte nicht generiert werden",
                    "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error("Rechnungsnummer konnte nicht generiert werden-> Eintrag in der Datenbank prüfen??");

                throw new Exception("Fehler bei der Initialisierung.\n\nDie Rechnungsnummer konnte nicht generiert werden");
            }
            else
            {
                DataRow row = dataTable_rechnung.Rows[0];
                string rechnungsnummer = (string)row["NUMMER"];
                object rechnungsjahr = row["JAHR"];
                if (rechnungsjahr == DBNull.Value)
                {
                    MessageBox.Show("Der Rechnungsnummer fehlt das Jahr.\n" + rechnungsjahr + " wird nun ergäntzt -> zB. 00001-" + rechnungsjahr);
                    DataAccessLayer.UpdateRechnungsnummerJAHR(Globals.FORMULAR_STORNO_RECHNUNG);
                    rechnungsjahr = DateTime.Now.Year;
                }
                if ((int)rechnungsjahr != DateTime.Now.Year)
                {
                    DialogResult result = MessageBox.Show("Das Jahr in der Rechnungsnummer passt nicht zum aktuellen Jahr." +
                        "\nAktuell ist" + rechnungsnummer + "-" + rechnungsjahr + ".\nDie Rechnungsnummer soll" + rechnungsnummer + DateTime.Now.Year + "." +
                        "Rechnungsdatum aktualisieren?", "Warnung!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataAccessLayer.UpdateRechnungsnummerJAHR(Globals.FORMULAR_STORNO_RECHNUNG);
                        rechnungsjahr = DateTime.Now.Year;
                    }

                }
                return rechnungsnummer + "-" + rechnungsjahr;
            }
        }
        public static string GetAktuelleStornoNummer()
        {
            DataTable dataTable_storno = DataAccessLayer.Get_AktStornoNummer();
            if (dataTable_storno == null || dataTable_storno.Rows.Count < 1)
            {
                MessageBox.Show("Fehler bei der Initialisierung.\n\nDie Stornonummer konnte nicht generiert werden",
                    "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Error("Stornonummer konnte nicht generiert werden-> Eintrag in der Datenbank prüfen??");

                throw new Exception("Fehler bei der Initialisierung.\n\nDie Stornonummer konnte nicht generiert werden");
            }
            else
            {
                DataRow row = dataTable_storno.Rows[0];
                string nummer = (string)row["NUMMER"];
                object jahr = row["JAHR"];
                if (jahr == DBNull.Value)
                {
                    MessageBox.Show("Der Stornonummer fehlt das Jahr.\n" + jahr + " wird nun ergäntzt -> zB. 00001-" + jahr);
                    DataAccessLayer.UpdateRechnungsnummerJAHR(Globals.FORMULAR_STORNO_RECHNUNG);
                    jahr = DateTime.Now.Year;
                }
                if ((int)jahr != DateTime.Now.Year)
                {
                    DialogResult result = MessageBox.Show("Das Jahr in der Stornonummer passt nicht zum aktuellen Jahr." +
                        "\nAktuell ist" + nummer + "-" + jahr + ".\nDie Stornonummer soll" + nummer + DateTime.Now.Year + "." +
                        "Stornorechnungsdatum aktualisieren?", "Warnung!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataAccessLayer.UpdateRechnungsnummerJAHR(Globals.FORMULAR_STORNO_RECHNUNG);
                        jahr = DateTime.Now.Year;
                    }

                }
                return nummer + "-" + jahr;
            }
        }
    }
}
