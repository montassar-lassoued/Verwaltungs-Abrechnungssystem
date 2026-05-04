using MyControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Kunde : MyForm
    {
        private MyDuseFieldText feld_Ort;
        private MyDuseFieldText feld_PLZ;
        private MyDuseFieldText feld_Straße;
        private MyFieldDate feld_Geburtsdatum;
        private MyDuseFieldText feld_Kontaktperson;
        private MyDuseFieldText feld_Anrede;
        private MyDuseRichTextBox mtl_Beschreibung;
        private MyDuseFieldText feld_Email;
        private MyDuseFieldText feld_Handy;
        private MyDuseComboBox comboBox_Land;
        private MyDuseFieldText feld_Kundenname;
        private MyFieldText feld_ID;
        private bool updateData;

        protected override void _InitializeComponent()
        {
            InitializeComponent();
        }
        protected override string _name()
        {
            return "Kunde";
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override void _OnLoad(EventArgs e)
        {
            comboBox_Land.Items.AddRange(GetAllCountrysNames().ToArray());
        }
        protected override bool _EnableSearchModeMenu()
        {
            return true;
        }
        protected override bool _Enable_MenuOption(MenuItem menu_Option)
        {
            return true;
        }
        protected override void _SearchModeEnabled()
        {
            updateData = true;
        }
        protected override bool _SearchModeEnabling()
        {
            return true;
        }
        protected override void _SearchModeDisabled()
        {
            updateData = false;
        }
        protected override bool _Populate()
        {
            Kunde kunde = CopieDataFromMask();

            if (!User.Rechte.KUNDEN_SUCHEN())
            {
                MessageBox.Show("Kein Berechtigung Kundendaten zu suchen!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            DataTable dataTable = DataAccessLayer.Get_Kunde(kunde);
            if (dataTable == null || dataTable.Rows.Count < 1)
            {
                MessageBox.Show("Keine Suchergebnisse gefunden", "Info!!");
                return false;
            }
            else if (dataTable.Rows.Count == 1)
            {
                DataRow row = dataTable.Rows[0];
                long id = (Int64)row["ID"];
                string name = (string)row["NAME"];
                string ansprechpartner = (string)row["ANSPRECHPARTNER"];
                DateTime geburtsdatum = row.Field<DateTime>("GEBURTSDATUM");
                string strasse = row.Field<string>("STRASSE");
                string plz = row.Field<string>("PLZ");
                string ort = row.Field<string>("ORT");
                string land = row.Field<string>("LAND");
                string handy = row.Field<string>("HANDY");
                string email = row.Field<string>("EMAIL");
                string anrede = row.Field<string>("ANREDE");
                string beschreibung = row.Field<string>("BESCHREIBUNG");

                feld_ID.Texts = id.ToString();
                feld_Anrede.Texts = anrede;
                feld_Kundenname.Texts = name;
                feld_Kontaktperson.Texts = ansprechpartner;
                feld_Geburtsdatum.Texts = geburtsdatum.ToString();
                feld_Straße.Texts = strasse;
                feld_PLZ.Texts = plz;
                feld_Ort.Texts = ort;
                feld_Handy.Texts = handy;
                feld_Email.Texts = email;
                comboBox_Land.Text = land;
                mtl_Beschreibung.Texts = beschreibung;
            }
            else
            {
                MyTable myTable = new MyTable(dataTable);
                myTable.ShowDialog();

                if (myTable.DataSelected())
                {
                    var list = myTable.GetSelectedRow();
                    string id = list.Find(l => l.Key == "ID").Value;
                    string anrede = list.Find(l => l.Key == "ANREDE").Value;
                    string name = list.Find(l => l.Key == "NAME").Value;
                    string ansprechpartner = list.Find(l => l.Key == "ANSPRECHPARTNER").Value;
                    string geburtsdatum = list.Find(l => l.Key == "GEBURTSDATUM").Value;
                    string strasse = list.Find(l => l.Key == "STRASSE").Value;
                    string plz = list.Find(l => l.Key == "PLZ").Value;
                    string ort = list.Find(l => l.Key == "ORT").Value;
                    string land = list.Find(l => l.Key == "LAND").Value;
                    string handy = list.Find(l => l.Key == "HANDY").Value;
                    string email = list.Find(l => l.Key == "EMAIL").Value;
                    string beschreibung = list.Find(l => l.Key == "BESCHREIBUNG").Value;

                    feld_ID.Texts = id;
                    feld_Anrede.Texts = anrede;
                    feld_Kundenname.Texts = name;
                    feld_Kontaktperson.Texts = ansprechpartner;
                    feld_Geburtsdatum.Texts = DateTime.Parse(geburtsdatum).GetDateTimeFormats()[0]; //DateTime.Parse(geburtsdatum).Date.ToString();
                    feld_Straße.Texts = strasse;
                    feld_PLZ.Texts = plz;
                    feld_Ort.Texts = ort;
                    comboBox_Land.Text = land;
                    feld_Handy.Texts = handy;
                    feld_Email.Texts = email;
                    mtl_Beschreibung.Texts = beschreibung;
                }

            }
            return true;
        }
        protected override bool _PlausibleBevorSave()
        {
            if (string.IsNullOrWhiteSpace(feld_Kundenname.Texts))
            {
                errorProvider.SetError(feld_Kundenname, "Kundenname eingeben");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Straße.Texts))
            {
                errorProvider.SetError(feld_Straße, "Straße eingeben");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_PLZ.Texts))
            {
                errorProvider.SetError(feld_PLZ, "PLZ eingeben");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Ort.Texts))
            {
                errorProvider.SetError(feld_Ort, "Ort eingeben");
                return false;
            }
            if (string.IsNullOrWhiteSpace(comboBox_Land.Text))
            {
                errorProvider.SetError(comboBox_Land, "Land auswählen");
                return false;
            }

            return true;
        }
        protected override bool _Save()
        {
            Kunde kunde = CopieDataFromMask();
            if (updateData)
            {
                // update
                if (!User.Rechte.KUNDEN_BEARBEITEN())
                {
                    MessageBox.Show("Kein Berechtigung Kundendaten zu bearbeiten!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                DataAccessLayer.Update_Kunde(kunde);
                return true;
            }
            else
            {
                if (!User.Rechte.KUNDEN_ANLEGEN())
                {
                    MessageBox.Show("Kein Berechtigung Kunden in der Datenbank anzulegen!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
                // prüfen, ob den gibt
                int id = DataAccessLayer.Get_KundeExists(feld_Kundenname.Texts);
                if (id > 0)
                {
                    MessageBox.Show("der Name ist in der Datenbank schon vorhanden ID{" + id + "}", "Error!!");
                    return false;
                }
                // sonst speichern
                DataAccessLayer.Insert_Kunde(kunde);
                return true;
            }
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv document)
        {
            document.IdColumn = long.Parse(string.IsNullOrEmpty(feld_ID.Texts) ? "0" : feld_ID.Texts);
            document.TableName = "KUNDE";

            return document;
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Kunde));
            this.feld_Kundenname = new MyControls.MyDuseFieldText();
            this.feld_Anrede = new MyControls.MyDuseFieldText();
            this.feld_Kontaktperson = new MyControls.MyDuseFieldText();
            this.feld_Geburtsdatum = new MyControls.MyFieldDate();
            this.feld_Straße = new MyControls.MyDuseFieldText();
            this.feld_PLZ = new MyControls.MyDuseFieldText();
            this.feld_Ort = new MyControls.MyDuseFieldText();
            this.comboBox_Land = new MyControls.MyDuseComboBox();
            this.feld_Handy = new MyControls.MyDuseFieldText();
            this.feld_Email = new MyControls.MyDuseFieldText();
            this.mtl_Beschreibung = new MyControls.MyDuseRichTextBox();
            feld_ID = new MyFieldText();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOk
            // 
            this.pbOk.BackColor = System.Drawing.Color.Transparent;
            this.pbOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbOk.FlatAppearance.BorderSize = 2;
            this.pbOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbOk.Image = ((System.Drawing.Image)(resources.GetObject("pbOk.Image")));
            this.pbOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbOk.UseVisualStyleBackColor = false;
            // 
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.mtl_Beschreibung);
            this.BorderBody.Controls.Add(this.feld_Email);
            this.BorderBody.Controls.Add(this.feld_Handy);
            this.BorderBody.Controls.Add(this.comboBox_Land);
            this.BorderBody.Controls.Add(this.feld_Ort);
            this.BorderBody.Controls.Add(this.feld_PLZ);
            this.BorderBody.Controls.Add(this.feld_Straße);
            this.BorderBody.Controls.Add(this.feld_Geburtsdatum);
            this.BorderBody.Controls.Add(this.feld_Kontaktperson);
            this.BorderBody.Controls.Add(this.feld_Anrede);
            this.BorderBody.Controls.Add(this.feld_Kundenname);
            // 
            // feld_Kundenname
            // 
            this.feld_Kundenname.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Kundenname.Location = new System.Drawing.Point(8, 19);
            this.feld_Kundenname.Name = "feld_Kundenname";
            this.feld_Kundenname.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Kundenname.PlaceholderText = " Kundenname / Bezeichnung";
            this.feld_Kundenname.Size = new System.Drawing.Size(538, 27);
            this.feld_Kundenname.TabIndex = 0;
            this.feld_Kundenname.Text = " Kundenname / Bezeichnung";
            this.feld_Kundenname.Texts = "";
            // 
            // feld_Anrede
            // 
            this.feld_Anrede.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Anrede.Location = new System.Drawing.Point(552, 18);
            this.feld_Anrede.Name = "feld_Anrede";
            this.feld_Anrede.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Anrede.PlaceholderText = " Anrede";
            this.feld_Anrede.Size = new System.Drawing.Size(244, 27);
            this.feld_Anrede.TabIndex = 1;
            this.feld_Anrede.Text = " Anrede";
            this.feld_Anrede.Texts = "";
            // 
            // feld_Kontaktperson
            // 
            this.feld_Kontaktperson.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Kontaktperson.Location = new System.Drawing.Point(8, 56);
            this.feld_Kontaktperson.Name = "feld_Kontaktperson";
            this.feld_Kontaktperson.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Kontaktperson.PlaceholderText = "Ansprechpartner / Kontaktperson";
            this.feld_Kontaktperson.Size = new System.Drawing.Size(788, 27);
            this.feld_Kontaktperson.TabIndex = 2;
            this.feld_Kontaktperson.Text = "Ansprechpartner / Kontaktperson";
            this.feld_Kontaktperson.Texts = "";
            // 
            // feld_Geburtsdatum
            // 
            this.feld_Geburtsdatum.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.feld_Geburtsdatum.Location = new System.Drawing.Point(8, 92);
            this.feld_Geburtsdatum.Name = "feld_Geburtsdatum";
            this.feld_Geburtsdatum.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Geburtsdatum.PlaceholderText = "";
            this.feld_Geburtsdatum.Size = new System.Drawing.Size(203, 27);
            this.feld_Geburtsdatum.TabIndex = 3;
            this.feld_Geburtsdatum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.feld_Geburtsdatum.Texts = "  .  .";
            this.feld_Geburtsdatum.ToolTip = " Geburtstag ";
            this.feld_Geburtsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Straße
            // 
            this.feld_Straße.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Straße.Location = new System.Drawing.Point(8, 130);
            this.feld_Straße.Name = "feld_Straße";
            this.feld_Straße.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Straße.PlaceholderText = " Straße";
            this.feld_Straße.Size = new System.Drawing.Size(789, 27);
            this.feld_Straße.TabIndex = 4;
            this.feld_Straße.Text = " Straße";
            this.feld_Straße.Texts = "";
            // 
            // feld_PLZ
            // 
            this.feld_PLZ.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_PLZ.Location = new System.Drawing.Point(214, 92);
            this.feld_PLZ.Name = "feld_PLZ";
            this.feld_PLZ.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_PLZ.PlaceholderText = " PLZ";
            this.feld_PLZ.Size = new System.Drawing.Size(140, 27);
            this.feld_PLZ.TabIndex = 5;
            this.feld_PLZ.Text = " PLZ";
            this.feld_PLZ.Texts = "";
            // 
            // feld_Ort
            // 
            this.feld_Ort.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Ort.Location = new System.Drawing.Point(360, 92);
            this.feld_Ort.Name = "feld_Ort";
            this.feld_Ort.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Ort.PlaceholderText = " Ort";
            this.feld_Ort.Size = new System.Drawing.Size(276, 27);
            this.feld_Ort.TabIndex = 6;
            this.feld_Ort.Text = " Ort";
            this.feld_Ort.Texts = "";
            // 
            // comboBox_Land
            // 
            this.comboBox_Land.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBox_Land.FormattingEnabled = true;
            this.comboBox_Land.Location = new System.Drawing.Point(642, 92);
            this.comboBox_Land.Name = "comboBox_Land";
            this.comboBox_Land.Size = new System.Drawing.Size(154, 27);
            this.comboBox_Land.TabIndex = 7;
            // 
            // feld_Handy
            // 
            this.feld_Handy.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Handy.Location = new System.Drawing.Point(8, 167);
            this.feld_Handy.Name = "feld_Handy";
            this.feld_Handy.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Handy.PlaceholderText = " Handy";
            this.feld_Handy.Size = new System.Drawing.Size(394, 27);
            this.feld_Handy.TabIndex = 8;
            this.feld_Handy.Text = " Handy";
            this.feld_Handy.Texts = "";
            // 
            // feld_Email
            // 
            this.feld_Email.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Email.Location = new System.Drawing.Point(409, 167);
            this.feld_Email.Name = "feld_Email";
            this.feld_Email.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Email.PlaceholderText = " E-Mail";
            this.feld_Email.Size = new System.Drawing.Size(388, 27);
            this.feld_Email.TabIndex = 9;
            this.feld_Email.Text = " E-Mail";
            this.feld_Email.Texts = "";
            // 
            // mtl_Beschreibung
            // 
            this.mtl_Beschreibung.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mtl_Beschreibung.ForeColor = System.Drawing.Color.DarkGray;
            this.mtl_Beschreibung.Location = new System.Drawing.Point(8, 214);
            this.mtl_Beschreibung.Name = "mtl_Beschreibung";
            this.mtl_Beschreibung.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.mtl_Beschreibung.PlaceholderText = " Bescheibung / Freitext (auf Rechnung nicht sichtbar)";
            this.mtl_Beschreibung.Size = new System.Drawing.Size(787, 264);
            this.mtl_Beschreibung.TabIndex = 10;
            this.mtl_Beschreibung.Text = " Bescheibung / Freitext (auf Rechnung nicht sichtbar)";
            this.mtl_Beschreibung.Texts = "";
            this.BorderBody.ResumeLayout(false);
            this.BorderBody.PerformLayout();
            Icon = Properties.Resources.Kunde_ico;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private static List<string> GetAllCountrysNames()
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            return cultures
                    .Select(cult => (new RegionInfo(cult.LCID)).DisplayName)
                    .Distinct()
                    .OrderBy(q => q)
                    .ToList();
        }
        private Kunde CopieDataFromMask()
        {
            Kunde kunde = new Kunde();
            if (!string.IsNullOrWhiteSpace(feld_ID.Texts))
            {
                kunde.Id = int.Parse(feld_ID.Texts);
            }
            kunde.Anrede = feld_Anrede.Texts;
            kunde.Name = feld_Kundenname.Texts;
            kunde.Ansprechpartner = feld_Kontaktperson.Texts;
            DateTime g_date;
            if (DateTime.TryParse(feld_Geburtsdatum.Texts, out g_date))
            {
                kunde.Geburtsdatum = g_date;
            }
            kunde.Strasse = feld_Straße.Texts;
            kunde.Plz = feld_PLZ.Texts;
            kunde.Ort = feld_Ort.Texts;
            kunde.Land = comboBox_Land.Text;
            kunde.Handy = feld_Handy.Texts;
            kunde.Email = feld_Email.Texts;
            kunde.Beschreibung = mtl_Beschreibung.Texts;

            return kunde;
        }
    }
}