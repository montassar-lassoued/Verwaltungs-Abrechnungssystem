using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyControls
{
    public abstract partial class LoginBase : MyBaseWinForm
    {
        public LoginBase()
        {
            InitializeComponent();
        }

        public override void OnCloseWindow(object sender, EventArgs e)
        {
            //Close();
        }

        public override void OnEditMask(object sender, EventArgs e)
        {
            EnablePbSave();
        }

        /*public override void OnSaveData(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(feld_Addresse.Text))
            {
                pictureBox1.Show();
                Meldung.Text = "Fehler!!\n Anmeldename eingeben";
                Meldung.Show();
                Log.Error("Falsche Anmeldedaten");
                return;
            }

            string En_psw = PasswordManager.EncryptString(feld_Passwort.Text);

            Transaction transaction = new Transaction();
            string sql = "SELECT * FROM _USER WHERE ANMELDENAME COLLATE Latin1_General_CS_AS Like '" + feld_Addresse.Text + "'" +
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
                DataRow row = myDataTable.Rows[0];
                long id = (Int64)row["ID"];
                string name = (string)row["ANMELDENAME"];
                string passwort = (string)row["PASSWORT"];
                int rolle = (int)row["ROLLE"];
                string status = "";//row.Field<string>("STATUS");
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
        }*/

        public override void OnSearchModeCheckBoxStateChanged(object sender, MyEventArgs e)
        {

        }

        private void HideWarning(object sender, EventArgs e)
        {
            pictureBox1.Hide();
            Meldung.Text = "";
            Meldung.Hide();
        }
        private void EnablePbSave()
        {
            foreach (var ctrl in GetAllControls(this))
            {
                if (ctrl is myPbSave handler && !ctrl.Enabled && ctrl.Visible)
                {
                    handler.OnEnableControl();
                }
            }
        }
        private static IEnumerable<Control> GetAllControls(Control root)
        {
            var stack = new Stack<Control>();
            stack.Push(root);

            while (stack.Any())
            {
                var next = stack.Pop();
                foreach (Control child in next.Controls)
                    stack.Push(child);

                yield return next;
            }
        }
    }
}
