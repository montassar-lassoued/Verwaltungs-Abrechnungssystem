using MyControls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Journal : MyForm
    {
        private MyGroupBox myGroupBox2;
        private MySearchFieldText feld_Rechnungsnummer;
        private MyRadioButton rb_Kunde;
        private MySearchFieldText feld_Ort;
        private MySearchFieldText feld_PLZ;
        private MySearchFieldText feld_Strasse;
        private MySearchFieldText feld_Nachname;
        private MySearchFieldData feld_Geburtsdatum;
        private MySearchFieldText feld_Kundenname;
        private MySearchFieldData feld_Leistungsdatum;
        private MySearchFieldData feld_Rechnungsdatum;
        private MyRadioButton rb_Rechnung;
        private MySearchCheckBox cb_RechnungStorniert;
        private MySearchCheckBox cb_RechnungBezahlt;
        private MySearchCheckBox cb_RechnungOffen;
        private RowMergeView dataGrid_Rechnung;
        private RowMergeView dataGrid_Kunde;
        private MyLabel myLabel2;
        private MyLabel myLabel1;
        private MyCircle myCircle1;
        private Label label4;
        private MyCircle myCircle3;
        private Label label3;
        private MyCircle myCircle2;
        private Label label2;
        private Label label1;
        private MyCircle myCircle4;
        private Label label5;
        private MyCircle myCircle5;
        private MyGroupBox myGroupBox1;

        protected override string _name()
        {
            return "Journal";
        }
        protected override void _OnLoad(EventArgs e)
        {
            rb_Kunde.Checked = true;
        }
        protected override bool _Populate()
        {
            dataGrid_Kunde.DataSource = null;
            dataGrid_Rechnung.DataSource = null;
            if (!User.Rechte.KUNDEN_SUCHEN())
            {
                MessageBox.Show("Keine Berechtigung Kundendaten zu suchen!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            DataTable dataTable;
            // suche nach Kunden
            if (rb_Kunde.Checked)
            {
                Kunde kunde = copieKundenDataFromMask();
                dataTable = DataAccessLayer.Get_Kunde(kunde);
            }
            else if (rb_Rechnung.Checked)
            {
                RechnungHelper rechnung = copieRechnungDataFromMask();
                if(rechnung == null)
                {
                    return false;
                }
                dataTable = DataAccessLayer.get_KundeByRechnung(rechnung);
            }
            else
            {
                MessageBox.Show("Die Suche nach Kunde oder nach Rechnung" +
                    "soll ausgewählt werden", "Info!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (dataTable == null || dataTable.Rows.Count < 1)
            {
                MessageBox.Show("Keine Daten gefunden", "Info!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = dataTable;
            dataGrid_Kunde.DataSource = dataTable;
            dataGrid_Kunde.ClearSelection();

            return true;
        }
        protected override bool _Save()
        {
            return false;
        }
        private Kunde copieKundenDataFromMask()
        {
            Kunde kunde = new Kunde();
            kunde.Name = feld_Kundenname.Texts;
            if (!string.IsNullOrWhiteSpace(feld_Geburtsdatum.Texts))
            {
                kunde.Geburtsdatum = DateTime.Parse(feld_Geburtsdatum.Texts);
            }
            kunde.Strasse = feld_Strasse.Texts;
            kunde.Plz = feld_PLZ.Texts;
            kunde.Ort = feld_Ort.Texts;

            return kunde;
        }
        private RechnungHelper copieRechnungDataFromMask()
        {
            RechnungHelper rechnung = new RechnungHelper();
            rechnung.Rechnungsnummer = feld_Rechnungsnummer.Texts;

            if (cb_RechnungOffen.Checked)
            {
                rechnung.Zahlungsstatus = FuegeStatus(rechnung.Zahlungsstatus,Zahlungsstatus.OFFEN);
            }
            if (cb_RechnungBezahlt.Checked)
            {
                rechnung.Zahlungsstatus = FuegeStatus(rechnung.Zahlungsstatus, Zahlungsstatus.BEZAHLT);
            }
            if (cb_RechnungStorniert.Checked)
            {
                rechnung.Zahlungsstatus = FuegeStatus(rechnung.Zahlungsstatus, Zahlungsstatus.STORNIERT);
            }
            if(rechnung.Zahlungsstatus == null)
            {
                MessageBox.Show("Bitte mind. 1 Zahlungsstatus für die Rechnung auswählen");
                return null;
            }
            if (!string.IsNullOrEmpty(feld_Leistungsdatum.Texts))
            {
                rechnung.LeistungsBeginn = DateTime.Parse(feld_Leistungsdatum.Texts).Date;
            }
            if (!string.IsNullOrEmpty(feld_Rechnungsdatum.Texts))
            {
                rechnung.Rechnungsdatum = DateTime.Parse(feld_Rechnungsdatum.Texts).Date;
            }

            return rechnung;
        }
        private Zahlungsstatus FuegeStatus(Zahlungsstatus? zahlungsstatus , Zahlungsstatus newstatus)
        {
            if (zahlungsstatus == null)
                return newstatus;

            return zahlungsstatus.Value | newstatus;
        }
        private void dataGrid_Kunde_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (!User.Rechte.RECHNUNG_SUCHEN())
            {
                MessageBox.Show("Kein Recht eine Rechnung zu suchen!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            dataGrid_Rechnung.DataSource = null;
            RechnungHelper rechnung = new RechnungHelper();
            string strKundenID = dataGrid_Kunde.Rows[e.RowIndex].Cells["ID"].Value.ToString().Trim();
            int kundenID;
            if (int.TryParse(strKundenID, out kundenID))
            {
                if (rb_Rechnung.Checked)
                {
                    rechnung = copieRechnungDataFromMask();
                    if (rechnung == null)
                    {
                        return ;
                    }
                }
                rechnung.Kunden_id = kundenID;
                DataTable dataTable = DataAccessLayer.QueryRechnung(rechnung);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;
                dataGrid_Rechnung.DataSource = dataTable;
                dataGrid_Rechnung.ClearSelection();
                setRowHeaderColor();
            }

        }
        private void dataGrid_Rechnung_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (!User.Rechte.RECHNUNG_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung eine Rechnung zu bearbeiten!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            string rechnungsnummer = dataGrid_Rechnung.Rows[e.RowIndex].Cells["Rechnungsnummer"].Value.ToString().Trim();
            if (string.IsNullOrWhiteSpace(rechnungsnummer))
            {
                MessageBox.Show("Rechnungsnummer für diese Rechnung ist nicht vorhanden", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            Maske_RechnungBearbeiten maske_Rechnungbearbeitung = new Maske_RechnungBearbeiten(rechnungsnummer);
            maske_Rechnungbearbeitung.ShowDialog();

            dataGrid_Kunde_CellClick(dataGrid_Kunde, new DataGridViewCellEventArgs(0, dataGrid_Kunde.CurrentCell.RowIndex));

        }
        private void setRowHeaderColor()
        {
            for (int i = 0; i < dataGrid_Rechnung.Rows.Count; i++)
            {
                bool rechnungBezahlt = (bool)dataGrid_Rechnung.Rows[i].Cells["BEZAHLT"].Value;

                if (rechnungBezahlt)
                {
                    dataGrid_Rechnung.Rows[i].HeaderCell.Style.BackColor = Color.Green;
                    dataGrid_Rechnung.Rows[i].HeaderCell.Style.SelectionBackColor = Color.Green;
                }
                else
                {
                    Prozessstatus prozessstatus;
                    string status = dataGrid_Rechnung.Rows[i].Cells["STATUS"].Value.ToString();
                    //prüfen, ob der Status stimmt bzw. eingetragen ist.
                    if (!Enum.TryParse(status, out prozessstatus))
                    {
                        dataGrid_Rechnung.Rows[i].HeaderCell.Style.BackColor = Color.DarkGray;
                        dataGrid_Rechnung.Rows[i].HeaderCell.Style.SelectionBackColor = Color.DarkGray;
                    }
                    else
                    {
                        if (prozessstatus.Equals(Prozessstatus.ERSTELLT) || prozessstatus.Equals(Prozessstatus.GEDRUCKT))
                        {
                            dataGrid_Rechnung.Rows[i].HeaderCell.Style.BackColor = Color.Tan;
                            dataGrid_Rechnung.Rows[i].HeaderCell.Style.SelectionBackColor = Color.Tan;
                        }
                        else if (prozessstatus.Equals(Prozessstatus.VERSANDT))
                        {
                            string strZahlungsziel = dataGrid_Rechnung.Rows[i].Cells["ZAHLUNGSZIEL"].Value.ToString();
                            if (string.IsNullOrWhiteSpace(strZahlungsziel))
                            {
                                string rechnungsnr = dataGrid_Rechnung.Rows[i].Cells["RECHNUNGSNUMMER"].Value.ToString();
                                MessageBox.Show("Das Zahlungsziel für Rechnungsnummer " + rechnungsnr + " ist ubekannt");
                            }
                            else
                            {
                                DateTime zahlungsziel = DateTime.Parse(strZahlungsziel).Date;
                                if (zahlungsziel < DateTime.Now.Date)
                                {
                                    dataGrid_Rechnung.Rows[i].HeaderCell.Style.BackColor = Color.Red;
                                    dataGrid_Rechnung.Rows[i].HeaderCell.Style.SelectionBackColor = Color.Red;
                                }
                                else
                                {
                                    dataGrid_Rechnung.Rows[i].HeaderCell.Style.BackColor = Color.Orange;
                                    dataGrid_Rechnung.Rows[i].HeaderCell.Style.SelectionBackColor = Color.Orange;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void SuchbereichAktivieren(object sender, EventArgs e)
        {
            if (rb_Kunde.Checked)
            {
                myGroupBox1.Enabled = true;
                myGroupBox2.Enabled = false;
            }
            else if (rb_Rechnung.Checked)
            {
                myGroupBox1.Enabled = false;
                myGroupBox2.Enabled = true;
            }
        }
        protected override void _InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Journal));
            myGroupBox1 = new MyGroupBox();
            feld_Ort = new MySearchFieldText();
            feld_PLZ = new MySearchFieldText();
            feld_Strasse = new MySearchFieldText();
            feld_Nachname = new MySearchFieldText();
            feld_Geburtsdatum = new MySearchFieldData();
            feld_Kundenname = new MySearchFieldText();
            rb_Kunde = new MyRadioButton();
            myGroupBox2 = new MyGroupBox();
            cb_RechnungStorniert = new MySearchCheckBox();
            cb_RechnungBezahlt = new MySearchCheckBox();
            cb_RechnungOffen = new MySearchCheckBox();
            feld_Leistungsdatum = new MySearchFieldData();
            feld_Rechnungsdatum = new MySearchFieldData();
            feld_Rechnungsnummer = new MySearchFieldText();
            rb_Rechnung = new MyRadioButton();
            dataGrid_Kunde = new RowMergeView();
            dataGrid_Rechnung = new RowMergeView();
            myLabel1 = new MyLabel();
            myLabel2 = new MyLabel();
            myCircle1 = new MyCircle();
            myCircle2 = new MyCircle();
            myCircle3 = new MyCircle();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            myCircle4 = new MyCircle();
            myCircle5 = new MyCircle();
            label5 = new Label();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            myGroupBox1.SuspendLayout();
            myGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGrid_Kunde)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGrid_Rechnung)).BeginInit();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(label5);
            BorderBody.Controls.Add(myCircle5);
            BorderBody.Controls.Add(myCircle4);
            BorderBody.Controls.Add(label4);
            BorderBody.Controls.Add(myCircle3);
            BorderBody.Controls.Add(label3);
            BorderBody.Controls.Add(myCircle2);
            BorderBody.Controls.Add(myCircle1);
            BorderBody.Controls.Add(label2);
            BorderBody.Controls.Add(label1);
            BorderBody.Controls.Add(myLabel2);
            BorderBody.Controls.Add(myLabel1);
            BorderBody.Controls.Add(dataGrid_Rechnung);
            BorderBody.Controls.Add(dataGrid_Kunde);
            BorderBody.Controls.Add(rb_Rechnung);
            BorderBody.Controls.Add(myGroupBox2);
            BorderBody.Controls.Add(rb_Kunde);
            BorderBody.Controls.Add(myGroupBox1);
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = Color.DimGray;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(feld_Ort);
            myGroupBox1.Controls.Add(feld_PLZ);
            myGroupBox1.Controls.Add(feld_Strasse);
            myGroupBox1.Controls.Add(feld_Nachname);
            myGroupBox1.Controls.Add(feld_Geburtsdatum);
            myGroupBox1.Controls.Add(feld_Kundenname);
            myGroupBox1.Location = new Point(8, 28);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new Size(423, 128);
            myGroupBox1.TabIndex = 0;
            myGroupBox1.TabStop = false;
            // 
            // feld_Ort
            // 
            feld_Ort.ForeColor = Color.DarkGray;
            feld_Ort.Location = new Point(212, 85);
            feld_Ort.Name = "feld_Ort";
            feld_Ort.PlaceholderColor = Color.DarkGray;
            feld_Ort.PlaceholderText = " Ort";
            feld_Ort.Size = new Size(203, 27);
            feld_Ort.TabIndex = 5;
            feld_Ort.Text = " Ort";
            feld_Ort.Texts = "";
            // 
            // feld_PLZ
            // 
            feld_PLZ.ForeColor = Color.DarkGray;
            feld_PLZ.Location = new Point(6, 85);
            feld_PLZ.Name = "feld_PLZ";
            feld_PLZ.PlaceholderColor = Color.DarkGray;
            feld_PLZ.PlaceholderText = " PLZ";
            feld_PLZ.Size = new Size(203, 27);
            feld_PLZ.TabIndex = 4;
            feld_PLZ.Text = " PLZ";
            feld_PLZ.Texts = "";
            // 
            // feld_Strasse
            // 
            feld_Strasse.ForeColor = Color.DarkGray;
            feld_Strasse.Location = new Point(212, 52);
            feld_Strasse.Name = "feld_Strasse";
            feld_Strasse.PlaceholderColor = Color.DarkGray;
            feld_Strasse.PlaceholderText = " Straße";
            feld_Strasse.Size = new Size(203, 27);
            feld_Strasse.TabIndex = 3;
            feld_Strasse.Text = " Straße";
            feld_Strasse.Texts = "";
            // 
            // feld_Nachname
            // 
            feld_Nachname.ForeColor = Color.DarkGray;
            feld_Nachname.Location = new Point(212, 19);
            feld_Nachname.Name = "feld_Nachname";
            feld_Nachname.PlaceholderColor = Color.DarkGray;
            feld_Nachname.PlaceholderText = " Nachname";
            feld_Nachname.Size = new Size(203, 27);
            feld_Nachname.TabIndex = 2;
            feld_Nachname.Text = " Nachname";
            feld_Nachname.Texts = "";
            // 
            // feld_Geburtsdatum
            // 
            feld_Geburtsdatum.Location = new Point(6, 52);
            feld_Geburtsdatum.Name = "feld_Geburtsdatum";
            feld_Geburtsdatum.PlaceholderColor = Color.DarkGray;
            feld_Geburtsdatum.PlaceholderText = "";
            feld_Geburtsdatum.Size = new Size(203, 27);
            feld_Geburtsdatum.TabIndex = 1;
            feld_Geburtsdatum.TextAlign = HorizontalAlignment.Center;
            feld_Geburtsdatum.ToolTip = " Geburtsdatum";
            feld_Geburtsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Kundenname
            // 
            feld_Kundenname.ForeColor = Color.DarkGray;
            feld_Kundenname.Location = new Point(6, 19);
            feld_Kundenname.Name = "feld_Kundenname";
            feld_Kundenname.PlaceholderColor = Color.DarkGray;
            feld_Kundenname.PlaceholderText = " Vorname";
            feld_Kundenname.Size = new Size(203, 27);
            feld_Kundenname.TabIndex = 0;
            feld_Kundenname.Text = " Vorname";
            feld_Kundenname.Texts = "";
            // 
            // rb_Kunde
            // 
            rb_Kunde.Location = new Point(14, 18);
            rb_Kunde.Name = "rb_Kunde";
            rb_Kunde.Size = new Size(67, 23);
            rb_Kunde.TabIndex = 1;
            rb_Kunde.TabStop = true;
            rb_Kunde.Text = "Kunde";
            rb_Kunde.CheckedChanged += SuchbereichAktivieren;
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = Color.DimGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(cb_RechnungStorniert);
            myGroupBox2.Controls.Add(cb_RechnungBezahlt);
            myGroupBox2.Controls.Add(cb_RechnungOffen);
            myGroupBox2.Controls.Add(feld_Leistungsdatum);
            myGroupBox2.Controls.Add(feld_Rechnungsdatum);
            myGroupBox2.Controls.Add(feld_Rechnungsnummer);
            myGroupBox2.Location = new Point(447, 28);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new Size(350, 128);
            myGroupBox2.TabIndex = 2;
            myGroupBox2.TabStop = false;
            // 
            // cb_RechnungStorniert
            // 
            cb_RechnungStorniert.Enabled = false;
            cb_RechnungStorniert.Location = new Point(238, 89);
            cb_RechnungStorniert.Name = "cb_RechnungStorniert";
            cb_RechnungStorniert.Size = new Size(83, 23);
            cb_RechnungStorniert.TabIndex = 9;
            cb_RechnungStorniert.Text = "Storniert";
            // 
            // cb_RechnungBezahlt
            // 
            cb_RechnungBezahlt.Location = new Point(238, 56);
            cb_RechnungBezahlt.Name = "cb_RechnungBezahlt";
            cb_RechnungBezahlt.Size = new Size(68, 23);
            cb_RechnungBezahlt.TabIndex = 8;
            cb_RechnungBezahlt.Text = "Bezalt";
            // 
            // cb_RechnungOffen
            // 
            cb_RechnungOffen.Location = new Point(238, 23);
            cb_RechnungOffen.Name = "cb_RechnungOffen";
            cb_RechnungOffen.Size = new Size(63, 23);
            cb_RechnungOffen.TabIndex = 7;
            cb_RechnungOffen.Text = "Offen";
            // 
            // feld_Leistungsdatum
            // 
            feld_Leistungsdatum.Location = new Point(13, 52);
            feld_Leistungsdatum.Name = "feld_Leistungsdatum";
            feld_Leistungsdatum.PlaceholderColor = Color.DarkGray;
            feld_Leistungsdatum.PlaceholderText = "";
            feld_Leistungsdatum.Size = new Size(203, 27);
            feld_Leistungsdatum.TabIndex = 6;
            feld_Leistungsdatum.TextAlign = HorizontalAlignment.Center;
            feld_Leistungsdatum.Texts = "  .  .";
            feld_Leistungsdatum.ToolTip = " Leistungsbeginn";
            feld_Leistungsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Rechnungsdatum
            // 
            feld_Rechnungsdatum.Location = new Point(13, 85);
            feld_Rechnungsdatum.Name = "feld_Rechnungsdatum";
            feld_Rechnungsdatum.PlaceholderColor = Color.DarkGray;
            feld_Rechnungsdatum.PlaceholderText = "";
            feld_Rechnungsdatum.Size = new Size(203, 27);
            feld_Rechnungsdatum.TabIndex = 6;
            feld_Rechnungsdatum.TextAlign = HorizontalAlignment.Center;
            feld_Rechnungsdatum.Texts = "  .  .";
            feld_Rechnungsdatum.ToolTip = " Rechnungsdatum";
            feld_Rechnungsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Rechnungsnummer
            // 
            feld_Rechnungsnummer.ForeColor = Color.DarkGray;
            feld_Rechnungsnummer.Location = new Point(13, 19);
            feld_Rechnungsnummer.Name = "feld_Rechnungsnummer";
            feld_Rechnungsnummer.PlaceholderColor = Color.DarkGray;
            feld_Rechnungsnummer.PlaceholderText = " Rechnungsnummer";
            feld_Rechnungsnummer.Size = new Size(203, 27);
            feld_Rechnungsnummer.TabIndex = 6;
            feld_Rechnungsnummer.Text = " Rechnungsnummer";
            feld_Rechnungsnummer.Texts = "";
            // 
            // rb_Rechnung
            // 
            rb_Rechnung.Location = new Point(460, 18);
            rb_Rechnung.Name = "rb_Rechnung";
            rb_Rechnung.Size = new Size(91, 23);
            rb_Rechnung.TabIndex = 3;
            rb_Rechnung.TabStop = true;
            rb_Rechnung.Text = "Rechnung";
            rb_Rechnung.CheckedChanged += SuchbereichAktivieren;
            // 
            // dataGrid_Kunde
            // 
            dataGrid_Kunde.AllowUserToAddRows = false;
            dataGrid_Kunde.AllowUserToDeleteRows = false;
            dataGrid_Kunde.AllowUserToResizeColumns = false;
            dataGrid_Kunde.AllowUserToResizeRows = false;
            dataGrid_Kunde.Location = new Point(8, 183);
            dataGrid_Kunde.MultiSelect = false;
            dataGrid_Kunde.Name = "dataGrid_Kunde";
            dataGrid_Kunde.ReadOnly = true;
            dataGrid_Kunde.Size = new Size(788, 150);
            dataGrid_Kunde.CellClick += dataGrid_Kunde_CellClick;

            // 
            // dataGrid_Rechnung
            // 
            dataGrid_Rechnung.AllowUserToAddRows = false;
            dataGrid_Rechnung.AllowUserToDeleteRows = false;
            dataGrid_Rechnung.AllowUserToResizeColumns = false;
            dataGrid_Rechnung.AllowUserToResizeRows = false;
            dataGrid_Rechnung.Location = new Point(8, 363);
            dataGrid_Rechnung.MultiSelect = false;
            dataGrid_Rechnung.Name = "dataGrid_Rechnung";
            dataGrid_Rechnung.ReadOnly = true;
            dataGrid_Rechnung.Size = new Size(788, 140);
            dataGrid_Rechnung.CellDoubleClick += dataGrid_Rechnung_CellDoubleClick;
            // 
            // myLabel1
            // 
            myLabel1.AutoSize = true;
            myLabel1.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel1.ForeColor = Color.Black;
            myLabel1.Location = new Point(4, 162);
            myLabel1.Name = "myLabel1";
            myLabel1.Size = new Size(57, 19);
            myLabel1.TabIndex = 6;
            myLabel1.Text = "Kunden";
            myLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel2
            // 
            myLabel2.AutoSize = true;
            myLabel2.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel2.ForeColor = Color.Black;
            myLabel2.Location = new Point(4, 341);
            myLabel2.Name = "myLabel2";
            myLabel2.Size = new Size(89, 19);
            myLabel2.TabIndex = 7;
            myLabel2.Text = "Rechnungen";
            myLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myCircle1
            // 
            myCircle1.FillColor = Color.OliveDrab;
            myCircle1.ForeColor = Color.OliveDrab;
            myCircle1.Location = new Point(121, 340);
            myCircle1.Name = "myCircle1";
            myCircle1.Size = new Size(17, 18);
            myCircle1.TabIndex = 8;
            myCircle1.Text = "myCircle1";
            // 
            // myCircle2
            // 
            myCircle2.FillColor = Color.Orange;
            myCircle2.ForeColor = Color.Orange;
            myCircle2.Location = new Point(212, 340);
            myCircle2.Name = "myCircle2";
            myCircle2.Size = new Size(17, 18);
            myCircle2.TabIndex = 9;
            myCircle2.Text = "myCircle2";
            // 
            // myCircle3
            // 
            myCircle3.FillColor = Color.OrangeRed;
            myCircle3.ForeColor = Color.Orange;
            myCircle3.Location = new Point(295, 340);
            myCircle3.Name = "myCircle3";
            myCircle3.Size = new Size(17, 18);
            myCircle3.TabIndex = 10;
            myCircle3.Text = "myCircle3";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new Point(137, 343);
            label1.Name = "label1";
            label1.Size = new Size(41, 13);
            label1.TabIndex = 11;
            label1.Text = "bezahlt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new Point(230, 343);
            label2.Name = "label2";
            label2.Size = new Size(31, 13);
            label2.TabIndex = 12;
            label2.Text = "offen";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new Point(312, 343);
            label3.Name = "label3";
            label3.Size = new Size(49, 13);
            label3.TabIndex = 13;
            label3.Text = "überfällig";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new Point(406, 343);
            label4.Name = "label4";
            label4.Size = new Size(219, 13);
            label4.TabIndex = 14;
            label4.Text = "erstellt / gedruckt aber noch nicht verschickt";
            // 
            // myCircle4
            // 
            myCircle4.FillColor = Color.Tan;
            myCircle4.ForeColor = Color.Orange;
            myCircle4.Location = new Point(388, 340);
            myCircle4.Name = "myCircle4";
            myCircle4.Size = new Size(17, 18);
            myCircle4.TabIndex = 18;
            myCircle4.Text = "myCircle4";
            // 
            // myCircle5
            // 
            myCircle5.FillColor = Color.DarkGray;
            myCircle5.ForeColor = Color.Orange;
            myCircle5.Location = new Point(653, 340);
            myCircle5.Name = "myCircle5";
            myCircle5.Size = new Size(17, 18);
            myCircle5.TabIndex = 19;
            myCircle5.Text = "myCircle5";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new Point(674, 343);
            label5.Name = "label5";
            label5.Size = new Size(112, 13);
            label5.TabIndex = 20;
            label5.Text = "staus nicht vorhanden";
            BorderBody.ResumeLayout(false);
            BorderBody.PerformLayout();
            Icon = Properties.Resources.Jouran_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            myGroupBox1.ResumeLayout(false);
            myGroupBox1.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGrid_Kunde)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGrid_Rechnung)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
    }
}