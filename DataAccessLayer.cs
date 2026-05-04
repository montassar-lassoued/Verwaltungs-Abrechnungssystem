using MyControls;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class DataAccessLayer
    {
        public static DataTable Query_Vertrag(Vertrag vertrag)
        {
            Transaction transaction = new Transaction();
            string sql_query = SqlHelper.getSql_QueryVertrag(vertrag);
            DataTable datatable = transaction.Query(sql_query);
            transaction.Commit();
            return datatable;
        }
        public static DataTable Query_Journal(Vertrag vertrag)
        {
            Transaction transaction = new Transaction();
            string sql_query = SqlHelper.getSql_QueryVertragJournal(vertrag);
            DataTable datatable = transaction.Query(sql_query);
            transaction.Commit();
            return datatable;
        }
        /*public static DataTable Query_Vertrag(VertragHelper helper)
        {
            Transaction transaction = new Transaction();
            string sql_query = SqlHelper.getSql_QueryVertrag(helper);
            DataTable datatable = transaction.Query(sql_query);
            transaction.Commit();
            return datatable;
        }*/
        public static DataTable Query_User()
        {
            string sql = SqlHelper.getSql_QueryUser();
            Transaction transaction = new Transaction();
            DataTable myDataTable = transaction.Query(sql);
            transaction.Commit();

            return myDataTable;
        }
        public static void Insert_Vertrag(Vertrag vertrag)
        {
            string sql_exist = SqlHelper.getSql_MitgliedByName(vertrag.Mitglied);

            Transaction transaction = new Transaction();
            int mitglied_ID = transaction.GetID_IfExist(sql_exist);
            if (mitglied_ID < 1)
            {
                // Mitglied in DB einfügen
                string sql_insert = SqlHelper.getSql_InsertMitglied(vertrag.Mitglied);
                mitglied_ID = transaction.Insert(sql_insert);
            }
            // Mitglied_id verknüpfen
            vertrag.Mitglied.Id = mitglied_ID;

            string sql_Vertreter = SqlHelper.getSql_VertreterByMitgliedID(vertrag.Mitglied);
            int vertreter_ID = transaction.GetID_IfExist(sql_Vertreter);

            //**falls ein Vertreter vorhanden ist, und der Mitglied Volljährige ist
            // dann sollen wir den Vertreter löschen
            if (!vertrag.Mitglied.Miderjaehrige)
            {
                if (vertreter_ID > 0)
                {
                    string sql_deleteVertreter = SqlHelper.getSql_DeleteVertreterByMitgliedID(vertrag.Mitglied);
                    transaction.Delete(sql_deleteVertreter);
                }
            }
            /** der Mitglied ist schon Minderjähriger*/
            else if (vertrag.Mitglied.Miderjaehrige)
            {
                if (vertreter_ID > 0)
                {
                    //**Vertreter ist vorhanden
                    string sql_UpdateVertreter = SqlHelper.getSql_UpdateVertreter(vertrag.Mitglied.Vertreter);
                    transaction.Update(sql_UpdateVertreter);
                }
                else
                {
                    //**Vertreter ist nicht vorhanden => einfügen
                    string sql_insertVertreter = SqlHelper.getSql_InsertVertreter(vertrag.Mitglied);
                    transaction.Insert(sql_insertVertreter);
                }

            }

            // Vertrag hinzufügen
            string sql_vertrag = SqlHelper.getSql_InsertVertrag(vertrag);
            int vertrag_id = transaction.Insert(sql_vertrag);
            string sql_kurs = "";
            foreach (var kurs in vertrag.Kurse)
            {
                sql_kurs += SqlHelper.getSql_InsertVertrag_Kurs(vertrag_id, kurs.Id, kurs.Betrag);
            }
            transaction.Insert(sql_kurs);
            transaction.Commit();
        }

        //*************** methode Update vertrag************////////
        public static void Update_Vertrag(Vertrag vertrag)
        {
            Transaction transaction = new Transaction();
            //*** update Vertrag
            string sql_Vertrag = SqlHelper.getSql_UpdateVertrag(vertrag);
            transaction.Update(sql_Vertrag);

            //*** update Mitglied
            string sql_Mitglied = SqlHelper.getSql_UpdateMitglied(vertrag.Mitglied);
            transaction.Update(sql_Mitglied);

            //*** update Vertreter
            // schauen, ob für den Mitglied schon einen Vertreter gab
            string sql_Vertreter = SqlHelper.getSql_VertreterByMitgliedID(vertrag.Mitglied);
            int vertreter_ID = transaction.GetID_IfExist(sql_Vertreter);

            //**falls ein Vertreter vorhanden ist, und der Mitglied Volljährige ist
            // dann sollen wir den Vertreter löschen
            if (!vertrag.Mitglied.Miderjaehrige)
            {
                if (vertreter_ID > 0)
                {
                    string sql_deleteVertreter = SqlHelper.getSql_DeleteVertreterByMitgliedID(vertrag.Mitglied);
                    transaction.Delete(sql_deleteVertreter);
                }
            }
            /** der Mitglied ist schon Minderjähriger*/
            else if (vertrag.Mitglied.Miderjaehrige)
            {
                if (vertreter_ID > 0)
                {
                    //**Vertreter ist vorhanden
                    string sql_UpdateVertreter = SqlHelper.getSql_UpdateVertreter(vertrag.Mitglied.Vertreter);
                    transaction.Update(sql_UpdateVertreter);
                }
                else
                {
                    //**Vertreter ist nicht vorhanden => einfügen
                    string sql_insertVertreter = SqlHelper.getSql_InsertVertreter(vertrag.Mitglied);
                    transaction.Insert(sql_insertVertreter);
                }
            }
            string sql_kurs = "";
            string sql_deleteKurs = SqlHelper.getSql_DeleteVertragKurs(vertrag.Id) + ";";
            foreach (var kurs in vertrag.Kurse)
            {
                sql_kurs += SqlHelper.getSql_InsertVertrag_Kurs(vertrag.Id, kurs.Id, kurs.Betrag);
            }
            transaction.Insert(sql_deleteKurs + sql_kurs);
            transaction.Commit();
        }
        //*************** methode Update vertrag status************////////
        public static void Update_StatusVertrag(Vertrag vertrag)
        {
            string sql_updateStatus = SqlHelper.getSql_UpdateStatusVertrag(vertrag);
            Transaction transaction = new Transaction();
            transaction.Update(sql_updateStatus);
            transaction.Commit();
        }
        //*************** methode Delete vertrag************////////
        public static void Delete_Vertrag(Vertrag vertrag)
        {
            string sql_deleteVertrag = SqlHelper.getSql_DeleteVetrag(vertrag);
            Transaction transaction = new Transaction();
            transaction.Delete(sql_deleteVertrag);
            transaction.Commit();
        }
        public static int Get_KursIdByName(string kursname)
        {
            string sql = SqlHelper.getSql_KrusByName(kursname);
            Transaction transaction = new Transaction();
            int kurs_id = transaction.GetID_IfExist(sql);
            if (kurs_id < 0)
            {
                throw new Exception("Kurs in der Datenbank nicht vorhanden");
            }
            return kurs_id;
        }
        public static int Get_UserByName(string vorname, string nachname)
        {
            string sql = SqlHelper.getSql_UserByName(vorname, nachname);
            Transaction transaction = new Transaction();
            int user_id = transaction.GetID_IfExist(sql);

            return user_id;
        }
        public static int Insert_UserKonto(string vorname, string nachname, string anmeldename, string passwort)
        {
            string sql = SqlHelper.getSql_InsertUser(vorname, nachname, anmeldename, passwort);
            Transaction transaction = new Transaction();
            int konto_id = transaction.Insert(sql);
            transaction.Commit();

            return konto_id;
        }
        public static void Delete_UserKontoByID(int userID)
        {
            string sql = SqlHelper.getSql_DeleteUserKonto(userID);
            Transaction transaction = new Transaction();
            transaction.Delete(sql);
            transaction.Commit();
        }
        public static DataTable Query_Kurs()
        {
            string sql_query = SqlHelper.getSql_QueryKrus();
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql_query);
            transaction.Commit();
            return datatable;
        }
        public static DataTable GetKurseByVertragID(int vertragID)
        {
            string sql = SqlHelper.getSql_KursByVertragID(vertragID);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();
            return datatable;
        }
        public static bool GetExists_VertragByKursID(int Kurs_ID)
        {
            string sql = SqlHelper.getSql_VertragByKursID(Kurs_ID);
            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(sql);
            transaction.Commit();
            if (id > 0)
            {
                return true;
            }
            return false;
        }
        public static void Delete_KursByID(int Kurs_ID)
        {
            string sql = SqlHelper.getSql_DeleteKurs(Kurs_ID);
            Transaction transaction = new Transaction();
            transaction.Delete(sql);
            transaction.Commit();
        }
        internal static void update_KursBetrag(Kurs kurs)
        {
            string sql = SqlHelper.getSql_UpdateKursart(kurs);
            Transaction transaction = new Transaction();
            transaction.Update(sql);
            transaction.Commit();
        }
        public static void Insert_Kursart(Kurs kurs)
        {
            string sql = SqlHelper.getSql_InsertKursart(kurs);
            Transaction transaction = new Transaction();
            transaction.Insert(sql);
            transaction.Commit();
        }
        public static DataTable Query_Rechte()
        {
            string sql_query = SqlHelper.getSql_QueryRechte();
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql_query);
            transaction.Commit();
            return datatable;
        }
        public static DataTable Query_User_Rechte(int user_id)
        {
            string sql_query = SqlHelper.getSql_QueryUserRechte(user_id);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql_query);
            transaction.Commit();
            return datatable;
        }
        public static void Insert_UserRecht(int user_id, int recht_id)
        {
            string sql = SqlHelper.getSql_InsertUserRecht(user_id, recht_id);
            Transaction transaction = new Transaction();
            transaction.Insert(sql);
            transaction.Commit();
        }
        public static void DELETE_UserRechte(int user_id)
        {
            string sql = SqlHelper.getSql_DeleteUserRechte(user_id);
            Transaction transaction = new Transaction();
            transaction.Delete(sql);
            transaction.Commit();
        }
        public static void Insert_Kunde(Kunde kunde)
        {
            string sql = SqlHelper.getSql_InsertKunde(kunde);
            Transaction transaction = new Transaction();
            transaction.Insert(sql);
            transaction.Commit();
        }
        public static int Get_KundeExists(string kundenname)
        {
            string sql = SqlHelper.getSql_KundeByName(kundenname);
            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(sql);
            transaction.Commit();
            return id;
        }
        public static DataTable Get_Kunde(Kunde kunde)
        {
            string sql = SqlHelper.getSql_QueryKunde(kunde);
            Transaction transaction = new Transaction();
            DataTable dataTable = transaction.Query(sql);
            transaction.Commit();

            return dataTable;
        }
        public static void Update_Kunde(Kunde kunde)
        {
            string sql = SqlHelper.getSql_UpdateKunde(kunde);
            Transaction transaction = new Transaction();
            transaction.Update(sql);
            transaction.Commit();
        }
        public static void Insert_Rechnung(RechnungHelper rechnung)
        {
            string sql = SqlHelper.getSql_InsertRechnung(rechnung);
            string sql_nummerkreis = SqlHelper.getSQL_UpdateRechnungsnummerkreis();
            Transaction transaction = new Transaction();
            int rechnung_id = transaction.Insert(sql);
            foreach (Leistung leistung in rechnung.Leistungen)
            {
                string sql_Leistung = SqlHelper.getSql_InsertLeistungen(leistung, rechnung_id);
                transaction.Insert(sql_Leistung);
            }

            transaction.Update(sql_nummerkreis);
            transaction.Commit();
        }
        public static void Insert_StornoRechnung(StornoRechnung stornoRechnung)
        {
            string sql = SqlHelper.getSql_InsertStornoRechnung(stornoRechnung);
            string sql_nummerkreis = SqlHelper.getSQL_UpdateSTORNORechnungsnummerkreis();
            Transaction transaction = new Transaction();
            int stornoRechnungID = transaction.Insert(sql);
            foreach (Leistung leistung in stornoRechnung.Leistungen)
            {
                string sql_Leistung = SqlHelper.getSql_InsertStornoLeistungen(leistung, stornoRechnungID);
                transaction.Insert(sql_Leistung);
            }

            transaction.Update(sql_nummerkreis);
            transaction.Commit();
        }
        public static DataTable Get_AktRechnungNummer()
        {
            string sql = SqlHelper.getSql_QueryRechnungsnummerkreis();
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable Get_AktStornoNummer()
        {
            string sql = SqlHelper.getSql_QuerySTORNORechnungsnummerkreis();
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable UpdateRechnungsnummerJAHR(string formular)
        {
            string sql = SqlHelper.getSQL_UpdateRechnungsnummerJAHR(formular);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static void Rechnungnummerkreis_Zuruecksetzen()
        {
            string sql = SqlHelper.getSQL_Rechnungsnummerkreis_Zuruecksetzen();
            Transaction transaction = new Transaction();
            transaction.Update(sql);
            transaction.Commit();
        }
        public static void Stornonummerkreis_Zuruecksetzen()
        {
            string sql = SqlHelper.getSQL_Stornonummerkreis_Zuruecksetzen();
            Transaction transaction = new Transaction();
            transaction.Update(sql);
            transaction.Commit();
        }
        public static DataTable Query_Kunden()
        {
            string sql = SqlHelper.getSql_QueryKunden();
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getLeistungenByRechnungsnummer(string rechnungsnummer)
        {
            string sql = SqlHelper.getSql_QueryLeistungenByRechnungsNummer(rechnungsnummer);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getRechnungByNummer(string rechnungsnummer)
        {
            string sql = SqlHelper.getSql_QueryRechnungByNummer(rechnungsnummer);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getStornoLeistungenByRechnungsnummer(string stornoNummer)
        {
            string sql = SqlHelper.getSql_QueryStornoLeistungenByRechnungsNummer(stornoNummer);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getStornoRechnungByNummer(string rechnungsnummer)
        {
            string sql = SqlHelper.getSql_QueryStornoRechnungByNummer(rechnungsnummer);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable QueryKundeRechnung(RechnungHelper rechnung)
        {
            string sql = SqlHelper.getSql_QueryKundeRechnung(rechnung);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable QueryRechnung(RechnungHelper rechnung)
        {
            string sql = SqlHelper.getSql_QueryRechnung(rechnung);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static void update_Rechnung(RechnungHelper rechnung)
        {
            Transaction transaction = new Transaction();
            string sql_update_Rechnung = SqlHelper.getSql_UpdateRechnung(rechnung);
            string sql_delete_leistungen = SqlHelper.getSql_DeleteLeistungenRechnungsnummer(rechnung.Rechnungsnummer);
            string sql_insert_Leistung = "";
            foreach (Leistung leistung in rechnung.Leistungen)
            {
                sql_insert_Leistung += SqlHelper.getSql_InsertLeistungen(leistung, rechnung.Id);
                sql_insert_Leistung += ";";
            }
            transaction.Update(sql_update_Rechnung + ";" + sql_delete_leistungen + ";" + sql_insert_Leistung);
            transaction.Commit();
        }
        public static void update_RechnungStatus(string rechnungsnummer, Prozessstatus status)
        {
            string sql = SqlHelper.getSql_UpdateRechnungStatus(rechnungsnummer, status);
            Transaction transaction = new Transaction();
            transaction.Update(sql);
            transaction.Commit();
        }
        public static DataTable get_KundeByRechnung(RechnungHelper rechnung)
        {
            string sql = SqlHelper.getSql_QueryKundeByRechnung(rechnung);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static void delete_LeistungByID(Leistung leistung)
        {
            string sql = SqlHelper.getSql_DeleteLeistungByID(leistung);
            Transaction transaction = new Transaction();
            transaction.Delete(sql);
            transaction.Commit();
        }
        public static void delete_LeistungenByRechnungsnummer(string rechnungsnummer)
        {
            string sql = SqlHelper.getSql_DeleteLeistungenRechnungsnummer(rechnungsnummer);
            Transaction transaction = new Transaction();
            transaction.Delete(sql);
            transaction.Commit();
        }
        public static void update_Leistungsposition(Leistung leistung)
        {
            string sql = SqlHelper.getSql_UpadteLeistungsposition(leistung);
            Transaction transaction = new Transaction();
            transaction.Update(sql);
            transaction.Commit();
        }
        public static void delete_RechnungByRechnungsnummer(string rechnungsnummer)
        {
            string sql = SqlHelper.getSql_DeleteLeistungenRechnungsnummer(rechnungsnummer);
            sql += "; ";
            sql += SqlHelper.getSql_DeleteRechnungByNummer(rechnungsnummer);
            Transaction transaction = new Transaction();
            transaction.Delete(sql);
            transaction.Commit();
        }
        public static void Update_FirmaStammdaten()
        {
            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(SqlHelper.getSql_QueryStammdaten());
            string sql;
            if (id > 0)
            {
                sql = SqlHelper.getSql_UpdateStammdaten();
                transaction.Update(sql);
            }
            else
            {
                sql = SqlHelper.getSql_InsertStammdaten();
                transaction.Insert(sql);
            }
            transaction.Commit();
        }
        public static DataTable QueryStammdaten()
        {
            string sql = SqlHelper.getSql_QueryStammdaten();
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static void Update_Formular(Formular formular)
        {
            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(SqlHelper.getSql_QueryFormular(formular));
            string sql;
            if (id > 0)
            {
                sql = SqlHelper.getSql_UpdateFormular(formular);
                transaction.Update(sql);
            }
            else
            {
                sql = SqlHelper.getSql_InsertFormular(formular);
                transaction.Insert(sql);
            }
            transaction.Commit();
        }
        public static void Update_AblageortFormular(Formular formular)
        {
            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(SqlHelper.getSql_QueryFormular(formular));
            string sql;
            if (id > 0)
            {
                sql = SqlHelper.getSql_UpdateAblageortFormular(formular);
                transaction.Update(sql);
            }
            else
            {
                sql = SqlHelper.getSql_InsertAblageFormular(formular);
                transaction.Insert(sql);
            }
            transaction.Commit();
        }
        public static DataTable queryFormular(Formular formular)
        {
            string sql = SqlHelper.getSql_QueryFormular(formular);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable queryKalenderEinstellungen()
        {
            string sql = SqlHelper.getSql_QueryKalenderEinstellungen(User.ID);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);

            return datatable;
        }
        public static void Update_KalendarEinstellungen(string mail, string path, bool geloeschte_termine, bool d_feiertage, bool i_feiertage)
        {
            string sql = SqlHelper.getSql_QueryKalenderEinstellungen(User.ID);

            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(sql);
            if (id < 1)
            {
                sql = SqlHelper.getSql_InsertKalenderEinstellungen(mail, path, geloeschte_termine, d_feiertage, i_feiertage, User.ID);
                transaction.Insert(sql);
            }
            else
            {
                sql = SqlHelper.getSql_UpdateKalenderEinstellungen(mail, path, geloeschte_termine, d_feiertage, i_feiertage, User.ID);
                transaction.Update(sql);
            }
            transaction.Commit();
        }
        public static DataTable query_MailEinstellungen()
        {
            string sql = SqlHelper.getSql_QueryMailEinstellungen(User.ID);
            Transaction transaction = new Transaction();
            DataTable data = transaction.Query(sql);
            transaction.Commit();
            return data;
        }
        public static void update_MailEinstellungen(string email_smtp, string passwort, int port_smtp,
            string host_smtp, bool ssl_smtp, bool sInfo_smtp, string email_outl, bool display_outl, string aktive_einst)
        {
            string sql = SqlHelper.getSql_QueryMailEinstellungen(User.ID);
            Transaction transaction = new Transaction();
            int id = transaction.GetID_IfExist(sql);
            if (id < 1)
            {
                sql = SqlHelper.getSql_Insert_MailEinstellungen(User.ID, email_smtp, passwort, port_smtp,
             host_smtp, ssl_smtp, sInfo_smtp, email_outl, display_outl, aktive_einst);

                transaction.Insert(sql);
            }
            else
            {
                sql = SqlHelper.getSql_UpdateMailEinstellungen(User.ID, email_smtp, passwort, port_smtp,
             host_smtp, ssl_smtp, sInfo_smtp, email_outl, display_outl, aktive_einst);

                transaction.Update(sql);
            }
            transaction.Commit();
        }
        public static DataTable queryStornosByBezugsrechnung(string bezugRechnungsnummer)
        {
            string sql = SqlHelper.getSql_QueryStornoRechnung(bezugRechnungsnummer);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable queryStornoLeistungenByBezugsrechnung(string Rechnungsnummer)
        {
            string sql = SqlHelper.getSql_Query_STORNO_LeistungenBySTORNO_RechnungsNummer(Rechnungsnummer);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable queryUmsatz(Umsatz umsatz)
        {
            string sql = SqlHelper.getSql_QueryUmsatz(umsatz);
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static void Insert_Kosten(Kosten kosten)
        {
            string sql = SqlHelper.getSql_InsertKosten(kosten);

            Transaction transaction = new Transaction();
            int kosten_id = transaction.Insert(sql);
            foreach (Kostenzeitraum zeitram in kosten.Kostenzeitraum)
            {
                string sql_Leistung = SqlHelper.getSql_InsertKostenzeitraum(zeitram, kosten_id);
                transaction.Insert(sql_Leistung);
            }
            transaction.Commit();
        }
        internal static void Update_Kosten(Kosten kosten)
        {
            string sql = SqlHelper.getSql_UpdateKosten(kosten) + ";";

            Transaction transaction = new Transaction();
            //transaction.Update(sql);
            sql += SqlHelper.getSql_DeleteKostenZeitraum(kosten.Id) + ";";
            foreach (Kostenzeitraum zeitram in kosten.Kostenzeitraum)
            {
                sql += SqlHelper.getSql_InsertKostenzeitraum(zeitram, kosten.Id) + ";";

            }
            transaction.Update(sql);
            transaction.Commit();
        }
        public static DataTable queryKosten(string ansicht, string datum_von, string datum_bis)
        {
            string sql = "";
            if (ansicht.Equals(Globals.KOSTEN_INTERVALL_MONATLICH))
            {
                sql = SqlHelper.getSql_QueryKostenMonatlich(datum_von, datum_bis);
            }
            else if (ansicht.Equals(Globals.KOSTEN_INTERVALL_JAEHRLICH))
            {
                sql = SqlHelper.getSql_QueryKostenJaehrlich(datum_von, datum_bis);
            }
            else
            {
                return null;
            }
            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getKosten()
        {
            string sql = SqlHelper.getSql_QueryKosten();

            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getKostenByID(long id)
        {
            string sql = SqlHelper.getSql_QueryKostenByID(id);

            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static DataTable getBlacklist()
        {
            string sql = SqlHelper.getSql_QueryBlacklist();

            Transaction transaction = new Transaction();
            DataTable datatable = transaction.Query(sql);
            transaction.Commit();

            return datatable;
        }
        public static void UpdateBlacklist(RowMergeView dataGridView)
        {
            string sql = "";
            Transaction transaction = new Transaction();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.IsNewRow)
                    continue;
                string name = row.Cells["NAME"].Value.ToString();
                string beschr = row.Cells["BESCHREIBUNG"].Value.ToString();
                string id = row.Cells["ID"].Value.ToString();
                if (string.IsNullOrEmpty(id))
                {
                    sql += SqlHelper.getSql_InsertBlacklistEintrag(name, beschr) + ";";
                }
                else
                {
                    sql += SqlHelper.getSql_UpdateBlacklistEintrag(id, name, beschr) + ";";
                }
            }
            transaction.Update(sql);
            transaction.Commit();
        }
    }
}