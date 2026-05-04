using System;
using System.Collections.Generic;
using System.Data;

namespace FCC_Verwaltungssystem
{
    public class Rechte
    {
        private List<Recht> rechte;
        /*INSERT INTO RECHT(ID, RECHT) VALUES(1, 'Konten bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(2, 'Kurse bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(3, 'Option bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(4, 'Vertrag erfassen');
        INSERT INTO RECHT(ID, RECHT) VALUES(5, 'Beitrag ändern');
        INSERT INTO RECHT(ID, RECHT) VALUES(6, 'Vertrag suchen');
        INSERT INTO RECHT(ID, RECHT) VALUES(14, 'Suchfilter nutzen');
        INSERT INTO RECHT(ID, RECHT) VALUES(7, 'Vertrag bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(8, 'Vertrag ausdrucken');
        INSERT INTO RECHT(ID, RECHT) VALUES(9, 'Summenverträge sehen');
        INSERT INTO RECHT(ID, RECHT) VALUES(10, 'Vertrag kündigen');
        INSERT INTO RECHT(ID, RECHT) VALUES(11, 'Vertrag pausieren');
        INSERT INTO RECHT(ID, RECHT) VALUES(12, 'Vertrag löschen');
        INSERT INTO RECHT(ID, RECHT) VALUES(13, 'Vertrag reaktivieren');
        INSERT INTO RECHT(ID, RECHT) VALUES(15, 'Kunden bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(16, 'Rechnung erstellen');
        INSERT INTO RECHT(ID, RECHT) VALUES(17, 'Rechnung bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(18, 'Rechnung löschen');
        INSERT INTO RECHT(ID, RECHT) VALUES(19, 'Rechnung suchen');
        INSERT INTO RECHT(ID, RECHT) VALUES(20, 'Journal nutzen');
        INSERT INTO RECHT(ID, RECHT) VALUES(21, 'Kunden anlegen');
        INSERT INTO RECHT(ID, RECHT) VALUES(22, 'Kunden suchen');
        INSERT INTO RECHT(ID, RECHT) VALUES(23, 'Backup-Einstellungen');
        INSERT INTO RECHT(ID, RECHT) VALUES(24, 'Backup durchführen');
        INSERT INTO RECHT(ID, RECHT) VALUES(25, 'Kalendar nutzen');
        INSERT INTO RECHT(ID, RECHT) VALUES(26, 'Kalendar-Einstellungen');
        INSERT INTO RECHT(ID, RECHT) VALUES(27, 'Kalendar-Termin erstellen');
        INSERT INTO RECHT(ID, RECHT) VALUES(28, 'Kalendar-Termin bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(29, 'Kalendar-Termin löschen');
        INSERT INTO RECHT(ID, RECHT) VALUES(30, 'Mail-Konfiguration');
        INSERT INTO RECHT(ID, RECHT) VALUES(31, 'Umsatz-Übersicht');
        INSERT INTO RECHT(ID, RECHT) VALUES(32, 'Einnahme-Übersicht');
        INSERT INTO RECHT(ID, RECHT) VALUES(33, 'Kosten-Übersicht');
        INSERT INTO RECHT(ID, RECHT) VALUES(34, 'Stammdaten-bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(35, 'Blacklist-bearbeiten');
        INSERT INTO RECHT(ID, RECHT) VALUES(36, 'Rechnung-Konfiguration');*/

        public void initialize(DataTable dataTable, int _rolle)
        {
            bool state = false;
            if (_rolle == 1)
            {
                state = true;
            }
            rechte = new List<Recht>() {
                new Recht(1,"Konten bearbeiten", state),
                new Recht(2,"Kurse bearbeiten", state),
                new Recht(3,"Option bearbeiten", state),
                new Recht(4,"Vertrag erfassen", state),
                new Recht(5,"Beitrag ändern", state),
                new Recht(6,"Vertrag suchen", state),
                new Recht(14,"Suchfilter nutzen", state),
                new Recht(7,"Vertrag bearbeiten", state),
                new Recht(8,"Vertrag ausdrucken", state),
                new Recht(9,"Summenverträge sehen", state),
                new Recht(10,"Vertrag kündigen", state),
                new Recht(11,"Vertrag pausieren", state),
                new Recht(12,"Vertrag löschen", state),
                new Recht(13,"Vertrag reaktivieren", state),
                new Recht(15,"Kunden bearbeiten", state),
                new Recht(16,"Rechnung erstellen", state),
                new Recht(17,"Rechnung bearbeiten", state),
                new Recht(18,"Rechnung löschen", state),
                new Recht(19,"Rechnung suchen", state),
                new Recht(20,"Journal nutzen", state),
                new Recht(21,"Kunden anlegen", state),
                new Recht(22,"Kunden suchen", state),
                new Recht(23,"Backup-Einstellungen", state),
                new Recht(24,"Backup durchführen", state),
                new Recht(25,"Kalendar nutzen", state),
                new Recht(26,"Kalendar-Einstellungen", state),
                new Recht(27,"Kalendar-Termin erstellen", state),
                new Recht(28,"Kalendar-Termin bearbeiten", state),
                new Recht(29,"Kalendar-Termin löschen", state),
                new Recht(30,"Mail-Konfiguration", state),
                new Recht(31,"Umsatz-Übersicht", state),
                new Recht(32,"Einnahme-Übersicht", state),
                new Recht(33,"Kosten-Übersicht", state),
                new Recht(34,"Stammdaten-bearbeiten", state),
                new Recht(35,"Blacklist-bearbeiten", state),
                new Recht(36,"Rechnung-Konfiguration", state),
            };

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                long recht_id = (Int64)row["RECHT_ID"];
                //int beschreibung = (int)row["USER_ID"];
                rechte.Find(r => r.Id.Equals((int)recht_id)).Aktiv = true;


            }
        }
        public bool KONTEN_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(1)).Aktiv;
        }
        public bool KURSE_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(2)).Aktiv;
        }
        public bool OPTION_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(3)).Aktiv;
        }
        public bool VERTRAG_ERFASSEN()
        {
            return rechte.Find(r => r.Id.Equals(4)).Aktiv;
        }
        public bool BEITRAG_AENDERN()
        {
            return rechte.Find(r => r.Id.Equals(5)).Aktiv;
        }
        public bool VERTRAG_SUCHEN()
        {
            return rechte.Find(r => r.Id.Equals(6)).Aktiv;
        }
        public bool SUCHFILTER_NUTZEN()
        {
            return rechte.Find(r => r.Id.Equals(14)).Aktiv;
        }
        public bool VERTRAG_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(7)).Aktiv;
        }
        public bool VERTRAG_AUSDRUCKEN()
        {
            return rechte.Find(r => r.Id.Equals(8)).Aktiv;
        }
        public bool SUMMENVERTRAEGE_SEHEN()
        {
            return rechte.Find(r => r.Id.Equals(9)).Aktiv;
        }
        public bool VERTRAG_KUENDIGEN()
        {
            return rechte.Find(r => r.Id.Equals(10)).Aktiv;
        }
        public bool VERTRAG_PAUSIEREN()
        {
            return rechte.Find(r => r.Id.Equals(11)).Aktiv;
        }
        public bool VERTRAG_LOESCHEN()
        {
            return rechte.Find(r => r.Id.Equals(12)).Aktiv;
        }
        public bool VERTRAG_REAKTIVIEREN()
        {
            return rechte.Find(r => r.Id.Equals(13)).Aktiv;
        }
        public bool KUNDEN_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(15)).Aktiv;
        }
        public bool RECHNUNG_ERSTELLEN()
        {
            return rechte.Find(r => r.Id.Equals(16)).Aktiv;
        }
        public bool RECHNUNG_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(17)).Aktiv;
        }
        public bool RECHNUNG_LOESCHEN()
        {
            return rechte.Find(r => r.Id.Equals(18)).Aktiv;
        }
        public bool RECHNUNG_SUCHEN()
        {
            return rechte.Find(r => r.Id.Equals(19)).Aktiv;
        }
        public bool JOURNAL_NUTZEN()
        {
            return rechte.Find(r => r.Id.Equals(20)).Aktiv;
        }
        public bool KUNDEN_ANLEGEN()
        {
            return rechte.Find(r => r.Id.Equals(21)).Aktiv;
        }
        public bool KUNDEN_SUCHEN()
        {
            return rechte.Find(r => r.Id.Equals(22)).Aktiv;
        }
        public bool BACKUP_EINSTELLUNG()
        {
            return rechte.Find(r => r.Id.Equals(23)).Aktiv;
        }
        public bool BACKUP_ERSTELLEN()
        {
            return rechte.Find(r => r.Id.Equals(24)).Aktiv;
        }
        public bool KALENDAR_NUTZEN()
        {
            return rechte.Find(r => r.Id.Equals(25)).Aktiv;
        }
        public bool KALENDER_EINSTELLUNG()
        {
            return rechte.Find(r => r.Id.Equals(26)).Aktiv;
        }
        public bool KALENDER_TERMIN_ERSTELLEN()
        {
            return rechte.Find(r => r.Id.Equals(27)).Aktiv;
        }
        public bool KALENDER_TERMIN_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(28)).Aktiv;
        }
        public bool KALENDER_TERMIN_LOESCHEN()
        {
            return rechte.Find(r => r.Id.Equals(29)).Aktiv;
        }
        public bool KALENDER_MAIL_KONFIG()
        {
            return rechte.Find(r => r.Id.Equals(30)).Aktiv;
        }
        public bool UMSATZ_UEBERSICHT()
        {
            return rechte.Find(r => r.Id.Equals(31)).Aktiv;
        }
        public bool EINNAHME_UEBERSICHT()
        {
            return rechte.Find(r => r.Id.Equals(32)).Aktiv;
        }
        public bool KOSTEN_UEBERSICHT()
        {
            return rechte.Find(r => r.Id.Equals(33)).Aktiv;
        }
        public bool STAMMDATEN_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(34)).Aktiv;
        }
        public bool BLACKLIST_BEARBEITEN()
        {
            return rechte.Find(r => r.Id.Equals(35)).Aktiv;
        }
        public bool RECHNUNG_KONFIG()
        {
            return rechte.Find(r => r.Id.Equals(36)).Aktiv;
        }
        public bool getStatusByName(string name)
        {
            return rechte.Find(r => r.Beschreibung.Equals(name)).Aktiv;
        }
        public Recht getRechtByName(string name)
        {
            return rechte.Find(r => r.Beschreibung.Equals(name));
        }
    }
}