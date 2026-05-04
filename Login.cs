using Microsoft.Win32;
using MyControls;
using Serilog;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Login : LoginBase
    {
        private const string RegistryPath = @"Software\Fight_Club_Chemnitz\Verwaltungssystem";
        public DialogResult Succes()
        {
            if (succes)
            {
                return DialogResult.OK;
            }
            return DialogResult.Abort;
        }
        protected override void OnLoad(EventArgs e)
        {
            pictureBox1.Hide();
            Meldung.Hide();
            LoadLoginData();
        }
        public override void OnSaveData(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(feld_Addresse.Texts))
            {
                pictureBox1.Show();
                Meldung.Text = "Fehler!!\n Anmeldename eingeben";
                Meldung.Show();
                Log.Error("Falsche Anmeldedaten");
                return;
            }

            string En_psw = PasswordManager.EncryptString(feld_Passwort.Texts);

            Transaction transaction = new Transaction();
            string sql = "SELECT * FROM _USER WHERE ANMELDENAME COLLATE Latin1_General_CS_AS Like '" + feld_Addresse.Texts + "'" +
                " AND PASSWORT COLLATE Latin1_General_CS_AS Like '" + En_psw + "'";
            DataTable myDataTable = transaction.Query(sql);

            if (myDataTable == null || myDataTable.Rows.Count < 1)
            {
                pictureBox1.Show();
                Meldung.Text = "Fehler!!\n Falsche E-Mail und/oder falsche Passwort";
                Meldung.Show();
                Log.Error("Falsche Anmeldedaten");
                succes = false;
                return;
            }
            else
            {
                //erstmal daten merken
                if (cb_AnmeldungMerken.Checked)
                {
                    SaveLoginData(feld_Addresse.Texts, En_psw);
                }
                else
                {
                    DeleteLoginData();
                }

                DataRow row = myDataTable.Rows[0];
                long id = (Int64)row["ID"];
                string name = (string)row["ANMELDENAME"];
                string passwort = (string)row["PASSWORT"];
                int rolle = (int)row["ROLLE"];
                string status = row.Field<string>("STATUS");
                string vorname = row.Field<string>("VORNAME");
                string nachname = row.Field<string>("NACHNAME");
                if (!string.IsNullOrWhiteSpace(status))
                {
                    if (status.Equals("connected"))
                    {
                        pictureBox1.Show();
                        Meldung.Text = "Fehler!!\n Der Benutzer ist schon angemeldet";
                        Meldung.Show();
                        transaction.Commit();
                        Log.Error("Der Benutzer '{0}' ist schon angemeldet", name + " " + nachname);
                        succes = false;
                        return;
                    }
                }

                string sql_update = "Update _USER SET STATUS='connected' WHERE ID =" + id;
                transaction.Update(sql_update);
                transaction.Commit();
                Log.Information("Der Benutzer '{0}' hat sich angemeldet", name + " " + nachname);
                //****USER*****
                new User(id, name, passwort, rolle, vorname, nachname);
                //******USER-RECHTE
                DataTable user_rechte = DataAccessLayer.Query_User_Rechte(((int)id));
                Log.Information("Rechte werden dem Benutzer '{0}' zugeordnet", name + " " + nachname);
                User.addRechte(user_rechte);
                //*******//
                Anmeldung.Vorname = vorname;
                Anmeldung.Nachname = nachname;
                Anmeldung.Id = id;
                Anmeldung.Anmeldename = name;
                //******FIRMA-STAMMDATEN
                DataTable stammdaten = DataAccessLayer.QueryStammdaten();
                if (stammdaten != null && stammdaten.Rows.Count > 0)
                {
                    DataRow rowf = stammdaten.Rows[0];
                    string fname = rowf.Field<string>("NAME");
                    string fadresse = rowf.Field<string>("ADRESSE");
                    string fWebseite = rowf.Field<string>("WEBSEITE");
                    string fg_fuehrer = rowf.Field<string>("GESCHAEFTSFUEHRER");
                    string fmail = rowf.Field<string>("MAIL");
                    string ftel = rowf.Field<string>("TEL");
                    string fumsatzid = rowf.Field<string>("UMSATZSTEUER_ID");
                    string fbank = rowf.Field<string>("BANK");
                    string fkinhaber = rowf.Field<string>("KONTOINHABER");
                    string fiban = rowf.Field<string>("IBAN");
                    string fbic = rowf.Field<string>("BIC");

                    Firma.Name = fname;
                    Firma.Adresse = fadresse;
                    Firma.G_Fuehrer = fg_fuehrer;
                    Firma.Webseite = fWebseite;
                    Firma.Mail = fmail;
                    Firma.Tel = ftel;
                    Firma.UmsatzsteuerID = fumsatzid;
                    Firma.Bankbezeichnung = fbank;
                    Firma.Kontoinhaber = fkinhaber;
                    Firma.Iban = fiban;
                    Firma.Bic = fbic;
                }
                succes = true;
                Close();
            }
        }

        public override void RegisterControl(ICustomControl control)
        {
        }
        public override void OnArchivDragEnter(object sender, EventArgs e)
        {
        }
        private void SaveLoginData(string username, string password)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
            {
                key.SetValue("Username", username);
                key.SetValue("Password", password);
            }
        }

        private void LoadLoginData()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
            {
                if (key != null)
                {
                    feld_Addresse.Texts = key.GetValue("Username")?.ToString();

                    string encPass = key.GetValue("Password")?.ToString();
                    if (!string.IsNullOrEmpty(encPass))
                    {
                        feld_Passwort.Texts = PasswordManager.DecryptString(encPass);
                        cb_AnmeldungMerken.Checked = true;
                    }
                }
            }
        }

        private void DeleteLoginData()
        {
            Registry.CurrentUser.DeleteSubKeyTree(RegistryPath, false);
        }
    }
}