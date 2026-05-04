using MyControls;
using System;

namespace FCC_Verwaltungssystem
{
    public class SqlHelper
    {
        /****** Query *********************/
        public static string getSql_QueryVertrag(Vertrag vertrag)
        {
            Mitglied mitglied = vertrag.Mitglied;
            VertreterMitglied vertreterMitglied = mitglied.Vertreter;
            string conditions = "VERTRAG.ID IS NOT NULL";

            if (vertreterMitglied != null)
            {
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Vorname))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Vorname", vertreterMitglied.Vorname);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Nachname))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Nachname", vertreterMitglied.Nachname);
                }
                if (vertreterMitglied.Geburtsdatum != null)
                {
                    conditions = buildDateWhere(conditions, "MITGVERTRETERLIED.Geburtsdatum", "=", vertreterMitglied.Geburtsdatum);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Strasse))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Strasse", vertreterMitglied.Strasse);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Plz))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.PLZ", vertreterMitglied.Plz);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Ort))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Ort", vertreterMitglied.Ort);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Handy))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Handy", vertreterMitglied.Handy);
                }
            }

            if (!string.IsNullOrWhiteSpace(mitglied.Vorname))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Vorname", mitglied.Vorname);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Nachname))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Nachname", mitglied.Nachname);
            }
            if (mitglied.Geburtsdatum != null)
            {
                conditions = buildDateWhere(conditions, "MITGLIED.Geburtsdatum", "=", mitglied.Geburtsdatum);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Strasse))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Strasse", mitglied.Strasse);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Plz))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.PLZ", mitglied.Plz);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Ort))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Ort", mitglied.Ort);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Handy))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Handy", mitglied.Handy);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Geschlecht))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Geschlecht", mitglied.Geschlecht);
            }
            if (mitglied.Miderjaehrige)
            {
                conditions = buildIntWhere(conditions, "MITGLIED.Minderjaehriger", 1);
            }
            if (vertrag.Kategorie != null)
            {
                conditions = buildStringWhere(conditions, "VERTRAG.Kategorie", vertrag.Kategorie.ToString());
            }
            string kurse = "";
            foreach (Kurs kurs in vertrag.Kurse)
            {
                if (!string.IsNullOrEmpty(kurse))
                {
                    kurse += ",";
                }
                kurse += "'" + kurs.Name + "'";
            }
            if (!string.IsNullOrWhiteSpace(kurse))
            {
                conditions += " AND KURS.NAME IN (" + kurse + ") ";
            }
            if (vertrag.Beginn != null)
            {
                conditions = buildDateWhere(conditions, "VERTRAG.Beginn", "=", vertrag.Beginn);
            }
            if (!string.IsNullOrWhiteSpace(vertrag.Anmerkung))
            {
                conditions = buildStringWhere(conditions, "VERTRAG.Anmerkung", vertrag.Anmerkung);
            }
            if (vertrag.Status != null)
            {
                conditions += " AND VERTRAG.STATUS IN (" + vertrag.Status + ")";
            }

            string sql = "SELECT VERTRAG.ID as ID " +
            ",MITGLIED.Vorname as Vorname" +
            ",MITGLIED.Nachname as Nachname" +
            ",MITGLIED.Geburtsdatum as Geburtsdatum" +
            ",MITGLIED.Strasse as Strasse" +
            ",MITGLIED.Plz as Plz" +
            ",MITGLIED.Ort as Ort" +
            ",MITGLIED.Handy as Handy" +
            ",MITGLIED.Geschlecht as Geschlecht" +
            ",MITGLIED.Minderjaehriger as Minderjaehriger" +
            ",VERTRAG.Beginn as Vertragsbeginn" +
            ",VERTRAG.Status as Status" +
            ",VERTRAG.Status_Am as Geandert_am" +
            ",VERTRAG.Kategorie as Kategorie" +
            ",VERTRETER.Vorname as Vertreter_Vorname" +
            ",VERTRETER.Nachname as Vertreter_Nachname" +
            ",VERTRETER.Geburtsdatum as Vertreter_Geburtsdatum" +
            ",VERTRETER.Strasse as Vertreter_Strasse" +
            ",VERTRETER.Plz as Vertreter_Plz" +
            ",VERTRETER.Ort as Vertreter_Ort" +
            ",VERTRETER.Handy as Vertreter_Handy" +
            ",VERTRAG.Anmerkung as Anmerkung " +
            ",MITGLIED.ID as MITGLIED_ID " +

            "FROM VERTRAG " +
            "LEFT JOIN MITGLIED ON (MITGLIED.ID = VERTRAG.MITGLIED_ID)" +
            "LEFT JOIN VERTRETER ON (MITGLIED.ID = VERTRETER.MITGLIED_ID)" +
            //"LEFT JOIN VERTRAG_KURS ON (VERTRAG.ID = VERTRAG_KURS.VERTRAG_ID) " +
            //"LEFT JOIN KURS ON (KURS.ID = VERTRAG_KURS.KURS_ID) " +
                "WHERE " + conditions;


            return sql;
        }
        public static string getSql_QueryVertragJournal(Vertrag vertrag)
        {
            Mitglied mitglied = vertrag.Mitglied;
            VertreterMitglied vertreterMitglied = mitglied.Vertreter;
            string conditions = "VERTRAG.ID IS NOT NULL";

            if (vertreterMitglied != null)
            {
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Vorname))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Vorname", vertreterMitglied.Vorname);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Nachname))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Nachname", vertreterMitglied.Nachname);
                }
                if (vertreterMitglied.Geburtsdatum != null)
                {
                    conditions = buildDateWhere(conditions, "MITGVERTRETERLIED.Geburtsdatum", "=", vertreterMitglied.Geburtsdatum);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Strasse))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Strasse", vertreterMitglied.Strasse);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Plz))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.PLZ", vertreterMitglied.Plz);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Ort))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Ort", vertreterMitglied.Ort);
                }
                if (!string.IsNullOrWhiteSpace(vertreterMitglied.Handy))
                {
                    conditions = buildStringWhere(conditions, "VERTRETER.Handy", vertreterMitglied.Handy);
                }
            }

            if (!string.IsNullOrWhiteSpace(mitglied.Vorname))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Vorname", mitglied.Vorname);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Nachname))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Nachname", mitglied.Nachname);
            }
            if (mitglied.Geburtsdatum != null)
            {
                conditions = buildDateWhere(conditions, "MITGLIED.Geburtsdatum", "=", mitglied.Geburtsdatum);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Strasse))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Strasse", mitglied.Strasse);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Plz))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.PLZ", mitglied.Plz);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Ort))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Ort", mitglied.Ort);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Handy))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Handy", mitglied.Handy);
            }
            if (!string.IsNullOrWhiteSpace(mitglied.Geschlecht))
            {
                conditions = buildStringWhere(conditions, "MITGLIED.Geschlecht", mitglied.Geschlecht);
            }
            if (mitglied.Miderjaehrige)
            {
                conditions = buildIntWhere(conditions, "MITGLIED.Minderjaehriger", 1);
            }
            if (vertrag.Kategorie.HasValue)
            {
                conditions = buildStringWhere(conditions, "VERTRAG.Kategorie", vertrag.Kategorie.ToString());
            }
            string kursnamen = "";
            string betrag = "";
            foreach (Kurs kurs in vertrag.Kurse)
            {
                if (!string.IsNullOrEmpty(kursnamen))
                {
                    kursnamen += ",";
                }
                if (!string.IsNullOrEmpty(betrag))
                {
                    betrag += ",";
                }
                kursnamen += "'" + kurs.Name + "'";
                betrag = kurs.Betrag != null ? "" : kurs.Betrag.ToString();
            }
            if (!string.IsNullOrWhiteSpace(kursnamen))
            {
                conditions += " AND KURS.NAME IN (" + kursnamen + ") ";
            }
            if (!string.IsNullOrWhiteSpace(betrag))
            {
                conditions += " AND KURS.BETRAG IN (" + betrag + ") ";
            }
            if (vertrag.Beginn != null)
            {
                conditions = buildDateWhere(conditions, "VERTRAG.Beginn", "=", vertrag.Beginn);
            }
            if (!string.IsNullOrWhiteSpace(vertrag.Anmerkung))
            {
                conditions = buildStringWhere(conditions, "VERTRAG.Anmerkung", vertrag.Anmerkung);
            }
            string status = "";
            var active = vertrag.Status?.HasFlag(StatusVertrag.ACTIVE);
            if (active == true)
            {
                status += "'" + StatusVertrag.ACTIVE + "'";
            }
            var gekuendigt = vertrag.Status?.HasFlag(StatusVertrag.GEKUENDIGT);
            if (gekuendigt == true)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",";
                }
                status += "'" + StatusVertrag.GEKUENDIGT + "'";
            }
            var pausiert = vertrag.Status?.HasFlag(StatusVertrag.PAUSIERT);
            if (pausiert == true)
            {
                if (!string.IsNullOrEmpty(status))
                {
                    status += ",";
                }
                status += "'" + StatusVertrag.PAUSIERT + "'";
            }
            if (!string.IsNullOrEmpty(status))
            {
                conditions += " AND VERTRAG.STATUS IN (" + status + ")";
            }

            string sql = "SELECT VERTRAG.ID as ID " +
            ",MITGLIED.Vorname as Vorname" +
            ",MITGLIED.Nachname as Nachname" +
            ",MITGLIED.Geburtsdatum as Geburtsdatum" +
            ",MITGLIED.Strasse as Strasse" +
            ",MITGLIED.Plz as Plz" +
            ",MITGLIED.Ort as Ort" +
            ",MITGLIED.Handy as Handy" +
            ",MITGLIED.Geschlecht as Geschlecht" +
            ",MITGLIED.Minderjaehriger as Minderjaehriger" +
            ",KURS.NAME as Kursart" +
            ",VERTRAG_KURS.BETRAG as Betrag" +
            ",VERTRAG.Beginn as Vertragsbeginn" +
            ",VERTRAG.Status as Status" +
            ",VERTRAG.Status_Am as Geandert_am" +
            ",VERTRAG.Kategorie as Kategorie" +
            ",VERTRETER.Vorname as Vertreter_Vorname" +
            ",VERTRETER.Nachname as Vertreter_Nachname" +
            ",VERTRETER.Geburtsdatum as Vertreter_Geburtsdatum" +
            ",VERTRETER.Strasse as Vertreter_Strasse" +
            ",VERTRETER.Plz as Vertreter_Plz" +
            ",VERTRETER.Ort as Vertreter_Ort" +
            ",VERTRETER.Handy as Vertreter_Handy" +
            ",VERTRAG.Anmerkung as Anmerkung " +
            ",MITGLIED.ID as MITGLIED_ID " +

            "FROM VERTRAG " +
            "LEFT JOIN MITGLIED ON (MITGLIED.ID = VERTRAG.MITGLIED_ID)" +
            "LEFT JOIN VERTRETER ON (MITGLIED.ID = VERTRETER.MITGLIED_ID)" +
            "LEFT JOIN VERTRAG_KURS ON (VERTRAG.ID = VERTRAG_KURS.VERTRAG_ID) " +
            "LEFT JOIN KURS ON (KURS.ID = VERTRAG_KURS.KURS_ID) " +
                "WHERE " + conditions;


            return sql;
        }
        public static string getSql_MitgliedByID(Mitglied mitglied)
        {
            string sql = "SELECT ID FROM MITGLIED WHERE ID ='" + mitglied.Id + "'";

            return sql;
        }
        public static string getSql_MitgliedByName(Mitglied mitglied)
        {
            string sql = "SELECT ID FROM MITGLIED WHERE VORNAME ='" + mitglied.Vorname + "'" +
                " AND NACHNAME = '" + mitglied.Nachname + "'" +
                 " AND STRASSE = '" + mitglied.Strasse + "'" +
                  " AND PLZ = '" + mitglied.Plz + "'" +
                   " AND ORT = '" + mitglied.Ort + "'" +
                   " AND MINDERJAEHRIGER = '" + Convert.ToInt32(mitglied.Miderjaehrige) + "'";

            return sql;
        }
        public static string getSql_VertreterByMitgliedID(Mitglied mitglied)
        {
            string sql = "SELECT * FROM VERTRETER WHERE MITGLIED_ID =" + mitglied.Id;
            return sql;
        }
        public static string getSql_QueryKrus()
        {
            string sql = "SELECT Id, Name, Betrag FROM KURS";

            return sql;
        }
        public static string getSql_KursByVertragID(int vertrag_ID)
        {
            string sql = "SELECT KURS.ID, KURS.NAME,VERTRAG_KURS.BETRAG " +
                "FROM KURS " +
                "LEFT JOIN VERTRAG_KURS ON (KURS.ID = VERTRAG_KURS.KURS_ID) " +
                "WHERE VERTRAG_KURS.VERTRAG_ID =" + vertrag_ID;

            return sql;
        }
        public static string getSql_VertragByKursID(int KursID)
        {
            string sql = "SELECT COUNT(*) FROM VERTRAG LEFT JOIN VERTRAG_KURS ON (VERTRAG.ID = VERTRAG_KURS.VERTRAG_ID) " +
                "WHERE VERTRAG_KURS.KURS_ID =" + KursID;

            return sql;
        }
        public static string getSql_KrusByName(string name)
        {
            string sql = "SELECT Id, Name, Betrag FROM KURS WHERE NAME= '" + name + "'";

            return sql;
        }
        public static string getSql_UserByName(string vorname, string nachname)
        {
            string sql = "SELECT * FROM _USER WHERE VORNAME= '" + vorname + "' AND NACHNAME = '" + nachname + "'";

            return sql;
        }
        public static string getSql_QueryUser()
        {
            string sql = "SELECT ID, VORNAME, NACHNAME, ANMELDENAME, PASSWORT, ROLLE, STATUS FROM _USER";
            return sql;
        }
        public static string getSql_QueryRechte()
        {
            string sql = "SELECT ID, RECHT FROM RECHT";
            return sql;
        }
        public static string getSql_QueryUserRechte(int user_id)
        {
            string sql = "SELECT ID, USER_ID, RECHT_ID FROM USER_RECHT WHERE USER_ID=" + user_id;
            return sql;
        }
        public static string getSql_KundeByName(string kundenname)
        {
            string sql = "SELECT * FROM KUNDE WHERE NAME LIKE '" + kundenname + "'";
            return sql;
        }
        public static string getSql_QueryKunde(Kunde kunde)
        {
            string conditions = "ID IS NOT NULL";
            if (!string.IsNullOrWhiteSpace(kunde.Anrede))
            {
                conditions = buildStringWhere(conditions, "ANREDE", kunde.Anrede);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Name))
            {
                conditions = buildStringWhere(conditions, "NAME", kunde.Name);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Ansprechpartner))
            {
                conditions = buildStringWhere(conditions, "ANSPRECHPARTNER", kunde.Ansprechpartner);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Strasse))
            {
                conditions = buildStringWhere(conditions, "STRASSE", kunde.Strasse);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Plz))
            {
                conditions = buildStringWhere(conditions, "PLZ", kunde.Plz);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Ort))
            {
                conditions = buildStringWhere(conditions, "ORT", kunde.Ort);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Land))
            {
                conditions = buildStringWhere(conditions, "LAND", kunde.Land);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Ort))
            {
                conditions = buildStringWhere(conditions, "HANDY", kunde.Handy);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Ort))
            {
                conditions = buildStringWhere(conditions, "EMAIL", kunde.Email);
            }
            if (!string.IsNullOrWhiteSpace(kunde.Beschreibung))
            {
                conditions = buildStringWhere(conditions, "BESCHREIBUNG", kunde.Beschreibung);
            }
            if (!kunde.Geburtsdatum.Equals(null))
                conditions = buildDateWhere(conditions, "GEBURTSDATUM", "=", kunde.Geburtsdatum);

            string sql = "SELECT * FROM KUNDE WHERE " + conditions;
            return sql;
        }
        public static string getSql_QueryRechnungsnummerkreis()
        {
            string sql = "SELECT NUMMER, JAHR FROM NUMMERKREIS WHERE NAME = '" + Globals.FORMULAR_RECHNUNG + "'";
            return sql;
        }
        public static string getSql_QuerySTORNORechnungsnummerkreis()
        {
            string sql = "SELECT NUMMER, JAHR FROM NUMMERKREIS WHERE NAME = '" + Globals.FORMULAR_STORNO_RECHNUNG + "'";
            return sql;
        }
        public static string getSql_QueryRechnungByNummer(string rechnungsnummer)
        {
            string sql = "SELECT KUNDE.ID,KUNDE.ANREDE,KUNDE.NAME,KUNDE." +
                "ANSPRECHPARTNER,KUNDE.GEBURTSDATUM,KUNDE.STRASSE,KUNDE.PLZ," +
                "KUNDE.ORT,KUNDE.LAND,KUNDE.HANDY,KUNDE.EMAIL,KUNDE.BESCHREIBUNG," +
                "RECHNUNG.ID AS RECHNUNGID ,RECHNUNG.KUNDEN_ID,RECHNUNG.RECHNUNGSNUMMER," +
                "RECHNUNG.KURZBEZEICHNUNG AS R_BEZEICHNUNG,RECHNUNG.ERSTLLUNGDATUM AS ERSTELLUNGSDATUM," +
                "RECHNUNG.LEISTUNGSBEGINN AS LEISTUNGSBEGINN,RECHNUNG.LEISTUNGSENDE AS LEISTUNGSENDE," +
                "RECHNUNG.RECHNUNGSDATUM,RECHNUNG.DRUCKDATUM,RECHNUNG.BEZAHLT,RECHNUNG.BEZAHLT_AM,RECHNUNG.ZAHLUNGSART AS ZAHLUNGSART," +
                "RECHNUNG.ZAHLUNGSZIEL,RECHNUNG.STATUS,RECHNUNG.NOTIZEN,RECHNUNG.WAEHRUNG " +
                "FROM RECHNUNG LEFT JOIN KUNDE ON (KUNDE.ID = RECHNUNG.KUNDEN_ID) " +
                "WHERE RECHNUNG.RECHNUNGSNUMMER = '" + rechnungsnummer + "'";
            return sql;
        }
        public static string getSql_QueryStornoRechnungByNummer(string rechnungsnummer)
        {
            string sql = "SELECT KUNDE.ID,KUNDE.ANREDE,KUNDE.NAME,KUNDE." +
                "ANSPRECHPARTNER,KUNDE.GEBURTSDATUM,KUNDE.STRASSE,KUNDE.PLZ," +
                "KUNDE.ORT,KUNDE.LAND,KUNDE.HANDY,KUNDE.EMAIL,KUNDE.BESCHREIBUNG," +
                "RECHNUNG.ID AS RECHNUNGID ,RECHNUNG.KUNDEN_ID,RECHNUNG.RECHNUNGSNUMMER AS BEZUG_RECHNUNG," +
                "RECHNUNG.KURZBEZEICHNUNG AS R_BEZEICHNUNG,RECHNUNG.ERSTLLUNGDATUM AS ERSTELLUNGSDATUM," +
                "RECHNUNG.LEISTUNGSBEGINN AS LEISTUNGSBEGINN,RECHNUNG.LEISTUNGSENDE AS LEISTUNGSENDE ," +
                "RECHNUNG.RECHNUNGSDATUM,RECHNUNG.DRUCKDATUM,RECHNUNG.BEZAHLT,RECHNUNG.BEZAHLT_AM," +
                "RECHNUNG.ZAHLUNGSZIEL,RECHNUNG.STATUS,RECHNUNG.NOTIZEN, RECHNUNG.WAEHRUNG, " +
                "STORNO_RECHNUNG.RECHNUNGSNUMMER AS STORNO_RECHNUNGNR " +
                ",STORNO_RECHNUNG.BEZUG_RECHNUNG " +
                ",STORNO_RECHNUNG.KURZBEZEICHNUNG AS STORNO_KURZBEZ " +
                ",STORNO_RECHNUNG.RECHNUNGSDATUM AS STORNO_DATUM" +
                ",STORNO_RECHNUNG.ERSTELLUNGSDATUM AS STORNO_ERSTELLUNG" +
                ",STORNO_RECHNUNG.KORREKTUR " +
                "FROM STORNO_RECHNUNG LEFT JOIN RECHNUNG  ON (STORNO_RECHNUNG.BEZUG_RECHNUNG = RECHNUNG.RECHNUNGSNUMMER) " +
                "LEFT JOIN KUNDE ON (KUNDE.ID = RECHNUNG.KUNDEN_ID) " +
                "WHERE STORNO_RECHNUNG.RECHNUNGSNUMMER = '" + rechnungsnummer + "'";
            return sql;
        }
        public static string getSql_QueryKundeRechnung(RechnungHelper rechnung)
        {
            string conditions = "RECHNUNG.ID IS NOT NULL";
            if (!string.IsNullOrWhiteSpace(rechnung.Rechnungsnummer))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.RECHNUNGSNUMMER", rechnung.Rechnungsnummer);
            }
            if (!string.IsNullOrWhiteSpace(rechnung.Rechnungsbezeichnung))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.KURZBEZEICHNUNG", rechnung.Rechnungsbezeichnung);
            }
            if (rechnung.Erstellungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.ERSTLLUNGDATUM", rechnung.Erstellungsdatum.ToString());
            }
            if (rechnung.LeistungsBeginn != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.LEISTUNGSBEGINN", rechnung.LeistungsBeginn.ToString());
            }
            if (rechnung.LeistungsEnde != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.LEISTUNGSENDE", rechnung.LeistungsEnde.ToString());
            }
            if (rechnung.Rechnungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.RECHNUNGSDATUM", rechnung.Rechnungsdatum.ToString());
            }
            if (rechnung.Druckdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.DRUCKDATUM", rechnung.Druckdatum.ToString());
            }
            if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT", "1");
            }
            if (rechnung.Zahlungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.ZAHLUNGSZIEL", rechnung.Zahlungsdatum.ToString());
            }
            if (rechnung.Prozessstatus != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.STATUS", rechnung.Prozessstatus.ToString());
            }
            if (!string.IsNullOrWhiteSpace(rechnung.Notizen))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.NOTIZEN", rechnung.Notizen);
            }
            if (rechnung.Kunden_id > 0)
            {
                conditions = buildIntWhere(conditions, "RECHNUNG.KUNDEN_ID", rechnung.Kunden_id);
            }

            string sql = "SELECT RECHNUNG.RECHNUNGSNUMMER AS RECHNUNGSNUMMER," +
                "RECHNUNG.LEISTUNGSBEGINN AS LEISTUNGSBEGINN,RECHNUNG.LEISTUNGSENDE AS LEISTUNGSENDE" +
                ",RECHNUNG.RECHNUNGSDATUM AS RECHNUNGSDATUM," +
                "KUNDE.ANREDE AS ANREDE,KUNDE.NAME AS NAME," +
                "KUNDE.ANSPRECHPARTNER AS ANSPRECHPARTNER,KUNDE.GEBURTSDATUM AS GEBURTSDATUM," +
                "KUNDE.STRASSE AS STRASSE,KUNDE.PLZ AS PLZ,KUNDE.ORT AS ORT,KUNDE.LAND AS LAND," +
                "KUNDE.HANDY AS HANDY,KUNDE.EMAIL AS EMAIL,KUNDE.BESCHREIBUNG AS KUNDE_BESCHREIBUNG, KUNDE.ID AS KUNDENID," +
                "RECHNUNG.KURZBEZEICHNUNG AS R_BEZEICHNUNG,RECHNUNG.ERSTLLUNGDATUM AS ERSTELLUNGSDATUM," +
                "RECHNUNG.DRUCKDATUM AS DRUCKDATUM,RECHNUNG.BEZAHLT AS BEZAHLT,RECHNUNG.BEZAHLT_AM AS BEZAHLT_AM," +
                "RECHNUNG.ZAHLUNGSZIEL AS ZAHLUNGSZIEL,RECHNUNG.STATUS AS STATUS,RECHNUNG.NOTIZEN AS NOTIZEN," +
                "RECHNUNG.ID AS RECHNUNGID, RECHNUNG.ZAHLUNGSART AS ZAHLUNGSART, RECHNUNG.WAEHRUNG AS WAEHRUNG " +
                "FROM RECHNUNG LEFT JOIN KUNDE ON (RECHNUNG.KUNDEN_ID = KUNDE.ID) WHERE " + conditions;
            return sql;
        }
        public static string getSql_QueryLeistungenByRechnungsNummer(string rechnungsnummer)
        {
            string sql = "SELECT ID as ID, POS AS Pos,KURZBEZEICHNUNG AS Bezeichnung,EINHEIT AS Einheit," +
                "MENGE AS Menge,STEUERSATZ AS Steuersatz,CONVERT(DECIMAL(10,2), BRUTTO) AS Brutto," +
                "CONVERT(DECIMAL(10,2), STEUER) AS Steuer, CONVERT(DECIMAL(10,2), NETTO) AS Netto,BESCHREIBUNG AS Beschreibung " +
                "FROM LEISTUNGEN WHERE RECHNUNG_ID = (SELECT ID FROM RECHNUNG WHERE  RECHNUNGSNUMMER = '" + rechnungsnummer + "' )";
            return sql;
        }
        public static string getSql_QueryStornoLeistungenByRechnungsNummer(string stornoRechnungsnummer)
        {
            string sql = "SELECT ID as ID, POS AS Pos,KURZBEZEICHNUNG AS Bezeichnung,EINHEIT AS Einheit," +
                "MENGE AS Menge,STEUERSATZ AS Steuersatz,BRUTTO AS Brutto,STEUER AS Steuer," +
                "NETTO AS Netto,BESCHREIBUNG AS Beschreibung " +
                "FROM STORNO_LEISTUNGEN WHERE STORNO_RECHNUNG_ID = (SELECT ID FROM STORNO_RECHNUNG WHERE  RECHNUNGSNUMMER = '" + stornoRechnungsnummer + "' )";
            return sql;
        }
        public static string getSql_QueryKunden()
        {
            string sql = "SELECT * FROM KUNDE";
            return sql;
        }
        internal static string getSql_QueryKundeByRechnung(RechnungHelper rechnung)
        {
            string conditions = "RECHNUNG.ID IS NOT NULL";
            if (!string.IsNullOrWhiteSpace(rechnung.Rechnungsnummer))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.RECHNUNGSNUMMER", rechnung.Rechnungsnummer);
            }
            if (rechnung.LeistungsBeginn != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.LEISTUNGSBEGINN", rechnung.LeistungsBeginn.ToString());
            }
            if (rechnung.LeistungsEnde != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.LEISTUNGSENDE", rechnung.LeistungsEnde.ToString());
            }
            if (rechnung.Rechnungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.RECHNUNGSDATUM", rechnung.Rechnungsdatum.ToString());
            }
            if(((rechnung.Zahlungsstatus & (Zahlungsstatus.BEZAHLT & Zahlungsstatus.OFFEN)) != 0))
            {

            }
            else if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT", "1");
            }
            else if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.OFFEN))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT", "0");
            }
            if (rechnung.Kunden_id > 0)
            {
                conditions = buildIntWhere(conditions, "RECHNUNG.KUNDEN_ID", rechnung.Kunden_id);
            }

            string sql = "SELECT KUNDE.ID AS ID,KUNDE.ANREDE AS ANREDE,KUNDE.NAME AS NAME" +
                ",KUNDE.ANSPRECHPARTNER AS ANSPRECHPARTNER,KUNDE.GEBURTSDATUM AS GEBURTSDATUM," +
                "KUNDE.STRASSE AS STRASSE,KUNDE.PLZ AS PLZ,KUNDE.ORT AS ORT,KUNDE.LAND AS LAND," +
                "KUNDE.HANDY AS HANDY,KUNDE.EMAIL AS EMAIL,KUNDE.BESCHREIBUNG AS KUNDE_BESCHREIBUNG " +
                "FROM KUNDE WHERE KUNDE.ID IN (SELECT RECHNUNG.KUNDEN_ID " +
                "FROM RECHNUNG WHERE RECHNUNG.KUNDEN_ID = KUNDE.ID AND " + conditions+")";
            return sql;
        }
        public static string getSql_QueryRechnung(RechnungHelper rechnung)
        {
            string conditions = "RECHNUNG.ID IS NOT NULL";
            if (!string.IsNullOrWhiteSpace(rechnung.Rechnungsnummer))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.RECHNUNGSNUMMER", rechnung.Rechnungsnummer);
            }
            if (!string.IsNullOrWhiteSpace(rechnung.Rechnungsbezeichnung))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.KURZBEZEICHNUNG", rechnung.Rechnungsbezeichnung);
            }
            if (rechnung.Erstellungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.ERSTLLUNGDATUM", rechnung.Erstellungsdatum.ToString());
            }
            if (rechnung.LeistungsBeginn != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.LEISTUNGSBEGINN", rechnung.LeistungsBeginn.ToString());
            }
            if (rechnung.LeistungsEnde != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.LEISTUNGSENDE", rechnung.LeistungsEnde.ToString());
            }
            if (rechnung.Rechnungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.RECHNUNGSDATUM", rechnung.Rechnungsdatum.ToString());
            }
            if (rechnung.Druckdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.DRUCKDATUM", rechnung.Druckdatum.ToString());
            }
            if ((rechnung.Zahlungsstatus & (Zahlungsstatus.BEZAHLT & Zahlungsstatus.OFFEN)) != 0)
            {

            }
            else if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT", "1");
            }
            else if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.OFFEN))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT", "0");
            }
            if (rechnung.Zahlungsdatum != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT_AM", rechnung.Zahlungsdatum.ToString());
            }
            if (rechnung.Prozessstatus != null)
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT_AM", rechnung.Prozessstatus.ToString());
            }
            if (!string.IsNullOrWhiteSpace(rechnung.Notizen))
            {
                conditions = buildStringWhere(conditions, "RECHNUNG.BEZAHLT_AM", rechnung.Notizen);
            }
            if (rechnung.Kunden_id > 0)
            {
                conditions = buildIntWhere(conditions, "RECHNUNG.KUNDEN_ID", rechnung.Kunden_id);
            }

            string sql = "SELECT RECHNUNG.RECHNUNGSNUMMER AS Rechnungsnummer," +
                "RECHNUNG.KURZBEZEICHNUNG AS Bezeichnung,RECHNUNG.ERSTLLUNGDATUM AS Erstellungsdatum," +
                "RECHNUNG.LEISTUNGSBEGINN AS Leistungsbeginn,RECHNUNG.LEISTUNGSENDE AS Leistungsende,RECHNUNG.RECHNUNGSDATUM AS Rechnungsdatum," +
                "RECHNUNG.DRUCKDATUM AS Druckdatum,RECHNUNG.BEZAHLT AS Bezahlt,RECHNUNG.BEZAHLT_AM AS Bezahlt_am," +
                "RECHNUNG.ZAHLUNGSZIEL AS Zahlungsziel,RECHNUNG.STATUS AS Status,RECHNUNG.NOTIZEN AS Notizen " +
                "FROM RECHNUNG LEFT JOIN KUNDE ON (RECHNUNG.KUNDEN_ID = KUNDE.ID) WHERE " + conditions;
            return sql;
        }
        public static string getSql_QueryStammdaten()
        {
            string sql = "SELECT * FROM FIRMA";
            return sql;
        }
        public static string getSql_QueryFormular(Formular formular)
        {
            string sql = "SELECT * FROM FORMULAR WHERE NAME ='" + formular.Name + "' AND USER_ID=" + User.ID;
            return sql;
        }
        public static string getSql_QueryKalenderEinstellungen(long user_id)
        {
            string sql = "SELECT KONFIG_KALENDER.*, _USER.VORNAME+_USER.NACHNAME AS USERNAME " +
                "FROM KONFIG_KALENDER LEFT JOIN _USER ON (_USER.ID = KONFIG_KALENDER.USER_ID ) WHERE USER_ID=" + user_id;

            return sql;
        }
        public static string getSql_QueryMailEinstellungen(long user_id)
        {
            string sql = "SELECT * FROM KONFIG_MAIL WHERE USER_ID=" + user_id;

            return sql;
        }
        public static string getSql_QueryStornoRechnung(string bezugRechnungsnummer)
        {
            string sql = "SELECT RECHNUNGSNUMMER AS STORNONUMMER , POS AS Pos , " +
                "EINHEIT AS Einheit , MENGE AS Menge , STEUERSATZ AS MWst , BRUTTO AS Brutto, STEUER AS Steuer , NETTO AS Netto " +
                "FROM STORNO_RECHNUNG  " +
                "LEFT JOIN STORNO_LEISTUNGEN ON (STORNO_RECHNUNG.ID = STORNO_LEISTUNGEN.STORNO_RECHNUNG_ID) " +
                "WHERE STORNO_RECHNUNG.BEZUG_RECHNUNG ='" + bezugRechnungsnummer + "'";

            return sql;
        }
        public static string getSql_QueryStornoRechnung____________(string bezugRechnungsnummer)
        {
            string sql = "SELECT * FROM STORNO_RECHNUNG WHERE BEZUG_RECHNUNG ='" + bezugRechnungsnummer + "'";

            return sql;
        }
        public static string getSql_Query_STORNO_LeistungenBySTORNO_RechnungsNummer(string Rechnungsnummer)
        {
            string sql = "SELECT L.ID,L.KURZBEZEICHNUNG AS BEZEICHNUNG,L.EINHEIT,L.MENGE," +
                "L.STEUERSATZ,L.BRUTTO,L.STEUER,L.NETTO,L.BESCHREIBUNG,L.POS," +
                "CASE WHEN SL.POS IS NOT NULL " +
                "THEN 'STORNIERT' " +
                "ELSE 'AKTIV' " +
                "END AS STATUS " +
                "FROM LEISTUNGEN L " +
                "LEFT JOIN RECHNUNG R ON L.RECHNUNG_ID = R.ID " +
                "LEFT JOIN (  SELECT DISTINCT STORNO_LEISTUNGEN.POS, STORNO_LEISTUNGEN.STORNO_RECHNUNG_ID  " +
                "FROM STORNO_LEISTUNGEN) SL ON SL.STORNO_RECHNUNG_ID IN " +
                "( SELECT ID FROM STORNO_RECHNUNG SR WHERE SR.BEZUG_RECHNUNG = R.RECHNUNGSNUMMER ) " +
                "AND SL.POS = L.POS WHERE R.RECHNUNGSNUMMER = '" + Rechnungsnummer + "'";

            return sql;
        }
        public static string getSql_QueryUmsatz(Umsatz umsatz)
        {
            string sql = "";
            string where = "WHERE RECHNUNG.ID IS NOT NULL ";

            if (umsatz.TypAlle)
            {
                if (!string.IsNullOrEmpty(umsatz.DatumVon))
                {
                    where += " AND RECHNUNG.RECHNUNGSDATUM >='" + umsatz.DatumVon + "' ";
                }
                if (!string.IsNullOrEmpty(umsatz.DatumBis))
                {
                    where += " AND RECHNUNG.RECHNUNGSDATUM <='" + umsatz.DatumBis + "' ";
                }

                sql = "SELECT 'RECHNUNG' as Typ, RECHNUNGSNUMMER as Nummer,'        ' as Bezugsrechnung, RECHNUNGSDATUM as Datum, " +
                    "MIN(LEISTUNGSBEGINN) as Leistungsbeginn,MAX(LEISTUNGSENDE) as Leistungsende, " +
                    "ZAHLUNGSZIEL as Zahlungsziel, BEZAHLT as Bezahlt, " +
                    "SUM(NETTO) as Zwischensumme, SUM(STEUER) as Steuer, SUM(BRUTTO) as Brutto FROM  LEISTUNGEN " +
                    "LEFT JOIN RECHNUNG ON (RECHNUNG.ID = LEISTUNGEN.RECHNUNG_ID) " + where +
                    " GROUP BY  RECHNUNG_ID, RECHNUNG.RECHNUNGSNUMMER," +
                    " RECHNUNG.RECHNUNGSDATUM,RECHNUNG.BEZAHLT, ZAHLUNGSZIEL ";

                sql += " UNION ";

                sql += "SELECT 'STORNO' as Typ, STORNO_RECHNUNG.RECHNUNGSNUMMER as Nummer,BEZUG_RECHNUNG as Bezugsrechnung, " +
                    "STORNO_RECHNUNG.RECHNUNGSDATUM as Datum," + "NULL as Leistungsbeginn,NULL as Leistungsende, NULL as Zahlungsziel," +
                    "'False' as Bezahlt,SUM(NETTO) as Zwischensumme, " + "SUM(STEUER) as Steuer, " +
                    "SUM(BRUTTO) as Brutto" +
                    " FROM  STORNO_LEISTUNGEN " +
                    "LEFT JOIN STORNO_RECHNUNG ON (STORNO_RECHNUNG.ID = STORNO_LEISTUNGEN.STORNO_RECHNUNG_ID) " +
                    "LEFT JOIN RECHNUNG ON (RECHNUNG.RECHNUNGSNUMMER = STORNO_RECHNUNG.BEZUG_RECHNUNG) " +
                    where +
                    " GROUP BY  STORNO_RECHNUNG_ID, STORNO_RECHNUNG.RECHNUNGSNUMMER, " +
                    "STORNO_RECHNUNG.BEZUG_RECHNUNG, STORNO_RECHNUNG.RECHNUNGSDATUM";

                if (umsatz.InklVertraege)
                {
                    if (!string.IsNullOrEmpty(sql))
                    {
                        sql += " UNION ";
                    }
                    sql += "SELECT 'VERTRAG' as Typ, '---' as Nummer,'' as Bezugsrechnung, Beginn as Datum, " +
                        "CAST( DATEADD(day, 3 - DAY(GETDATE()), GETDATE()) as date) Leistungsbeginn, " +
                        "cast (DATEADD(month, ((YEAR(GETDATE()) - 1900) * 12) + MONTH(GETDATE()), -1) as date) Leistungsende" +
                        ", CAST( DATEADD(day, 3 - DAY(GETDATE()), GETDATE()) as date) Zahlungsziel," +
                        "'True' as Bezahlt," + "'0.00' as Zwischensumme, " +
                        "'0.00' as Steuer, VERTRAG_KURS.Betrag as Brutto " +
                        "FROM VERTRAG " +
                        "LEFT JOIN VERTRAG_KURS ON (VERTRAG_KURS.VERTRAG_ID = VERTRAG.ID) " +
                        //"LEFT JOIN KURS ON (VERTRAG_KURS.KURS_ID = KURS.ID)" +
                        "WHERE STATUS = 'ACTIVE' order by DATUM";
                }
            }
            else if (umsatz.TypRechnung)
            {
                if (umsatz.StatusBezahlt)
                {
                    where += "AND BEZAHLT = 1 ";
                }
                else if (umsatz.StatusOffen)
                {
                    where += "AND BEZAHLT = 0 ";
                }
                if (!string.IsNullOrEmpty(umsatz.DatumVon))
                {
                    where += " AND RECHNUNGSDATUM >='" + umsatz.DatumVon + "' ";
                }
                if (!string.IsNullOrEmpty(umsatz.DatumBis))
                {
                    where += " AND RECHNUNGSDATUM <='" + umsatz.DatumBis + "' ";
                }

                sql = "SELECT 'RECHNUNG' as Typ, RECHNUNGSNUMMER as Nummer, RECHNUNGSDATUM as Rechnungsdatum, " +
                    "MIN(LEISTUNGSBEGINN) as Leistungsbeginn,MAX(LEISTUNGSENDE) as Leistungsende, " +
                    "ZAHLUNGSZIEL as Zahlungsziel, BEZAHLT as Bezahlt, " +
                    "SUM(NETTO) as Zwischensumme, SUM(STEUER) as Steuer, SUM(BRUTTO) as Brutto FROM  LEISTUNGEN " +
                    "LEFT JOIN RECHNUNG ON (RECHNUNG.ID = LEISTUNGEN.RECHNUNG_ID) " + where +
                    " GROUP BY  RECHNUNG_ID, RECHNUNG.RECHNUNGSNUMMER," +
                    " RECHNUNG.RECHNUNGSDATUM,RECHNUNG.BEZAHLT, ZAHLUNGSZIEL ";
            }
            else if (umsatz.TypStorno)
            {
                //string where = " WHERE STORNO_LEISTUNGEN.ID IS NOT NULL ";
                if (!string.IsNullOrEmpty(umsatz.DatumVon))
                {
                    where += " AND (RECHNUNG.RECHNUNGSDATUM >='" + umsatz.DatumVon + "' ";
                    where += " OR STORNO_RECHNUNG.RECHNUNGSDATUM >='" + umsatz.DatumVon + "') ";
                }
                if (!string.IsNullOrEmpty(umsatz.DatumBis))
                {
                    where += " AND (RECHNUNG.RECHNUNGSDATUM <='" + umsatz.DatumBis + "' ";
                    where += " OR STORNO_RECHNUNG.RECHNUNGSDATUM <='" + umsatz.DatumBis + "') ";
                }
                sql += "SELECT 'STORNO' as Typ, STORNO_RECHNUNG.RECHNUNGSNUMMER as Nummer,BEZUG_RECHNUNG as Bezugsrechnung, " +
                    "STORNO_RECHNUNG.RECHNUNGSDATUM as Stornodatum,RECHNUNG.RECHNUNGSDATUM as Rechnungsdatum," +
                    "SUM(NETTO) as Zwischensumme, " +
                    "SUM(STEUER) as Steuer, SUM(BRUTTO) as Brutto FROM  STORNO_LEISTUNGEN " +
                    "LEFT JOIN STORNO_RECHNUNG ON (STORNO_RECHNUNG.ID = STORNO_LEISTUNGEN.STORNO_RECHNUNG_ID) " +
                    "LEFT JOIN RECHNUNG ON (RECHNUNG.RECHNUNGSNUMMER = STORNO_RECHNUNG.BEZUG_RECHNUNG) " +
                    where +
                    " GROUP BY  STORNO_RECHNUNG_ID, STORNO_RECHNUNG.RECHNUNGSNUMMER, STORNO_RECHNUNG.BEZUG_RECHNUNG, " +
                    "STORNO_RECHNUNG.RECHNUNGSDATUM, RECHNUNG.RECHNUNGSDATUM";
            }
            return sql;
        }
        internal static string getSql_UpdateBlacklistEintrag(string id, string name, string beschreibung)
        {
            string sql = "UPDATE BLACKLIST SET NAME='" + name + "', BESCHREIBUNG ='" + beschreibung + "' WHERE ID =" + id;
            return sql;
        }
        internal static string getSql_InsertBlacklistEintrag(string name, string beschreibung)
        {
            string sql = "INSERT INTO BLACKLIST (NAME,BESCHREIBUNG) VALUES ('" + name + "','" + beschreibung + "')";
            return sql;
        }
        public static string getSql_QueryKostenMonatlich(string von, string bis)
        {
            string sql = "declare @start date;set @start ='" + von + "'; declare @end date;set @end ='" + bis + "';" +
                "with T as ( " +
                "select @start as currentdate union all select DATEADD(MONTH,1,currentdate)" +
                " from T " +
                "where DATEADD(MONTH, 1,currentdate)<= @end" +
                ") " +
                "select isNull(BEZEICHNUNG,'--') AS BEZEICHNUNG, KOSTENART,INTERVALL,  CONCAT(DATENAME(MONTH,currentdate),'-',YEAR(currentdate)) as DATUM, ISNULL(SUM(BETRAG),0)as BETRAG " +
                "from T " +
                "LEFT JOIN KOSTEN_ZEITRAUM ON (currentdate between KOSTEN_ZEITRAUM.VON and KOSTEN_ZEITRAUM.BIS) " +
                "LEFT JOIN KOSTEN ON (KOSTEN.ID = KOSTEN_ZEITRAUM.KOSTEN_ID) " +
                "group by BEZEICHNUNG, currentdate, KOSTENART,INTERVALL " +
                " order by  YEAR(currentdate),MONTH(currentdate)" +
                "option(Maxrecursion 5000);";

            return sql;
        }
        public static string getSql_QueryKostenJaehrlich(string von, string bis)
        {
            string sql = "declare @start date;set @start ='" + von + "'; declare @end date;set @end ='" + bis + "';" +
                "with T as (" +
                "select @start as currentdate union all select DATEADD(MONTH,1,currentdate) " +
                "from T  " +
                "where DATEADD(YEAR, 1,currentdate)<= @end" +
                ") " +
                "select isNull(BEZEICHNUNG,'--') AS BEZEICHNUNG, KOSTENART,INTERVALL,  YEAR(currentdate) as DATUM, ISNULL(SUM(BETRAG),0)as BETRAG " +
                "from T " +
                "LEFT JOIN KOSTEN_ZEITRAUM  ON (YEAR(currentdate) between YEAR(KOSTEN_ZEITRAUM.VON) and YEAR(KOSTEN_ZEITRAUM.BIS)) " +
                "LEFT JOIN KOSTEN ON (KOSTEN.ID = KOSTEN_ZEITRAUM.KOSTEN_ID) " +
                "group by BEZEICHNUNG, YEAR(currentdate), KOSTENART,INTERVALL " +
                " order by  YEAR(currentdate)" +
                "option(Maxrecursion 5000);";

            return sql;
        }
        public static string getSql_QueryKosten()
        {
            string sql = "SELECT KOSTENART,INTERVALL,BEZEICHNUNG, FORMAT(VON, 'd', 'de-de') AS VON," +
                "FORMAT(BIS, 'd', 'de-de') AS BIS,BETRAG,KOSTEN.ID " +
                "FROM KOSTEN " +
                "LEFT JOIN KOSTEN_ZEITRAUM ON (KOSTEN.ID = KOSTEN_ZEITRAUM.KOSTEN_ID) " +
                "ORDER BY BEZEICHNUNG ASC";
            return sql;
        }
        public static string getSql_QueryKostenByID(long id)
        {
            string sql = "SELECT KOSTENART,INTERVALL,BEZEICHNUNG,VON,BIS,BETRAG " +
                "FROM KOSTEN " +
                "LEFT JOIN " +
                "KOSTEN_ZEITRAUM ON (KOSTEN.ID = KOSTEN_ZEITRAUM.KOSTEN_ID) " +
                "WHERE KOSTEN.ID = " + id + " " +
                "ORDER BY BEZEICHNUNG ASC";
            return sql;
        }
        public static string getSql_QueryBlacklist()
        {
            string sql = "SELECT * FROM BLACKLIST ";
            return sql;
        }
        /****** Insert-Befehle******************/
        public static string getSql_InsertVertrag(Vertrag vertrag)
        {
            string sql = "INSERT INTO Vertrag (Beginn,Status,Kategorie,Mitglied_ID,Anmerkung)" +
                " VALUES" +
                " ('" + vertrag.Beginn?.ToString("yyyy-MM-dd") + "'," +
                " '" + StatusVertrag.ACTIVE.ToString() + "'," +
                " '" + vertrag.Kategorie.ToString() + "'," +
                " '" + vertrag.Mitglied.Id + "'," +
                " '" + vertrag.Anmerkung + "')";

            return sql;
        }
        public static string getSql_InsertVertrag_Kurs(int vertrag_id, int kurs_id, float? betrag)
        {
            string sql = "INSERT INTO VERTRAG_KURS (VERTRAG_ID, KURS_ID,BETRAG)" +
                " VALUES" +
                " ('" + vertrag_id + "'," +
                " '" + kurs_id + "'," +
                "REPLACE('" + betrag + "',',','.'));";

            return sql;
        }
        public static string getSql_InsertMitglied(Mitglied mitglied)
        {
            string sql = "INSERT INTO MITGLIED (VORNAME,NACHNAME,GEBURTSDATUM,STRASSE,PLZ,ORT,HANDY,GESCHLECHT, MINDERJAEHRIGER)" +
                " VALUES" +
                " ('" + mitglied.Vorname + "'," +
                " '" + mitglied.Nachname + "'," +
                " '" + mitglied.Geburtsdatum?.ToString("yyyy-MM-dd") + "'," +
                " '" + mitglied.Strasse + "'," +
                " '" + mitglied.Plz + "'," +
                " '" + mitglied.Ort + "'," +
                " '" + mitglied.Handy + "'," +
                " '" + mitglied.Geschlecht + "'," +
                 " " + Convert.ToInt32(mitglied.Miderjaehrige) + ")";

            return sql;
        }
        public static string getSql_InsertVertreter(Mitglied mitglied)
        {
            string sql = "INSERT INTO VERTRETER (VORNAME,NACHNAME,GEBURTSDATUM,STRASSE,PLZ,ORT,HANDY,MITGLIED_ID)" +
                " VALUES" +
                " ('" + mitglied.Vertreter.Vorname + "'," +
                " '" + mitglied.Vertreter.Nachname + "'," +
                " '" + mitglied.Vertreter.Geburtsdatum?.ToString("yyyy-MM-dd") + "'," +
                " '" + mitglied.Vertreter.Strasse + "'," +
                " '" + mitglied.Vertreter.Plz + "'," +
                " '" + mitglied.Vertreter.Ort + "'," +
                " '" + mitglied.Vertreter.Handy + "'," +
                " '" + mitglied.Id + "')";

            return sql;
        }
        public static string getSql_InsertKursart(Kurs kurs)
        {
            string sql = "INSERT INTO KURS (NAME,BETRAG)" +
                " VALUES" +
                " ('" + kurs.Name + "'," +
                "REPLACE('" + kurs.Betrag + "',',','.'))";

            return sql;
        }
        public static string getSql_InsertUserRecht(int user_id, int recht_id)
        {
            string sql = "INSERT INTO USER_RECHT (USER_ID,RECHT_ID)" +
                " VALUES" +
                " ('" + user_id + "'," +
                " '" + recht_id + "')";

            return sql;
        }
        public static string getSql_InsertKunde(Kunde kunde)
        {
            string sql = " INSERT INTO KUNDE (ANREDE, NAME, ANSPRECHPARTNER ,GEBURTSDATUM, STRASSE, PLZ, ORT, LAND, HANDY, EMAIL, BESCHREIBUNG) VALUES " +
                "('" + kunde.Anrede + "','" + kunde.Name + "','" + kunde.Ansprechpartner + "','" + kunde.Geburtsdatum.ToString() + "'," +
                "'" + kunde.Strasse + "','" + kunde.Plz + "','" + kunde.Ort + "','" + kunde.Land + "','" + kunde.Handy + "'," +
                "'" + kunde.Email + "','" + kunde.Beschreibung + "')";

            return sql;
        }
        internal static string getSql_InsertRechnung(RechnungHelper rechnung)
        {
            string columns = "";
            string values = "";
            bool zahlungstatus = rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT) ? true : false;
            if (rechnung.Druckdatum != null)
            {
                columns += ",DRUCKDATUM";
                values += ",'" + rechnung.Druckdatum + "'";
            }
            if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT))
            {
                columns += ",BEZAHLT_AM";
                values += ",'" + rechnung.Zahlungsdatum + "'";
            }
            string sql = " INSERT INTO RECHNUNG (RECHNUNGSNUMMER,KUNDEN_ID,KURZBEZEICHNUNG,BEZAHLT,ZAHLUNGSART,STATUS," +
                "NOTIZEN,ERSTLLUNGDATUM,LEISTUNGSBEGINN,LEISTUNGSENDE,ZAHLUNGSZIEL,RECHNUNGSDATUM, WAEHRUNG" + columns + ") VALUES " +
                "('" + rechnung.Rechnungsnummer + "','" + rechnung.Kunden_id + "','" + rechnung.Rechnungsbezeichnung + "'," +
                "'" + zahlungstatus + "','" + rechnung.Zahlungsart + "','" + rechnung.Prozessstatus + "','" + rechnung.Notizen + "','" + rechnung.Erstellungsdatum + "'" +
                ",'" + rechnung.LeistungsBeginn + "','" + rechnung.LeistungsEnde + "','" + rechnung.Zahlungsziel + "'," +
                "'" + rechnung.Rechnungsdatum + "','" + rechnung.Waehrung + "'" + values + "); ";

            return sql;
        }
        internal static string getSql_InsertLeistungen(Leistung leistung, int rechnung_id)
        {
            string sql = "INSERT INTO LEISTUNGEN (POS,KURZBEZEICHNUNG,EINHEIT,MENGE,STEUERSATZ,BRUTTO,STEUER,NETTO,BESCHREIBUNG,RECHNUNG_ID) VALUES " +
                "('" + leistung.Pos + "','" + leistung.Bezeichnung + "','" + leistung.Einheit + "','" + leistung.Menge + "','" + leistung.Steuersatz + "'," +
                "REPLACE('" + leistung.Brutto + "',',','.')," +
                "REPLACE('" + leistung.Steuer + "',',','.')," +
                "REPLACE('" + leistung.Netto + "',',','.')," +
                "'" + leistung.Beschreibung + "','" + rechnung_id + "'); ";

            return sql;
        }
        internal static string getSql_InsertStornoRechnung(StornoRechnung stornoRechnung)
        {
            string sql = "INSERT INTO STORNO_RECHNUNG (RECHNUNGSNUMMER,BEZUG_RECHNUNG,KURZBEZEICHNUNG,RECHNUNGSDATUM,ERSTELLUNGSDATUM,KORREKTUR) VALUES" +
                "('" + stornoRechnung.StornoNummer + "','" + stornoRechnung.BezugRechnungsnummer + "','" + stornoRechnung.Kurzbez + "','" +
                stornoRechnung.StornoDatum + "','" + stornoRechnung.Erstellungsdatum + "','" + stornoRechnung.Korrektur + "' ); ";

            return sql;
        }
        internal static string getSql_InsertStornoLeistungen(Leistung leistung, int stornoRechnung_id)
        {
            string sql = "INSERT INTO STORNO_LEISTUNGEN (POS,KURZBEZEICHNUNG,EINHEIT,MENGE,STEUERSATZ,BRUTTO,STEUER,NETTO,BESCHREIBUNG,STORNO_RECHNUNG_ID) VALUES " +
                "('" + leistung.Pos + "','" + leistung.Bezeichnung + "','" + leistung.Einheit + "','" + leistung.Menge + "','" + leistung.Steuersatz + "'," +
                "REPLACE('" + leistung.Brutto + "',',','.')," +
                "REPLACE('" + leistung.Steuer + "',',','.')," +
                "REPLACE('" + leistung.Netto + "',',','.')," +
                "'" + leistung.Beschreibung + "','" + stornoRechnung_id + "'); ";

            return sql;
        }
        public static string getSql_InsertUser(string vorname, string nachname, string anmeldename, string passwort)
        {
            string sql = " INSERT INTO _USER (VORNAME, NACHNAME ,ANMELDENAME, PASSWORT, ROLLE) VALUES " +
                "('" + vorname + "','" + nachname + "','" + anmeldename + "','" + passwort + "','" + 2 + "')";

            return sql;

        }
        public static string getSql_InsertStammdaten()
        {
            string sql = "INSERT INTO FIRMA(NAME,ADRESSE,MAIL,TEL,WEBSEITE,GESCHAEFTSFUEHRER,UMSATZSTEUER_ID,BANK,KONTOINHABER,IBAN,BIC)" +
                " VALUES" +
                "('" + Firma.Name + "','" + Firma.Adresse + "','" + Firma.Mail + "','" + Firma.Tel + "','" + Firma.Webseite + "','" + Firma.G_Fuehrer + "" +
                "','" + Firma.UmsatzsteuerID + "','" + Firma.Bankbezeichnung + "','" + Firma.Kontoinhaber + "','" + Firma.Iban + "','" + Firma.Bic + "')";

            return sql;
        }
        public static string getSql_InsertFormular(Formular formular)
        {
            string sql = "INSERT INTO FORMULAR (NAME,KOPFTEXT,FUSSTEXT,ZAHLUNGSTEXT,ZUSATZTEXT,USER_ID)" +
                " VALUES " +
                "('" + formular.Name + "','" + formular.Kopfttext + "','" + formular.Fusstext + "','" + formular.Zahlungszieltext + "'," +
                "'" + formular.Zusatztext + "'," + User.ID + ")";

            return sql;
        }
        public static string getSql_InsertAblageFormular(Formular formular)
        {
            string sql = "INSERT INTO FORMULAR (NAME,ABLAGEORT,MWST,USER_ID)" +
                " VALUES " +
                "('" + formular.Name + "','" + formular.SpeicherOrdner + "','" + formular.BMwst + "'," + User.ID + ")";

            return sql;
        }
        public static string getSql_InsertKalenderEinstellungen(string mail, string path, bool geloeschte_termine,
            bool d_feiertage, bool i_feiertage, long user_id)
        {
            string sql = "INSERT INTO KONFIG_KALENDER (USER_ID,EMAIL,SECRET_PATH,GELOESCHTE_TERMINE,FEIERTAGE,ISLAMISCHE_FEIERTAGE) VALUES" +
                "(" + user_id + ",'" + mail + "','" + path + "'," + Convert.ToInt32(geloeschte_termine) + "," + Convert.ToInt32(d_feiertage) + "," + Convert.ToInt32(i_feiertage) + ")";

            return sql;
        }
        public static string getSql_Insert_MailEinstellungen(long user_id, string email_smtp, string passwort, int port_smtp,
            string host_smtp, bool ssl_smtp, bool sInfo_smtp, string email_outl, bool display_outl, string aktive_einst)
        {
            string sql = "INSERT INTO KONFIG_MAIL (USER_ID,EMAIL_SMTP,PASSWORT_SMTP,PORT,HOST,SSL,STANDARD_INFO,EMAIL_OUT,DISPLAY_OUT,AKTIV_EINST) " +
                "VALUES" +
                "(" + user_id + ",'" + email_smtp + "','" + passwort + "'," + port_smtp + ",'" + host_smtp + "'," + Convert.ToInt32(ssl_smtp) + "," +
                "" + Convert.ToInt32(sInfo_smtp) + ",'" + email_outl + "','" + Convert.ToInt32(display_outl) + "','" + aktive_einst + "')";

            return sql;
        }
        public static string getSql_InsertKosten(Kosten kosten)
        {
            string sql = " INSERT INTO KOSTEN (BEZEICHNUNG,KOSTENART,INTERVALL) VALUES " +
                "('" + kosten.Bezeichnung + "','" + kosten.Kostenart + "','" + kosten.Intervall + "')";

            return sql;
        }
        public static string getSql_InsertKostenzeitraum(Kostenzeitraum kostenzeitraum, int kosten_ID)
        {
            string sql = " INSERT INTO KOSTEN_ZEITRAUM (VON,BIS,BETRAG,KOSTEN_ID) VALUES " +
                "('" + kostenzeitraum.Datum_von + "','" + kostenzeitraum.Datum_bis + "',REPLACE('" + kostenzeitraum.Betrag + "',',','.'), '" + kosten_ID + "')";

            return sql;
        }
        /****** Update-Befehle***********************/
        public static string getSql_UpdateVertrag(Vertrag vertrag)
        {
            string sql = "UPDATE VERTRAG SET " +
                "BEGINN ='" + vertrag.Beginn?.ToString("yyyy-MM-dd") + "' ," +
                "KATEGORIE = '" + vertrag.Kategorie.ToString() + "', " +
                //"MITGLIED_ID ='" + vertrag.Mitglied.Id + "'," +
                "ANMERKUNG ='" + vertrag.Anmerkung + "'" +
                " WHERE ID =" + vertrag.Id;

            return sql;
        }
        public static string getSql_UpdateStatusVertrag(Vertrag vertrag)
        {
            string sql = "UPDATE VERTRAG SET " +
                "STATUS = '" + vertrag.Status + "'," +
                "STATUS_AM ='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                "WHERE ID =" + vertrag.Id;

            return sql;
        }
        internal static string getSql_UpdateKursart(Kurs kurs)
        {
            string sql = "UPDATE KURS SET " +
                "BETRAG = REPLACE('" + kurs.Betrag + "', ',', '.') " +
                "WHERE NAME ='" + kurs.Name + "'";

            return sql;
        }
        public static string getSql_UpdateMitglied(Mitglied mitglied)
        {
            string sql = "UPDATE MITGLIED SET " +
                "VORNAME ='" + mitglied.Vorname + "' ," +
                "NACHNAME = '" + mitglied.Nachname + "'," +
                "GEBURTSDATUM ='" + mitglied.Geburtsdatum?.ToString("yyyy-MM-dd") + "'," +
                "STRASSE = '" + mitglied.Strasse + "', " +
                "PLZ = '" + mitglied.Plz + "' ," +
                " ORT ='" + mitglied.Ort + "', " +
                "HANDY ='" + mitglied.Handy + "'," +
                "GESCHLECHT = '" + mitglied.Geschlecht + "', " +
                "MINDERJAEHRIGER = " + Convert.ToInt32(mitglied.Miderjaehrige) + " " +
                "WHERE ID =" + mitglied.Id;

            return sql;
        }
        public static string getSql_UpdateVertreter(VertreterMitglied vertreter)
        {
            string sql = "UPDATE VERTRETER SET " +
                "VORNAME ='" + vertreter.Vorname + "' ," +
                "NACHNAME = '" + vertreter.Nachname + "'," +
                "GEBURTSDATUM ='" + vertreter.Geburtsdatum?.ToString("yyyy-MM-dd") + "'," +
                "STRASSE = '" + vertreter.Strasse + "', " +
                "PLZ = '" + vertreter.Plz + "' ," +
                "ORT ='" + vertreter.Ort + "', " +
                "HANDY ='" + vertreter.Handy + "'" +
                "WHERE ID =" + vertreter.Id;

            return sql;
        }
        public static string getSql_UpdateKunde(Kunde kunde)
        {
            string sql = "UPDATE KUNDE SET ANREDE ='" + kunde.Anrede + "', NAME ='" + kunde.Name + "', ANSPRECHPARTNER='" + kunde.Ansprechpartner + "'," +
                " GEBURTSDATUM='" + kunde.Geburtsdatum.ToString() + "', STRASSE ='" + kunde.Strasse + "', PLZ ='" + kunde.Plz + "', ORT ='" + kunde.Ort + "'," +
                " LAND ='" + kunde.Land + "',  HANDY= '" + kunde.Handy + "', EMAIL ='" + kunde.Email + "', BESCHREIBUNG = '" + kunde.Beschreibung + "'" +
                "WHERE ID = " + kunde.Id;

            return sql;
        }
        public static string getSQL_UpdateRechnungsnummerkreis()
        {
            string sql = "UPDATE NUMMERKREIS SET NUMMER = RIGHT('" + Globals.KREIS_FORMAT_0 + "' + CONVERT(VARCHAR(" + Globals.KREIS_FORMAT_int + ")," +
                "+(CONVERT(INT, NUMMER) +1)), " + Globals.KREIS_FORMAT_int + "), JAHR =YEAR(GETDATE()) WHERE NAME ='" + Globals.FORMULAR_RECHNUNG + "'";

            return sql;
        }
        public static string getSQL_UpdateSTORNORechnungsnummerkreis()
        {
            string sql = "UPDATE NUMMERKREIS SET NUMMER = RIGHT('" + Globals.KREIS_FORMAT_0 + "' + CONVERT(VARCHAR(" + Globals.KREIS_FORMAT_int + ")," +
                "+(CONVERT(INT, NUMMER) +1)), " + Globals.KREIS_FORMAT_int + "), JAHR =YEAR(GETDATE()) WHERE NAME ='" + Globals.FORMULAR_STORNO_RECHNUNG + "'";

            return sql;
        }
        public static string getSQL_Rechnungsnummerkreis_Zuruecksetzen()
        {
            string sql = "UPDATE NUMMERKREIS SET NUMMER = RIGHT('" + Globals.KREIS_FORMAT_0 + "' + CONVERT(VARCHAR(" + Globals.KREIS_FORMAT_int + ")," +
                "+(1)), " + Globals.KREIS_FORMAT_int + "), JAHR =YEAR(GETDATE()) WHERE NAME ='" + Globals.FORMULAR_RECHNUNG + "'";

            return sql;
        }
        public static string getSQL_Stornonummerkreis_Zuruecksetzen()
        {
            string sql = "UPDATE NUMMERKREIS SET NUMMER = RIGHT('" + Globals.KREIS_FORMAT_0 + "' + CONVERT(VARCHAR(" + Globals.KREIS_FORMAT_int + ")," +
                "+(1)), " + Globals.KREIS_FORMAT_int + "), JAHR =YEAR(GETDATE()) WHERE NAME ='" + Globals.FORMULAR_STORNO_RECHNUNG + "'";

            return sql;
        }
        public static string getSQL_UpdateRechnungsnummerJAHR(string formular)
        {
            string sql = "UPDATE NUMMERKREIS SET JAHR = YEAR(GETDATE()) WHERE NAME ='" + Globals.FORMULAR_RECHNUNG + "'";

            return sql;
        }
        public static string getSql_UpdateRechnung(RechnungHelper rechnung)
        {
            string zahlungsdatum = "NULL";
            string druckdatum = "NULL";
            bool zahlungsstatus = rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT) ? true : false;
            if (rechnung.Zahlungsstatus.Equals(Zahlungsstatus.BEZAHLT))
            {
                zahlungsdatum = "'" + rechnung.Zahlungsdatum.GetValueOrDefault().Date + "'";

            }
            if (rechnung.Druckdatum != null)
            {
                druckdatum = "'" + rechnung.Druckdatum.GetValueOrDefault().Date + "'";
            }
            string sql = "UPDATE RECHNUNG SET  KURZBEZEICHNUNG = '" + rechnung.Rechnungsbezeichnung + "',LEISTUNGSBEGINN = '" + rechnung.LeistungsBeginn + "'," +
                "LEISTUNGSENDE = '" + rechnung.LeistungsEnde + "',RECHNUNGSDATUM = '" + rechnung.Rechnungsdatum + "',BEZAHLT = '" + zahlungsstatus + "" +
                "',BEZAHLT_AM = " + zahlungsdatum + ",ZAHLUNGSART = '" + rechnung.Zahlungsart + "'," +
                "ZAHLUNGSZIEL = '" + rechnung.Zahlungsziel + "',NOTIZEN = '" + rechnung.Notizen + "', WAEHRUNG ='" + rechnung.Waehrung + "'," +
                "KUNDEN_ID ='" + rechnung.Kunden_id + "', DRUCKDATUM =" + druckdatum + " WHERE RECHNUNGSNUMMER='" + rechnung.Rechnungsnummer + "'";

            return sql;
        }
        public static string getSql_UpdateRechnungStatus(string rechnungsnummer, Prozessstatus status)
        {
            string sql = "UPDATE RECHNUNG SET STATUS = '" + status + "',DRUCKDATUM = '" + DateTime.Now.Date + "' " +
                "WHERE RECHNUNGSNUMMER='" + rechnungsnummer + "'";

            return sql;
        }
        public static string getSql_UpadteLeistungsposition(Leistung leistung)
        {
            string sql = "UPDATE LEISTUNGEN SET POS = " + leistung.Pos + " WHERE ID = " + leistung.Id;

            return sql;
        }
        public static string getSql_UpdateStammdaten()
        {
            string sql = "UPDATE FIRMA SET NAME ='" + Firma.Name + "', ADRESSE = '" + Firma.Adresse + "', MAIL = '" + Firma.Mail + "'" +
                ", TEL = '" + Firma.Tel + "', WEBSEITE = '" + Firma.Webseite + "', GESCHAEFTSFUEHRER = '" + Firma.G_Fuehrer + "'" +
                ", UMSATZSTEUER_ID = '" + Firma.UmsatzsteuerID + "', BANK = '" + Firma.Bankbezeichnung + "', KONTOINHABER = '" + Firma.Kontoinhaber + "', " +
                "IBAN = '" + Firma.Iban + "', BIC = '" + Firma.Bic + "'";

            return sql;
        }
        public static string getSql_UpdateFormular(Formular formular)
        {
            string sql = "UPDATE FORMULAR SET KOPFTEXT='" + formular.Kopfttext + "', FUSSTEXT='" + formular.Fusstext + "'," +
                "ZAHLUNGSTEXT='" + formular.Zahlungszieltext + "',ZUSATZTEXT='" + formular.Zusatztext + "' " +
                "WHERE NAME ='" + formular.Name + "' AND USER_ID=" + User.ID;

            return sql;
        }
        public static string getSql_UpdateAblageortFormular(Formular formular)
        {
            string sql = "UPDATE FORMULAR SET MWST='" + formular.BMwst + "', ABLAGEORT='" + formular.SpeicherOrdner + "' " +
                "WHERE NAME ='" + formular.Name + "' AND USER_ID=" + User.ID;

            return sql;
        }
        public static string getSql_UpdateKalenderEinstellungen(string mail, string path, bool geloeschte_termine,
            bool d_feiertage, bool i_feiertage, long user_id)
        {
            string sql = "UPDATE KONFIG_KALENDER SET EMAIL='" + mail + "', SECRET_PATH='" + path + "'," +
                "GELOESCHTE_TERMINE=" + Convert.ToInt32(geloeschte_termine) + ",FEIERTAGE=" + Convert.ToInt32(d_feiertage) + ", ISLAMISCHE_FEIERTAGE=" + Convert.ToInt32(i_feiertage) + " " +
                "WHERE USER_ID=" + user_id;

            return sql;
        }
        public static string getSql_UpdateMailEinstellungen(long user_id, string email_smtp, string passwort, int port_smtp,
            string host_smtp, bool ssl_smtp, bool sInfo_smtp, string email_outl, bool display_outl, string aktive_einst)
        {
            string sql = "UPDATE KONFIG_MAIL SET EMAIL_SMTP = '" + email_smtp + "', " +
                "PASSWORT_SMTP ='" + passwort + "', PORT =" + port_smtp + ", HOST ='" + host_smtp + "', SSL =" + Convert.ToInt32(ssl_smtp) + ", " +
                "STANDARD_INFO =" + Convert.ToInt32(sInfo_smtp) + ", EMAIL_OUT ='" + email_outl + "', DISPLAY_OUT = " + Convert.ToInt32(display_outl) + ", " +
                "AKTIV_EINST = '" + aktive_einst + "' WHERE USER_ID =" + user_id;

            return sql;
        }
        public static string getSql_UpdateKosten(Kosten kosten)
        {
            string sql = " UPDATE KOSTEN SET BEZEICHNUNG = '" + kosten.Bezeichnung + "'," +
                "KOSTENART = '" + kosten.Kostenart + "',INTERVALL= '" + kosten.Intervall + "' WHERE ID =" + kosten.Id + "";

            return sql;
        }
        /****** Delete-Befehle***********************/
        public static string getSql_DeleteUserKonto(int user_id)
        {
            string sql = "DELETE FROM _USER WHERE ID = " + user_id;

            return sql;
        }
        public static string getSql_DeleteVertreterByMitgliedID(Mitglied mitglied)
        {
            string sql = "DELETE FROM VERTRETER WHERE MITGLIED_ID ='" + mitglied.Id + "'";

            return sql;
        }
        public static string getSql_DeleteVetrag(Vertrag vertrag)
        {
            string sql =
            "DELETE FROM VERTRAG_KURS WHERE VERTRAG_ID = " + vertrag.Id + ";" +
            "DELETE FROM VERTRAG WHERE ID = " + vertrag.Id + ";" +
            "if 2 >(select count (Mitglied_ID) from VERTRAG where Mitglied_ID = " + vertrag.Mitglied.Id + " group by Mitglied_ID) DELETE from VERTRETER  where Mitglied_ID = " + vertrag.Mitglied.Id + ";" +
            "if 2 >(select count (Mitglied_ID) from VERTRAG where Mitglied_ID = " + vertrag.Mitglied.Id + " group by Mitglied_ID) DELETE from MITGLIED  where ID = " + vertrag.Mitglied.Id + ";";

            return sql;
        }
        public static string getSql_DeleteKurs(int KursID)
        {
            string sql = "DELETE FROM KURS WHERE ID = " + KursID;

            return sql;
        }
        public static string getSql_DeleteVertragKurs(int vertrag_id)
        {
            string sql = " DELETE FROM VERTRAG_KURS WHERE VERTRAG_ID=" + vertrag_id;
            return sql;
        }
        public static string getSql_DeleteUserRechte(int user_id)
        {
            string sql = "DELETE FROM USER_RECHT WHERE USER_ID = " + user_id;

            return sql;
        }
        public static string getSql_DeleteLeistungByID(Leistung leistung)
        {
            string sql = "DELETE FROM LEISTUNGEN WHERE ID = " + leistung.Id;

            return sql;
        }
        public static string getSql_DeleteLeistungenRechnungsnummer(string rechnungsnummer)
        {
            string sql = "DELETE FROM LEISTUNGEN WHERE RECHNUNG_ID = (SELECT ID FROM RECHNUNG WHERE RECHNUNGSNUMMER = '" + rechnungsnummer + "')";

            return sql;
        }
        public static string getSql_DeleteRechnungByNummer(string rechnungsnummer)
        {
            string sql = "DELETE FROM RECHNUNG WHERE RECHNUNGSNUMMER ='" + rechnungsnummer + "'";

            return sql;
        }
        public static string getSql_DeleteKostenZeitraum(int kosten_id)
        {
            string sql = " DELETE FROM KOSTEN_ZEITRAUM WHERE KOSTEN_ID = " + kosten_id + "";

            return sql;
        }
        /***********/
        private static string buildStringWhere(string where, string column, string value)
        {
            if (!string.IsNullOrWhiteSpace(where))
            {
                where += " AND ";
            }
            return where += " (" + column + " LIKE '" + value + "')";
        }
        private static string buildIntWhere(string where, string column, int value)
        {
            if (!string.IsNullOrWhiteSpace(where))
            {
                where += " AND ";
            }
            return where += " (" + column + "=" + value + ")";
        }
        private static string buildDateWhere(string where, string column, string operater, DateTime? datetime)
        {
            if (!string.IsNullOrWhiteSpace(where))
            {
                where += " AND ";
            }
            string value = datetime?.ToString("dd.MM.yyyy") ?? "";
            return where += " (" + column + " " + operater + " '" + value + "')";
        }
    }
}