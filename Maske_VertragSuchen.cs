using MyControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace FCC_Verwaltungssystem
{
    public class Maske_VertragSuchen : MyForm
    {
        private MyGroupBox myGroupBox4;
        private MySearchCheckBox sCheckbox_Kind;
        private MySearchCheckBox sCheckbox_Erwachsene;
        private MySearchFieldFactory sFeld_Betrag;
        private MySearchFieldData sFeld_Vertragsbeginn;
        private MyGroupBox myGroupBox3;
        private MyRadioButton sCheckbox_VertreterSuchen;
        private MyRadioButton sCheckbox_MitgliedSuchen;
        private MySearchCheckBox sCheckbox_Weiblich;
        private MySearchCheckBox sCheckbox_Maennlich;
        private MySearchCheckBox sCheckbox_Minderjaehrige;
        private MySearchNumericField sFeld_PLZ;
        private MySearchFieldText sFeld_Ort;
        private MySearchFieldText sFeld_Strasse;
        private MySearchFieldText sFeld_Handy;
        private MySearchFieldText sFeld_Nachname;
        private MySearchFieldData sFeld_Geburtsdatum;
        private MySerachRichTextBox sFeld_Anmerkung;
        private MySearchCheckBox sCheckbox_Pausiert;
        private MySearchCheckBox sCheckbox_Ehemaliger_Mitglieder;
        private RowMergeView dataGrid;
        private MyPrintButton pbPrint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private MyCheckedListBox checkedListKurse;
        private MySearchFieldText sFeld_Vorname;

        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_VertragSuchen));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            myGroupBox4 = new MyGroupBox();
            sFeld_Anmerkung = new MySerachRichTextBox();
            sCheckbox_Pausiert = new MySearchCheckBox();
            label6 = new System.Windows.Forms.Label();
            sCheckbox_Ehemaliger_Mitglieder = new MySearchCheckBox();
            sCheckbox_Kind = new MySearchCheckBox();
            sCheckbox_Erwachsene = new MySearchCheckBox();
            sFeld_Betrag = new MySearchFieldFactory();
            sFeld_Vertragsbeginn = new MySearchFieldData();
            myGroupBox3 = new MyGroupBox();
            sCheckbox_VertreterSuchen = new MyRadioButton();
            sCheckbox_MitgliedSuchen = new MyRadioButton();
            sCheckbox_Weiblich = new MySearchCheckBox();
            sCheckbox_Maennlich = new MySearchCheckBox();
            sCheckbox_Minderjaehrige = new MySearchCheckBox();
            sFeld_PLZ = new MySearchNumericField();
            sFeld_Ort = new MySearchFieldText();
            sFeld_Strasse = new MySearchFieldText();
            sFeld_Handy = new MySearchFieldText();
            sFeld_Nachname = new MySearchFieldText();
            sFeld_Geburtsdatum = new MySearchFieldData();
            sFeld_Vorname = new MySearchFieldText();
            pbPrint = new MyPrintButton();
            dataGrid = new RowMergeView();
            checkedListKurse = new MyCheckedListBox();
            label1 = new System.Windows.Forms.Label();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            myGroupBox4.SuspendLayout();
            myGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGrid)).BeginInit();
            SuspendLayout();
            // 
            // pbOk
            // 
            pbOk.BackColor = Color.Transparent;
            pbOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbOk.FlatAppearance.BorderSize = 2;
            pbOk.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbOk.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbOk.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            pbOk.Image = ((Image)(resources.GetObject("pbOk.Image")));
            pbOk.ImageAlign = ContentAlignment.TopLeft;
            pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbOk.UseVisualStyleBackColor = false;
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(dataGrid);
            BorderBody.Controls.Add(pbPrint);
            BorderBody.Controls.Add(myGroupBox4);
            BorderBody.Controls.Add(myGroupBox3);
            // 
            // myGroupBox4
            // 
            myGroupBox4.BorderColor = Color.DimGray;
            myGroupBox4.BorderThickness = 1;
            myGroupBox4.Controls.Add(label1);
            myGroupBox4.Controls.Add(checkedListKurse);
            myGroupBox4.Controls.Add(sFeld_Anmerkung);
            myGroupBox4.Controls.Add(sCheckbox_Pausiert);
            myGroupBox4.Controls.Add(label6);
            myGroupBox4.Controls.Add(sCheckbox_Ehemaliger_Mitglieder);
            myGroupBox4.Controls.Add(sCheckbox_Kind);
            myGroupBox4.Controls.Add(sCheckbox_Erwachsene);
            myGroupBox4.Controls.Add(sFeld_Betrag);
            myGroupBox4.Controls.Add(sFeld_Vertragsbeginn);
            myGroupBox4.Location = new Point(437, 11);
            myGroupBox4.Name = "myGroupBox4";
            myGroupBox4.Size = new Size(360, 245);
            myGroupBox4.TabIndex = 3;
            myGroupBox4.TabStop = false;
            myGroupBox4.Text = "Vertragsdaten";
            // 
            // sFeld_Anmerkung
            // 
            sFeld_Anmerkung.ForeColor = Color.DarkGray;
            sFeld_Anmerkung.Location = new Point(220, 169);
            sFeld_Anmerkung.Name = "sFeld_Anmerkung";
            sFeld_Anmerkung.PlaceholderColor = Color.DarkGray;
            sFeld_Anmerkung.PlaceholderText = " Anmerkung";
            sFeld_Anmerkung.Size = new Size(130, 70);
            sFeld_Anmerkung.TabIndex = 10;
            sFeld_Anmerkung.Text = " Anmerkung";
            sFeld_Anmerkung.Texts = "";
            // 
            // sCheckbox_Pausiert
            // 
            sCheckbox_Pausiert.Location = new Point(7, 215);
            sCheckbox_Pausiert.Name = "sCheckbox_Pausiert";
            sCheckbox_Pausiert.Size = new Size(111, 23);
            sCheckbox_Pausiert.TabIndex = 9;
            sCheckbox_Pausiert.Text = "inkl. pausiert";
            // 
            // label6
            // 
            label6.BackColor = Color.DimGray;
            label6.Location = new Point(17, 176);
            label6.Name = "label6";
            label6.Size = new Size(185, 1);
            label6.TabIndex = 7;
            // 
            // sCheckbox_Ehemaliger_Mitglieder
            // 
            sCheckbox_Ehemaliger_Mitglieder.Location = new Point(6, 186);
            sCheckbox_Ehemaliger_Mitglieder.Name = "sCheckbox_Ehemaliger_Mitglieder";
            sCheckbox_Ehemaliger_Mitglieder.Size = new Size(200, 23);
            sCheckbox_Ehemaliger_Mitglieder.TabIndex = 8;
            sCheckbox_Ehemaliger_Mitglieder.Text = "inkl. ehemalige Mitglieder";
            // 
            // sCheckbox_Kind
            // 
            sCheckbox_Kind.Location = new Point(7, 146);
            sCheckbox_Kind.Name = "sCheckbox_Kind";
            sCheckbox_Kind.Size = new Size(127, 23);
            sCheckbox_Kind.TabIndex = 3;
            sCheckbox_Kind.Text = "Katgorie Kinder";
            // 
            // sCheckbox_Erwachsene
            // 
            sCheckbox_Erwachsene.Location = new Point(7, 113);
            sCheckbox_Erwachsene.Name = "sCheckbox_Erwachsene";
            sCheckbox_Erwachsene.Size = new Size(163, 23);
            sCheckbox_Erwachsene.TabIndex = 2;
            sCheckbox_Erwachsene.Text = "Katgorie Erwachsene";
            // 
            // sFeld_Betrag
            // 
            sFeld_Betrag.ForeColor = Color.DarkGray;
            sFeld_Betrag.Location = new Point(7, 65);
            sFeld_Betrag.Name = "sFeld_Betrag";
            sFeld_Betrag.PlaceholderColor = Color.DarkGray;
            sFeld_Betrag.PlaceholderText = "Betrag";
            sFeld_Betrag.Size = new Size(203, 27);
            sFeld_Betrag.TabIndex = 1;
            sFeld_Betrag.Text = "Betrag";
            sFeld_Betrag.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            sFeld_Betrag.Texts = "";
            // 
            // sFeld_Vertragsbeginn
            // 
            sFeld_Vertragsbeginn.Location = new Point(7, 32);
            sFeld_Vertragsbeginn.Name = "sFeld_Vertragsbeginn";
            sFeld_Vertragsbeginn.PlaceholderColor = Color.DarkGray;
            sFeld_Vertragsbeginn.PlaceholderText = "";
            sFeld_Vertragsbeginn.Size = new Size(203, 27);
            sFeld_Vertragsbeginn.TabIndex = 0;
            sFeld_Vertragsbeginn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            sFeld_Vertragsbeginn.Texts = "";
            sFeld_Vertragsbeginn.ToolTip = " Vertragsbeginn";
            sFeld_Vertragsbeginn.ValidatingType = typeof(System.DateTime);
            // 
            // myGroupBox3
            // 
            myGroupBox3.BorderColor = Color.DimGray;
            myGroupBox3.BorderThickness = 1;
            myGroupBox3.Controls.Add(sCheckbox_VertreterSuchen);
            myGroupBox3.Controls.Add(sCheckbox_MitgliedSuchen);
            myGroupBox3.Controls.Add(sCheckbox_Weiblich);
            myGroupBox3.Controls.Add(sCheckbox_Maennlich);
            myGroupBox3.Controls.Add(sCheckbox_Minderjaehrige);
            myGroupBox3.Controls.Add(sFeld_PLZ);
            myGroupBox3.Controls.Add(sFeld_Ort);
            myGroupBox3.Controls.Add(sFeld_Strasse);
            myGroupBox3.Controls.Add(sFeld_Handy);
            myGroupBox3.Controls.Add(sFeld_Nachname);
            myGroupBox3.Controls.Add(sFeld_Geburtsdatum);
            myGroupBox3.Controls.Add(sFeld_Vorname);
            myGroupBox3.Location = new Point(9, 11);
            myGroupBox3.Name = "myGroupBox3";
            myGroupBox3.Size = new Size(422, 245);
            myGroupBox3.TabIndex = 2;
            myGroupBox3.TabStop = false;
            myGroupBox3.Text = "Mitglieg / Ansprechpartner";
            // 
            // sCheckbox_VertreterSuchen
            // 
            sCheckbox_VertreterSuchen.Location = new Point(229, 29);
            sCheckbox_VertreterSuchen.Name = "sCheckbox_VertreterSuchen";
            sCheckbox_VertreterSuchen.Size = new Size(133, 23);
            sCheckbox_VertreterSuchen.TabIndex = 12;
            sCheckbox_VertreterSuchen.TabStop = true;
            sCheckbox_VertreterSuchen.Text = "Ansprechpartner";
            sCheckbox_VertreterSuchen.CheckedChanged += SuchparametersChanged;
            // 
            // sCheckbox_MitgliedSuchen
            // 
            sCheckbox_MitgliedSuchen.Location = new Point(27, 29);
            sCheckbox_MitgliedSuchen.Name = "sCheckbox_MitgliedSuchen";
            sCheckbox_MitgliedSuchen.Size = new Size(82, 23);
            sCheckbox_MitgliedSuchen.TabIndex = 11;
            sCheckbox_MitgliedSuchen.TabStop = true;
            sCheckbox_MitgliedSuchen.Text = "Mitglied";
            sCheckbox_MitgliedSuchen.CheckedChanged += SuchparametersChanged;
            // 
            // sCheckbox_Weiblich
            // 
            sCheckbox_Weiblich.Location = new Point(327, 197);
            sCheckbox_Weiblich.Name = "sCheckbox_Weiblich";
            sCheckbox_Weiblich.Size = new Size(82, 23);
            sCheckbox_Weiblich.TabIndex = 10;
            sCheckbox_Weiblich.Text = "weiblich";
            // 
            // sCheckbox_Maennlich
            // 
            sCheckbox_Maennlich.Location = new Point(211, 198);
            sCheckbox_Maennlich.Name = "sCheckbox_Maennlich";
            sCheckbox_Maennlich.Size = new Size(88, 23);
            sCheckbox_Maennlich.TabIndex = 9;
            sCheckbox_Maennlich.Text = "männlich";
            // 
            // sCheckbox_Minderjaehrige
            // 
            sCheckbox_Minderjaehrige.Location = new Point(302, 102);
            sCheckbox_Minderjaehrige.Name = "sCheckbox_Minderjaehrige";
            sCheckbox_Minderjaehrige.Size = new Size(112, 23);
            sCheckbox_Minderjaehrige.TabIndex = 8;
            sCheckbox_Minderjaehrige.Text = "Minderjährig";
            // 
            // sFeld_PLZ
            // 
            sFeld_PLZ.ForeColor = Color.DarkGray;
            sFeld_PLZ.Location = new Point(6, 164);
            sFeld_PLZ.Name = "sFeld_PLZ";
            sFeld_PLZ.PlaceholderColor = Color.DarkGray;
            sFeld_PLZ.PlaceholderText = " PLZ";
            sFeld_PLZ.Size = new Size(203, 27);
            sFeld_PLZ.TabIndex = 7;
            sFeld_PLZ.Text = " PLZ";
            sFeld_PLZ.Texts = "";
            // 
            // sFeld_Ort
            // 
            sFeld_Ort.ForeColor = Color.DarkGray;
            sFeld_Ort.Location = new Point(211, 164);
            sFeld_Ort.Name = "sFeld_Ort";
            sFeld_Ort.PlaceholderColor = Color.DarkGray;
            sFeld_Ort.PlaceholderText = " Ort";
            sFeld_Ort.Size = new Size(203, 27);
            sFeld_Ort.TabIndex = 6;
            sFeld_Ort.Text = " Ort";
            sFeld_Ort.Texts = "";
            // 
            // sFeld_Strasse
            // 
            sFeld_Strasse.ForeColor = Color.DarkGray;
            sFeld_Strasse.Location = new Point(6, 131);
            sFeld_Strasse.Name = "sFeld_Strasse";
            sFeld_Strasse.PlaceholderColor = Color.DarkGray;
            sFeld_Strasse.PlaceholderText = " Straße";
            sFeld_Strasse.Size = new Size(408, 27);
            sFeld_Strasse.TabIndex = 4;
            sFeld_Strasse.Text = " Straße";
            sFeld_Strasse.Texts = "";
            // 
            // sFeld_Handy
            // 
            sFeld_Handy.ForeColor = Color.DarkGray;
            sFeld_Handy.Location = new Point(6, 197);
            sFeld_Handy.Name = "sFeld_Handy";
            sFeld_Handy.PlaceholderColor = Color.DarkGray;
            sFeld_Handy.PlaceholderText = " Handy";
            sFeld_Handy.Size = new Size(203, 27);
            sFeld_Handy.TabIndex = 3;
            sFeld_Handy.Text = " Handy";
            sFeld_Handy.Texts = "";
            // 
            // sFeld_Nachname
            // 
            sFeld_Nachname.ForeColor = Color.DarkGray;
            sFeld_Nachname.Location = new Point(211, 65);
            sFeld_Nachname.Name = "sFeld_Nachname";
            sFeld_Nachname.PlaceholderColor = Color.DarkGray;
            sFeld_Nachname.PlaceholderText = " Nachname";
            sFeld_Nachname.Size = new Size(203, 27);
            sFeld_Nachname.TabIndex = 2;
            sFeld_Nachname.Text = " Nachname";
            sFeld_Nachname.Texts = "";
            // 
            // sFeld_Geburtsdatum
            // 
            sFeld_Geburtsdatum.Location = new Point(6, 98);
            sFeld_Geburtsdatum.Name = "sFeld_Geburtsdatum";
            sFeld_Geburtsdatum.PlaceholderColor = Color.DarkGray;
            sFeld_Geburtsdatum.PlaceholderText = "";
            sFeld_Geburtsdatum.Size = new Size(203, 27);
            sFeld_Geburtsdatum.TabIndex = 1;
            sFeld_Geburtsdatum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            sFeld_Geburtsdatum.Texts = "";
            sFeld_Geburtsdatum.ToolTip = " Geburstdatum";
            sFeld_Geburtsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // sFeld_Vorname
            // 
            sFeld_Vorname.ForeColor = Color.DarkGray;
            sFeld_Vorname.Location = new Point(6, 65);
            sFeld_Vorname.Name = "sFeld_Vorname";
            sFeld_Vorname.PlaceholderColor = Color.DarkGray;
            sFeld_Vorname.PlaceholderText = " Vorname";
            sFeld_Vorname.Size = new Size(203, 27);
            sFeld_Vorname.TabIndex = 0;
            sFeld_Vorname.Text = " Vorname";
            sFeld_Vorname.Texts = "";
            // 
            // myPrintButton1
            // 
            pbPrint.BackColor = Color.Transparent;
            pbPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbPrint.FlatAppearance.BorderSize = 2;
            pbPrint.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbPrint.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbPrint.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            pbPrint.ForeColor = Color.Black;
            pbPrint.Image = ((Image)(resources.GetObject("myPrintButton1.Image")));
            pbPrint.ImageAlign = ContentAlignment.TopLeft;
            pbPrint.Location = new Point(9, 262);
            pbPrint.Name = "myPrintButton1";
            pbPrint.ReadOnly = false;
            pbPrint.Size = new Size(151, 33);
            pbPrint.TabIndex = 4;
            pbPrint.Text = "Drucken";
            pbPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbPrint.UseVisualStyleBackColor = false;
            pbPrint.ToPrintedTable = dataGrid;
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.ReadOnly = true;
            dataGrid.Location = new Point(10, 297);
            dataGrid.Name = "dataGrid";
            dataGrid.Size = new Size(786, 193);
            dataGrid.ColumnHeaderMouseClick += DataGrid_ColumnHeaderMouseClick;
            // 
            // checkedListKurse
            // 
            checkedListKurse.BackColor = SystemColors.Control;
            checkedListKurse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            checkedListKurse.CheckOnClick = true;
            checkedListKurse.ColumnWidth = 32;
            checkedListKurse.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            checkedListKurse.FormattingEnabled = true;
            checkedListKurse.Location = new Point(220, 32);
            checkedListKurse.MultiColumn = true;
            checkedListKurse.Name = "checkedListKurse";
            checkedListKurse.Size = new Size(130, 132);
            checkedListKurse.TabIndex = 0;
            // 
            // label1
            // 
            label1.BackColor = Color.DimGray;
            label1.Location = new Point(17, 102);
            label1.Name = "label1";
            label1.Size = new Size(185, 1);
            label1.TabIndex = 11;
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Vertragsuchen_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            myGroupBox4.ResumeLayout(false);
            myGroupBox4.PerformLayout();
            myGroupBox3.ResumeLayout(false);
            myGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGrid)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void SuchparametersChanged(object sender, EventArgs e)
        {
            sCheckbox_Minderjaehrige.Checked = false;
            sCheckbox_Maennlich.Checked = true;
            sCheckbox_Weiblich.Checked = true;

            if (sCheckbox_VertreterSuchen.Checked)
            {
                sCheckbox_Minderjaehrige.Enabled = false;
                sCheckbox_Maennlich.Enabled = false;
                sCheckbox_Weiblich.Enabled = false;
                
            }
            else if (sCheckbox_MitgliedSuchen.Checked)
            {
                sCheckbox_Minderjaehrige.Enabled = true;
                sCheckbox_Maennlich.Enabled = true;
                sCheckbox_Weiblich.Enabled = true;
            }
        }

        private void DataGrid_ColumnHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            setRowHeaderColor();
        }
        protected override string _name()
        {
            return "Verträge suchen";
        }
        protected override void _OnLoad(EventArgs e)
        {
            sCheckbox_MitgliedSuchen.Checked = true;
            sCheckbox_Maennlich.Checked = true;
            sCheckbox_Weiblich.Checked = true;
            sCheckbox_Erwachsene.Checked = true;
            sCheckbox_Kind.Checked = true;
            DataTable dt = DataAccessLayer.Query_Kurs();
            checkedListKurse.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                checkedListKurse.Items.Add(row["Name"].ToString(), true);
            }
        }
        protected override bool _Populate()
        {
            Vertrag vertrag = CopieDataFromMask();
            DataTable dataTable = DataAccessLayer.Query_Journal(vertrag);
            dataGrid.DataSource = dataTable;
            dataGrid.ClearSelection();

            setRowHeaderColor();
            if (User.Rechte.SUMMENVERTRAEGE_SEHEN())
            {
                //summeFelder();
            }
            return true;
        }
        protected override bool _Save()
        {
            return true;
        }
        private Vertrag CopieDataFromMask()
        {
            Vertrag vertrag = new Vertrag();
            Mitglied mitglied = new Mitglied();
            VertreterMitglied vertreterMitglied = new VertreterMitglied();
            List<Kurs> kurse = new List<Kurs>();

            if (sCheckbox_MitgliedSuchen.Checked)
            {
                mitglied.Vorname = sFeld_Vorname.Texts;
                mitglied.Nachname = sFeld_Nachname.Texts;
                mitglied.Geburtsdatum = string.IsNullOrEmpty(sFeld_Geburtsdatum.Texts) ? (DateTime?)null : DateTime.Parse(sFeld_Geburtsdatum.Texts);
                mitglied.Strasse = sFeld_Strasse.Texts;
                mitglied.Plz = sFeld_PLZ.Texts;
                mitglied.Ort = sFeld_Ort.Texts;
                mitglied.Handy = sFeld_Handy.Texts;
                mitglied.Miderjaehrige = sCheckbox_Minderjaehrige.Checked;
                if (sCheckbox_Weiblich.Checked && sCheckbox_Maennlich.Checked)
                {
                    // nix tun. Suche (M,W)
                }
                else if (!sCheckbox_Weiblich.Checked && !sCheckbox_Maennlich.Checked)
                {
                    // etwas, das nicht in der DB vorhanden ist.
                    // Hier muss mindestens ein Feld ausgewählt werden,
                    // sonst die Suche geht schief
                    mitglied.Geschlecht = "?";
                }
                else
                {
                    if (sCheckbox_Maennlich.Checked)
                    {
                        mitglied.Geschlecht = "M";
                    }
                    else
                    {
                        mitglied.Geschlecht = "W";
                    }

                }
            }
            else
            {
                vertreterMitglied.Vorname = sFeld_Vorname.Texts;
                vertreterMitglied.Nachname = sFeld_Nachname.Texts;
                vertreterMitglied.Geburtsdatum = string.IsNullOrEmpty(sFeld_Geburtsdatum.Texts) ? (DateTime?)null : DateTime.Parse(sFeld_Geburtsdatum.Texts);
                vertreterMitglied.Strasse = sFeld_Strasse.Texts;
                vertreterMitglied.Plz = sFeld_PLZ.Texts;
                vertreterMitglied.Ort = sFeld_Ort.Texts;
                vertreterMitglied.Handy = sFeld_Handy.Texts;
            }

            for (int i = 0; i < checkedListKurse.Items.Count; i++)
            {
                if (checkedListKurse.GetItemChecked(i))
                {
                    Kurs kurs = new Kurs();
                    kurs.Name = checkedListKurse.Items[i].ToString();
                    if (!string.IsNullOrEmpty(sFeld_Betrag.Texts))
                    {
                        kurs.Betrag = float.Parse(sFeld_Betrag.Texts);
                    }
                    kurse.Add(kurs);
                }
            }
            vertrag.Beginn = string.IsNullOrEmpty(sFeld_Vertragsbeginn.Texts) ? (DateTime?)null : DateTime.Parse(sFeld_Vertragsbeginn.Texts);
            if (sCheckbox_Erwachsene.Checked != sCheckbox_Kind.Checked)
            {
                if (sCheckbox_Erwachsene.Checked)
                {
                    vertrag.Kategorie = KategorieVertrag.ERWACHSENE;
                }
                else
                {
                    vertrag.Kategorie = KategorieVertrag.KIND;
                }
            }
            vertrag.Anmerkung = sFeld_Anmerkung.Texts;

            vertrag.Status = StatusVertrag.ACTIVE;
            if (sCheckbox_Ehemaliger_Mitglieder.Checked)
            {
                vertrag.Status |= StatusVertrag.GEKUENDIGT;
            }
            if (sCheckbox_Pausiert.Checked)
            {
                vertrag.Status |= StatusVertrag.PAUSIERT;
            }

            mitglied.Vertreter = vertreterMitglied;
            vertrag.Mitglied = mitglied;
            vertrag.Kurse = kurse;
            return vertrag;
        }
        private void setRowHeaderColor()
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                var status = dataGrid.Rows[i].Cells["Status"].Value.ToString();
                if (status.Equals(StatusVertrag.ACTIVE.ToString()))
                {
                    dataGrid.Rows[i].HeaderCell.Style.BackColor = Color.ForestGreen;
                    dataGrid.Rows[i].HeaderCell.Style.SelectionBackColor = Color.ForestGreen;
                }
                else if (status.Equals(StatusVertrag.GEKUENDIGT.ToString()))
                {
                    dataGrid.Rows[i].HeaderCell.Style.BackColor = Color.Brown;
                    dataGrid.Rows[i].HeaderCell.Style.SelectionBackColor = Color.Brown;
                }
                else if (status.Equals(StatusVertrag.PAUSIERT.ToString()))
                {
                    dataGrid.Rows[i].HeaderCell.Style.BackColor = Color.Yellow;
                    dataGrid.Rows[i].HeaderCell.Style.SelectionBackColor = Color.Yellow;
                }

            }
        }
    }
}