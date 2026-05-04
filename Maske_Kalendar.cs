using MyControls;
using System;
using System.Data;

namespace FCC_Verwaltungssystem
{
    public class Maske_Kalendar : MyFormCalendar
    {
        protected override string _name()
        {
            return "Kalendarübersicht";
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv dokument)
        {
            dokument.IdColumn = User.ID;
            dokument.TableName = "KALENDER";

            return dokument;
        }
        protected override void _OnLoad(EventArgs e)
        {
            DataTable dataTable = DataAccessLayer.queryKalenderEinstellungen();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                DataRow row = dataTable.Rows[0];
                Username = (string)row["USERNAME"];
                Mail = (string)row["EMAIL"];
                SecretPath = (string)row["SECRET_PATH"];
                Geloeschte_termine = (bool)row["GELOESCHTE_TERMINE"];
                Feiertage = (bool)row["FEIERTAGE"];
                I_feiertage = (bool)row["ISLAMISCHE_FEIERTAGE"];

                USER_RECHT_TERMIN_ERSTELLEN = User.Rechte.KALENDER_TERMIN_ERSTELLEN();
                USER_RECHT_TERMIN_BEARBEITEN = User.Rechte.KALENDER_TERMIN_BEARBEITEN();
                USER_RECHT_TERMIN_LOESCHEN = User.Rechte.KALENDER_TERMIN_LOESCHEN();

                PopulateCalendar();
            }
        }
    }
}