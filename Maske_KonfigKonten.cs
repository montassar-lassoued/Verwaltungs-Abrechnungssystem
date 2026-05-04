using MyControls;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_KonfigKonten : MyForm
    {
        private MyGroupBox myGroupBox2;
        private MyCheckedListBox checkedListRechte;
        private MyFieldText feld_ID_recht;
        private MyFieldText feld_Anmeldename_recht;
        private MyFieldText feld_Vorname_recht;
        private MyFieldText feld_nachname_recht;
        private MyGroupBox myGroupBox1;
        private RowMergeView dataGrid;
        private MyFieldText feld_passwort_2;
        private MyFieldText feld_passwort;
        private MyFieldText feld_Anmeldename;
        private MyFieldText feld_Nachname;
        private MyFieldText feld_Vorname;
        DataTable myUserDataTable = new DataTable();

        protected override string _name()
        {
            return "Konten-Konfiguration";
        }

        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }

        protected override bool _Populate()
        {
            myUserDataTable = DataAccessLayer.Query_User();
            dataGrid.DataSource = myUserDataTable;

            DataTable rechte = DataAccessLayer.Query_Rechte();
            populateRechte(rechte);

            return true;
        }
        private void populateRechte(DataTable rechte)
        {
            for (int i = 0; i < rechte.Rows.Count; i++)
            {
                DataRow row = rechte.Rows[i];
                string recht = (string)row["RECHT"];
                //bool check = User.Rechte.getStatusByName(recht);
                checkedListRechte.Items.Add(recht, false);
            }
        }
        protected override bool _Save()
        {
            if (UserDataOk())
            {
                int id = DataAccessLayer.Get_UserByName(feld_Vorname.Texts, feld_Nachname.Texts);
                if (id > 0)
                {
                    MessageBox.Show("Ein Konto mit diesen Daten ist schon vorhanden"
                        , "Fehler!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    string En_psw = PasswordManager.EncryptString(feld_passwort.Texts);

                    int konto_id = DataAccessLayer.Insert_UserKonto(feld_Vorname.Texts, feld_Nachname.Texts, feld_Anmeldename.Texts, En_psw);
                    refreshDataGrid(konto_id, feld_Vorname.Texts, feld_Nachname.Texts, feld_Anmeldename.Texts, En_psw);
                }

            }
            if (!string.IsNullOrWhiteSpace(feld_ID_recht.Texts))
            {
                //alle Rechte löschen
                DataAccessLayer.DELETE_UserRechte(int.Parse(feld_ID_recht.Texts));
                // nun wieder anlegen
                foreach (var item in checkedListRechte.CheckedItems)
                {
                    Recht recht = User.Rechte.getRechtByName(item.ToString());

                    DataAccessLayer.Insert_UserRecht(int.Parse(feld_ID_recht.Texts), recht.Id);

                }
                //******USER-RECHTE-Neu Einlesen
                DataTable user_rechte = DataAccessLayer.Query_User_Rechte(((int)User.ID));
                User.addRechte(user_rechte);
            }
            clearListBoxCheckedItem();
            return true;
        }
        private void Delete_Konto(object sender, DataGridViewRowCancelEventArgs e)
        {
            int index = e.Row.Index;
            int konto_id;
            bool ok = int.TryParse(dataGrid.Rows[index].Cells["ID"].Value.ToString(), out konto_id);
            if (ok)
            {
                string rolle = dataGrid.Rows[index].Cells["ROLLE"].Value.ToString();
                if (rolle.Equals("1"))
                {
                    MessageBox.Show("Dieses Konto besetzt eine SUPER-ADMIN-Rolle und kann nicht gelöscht " +
                        "werden", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Cancel = true;

                    return;
                }
                DataAccessLayer.Delete_UserKontoByID(konto_id);
            }

        }
        private void refreshDataGrid(int id, string vorname, string nachname, string anmeldename, string passwort)
        {
            DataRow newrow = myUserDataTable.NewRow();  //neue Reihe erzeugen
            newrow["ID"] = id;
            newrow["VORNAME"] = vorname;
            newrow["NACHNAME"] = nachname;
            newrow["ANMELDENAME"] = anmeldename;
            newrow["PASSWORT"] = passwort;
            newrow["ROLLE"] = 2;
            //
            myUserDataTable.Rows.Add(newrow);
        }
        private bool UserDataOk()
        {
            if (string.IsNullOrWhiteSpace(feld_Vorname.Texts))
            {
                //MessageBox.Show("Bitte Vorname ergänzen", "Fehler!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Nachname.Texts))
            {
                //MessageBox.Show("Bitte Nachname ergänzen", "Fehler!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Anmeldename.Texts))
            {
                //MessageBox.Show("Bitte Anmeldename ergänzen", "Fehler!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_passwort.Texts))
            {
                //MessageBox.Show("Bitte Passwort feslegen", "Fehler!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_passwort_2.Texts))
            {
                //MessageBox.Show("Passwort ist nicht identisch", "Fehler!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string id = dataGrid.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim();
            string vorname = dataGrid.Rows[e.RowIndex].Cells["VORNAME"].Value.ToString().Trim();
            string nachname = dataGrid.Rows[e.RowIndex].Cells["NACHNAME"].Value.ToString().Trim();
            string anmeldename = dataGrid.Rows[e.RowIndex].Cells["ANMELDENAME"].Value.ToString().Trim();
            int rolle = int.Parse(dataGrid.Rows[e.RowIndex].Cells["ROLLE"].Value.ToString().Trim());

            feld_ID_recht.Texts = id;
            feld_Vorname_recht.Texts = vorname;
            feld_nachname_recht.Texts = nachname;
            feld_Anmeldename_recht.Texts = anmeldename;

            DataTable dtSelected_User_Rechte = DataAccessLayer.Query_User_Rechte(int.Parse(id));
            Rechte selected_User_Rechte = new Rechte();
            selected_User_Rechte.initialize(dtSelected_User_Rechte, rolle);

            clearListBoxCheckedItem();

            for (int i = 0; i < checkedListRechte.Items.Count; i++)
            {
                string index = (string)checkedListRechte.Items[i];
                Recht recht = selected_User_Rechte.getRechtByName(index);
                bool value = false;
                if (recht != null)
                {
                    value = recht.Aktiv;
                }
                checkedListRechte.SetItemChecked(i, value);

            }
        }
        private void clearListBoxCheckedItem()
        {
            for (int i = 0; i < checkedListRechte.Items.Count; i++)
            {
                checkedListRechte.SetItemChecked(i, false);
            }
        }
        protected override void _InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_KonfigKonten));
            this.myGroupBox1 = new MyControls.MyGroupBox();
            this.dataGrid = new MyControls.RowMergeView();
            this.feld_passwort_2 = new MyControls.MyFieldText();
            this.feld_passwort = new MyControls.MyFieldText();
            this.feld_Anmeldename = new MyControls.MyFieldText();
            this.feld_Nachname = new MyControls.MyFieldText();
            this.feld_Vorname = new MyControls.MyFieldText();
            this.myGroupBox2 = new MyControls.MyGroupBox();
            this.checkedListRechte = new MyControls.MyCheckedListBox();
            this.feld_ID_recht = new MyControls.MyFieldText();
            this.feld_Anmeldename_recht = new MyControls.MyFieldText();
            this.feld_Vorname_recht = new MyControls.MyFieldText();
            this.feld_nachname_recht = new MyControls.MyFieldText();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.myGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.myGroupBox2);
            this.BorderBody.Controls.Add(this.myGroupBox1);
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox1.BorderThickness = 1;
            this.myGroupBox1.Controls.Add(this.dataGrid);
            this.myGroupBox1.Controls.Add(this.feld_passwort_2);
            this.myGroupBox1.Controls.Add(this.feld_passwort);
            this.myGroupBox1.Controls.Add(this.feld_Anmeldename);
            this.myGroupBox1.Controls.Add(this.feld_Nachname);
            this.myGroupBox1.Controls.Add(this.feld_Vorname);
            this.myGroupBox1.Location = new System.Drawing.Point(8, 32);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(788, 232);
            this.myGroupBox1.TabIndex = 0;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "Konto anlegen";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.Location = new System.Drawing.Point(255, 42);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.Size = new System.Drawing.Size(518, 155);
            dataGrid.CellDoubleClick += dataGrid_CellDoubleClick;
            this.dataGrid.TabIndex = 5;
            // 
            // feld_passwort_2
            // 
            this.feld_passwort_2.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_passwort_2.Location = new System.Drawing.Point(18, 171);
            this.feld_passwort_2.Name = "feld_passwort_2";
            this.feld_passwort_2.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_passwort_2.PlaceholderText = "Passwort wiederholen";
            this.feld_passwort_2.Size = new System.Drawing.Size(217, 27);
            this.feld_passwort_2.TabIndex = 4;
            this.feld_passwort_2.Text = "Passwort wiederholen";
            this.feld_passwort_2.Texts = "";
            feld_passwort_2.IsPasswordChar = true;
            // 
            // feld_passwort
            // 
            this.feld_passwort.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_passwort.Location = new System.Drawing.Point(19, 138);
            this.feld_passwort.Name = "feld_passwort";
            this.feld_passwort.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_passwort.PlaceholderText = "Passwort";
            this.feld_passwort.Size = new System.Drawing.Size(217, 27);
            this.feld_passwort.TabIndex = 3;
            this.feld_passwort.Text = "Passwort";
            this.feld_passwort.Texts = "";
            feld_passwort.IsPasswordChar = true;
            // 
            // feld_Anmeldename
            // 
            this.feld_Anmeldename.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Anmeldename.Location = new System.Drawing.Point(19, 105);
            this.feld_Anmeldename.Name = "feld_Anmeldename";
            this.feld_Anmeldename.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Anmeldename.PlaceholderText = " Anmeldename";
            this.feld_Anmeldename.Size = new System.Drawing.Size(217, 27);
            this.feld_Anmeldename.TabIndex = 2;
            this.feld_Anmeldename.Text = " Anmeldename";
            this.feld_Anmeldename.Texts = "";
            // 
            // feld_Nachname
            // 
            this.feld_Nachname.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Nachname.Location = new System.Drawing.Point(19, 72);
            this.feld_Nachname.Name = "feld_Nachname";
            this.feld_Nachname.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Nachname.PlaceholderText = " Nachname";
            this.feld_Nachname.Size = new System.Drawing.Size(217, 27);
            this.feld_Nachname.TabIndex = 1;
            this.feld_Nachname.Text = " Nachname";
            this.feld_Nachname.Texts = "";
            // 
            // feld_Vorname
            // 
            this.feld_Vorname.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Vorname.Location = new System.Drawing.Point(18, 39);
            this.feld_Vorname.Name = "feld_Vorname";
            this.feld_Vorname.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Vorname.PlaceholderText = " Vorname";
            this.feld_Vorname.Size = new System.Drawing.Size(217, 27);
            this.feld_Vorname.TabIndex = 0;
            this.feld_Vorname.Text = " Vorname";
            this.feld_Vorname.Texts = "";
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox2.BorderThickness = 1;
            this.myGroupBox2.Controls.Add(this.checkedListRechte);
            this.myGroupBox2.Controls.Add(this.feld_ID_recht);
            this.myGroupBox2.Controls.Add(this.feld_Anmeldename_recht);
            this.myGroupBox2.Controls.Add(this.feld_Vorname_recht);
            this.myGroupBox2.Controls.Add(this.feld_nachname_recht);
            this.myGroupBox2.Location = new System.Drawing.Point(9, 270);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Size = new System.Drawing.Size(788, 224);
            this.myGroupBox2.TabIndex = 1;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "Rechte zuordnen";
            // 
            // checkedListRechte
            // 
            this.checkedListRechte.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListRechte.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListRechte.CheckOnClick = true;
            this.checkedListRechte.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListRechte.FormattingEnabled = true;
            this.checkedListRechte.Location = new System.Drawing.Point(254, 39);
            this.checkedListRechte.MultiColumn = true;
            this.checkedListRechte.Name = "checkedListRechte";
            this.checkedListRechte.Size = new System.Drawing.Size(517, 176);
            this.checkedListRechte.TabIndex = 0;
            // 
            // feld_ID_recht
            // 
            this.feld_ID_recht.Enabled = false;
            this.feld_ID_recht.ForeColor = System.Drawing.Color.Black;
            this.feld_ID_recht.Location = new System.Drawing.Point(18, 39);
            this.feld_ID_recht.Name = "feld_ID_recht";
            this.feld_ID_recht.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_ID_recht.PlaceholderText = " ID";
            this.feld_ID_recht.ReadOnly = true;
            this.feld_ID_recht.Size = new System.Drawing.Size(217, 27);
            this.feld_ID_recht.TabIndex = 9;
            this.feld_ID_recht.Text = " ID";
            this.feld_ID_recht.Texts = "";
            // 
            // feld_Anmeldename_recht
            // 
            this.feld_Anmeldename_recht.Enabled = false;
            this.feld_Anmeldename_recht.ForeColor = System.Drawing.Color.Black;
            this.feld_Anmeldename_recht.Location = new System.Drawing.Point(18, 138);
            this.feld_Anmeldename_recht.Name = "feld_Anmeldename_recht";
            this.feld_Anmeldename_recht.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Anmeldename_recht.PlaceholderText = " Anmeldename";
            this.feld_Anmeldename_recht.ReadOnly = true;
            this.feld_Anmeldename_recht.Size = new System.Drawing.Size(217, 27);
            this.feld_Anmeldename_recht.TabIndex = 8;
            this.feld_Anmeldename_recht.Text = " Anmeldename";
            this.feld_Anmeldename_recht.Texts = "";
            // 
            // feld_Vorname_recht
            // 
            this.feld_Vorname_recht.Enabled = false;
            this.feld_Vorname_recht.ForeColor = System.Drawing.Color.Black;
            this.feld_Vorname_recht.Location = new System.Drawing.Point(17, 72);
            this.feld_Vorname_recht.Name = "feld_Vorname_recht";
            this.feld_Vorname_recht.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Vorname_recht.PlaceholderText = " Vorname";
            this.feld_Vorname_recht.ReadOnly = true;
            this.feld_Vorname_recht.Size = new System.Drawing.Size(217, 27);
            this.feld_Vorname_recht.TabIndex = 6;
            this.feld_Vorname_recht.Text = " Vorname";
            this.feld_Vorname_recht.Texts = "";
            // 
            // feld_nachname_recht
            // 
            this.feld_nachname_recht.Enabled = false;
            this.feld_nachname_recht.ForeColor = System.Drawing.Color.Black;
            this.feld_nachname_recht.Location = new System.Drawing.Point(18, 105);
            this.feld_nachname_recht.Name = "feld_nachname_recht";
            this.feld_nachname_recht.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_nachname_recht.PlaceholderText = " Nachname";
            this.feld_nachname_recht.ReadOnly = true;
            this.feld_nachname_recht.Size = new System.Drawing.Size(217, 27);
            this.feld_nachname_recht.TabIndex = 7;
            this.feld_nachname_recht.Text = " Nachname";
            this.feld_nachname_recht.Texts = "";
            this.BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Konfig_rad_ico;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.myGroupBox2.ResumeLayout(false);
            this.myGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}