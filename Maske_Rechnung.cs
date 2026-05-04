using MyControls;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Rechnung : MyForm
    {
        private TabControl tabControl;
        private TabPage tabRechnung;
        protected MyDuseFieldText feld_Rechnungsnummer;
        private MyDuseFieldText feld_Kurzbezeichnung;
        private MyDuseComboBox cmb_Kunde;
        private TabPage tabLeistungen;
        private MyGroupBox myGroupBox1;
        private MyDuseFieldDate feld_Leistungsbeginn;
        private MyGroupBox myGroupBox4;
        private MyGroupBox myGroupBox3;
        private MyGroupBox myGroupBox2;
        private MyDuseFieldDate feld_Leistungsende;
        private MyDuseRichTextBox feld_Notizen;
        private MyGroupBox myGroupBox5;
        private MyRadioButton rb_Zahlung_ueberweisung;
        private MyRadioButton rb_Zahlung_bar;
        private MyDuseFieldDate feld_Bezahlt_Am;
        private MyDuseCheckBox checkBox_Rechnung_beglichen;
        private MyLabel myLabel7;
        private NumericUpDown num_Zahlungsziel;
        private MyLabel myLabel6;
        private ComboBox cmb_Währung;
        private MyLabel myLabel5;
        private MyLabel myLabel4;
        private MyLabel myLabel3;
        private MyDuseFieldDate feld_Rechnungsdatum;
        private MyDuseFieldDate feld_Druckdatum;
        private MyDuseFieldDate feld_Erstellungsdatum;
        private MyLabel myLabel2;
        private MyLabel myLabel1;
        private Label label1;
        private MyLabel myLabel14;
        private RowMergeView dataGrid;
        private MyNumericField num_Leistung_Menge;
        private MyLabel myLabel13;
        private MyLabel myLabel12;
        private MyLabel myLabel11;
        private MyLabel myLabel10;
        private MyLabel myLabel9;
        private MyLabel myLabel8;
        private MyFieldText feld_Leistung_Netto;
        private MyFieldText feld_Leistung_steuern;
        private MyFieldText feld_Leistung_Brutto;
        private ComboBox cmb_Leistung_Steursatz;
        private ComboBox cmb_Leistung_Einheit;
        private MyDuseRichTextBox feld_Leistung_Beschreibung;
        private MyFieldText feld_Leistung_Bez;
        private MyPushButton pbDrucken;
        private MyPushButton pbMailen;
        private MyPushButton pbVorschau;
        private MyFieldText feld_ID;
        private TabPage tabStorno;
        private bool searchModeEnabled;
        private TreeGridView DataGridStorno;
        private MyPushButton pbStornoErzeugen;
        private MyPushButton pbLeistungHinzufuegen;
        private Label Label_Status;
        private bool RECHNUNG_BEARBEITEN;

        protected override string _name()
        {
            return "Rechnung";
        }
        protected override void _OnLoad(EventArgs e)
        {
            InitializeMask();
            EnablePrintTool(false);
        }
        protected virtual void InitializeMask()
        {
            initializeMaskDefaultValue();
            initilizeDataGrid();
            InitializeFieldsLeistung();
        }
        protected override bool _Populate()
        {
            RechnungHelper rechnung = CopieDataFromMask();
            if (!User.Rechte.RECHNUNG_SUCHEN())
            {
                MessageBox.Show("Kein Berechtigung Rechnungen zu suchen!" +
                    "\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                tabControl.Invalidate();
                return false;
            }
            DataTable datatable = DataAccessLayer.QueryKundeRechnung(rechnung);
            if (datatable == null || datatable.Rows.Count < 1)
            {
                MessageBox.Show("keine Daten gefunden", "Info!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return false;
            }
            else if (datatable.Rows.Count == 1)
            {
                CopieDataToMask(datatable);

                tabControl.Invalidate();

                feld_Rechnungsnummer.Enabled = false;
                feld_Erstellungsdatum.Enabled = false;
                feld_Druckdatum.Enabled = false;

                EnablePrintTool(true);
                return true;
            }
            else
            {
                MyTable myTable = new MyTable(datatable);
                myTable.ShowDialog();
                if (myTable.DataSelected())
                {
                    var result = myTable.GetSelectedRow();
                    string rechnungsnummer = result.Find(l => l.Key == "RECHNUNGSNUMMER").Value;
                    DataTable r_dataTable = DataAccessLayer.getRechnungByNummer(rechnungsnummer);
                    CopieDataToMask(r_dataTable);

                    tabControl.Invalidate();

                    feld_Rechnungsnummer.Enabled = false;
                    feld_Erstellungsdatum.Enabled = false;
                    feld_Druckdatum.Enabled = false;

                    EnablePrintTool(true);
                    return true;
                }
            }
            tabControl.Invalidate();
            EnablePrintTool(false);

            return false;
        }
        protected override bool _PlausibleBevorSave()
        {
            if (tabControl.SelectedTab == tabRechnung)
            {
                return Rechnung_Plausibility();
            }
            /*else if (tabControl.SelectedTab == tabLeistungen)
            {
                return Leistung_Plausibility();
            }
            else if (tabControl.SelectedTab == tabStorno)
            {

            }*/
            return false;
        }
        protected override bool _Save()
        {
            if (tabControl.SelectedTab == tabRechnung)
            {
                return saveRechnung();
            }
            /*else if (tabControl.SelectedTab == tabLeistungen)
            {
                return saveLeistung();
            }
            else if (tabControl.SelectedTab == tabStorno)
            {

            }*/

            return false;
        }
        protected override void _AfterSave()
        {
            //Standard Werte setzen
            InitializeMask();
        }
        protected override bool _EnableSearchModeMenu()
        {
            return true;
        }
        protected override bool _SearchModeEnabling()
        {
            if (!tabControl.SelectedTab.Equals(tabRechnung))
            {
                tabControl.SelectedTab = tabRechnung;
            }
            return true;
        }
        protected override void _SearchModeEnabled()
        {
            //Nummernkreise
            feld_Rechnungsnummer.Enabled = true;
            feld_Rechnungsnummer.Texts = "";
            // Erstellungsdatum
            feld_Erstellungsdatum.Enabled = true;
            feld_Erstellungsdatum.Texts = "";
            // Druckdatum
            feld_Druckdatum.Enabled = true;
            searchModeEnabled = true;
            EnablePrintTool(false);
        }
        protected override void _SearchModeDisabled()
        {
            searchModeEnabled = false;
            InitializeMask();
            EnablePrintTool(false);
        }
        protected override bool _Enable_MenuOption(MenuItem menu_Option)
        {
            return true;
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv dokument)
        {
            if (tabControl.SelectedTab.Equals(tabRechnung))
            {
                if (!string.IsNullOrEmpty(feld_ID.Text))
                {
                    dokument.IdColumn = long.Parse(feld_ID.Text);
                    dokument.TableName = "RECHNUNG";
                }
            }
            else if (tabControl.SelectedTab.Equals(tabStorno))
            {
                dokument.IdColumn = long.Parse(feld_ID.Text);
                dokument.TableName = "STORNO_RECHNUNG";
            }
            return dokument;
        }
        protected override void _InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Rechnung));
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            tabControl = new TabControl();
            tabRechnung = new TabPage();
            label1 = new Label();
            feld_Notizen = new MyDuseRichTextBox();
            myGroupBox5 = new MyGroupBox();
            rb_Zahlung_ueberweisung = new MyRadioButton();
            rb_Zahlung_bar = new MyRadioButton();
            feld_Bezahlt_Am = new MyDuseFieldDate();
            checkBox_Rechnung_beglichen = new MyDuseCheckBox();
            myGroupBox4 = new MyGroupBox();
            myLabel7 = new MyLabel();
            num_Zahlungsziel = new NumericUpDown();
            myLabel6 = new MyLabel();
            cmb_Währung = new ComboBox();
            myGroupBox3 = new MyGroupBox();
            myLabel5 = new MyLabel();
            myLabel4 = new MyLabel();
            myLabel3 = new MyLabel();
            feld_Rechnungsdatum = new MyDuseFieldDate();
            feld_Druckdatum = new MyDuseFieldDate();
            feld_Erstellungsdatum = new MyDuseFieldDate();
            myGroupBox2 = new MyGroupBox();
            myLabel2 = new MyLabel();
            myLabel1 = new MyLabel();
            feld_Leistungsende = new MyDuseFieldDate();
            feld_Leistungsbeginn = new MyDuseFieldDate();
            myGroupBox1 = new MyGroupBox();
            cmb_Kunde = new MyDuseComboBox();
            feld_Kurzbezeichnung = new MyDuseFieldText();
            feld_Rechnungsnummer = new MyDuseFieldText();
            tabLeistungen = new TabPage();
            myLabel14 = new MyLabel();
            dataGrid = new RowMergeView();
            num_Leistung_Menge = new MyNumericField();
            myLabel13 = new MyLabel();
            myLabel12 = new MyLabel();
            myLabel11 = new MyLabel();
            myLabel10 = new MyLabel();
            myLabel9 = new MyLabel();
            myLabel8 = new MyLabel();
            feld_Leistung_Netto = new MyFieldText();
            feld_Leistung_steuern = new MyFieldText();
            feld_Leistung_Brutto = new MyFieldText();
            cmb_Leistung_Steursatz = new ComboBox();
            cmb_Leistung_Einheit = new ComboBox();
            feld_Leistung_Beschreibung = new MyDuseRichTextBox();
            feld_Leistung_Bez = new MyFieldText();
            tabStorno = new TabPage();
            pbStornoErzeugen = new MyPushButton();
            DataGridStorno = new TreeGridView();
            pbVorschau = new MyPushButton();
            pbMailen = new MyPushButton();
            pbDrucken = new MyPushButton();
            feld_ID = new MyFieldText();
            pbLeistungHinzufuegen = new MyPushButton();
            Label_Status = new Label();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            tabControl.SuspendLayout();
            tabRechnung.SuspendLayout();
            myGroupBox5.SuspendLayout();
            myGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(num_Zahlungsziel)).BeginInit();
            myGroupBox3.SuspendLayout();
            myGroupBox2.SuspendLayout();
            myGroupBox1.SuspendLayout();
            tabLeistungen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGrid)).BeginInit();
            tabStorno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(DataGridStorno)).BeginInit();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(pbDrucken);
            BorderBody.Controls.Add(pbMailen);
            BorderBody.Controls.Add(pbVorschau);
            BorderBody.Controls.Add(tabControl);
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabRechnung);
            tabControl.Controls.Add(tabLeistungen);
            tabControl.Controls.Add(tabStorno);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(3, 16);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(800, 490);
            tabControl.Selecting += TabControl_Selecting;
            tabControl.Selected += TabControl_Selected;
            tabControl.DrawItem += TabControl_DrawItem;
            tabControl.TabIndex = 0;
            // 
            // tabRechnung
            // 
            tabRechnung.BackColor = SystemColors.Control;
            tabRechnung.Controls.Add(Label_Status);
            tabRechnung.Controls.Add(label1);
            tabRechnung.Controls.Add(feld_Notizen);
            tabRechnung.Controls.Add(myGroupBox5);
            tabRechnung.Controls.Add(myGroupBox4);
            tabRechnung.Controls.Add(myGroupBox3);
            tabRechnung.Controls.Add(myGroupBox2);
            tabRechnung.Controls.Add(myGroupBox1);
            tabRechnung.Location = new Point(4, 22);
            tabRechnung.Name = "tabRechnung";
            tabRechnung.Padding = new Padding(3);
            tabRechnung.Size = new Size(792, 464);
            tabRechnung.TabIndex = 0;
            tabRechnung.Text = "Rechnungsdaten";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Calibri", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new Point(10, 367);
            label1.Name = "label1";
            label1.Size = new Size(192, 14);
            label1.TabIndex = 10;
            label1.Text = "Notizen (auf Rechnung nicht sichtbar)";
            // 
            // feld_Notizen
            // 
            feld_Notizen.Location = new Point(13, 384);
            feld_Notizen.Name = "feld_Notizen";
            feld_Notizen.PlaceholderColor = Color.DarkGray;
            feld_Notizen.PlaceholderText = "";
            feld_Notizen.Size = new Size(761, 70);
            feld_Notizen.TabIndex = 9;
            feld_Notizen.Text = "";
            feld_Notizen.Texts = "";
            // 
            // myGroupBox5
            // 
            myGroupBox5.BorderColor = Color.DimGray;
            myGroupBox5.BorderThickness = 1;
            myGroupBox5.Controls.Add(rb_Zahlung_ueberweisung);
            myGroupBox5.Controls.Add(rb_Zahlung_bar);
            myGroupBox5.Controls.Add(feld_Bezahlt_Am);
            myGroupBox5.Controls.Add(checkBox_Rechnung_beglichen);
            myGroupBox5.Location = new Point(254, 206);
            myGroupBox5.Name = "myGroupBox5";
            myGroupBox5.Size = new Size(253, 147);
            myGroupBox5.TabIndex = 8;
            myGroupBox5.TabStop = false;
            myGroupBox5.Text = "Zahlungsstatus";
            // 
            // rb_Zahlung_ueberweisung
            // 
            rb_Zahlung_ueberweisung.Location = new Point(18, 107);
            rb_Zahlung_ueberweisung.Name = "rb_Zahlung_ueberweisung";
            rb_Zahlung_ueberweisung.Size = new Size(111, 23);
            rb_Zahlung_ueberweisung.TabIndex = 3;
            rb_Zahlung_ueberweisung.TabStop = true;
            rb_Zahlung_ueberweisung.Text = "Überweisung";
            // 
            // rb_Zahlung_bar
            // 
            rb_Zahlung_bar.Location = new Point(172, 106);
            rb_Zahlung_bar.Name = "rb_Zahlung_bar";
            rb_Zahlung_bar.Size = new Size(49, 23);
            rb_Zahlung_bar.TabIndex = 2;
            rb_Zahlung_bar.TabStop = true;
            rb_Zahlung_bar.Text = "Bar";
            // 
            // feld_Bereits_Bezahlt
            // 
            feld_Bezahlt_Am.Location = new Point(18, 50);
            feld_Bezahlt_Am.Name = "feld_Bereits_Bezahlt";
            feld_Bezahlt_Am.PlaceholderColor = Color.DarkGray;
            feld_Bezahlt_Am.PlaceholderText = "";
            feld_Bezahlt_Am.Size = new Size(203, 27);
            feld_Bezahlt_Am.TabIndex = 1;
            feld_Bezahlt_Am.TextAlign = HorizontalAlignment.Center;
            feld_Bezahlt_Am.Texts = "  .  .";
            feld_Bezahlt_Am.ToolTip = " ";
            feld_Bezahlt_Am.ValidatingType = typeof(System.DateTime);
            // 
            // checkBox_Bereits_bezahlt
            // 
            checkBox_Rechnung_beglichen.Location = new Point(18, 21);
            checkBox_Rechnung_beglichen.Name = "checkBox_Bereits_bezahlt";
            checkBox_Rechnung_beglichen.Size = new Size(159, 23);
            checkBox_Rechnung_beglichen.TabIndex = 0;
            checkBox_Rechnung_beglichen.Text = "Rechnung beglichen";
            checkBox_Rechnung_beglichen.CheckStateChanged += BezahltStateChanged;
            // 
            // myGroupBox4
            // 
            myGroupBox4.BorderColor = Color.DimGray;
            myGroupBox4.BorderThickness = 1;
            myGroupBox4.Controls.Add(myLabel7);
            myGroupBox4.Controls.Add(num_Zahlungsziel);
            myGroupBox4.Controls.Add(myLabel6);
            myGroupBox4.Controls.Add(cmb_Währung);
            myGroupBox4.Location = new Point(513, 206);
            myGroupBox4.Name = "myGroupBox4";
            myGroupBox4.Size = new Size(262, 147);
            myGroupBox4.TabIndex = 7;
            myGroupBox4.TabStop = false;
            myGroupBox4.Text = "Leistungszeitraum";
            // 
            // myLabel7
            // 
            myLabel7.AutoSize = true;
            myLabel7.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel7.ForeColor = Color.Black;
            myLabel7.Location = new Point(21, 83);
            myLabel7.Name = "myLabel7";
            myLabel7.Size = new Size(89, 19);
            myLabel7.TabIndex = 9;
            myLabel7.Text = "Zahlungsziel";
            myLabel7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // num_Zahlungsziel
            // 
            num_Zahlungsziel.Location = new Point(25, 111);
            num_Zahlungsziel.Name = "num_Zahlungsziel";
            num_Zahlungsziel.Size = new Size(219, 20);
            num_Zahlungsziel.TabIndex = 8;
            num_Zahlungsziel.Maximum = 256;
            num_Zahlungsziel.Minimum = -256;
            // 
            // myLabel6
            // 
            myLabel6.AutoSize = true;
            myLabel6.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel6.ForeColor = Color.Black;
            myLabel6.Location = new Point(21, 22);
            myLabel6.Name = "myLabel6";
            myLabel6.Size = new Size(67, 19);
            myLabel6.TabIndex = 7;
            myLabel6.Text = "Währung";
            myLabel6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cmb_Währung
            // 
            cmb_Währung.BackColor = SystemColors.ControlLightLight;
            cmb_Währung.FormattingEnabled = true;
            cmb_Währung.Location = new Point(25, 50);
            cmb_Währung.Name = "cmb_Währung";
            cmb_Währung.Size = new Size(219, 27);
            cmb_Währung.TabIndex = 0;
            cmb_Währung.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            // 
            // myGroupBox3
            // 
            myGroupBox3.BorderColor = Color.DimGray;
            myGroupBox3.BorderThickness = 1;
            myGroupBox3.Controls.Add(myLabel5);
            myGroupBox3.Controls.Add(myLabel4);
            myGroupBox3.Controls.Add(myLabel3);
            myGroupBox3.Controls.Add(feld_Rechnungsdatum);
            myGroupBox3.Controls.Add(feld_Druckdatum);
            myGroupBox3.Controls.Add(feld_Erstellungsdatum);
            myGroupBox3.Location = new Point(13, 118);
            myGroupBox3.Name = "myGroupBox3";
            myGroupBox3.Size = new Size(762, 82);
            myGroupBox3.TabIndex = 6;
            myGroupBox3.TabStop = false;
            myGroupBox3.Text = "Prozessstatus";
            // 
            // myLabel5
            // 
            myLabel5.AutoSize = true;
            myLabel5.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel5.ForeColor = Color.Black;
            myLabel5.Location = new Point(255, 24);
            myLabel5.Name = "myLabel5";
            myLabel5.Size = new Size(121, 19);
            myLabel5.TabIndex = 10;
            myLabel5.Text = "Rechnungsdatum";
            myLabel5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel4
            // 
            myLabel4.AutoSize = true;
            myLabel4.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel4.ForeColor = Color.Black;
            myLabel4.Location = new Point(537, 24);
            myLabel4.Name = "myLabel4";
            myLabel4.Size = new Size(87, 19);
            myLabel4.TabIndex = 9;
            myLabel4.Text = "Druckdatum";
            myLabel4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel3
            // 
            myLabel3.AutoSize = true;
            myLabel3.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel3.ForeColor = Color.Black;
            myLabel3.Location = new Point(9, 24);
            myLabel3.Name = "myLabel3";
            myLabel3.Size = new Size(121, 19);
            myLabel3.TabIndex = 7;
            myLabel3.Text = "Erstellungsdatum";
            myLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // feld_Rechnungsdatum
            // 
            feld_Rechnungsdatum.Location = new Point(259, 45);
            feld_Rechnungsdatum.Name = "feld_Rechnungsdatum";
            feld_Rechnungsdatum.PlaceholderColor = Color.DarkGray;
            feld_Rechnungsdatum.PlaceholderText = "";
            feld_Rechnungsdatum.Size = new Size(203, 27);
            feld_Rechnungsdatum.TabIndex = 8;
            feld_Rechnungsdatum.TextAlign = HorizontalAlignment.Center;
            feld_Rechnungsdatum.Texts = "  .  .";
            feld_Rechnungsdatum.ToolTip = " ";
            feld_Rechnungsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Druckdatum
            // 
            feld_Druckdatum.Location = new Point(541, 45);
            feld_Druckdatum.Name = "feld_Druckdatum";
            feld_Druckdatum.PlaceholderColor = Color.DarkGray;
            feld_Druckdatum.PlaceholderText = "";
            feld_Druckdatum.Size = new Size(203, 27);
            feld_Druckdatum.TabIndex = 7;
            feld_Druckdatum.TextAlign = HorizontalAlignment.Center;
            feld_Druckdatum.Texts = "  .  .";
            feld_Druckdatum.ToolTip = " ";
            feld_Druckdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Erstellungsdatum
            // 
            feld_Erstellungsdatum.Location = new Point(13, 45);
            feld_Erstellungsdatum.Name = "feld_Erstellungsdatum";
            feld_Erstellungsdatum.PlaceholderColor = Color.DarkGray;
            feld_Erstellungsdatum.PlaceholderText = "";
            feld_Erstellungsdatum.Size = new Size(203, 27);
            feld_Erstellungsdatum.TabIndex = 7;
            feld_Erstellungsdatum.TextAlign = HorizontalAlignment.Center;
            feld_Erstellungsdatum.Texts = "  .  .";
            feld_Erstellungsdatum.ToolTip = " ";
            feld_Erstellungsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = Color.DimGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(myLabel2);
            myGroupBox2.Controls.Add(myLabel1);
            myGroupBox2.Controls.Add(feld_Leistungsende);
            myGroupBox2.Controls.Add(feld_Leistungsbeginn);
            myGroupBox2.Location = new Point(13, 206);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new Size(235, 147);
            myGroupBox2.TabIndex = 5;
            myGroupBox2.TabStop = false;
            myGroupBox2.Text = "Leistungszeitraum";
            // 
            // myLabel2
            // 
            myLabel2.AutoSize = true;
            myLabel2.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel2.ForeColor = Color.Black;
            myLabel2.Location = new Point(9, 81);
            myLabel2.Name = "myLabel2";
            myLabel2.Size = new Size(101, 19);
            myLabel2.TabIndex = 6;
            myLabel2.Text = "Leistungsende";
            myLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel1
            // 
            myLabel1.AutoSize = true;
            myLabel1.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel1.ForeColor = Color.Black;
            myLabel1.Location = new Point(9, 22);
            myLabel1.Name = "myLabel1";
            myLabel1.Size = new Size(113, 19);
            myLabel1.TabIndex = 5;
            myLabel1.Text = "Leistungsbeginn";
            myLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // feld_Leistungsende
            // 
            feld_Leistungsende.Location = new Point(13, 106);
            feld_Leistungsende.Name = "feld_Leistungsende";
            feld_Leistungsende.PlaceholderColor = Color.DarkGray;
            feld_Leistungsende.PlaceholderText = "";
            feld_Leistungsende.Size = new Size(203, 27);
            feld_Leistungsende.TabIndex = 4;
            feld_Leistungsende.TextAlign = HorizontalAlignment.Center;
            feld_Leistungsende.Texts = "  .  .";
            feld_Leistungsende.ToolTip = " ";
            feld_Leistungsende.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Leistungsbeginn
            // 
            feld_Leistungsbeginn.Location = new Point(13, 50);
            feld_Leistungsbeginn.Name = "feld_Leistungsbeginn";
            feld_Leistungsbeginn.PlaceholderColor = Color.DarkGray;
            feld_Leistungsbeginn.PlaceholderText = "";
            feld_Leistungsbeginn.Size = new Size(203, 27);
            feld_Leistungsbeginn.TabIndex = 3;
            feld_Leistungsbeginn.TextAlign = HorizontalAlignment.Center;
            feld_Leistungsbeginn.Texts = "  .  .";
            feld_Leistungsbeginn.ToolTip = " ";
            feld_Leistungsbeginn.ValidatingType = typeof(System.DateTime);
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = Color.DimGray;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(cmb_Kunde);
            myGroupBox1.Controls.Add(feld_Kurzbezeichnung);
            myGroupBox1.Controls.Add(feld_Rechnungsnummer);
            myGroupBox1.Location = new Point(13, 16);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new Size(762, 96);
            myGroupBox1.TabIndex = 4;
            myGroupBox1.TabStop = false;
            // 
            // cmb_Kunde
            // 
            cmb_Kunde.BackColor = SystemColors.ControlLightLight;
            cmb_Kunde.ForeColor = Color.DarkGray;
            cmb_Kunde.FormattingEnabled = true;
            cmb_Kunde.Location = new Point(13, 54);
            cmb_Kunde.Name = "cmb_Kunde";
            cmb_Kunde.Size = new Size(731, 27);
            cmb_Kunde.TabIndex = 0;
            cmb_Kunde.Text = " Kunde auswählen";
            //cmb_Kunde.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            // 
            // feld_Kurzbezeichnung
            // 
            feld_Kurzbezeichnung.ForeColor = Color.DarkGray;
            feld_Kurzbezeichnung.Location = new Point(13, 19);
            feld_Kurzbezeichnung.Name = "feld_Kurzbezeichnung";
            feld_Kurzbezeichnung.PlaceholderColor = Color.DarkGray;
            feld_Kurzbezeichnung.PlaceholderText = " Rechnungsbezeichnung";
            feld_Kurzbezeichnung.Size = new Size(552, 27);
            feld_Kurzbezeichnung.TabIndex = 1;
            feld_Kurzbezeichnung.Text = " Rechnungsbezeichnung";
            feld_Kurzbezeichnung.Texts = "";
            // 
            // feld_Rechnungsnummer
            // 
            feld_Rechnungsnummer.ForeColor = Color.DarkGray;
            feld_Rechnungsnummer.Location = new Point(571, 19);
            feld_Rechnungsnummer.Name = "feld_Rechnungsnummer";
            feld_Rechnungsnummer.PlaceholderColor = Color.DarkGray;
            feld_Rechnungsnummer.PlaceholderText = " Rechnungsnummer";
            feld_Rechnungsnummer.Size = new Size(173, 27);
            feld_Rechnungsnummer.TabIndex = 2;
            feld_Rechnungsnummer.Text = " Rechnungsnummer";
            feld_Rechnungsnummer.Texts = "";
            // 
            // tabLeistungen
            // 
            tabLeistungen.BackColor = SystemColors.Control;
            tabLeistungen.Controls.Add(pbLeistungHinzufuegen);
            tabLeistungen.Controls.Add(myLabel14);
            tabLeistungen.Controls.Add(dataGrid);
            tabLeistungen.Controls.Add(num_Leistung_Menge);
            tabLeistungen.Controls.Add(myLabel13);
            tabLeistungen.Controls.Add(myLabel12);
            tabLeistungen.Controls.Add(myLabel11);
            tabLeistungen.Controls.Add(myLabel10);
            tabLeistungen.Controls.Add(myLabel9);
            tabLeistungen.Controls.Add(myLabel8);
            tabLeistungen.Controls.Add(feld_Leistung_Netto);
            tabLeistungen.Controls.Add(feld_Leistung_steuern);
            tabLeistungen.Controls.Add(feld_Leistung_Brutto);
            tabLeistungen.Controls.Add(cmb_Leistung_Steursatz);
            tabLeistungen.Controls.Add(cmb_Leistung_Einheit);
            tabLeistungen.Controls.Add(feld_Leistung_Beschreibung);
            tabLeistungen.Controls.Add(feld_Leistung_Bez);
            tabLeistungen.Location = new Point(4, 22);
            tabLeistungen.Name = "tabLeistungen";
            tabLeistungen.Padding = new Padding(3);
            tabLeistungen.Size = new Size(792, 464);
            tabLeistungen.TabIndex = 1;
            tabLeistungen.Text = "Leistungen";
            // 
            // myLabel14
            // 
            myLabel14.AutoSize = true;
            myLabel14.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel14.ForeColor = Color.Black;
            myLabel14.Location = new Point(17, 230);
            myLabel14.Name = "myLabel14";
            myLabel14.Size = new Size(79, 19);
            myLabel14.TabIndex = 20;
            myLabel14.Text = "Leistungen";
            myLabel14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToDeleteRows = true;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.ReadOnly = true;
            dataGrid.Location = new Point(20, 252);
            dataGrid.Name = "dataGrid";
            dataGrid.Size = new Size(754, 202);
            dataGrid.UserDeletingRow += Delete_Leistung;
            dataGrid.RowsRemoved += Leistung_Deleted;
            dataGrid.CellDoubleClick += dataGrid_CellDoubleClick;
            // 
            // num_Leistung_Menge
            // 
            num_Leistung_Menge.BackColor = SystemColors.ControlLightLight;
            num_Leistung_Menge.Location = new Point(160, 195);
            num_Leistung_Menge.Name = "num_Leistung_Menge";
            num_Leistung_Menge.PlaceholderColor = Color.DarkGray;
            num_Leistung_Menge.Size = new Size(120, 27);
            num_Leistung_Menge.TabIndex = 18;
            num_Leistung_Menge.Texts = "";
            // 
            // myLabel13
            // 
            myLabel13.AutoSize = true;
            myLabel13.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel13.ForeColor = Color.Black;
            myLabel13.Location = new Point(553, 173);
            myLabel13.Name = "myLabel13";
            myLabel13.Size = new Size(50, 19);
            myLabel13.TabIndex = 17;
            myLabel13.Text = "Steuer";
            myLabel13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel12
            // 
            myLabel12.AutoSize = true;
            myLabel12.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel12.ForeColor = Color.Black;
            myLabel12.Location = new Point(672, 173);
            myLabel12.Name = "myLabel12";
            myLabel12.Size = new Size(45, 19);
            myLabel12.TabIndex = 16;
            myLabel12.Text = "Netto";
            myLabel12.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel11
            // 
            myLabel11.AutoSize = true;
            myLabel11.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel11.ForeColor = Color.Black;
            myLabel11.Location = new Point(435, 173);
            myLabel11.Name = "myLabel11";
            myLabel11.Size = new Size(49, 19);
            myLabel11.TabIndex = 15;
            myLabel11.Text = "Brutto";
            myLabel11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel10
            // 
            myLabel10.AutoSize = true;
            myLabel10.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel10.ForeColor = Color.Black;
            myLabel10.Location = new Point(297, 173);
            myLabel10.Name = "myLabel10";
            myLabel10.Size = new Size(100, 19);
            myLabel10.TabIndex = 14;
            myLabel10.Text = "Steuersatz (%)";
            myLabel10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel9
            // 
            myLabel9.AutoSize = true;
            myLabel9.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel9.ForeColor = Color.Black;
            myLabel9.Location = new Point(157, 173);
            myLabel9.Name = "myLabel9";
            myLabel9.Size = new Size(55, 19);
            myLabel9.TabIndex = 13;
            myLabel9.Text = "Menge";
            myLabel9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel8
            // 
            myLabel8.AutoSize = true;
            myLabel8.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel8.ForeColor = Color.Black;
            myLabel8.Location = new Point(17, 173);
            myLabel8.Name = "myLabel8";
            myLabel8.Size = new Size(54, 19);
            myLabel8.TabIndex = 12;
            myLabel8.Text = "Einheit";
            myLabel8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // feld_Leistung_Netto
            // 
            feld_Leistung_Netto.Location = new Point(675, 195);
            feld_Leistung_Netto.Name = "feld_Leistung_Netto";
            feld_Leistung_Netto.PlaceholderColor = Color.DarkGray;
            feld_Leistung_Netto.PlaceholderText = "";
            feld_Leistung_Netto.Size = new Size(100, 27);
            feld_Leistung_Netto.TabIndex = 8;
            feld_Leistung_Netto.TextAlign = HorizontalAlignment.Center;
            feld_Leistung_Netto.TextChanged += brutto_Berechnung;
            feld_Leistung_Netto.Leave += leistungsfeldFormatieren;
            // 
            // feld_Leistung_steuern
            // 
            feld_Leistung_steuern.Location = new Point(556, 195);
            feld_Leistung_steuern.Name = "feld_Leistung_steuern";
            feld_Leistung_steuern.Size = new Size(100, 27);
            feld_Leistung_steuern.TabIndex = 7;
            feld_Leistung_steuern.TextAlign = HorizontalAlignment.Center;
            feld_Leistung_steuern.Enabled = false;
            // 
            // feld_Leistung_Brutto
            // 
            feld_Leistung_Brutto.Location = new Point(438, 195);
            feld_Leistung_Brutto.Name = "feld_Leistung_Brutto";
            feld_Leistung_Brutto.PlaceholderColor = Color.DarkGray;
            feld_Leistung_Brutto.PlaceholderText = "";
            feld_Leistung_Brutto.Size = new Size(100, 27);
            feld_Leistung_Brutto.TabIndex = 6;
            feld_Leistung_Brutto.TextAlign = HorizontalAlignment.Center;
            feld_Leistung_Brutto.TextChanged += netto_Berechnung;
            feld_Leistung_Brutto.Leave += leistungsfeldFormatieren;
            // 
            // cmb_Leistung_Steursatz
            // 
            cmb_Leistung_Steursatz.BackColor = SystemColors.ControlLightLight;
            cmb_Leistung_Steursatz.FormattingEnabled = true;
            cmb_Leistung_Steursatz.Location = new Point(300, 195);
            cmb_Leistung_Steursatz.Name = "cmb_Leistung_Steursatz";
            cmb_Leistung_Steursatz.Size = new Size(121, 27);
            cmb_Leistung_Steursatz.TabIndex = 5;
            cmb_Leistung_Steursatz.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            // 
            // cmb_Leistung_Einheit
            // 
            cmb_Leistung_Einheit.BackColor = SystemColors.ControlLightLight;
            cmb_Leistung_Einheit.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            cmb_Leistung_Einheit.FormattingEnabled = true;
            cmb_Leistung_Einheit.Location = new Point(20, 195);
            cmb_Leistung_Einheit.Name = "cmb_Leistung_Einheit";
            cmb_Leistung_Einheit.Size = new Size(121, 27);
            cmb_Leistung_Einheit.TabIndex = 3;
            // 
            // feld_Leistung_Beschreibung
            // 
            feld_Leistung_Beschreibung.BackColor = SystemColors.ControlLightLight;
            feld_Leistung_Beschreibung.ForeColor = Color.DarkGray;
            feld_Leistung_Beschreibung.Location = new Point(20, 78);
            feld_Leistung_Beschreibung.Name = "feld_Leistung_Beschreibung";
            feld_Leistung_Beschreibung.PlaceholderColor = Color.DarkGray;
            feld_Leistung_Beschreibung.PlaceholderText = "  Beschreibung der Leistung";
            feld_Leistung_Beschreibung.Size = new Size(755, 84);
            feld_Leistung_Beschreibung.TabIndex = 1;
            feld_Leistung_Beschreibung.Text = "  Beschreibung der Leistung";
            feld_Leistung_Beschreibung.Texts = "";
            // 
            // feld_Leistung_Bez
            // 
            feld_Leistung_Bez.ForeColor = Color.DarkGray;
            feld_Leistung_Bez.Location = new Point(20, 45);
            feld_Leistung_Bez.Name = "feld_Leistung_Bez";
            feld_Leistung_Bez.PlaceholderColor = Color.DarkGray;
            feld_Leistung_Bez.PlaceholderText = "  Leistungsbezeichnung";
            feld_Leistung_Bez.Size = new Size(754, 27);
            feld_Leistung_Bez.TabIndex = 0;
            feld_Leistung_Bez.Text = "  Leistungsbezeichnung";
            feld_Leistung_Bez.Texts = "";
            // 
            // tabStorno
            // 
            tabStorno.BackColor = SystemColors.ControlLight;
            tabStorno.Controls.Add(pbStornoErzeugen);
            tabStorno.Controls.Add(DataGridStorno);
            tabStorno.Location = new Point(4, 22);
            tabStorno.Name = "tabStorno";
            tabStorno.Padding = new Padding(3);
            tabStorno.Size = new Size(792, 464);
            tabStorno.TabIndex = 2;
            tabStorno.Text = "Stornos/Korrekturen";
            // 
            // pbStornoErzeugen
            // 
            pbStornoErzeugen.BackColor = Color.Transparent;
            pbStornoErzeugen.BackgroundImageLayout = ImageLayout.None;
            pbStornoErzeugen.FlatAppearance.BorderSize = 2;
            pbStornoErzeugen.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbStornoErzeugen.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbStornoErzeugen.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            pbStornoErzeugen.ImageAlign = ContentAlignment.TopLeft;
            pbStornoErzeugen.Location = new Point(520, 13);
            pbStornoErzeugen.Name = "pbStornoErzeugen";
            pbStornoErzeugen.Size = new Size(254, 40);
            pbStornoErzeugen.TabIndex = 1;
            pbStornoErzeugen.Text = "neue Stornorechnung erzeugen";
            pbStornoErzeugen.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbStornoErzeugen.UseVisualStyleBackColor = false;
            pbStornoErzeugen.Click += new System.EventHandler(stornoErzeugen);
            // 
            // DataGridStorno
            // 
            DataGridStorno.AllowUserToAddRows = false;
            DataGridStorno.AllowUserToDeleteRows = false;
            DataGridStorno.AllowUserToResizeRows = false;
            DataGridStorno.Location = new Point(17, 59);
            DataGridStorno.Name = "DataGridStorno";
            DataGridStorno.Size = new Size(757, 395);
            // 
            // pbVorschau
            // 
            pbVorschau.BackColor = Color.Transparent;
            pbVorschau.BackgroundImageLayout = ImageLayout.None;
            pbVorschau.FlatAppearance.BorderSize = 2;
            pbVorschau.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbVorschau.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbVorschau.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            errorProvider.SetIconAlignment(pbVorschau, ErrorIconAlignment.MiddleLeft);
            pbVorschau.Image = Properties.Resources.Vorschau_png;
            pbVorschau.ImageAlign = ContentAlignment.TopLeft;
            pbVorschau.Location = new Point(746, 18);
            pbVorschau.Name = "pbVorschau";
            pbVorschau.ReadOnly = false;
            pbVorschau.Size = new Size(36, 36);
            pbVorschau.TabIndex = 1;
            pbVorschau.TextAlign = ContentAlignment.BottomRight;
            pbVorschau.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbVorschau.UseVisualStyleBackColor = false;
            pbVorschau.Click += Report_Show;
            // 
            // pbMailen
            // 
            pbMailen.BackColor = Color.Transparent;
            pbMailen.BackgroundImageLayout = ImageLayout.None;
            pbMailen.FlatAppearance.BorderSize = 2;
            pbMailen.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbMailen.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbMailen.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            pbMailen.Image = Properties.Resources.Mail_png;
            pbMailen.ImageAlign = ContentAlignment.TopLeft;
            pbMailen.Location = new Point(711, 18);
            pbMailen.Name = "pbMailen";
            pbMailen.ReadOnly = false;
            pbMailen.Size = new Size(36, 36);
            pbMailen.TabIndex = 2;
            pbMailen.TextAlign = ContentAlignment.BottomRight;
            pbMailen.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbMailen.UseVisualStyleBackColor = true;
            pbMailen.Click += Report_Mail;
            // 
            // pbDrucken
            // 
            pbDrucken.BackColor = Color.Transparent;
            pbDrucken.BackgroundImageLayout = ImageLayout.None;
            pbDrucken.FlatAppearance.BorderColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            pbDrucken.FlatAppearance.BorderSize = 2;
            pbDrucken.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbDrucken.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbDrucken.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            pbDrucken.Image = Properties.Resources.print_png;
            pbDrucken.ImageAlign = ContentAlignment.TopLeft;
            pbDrucken.Location = new Point(676, 18);
            pbDrucken.Name = "pbDrucken";
            pbDrucken.ReadOnly = false;
            pbDrucken.Size = new Size(36, 36);
            pbDrucken.TabIndex = 48;
            pbDrucken.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbDrucken.UseVisualStyleBackColor = true;
            pbDrucken.Click += new System.EventHandler(Report_Print);
            // 
            // pbLeistungHinzufuegen
            // 
            this.pbLeistungHinzufuegen.BackColor = System.Drawing.Color.Transparent;
            this.pbLeistungHinzufuegen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbLeistungHinzufuegen.FlatAppearance.BorderSize = 2;
            this.pbLeistungHinzufuegen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbLeistungHinzufuegen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbLeistungHinzufuegen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbLeistungHinzufuegen.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.pbLeistungHinzufuegen.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbLeistungHinzufuegen.Location = new System.Drawing.Point(556, 9);
            this.pbLeistungHinzufuegen.Name = "pbLeistungHinzufuegen";
            this.pbLeistungHinzufuegen.ReadOnly = false;
            this.pbLeistungHinzufuegen.Size = new System.Drawing.Size(218, 30);
            this.pbLeistungHinzufuegen.TabIndex = 21;
            this.pbLeistungHinzufuegen.Text = "Leistung hinzufügen";
            this.pbLeistungHinzufuegen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbLeistungHinzufuegen.UseVisualStyleBackColor = false;
            pbLeistungHinzufuegen.Click += PbLeistungHinzufuegen_Click;
            //
            this.Label_Status.Location = new System.Drawing.Point(15, 6);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(107, 10);
            Label_Status.Height = 4;
            // 
            // feld_ID
            // 
            feld_ID.Location = new Point(0, 0);
            feld_ID.Name = "feld_ID";
            feld_ID.PlaceholderColor = Color.DarkGray;
            feld_ID.Size = new Size(100, 27);
            feld_ID.TabIndex = 0;
            feld_ID.Texts = "";
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Abrechnung_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            tabControl.ResumeLayout(false);
            tabRechnung.ResumeLayout(false);
            tabRechnung.PerformLayout();
            myGroupBox5.ResumeLayout(false);
            myGroupBox5.PerformLayout();
            myGroupBox4.ResumeLayout(false);
            myGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(num_Zahlungsziel)).EndInit();
            myGroupBox3.ResumeLayout(false);
            myGroupBox3.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            myGroupBox1.ResumeLayout(false);
            myGroupBox1.PerformLayout();
            tabLeistungen.ResumeLayout(false);
            tabLeistungen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGrid)).EndInit();
            tabStorno.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(DataGridStorno)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private void PbLeistungHinzufuegen_Click(object sender, EventArgs e)
        {
            if (Leistung_Plausibility())
            {
                saveLeistung();
            }
        }
        private void initializeMaskDefaultValue()
        {
            // Status
            Label_Status.BackColor = Color.Transparent;
            //ID
            feld_ID.Text = "";
            //Nummernkreise
            feld_Rechnungsnummer.Enabled = false;
            feld_Rechnungsnummer.Texts = NummernkreisManager.GetAktuelleRechnungsnummer();
            // cmbKunden generieren
            cmb_Kunde.DataSource = null;
            cmb_Kunde.Items.Clear();
            DataTable dataTable_Kunden = DataAccessLayer.Query_Kunden();
            cmb_Kunde.DataSource = dataTable_Kunden;
            cmb_Kunde.DisplayMember = "NAME"; // Spalte, die angezeigt wird
            cmb_Kunde.ValueMember = "ID";
            cmb_Kunde.Text = "";
            // Erstellungsdatum
            feld_Erstellungsdatum.Texts = DateTime.Now.Date.ToString();
            feld_Erstellungsdatum.Enabled = false;
            // Druckdatum
            feld_Druckdatum.Enabled = false;
            // Währung
            cmb_Währung.Items.Clear();
            cmb_Währung.Items.AddRange(new object[] { "€", "$" });
            cmb_Währung.Text = "€";
            // Zahlungsziel in Tage
            num_Zahlungsziel.Value = 14;
            num_Zahlungsziel.UpButton();
            num_Zahlungsziel.DownButton();
            //***disaable Vorschau***//
            pbVorschau.Enabled = false;
            // Einheiten
            cmb_Leistung_Einheit.Items.Clear();
            cmb_Leistung_Einheit.Items.AddRange(new object[] { "Kilogramm", "Kilometer", "Stunde(n)", "Tag(e)", "Woche(n)" });
            cmb_Leistung_Einheit.Text = "Stunde(n)";
            // Menge
            num_Leistung_Menge.Texts = "1";

            // Steuer
            cmb_Leistung_Steursatz.Items.Clear();
            cmb_Leistung_Steursatz.Items.AddRange(new object[] { "7", "19" });
            cmb_Leistung_Steursatz.Text = "19";
            // Netto - Brutto - Steuer
            feld_Leistung_Brutto.Texts = string.Format("{0:#,##0.00}", 0d);
            feld_Leistung_steuern.Texts = string.Format("{0:#,##0.00}", 0d);
            feld_Leistung_Netto.Texts = string.Format("{0:#,##0.00}", 0d);
        }
        private void initilizeDataGrid()
        {
            {
                dataGrid.Columns.Clear();
            }
            DataGridViewTextBoxColumn Col_Id = new DataGridViewTextBoxColumn();
            Col_Id.HeaderText = "ID";
            Col_Id.Name = "ID";
            Col_Id.Visible = false;
            DataGridViewTextBoxColumn Col0 = new DataGridViewTextBoxColumn();
            Col0.HeaderText = "Pos";
            Col0.Name = "Pos";
            Col0.ValueType = typeof(int);
            DataGridViewTextBoxColumn Col1 = new DataGridViewTextBoxColumn();
            Col1.HeaderText = "Bezeichnung";
            Col1.Name = "Bezeichnung";
            Col1.ValueType = typeof(string);
            DataGridViewTextBoxColumn Col2 = new DataGridViewTextBoxColumn();
            Col2.HeaderText = "Einheit";
            Col2.Name = "Einheit";
            Col2.ValueType = typeof(string);
            DataGridViewTextBoxColumn Col3 = new DataGridViewTextBoxColumn();
            Col3.HeaderText = "Menge";
            Col3.Name = "Menge";
            Col3.ValueType = typeof(int);
            DataGridViewTextBoxColumn Col4 = new DataGridViewTextBoxColumn();
            Col4.HeaderText = "Steuersatz";
            Col4.Name = "Steuersatz";
            Col4.ValueType = typeof(int);
            DataGridViewTextBoxColumn Col5 = new DataGridViewTextBoxColumn();
            Col5.HeaderText = "Brutto";
            Col5.Name = "Brutto";
            Col5.ValueType = typeof(float);
            DataGridViewTextBoxColumn Col6 = new DataGridViewTextBoxColumn();
            Col6.HeaderText = "Steuer";
            Col6.Name = "Steuer";
            Col6.ValueType = typeof(float);
            DataGridViewTextBoxColumn Col7 = new DataGridViewTextBoxColumn();
            Col7.HeaderText = "Netto";
            Col7.Name = "Netto";
            Col7.ValueType = typeof(float);
            DataGridViewTextBoxColumn Col8 = new DataGridViewTextBoxColumn();
            Col8.HeaderText = "Beschreibung";
            Col8.Name = "Beschreibung";
            Col8.ValueType = typeof(string);
            dataGrid.Columns.AddRange(Col_Id, Col0, Col1, Col2, Col3, Col4, Col5, Col6, Col7, Col8);
        }
        private RechnungHelper CopieDataFromMask()
        {
            List<Leistung> leistungs = new List<Leistung>();
            for (int i = 0; i < dataGrid.RowCount; i++)
            {
                Leistung leistung = new Leistung();
                leistung.Pos = int.Parse(dataGrid.Rows[i].Cells["Pos"].Value.ToString());
                leistung.Bezeichnung = (string)dataGrid.Rows[i].Cells["Bezeichnung"].Value;
                leistung.Einheit = dataGrid.Rows[i].Cells["Einheit"].Value.ToString();
                leistung.Menge = int.Parse(dataGrid.Rows[i].Cells["Menge"].Value.ToString());
                leistung.Steuersatz = int.Parse(dataGrid.Rows[i].Cells["Steuersatz"].Value.ToString());
                leistung.Brutto = float.Parse(dataGrid.Rows[i].Cells["Brutto"].Value.ToString());
                leistung.Steuer = float.Parse(dataGrid.Rows[i].Cells["Steuer"].Value.ToString());
                leistung.Netto = float.Parse(dataGrid.Rows[i].Cells["Netto"].Value.ToString());
                leistung.Beschreibung = (string)dataGrid.Rows[i].Cells["Beschreibung"].Value;

                leistungs.Add(leistung);
            }
            RechnungHelper rechnung = new RechnungHelper
            {
                Id = string.IsNullOrEmpty(feld_ID.Texts) ? 0 : int.Parse(feld_ID.Texts),
                Rechnungsnummer = feld_Rechnungsnummer.Texts,
                Rechnungsbezeichnung = feld_Kurzbezeichnung.Texts,
                LeistungsBeginn = string.IsNullOrEmpty(feld_Leistungsbeginn.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Leistungsbeginn.Texts),

                LeistungsEnde = string.IsNullOrEmpty(feld_Leistungsende.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Leistungsende.Texts),

                Rechnungsdatum = string.IsNullOrEmpty(feld_Rechnungsdatum.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Rechnungsdatum.Texts),

                Druckdatum = string.IsNullOrEmpty(feld_Druckdatum.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Druckdatum.Texts),

                Zahlungsziel = string.IsNullOrEmpty(feld_Rechnungsdatum.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Rechnungsdatum.Texts).Date.AddDays((int)num_Zahlungsziel.Value).Date,

                Notizen = feld_Notizen.Text,

                Erstellungsdatum = string.IsNullOrEmpty(feld_Erstellungsdatum.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Erstellungsdatum.Texts),
                Waehrung = cmb_Währung.Text,
            };

            if (!string.IsNullOrEmpty(cmb_Kunde.Text) && cmb_Kunde.SelectedIndex >= 0 && cmb_Kunde.SelectedValue != null)
            {
                rechnung.Kunden_id = Convert.ToInt32(cmb_Kunde.SelectedValue);
            }
            if (checkBox_Rechnung_beglichen.Checked)
            {
                rechnung.Zahlungsstatus = Zahlungsstatus.BEZAHLT;
                rechnung.Zahlungsdatum = string.IsNullOrEmpty(feld_Bezahlt_Am.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Bezahlt_Am.Texts);
                if (rb_Zahlung_bar.Checked)
                {
                    rechnung.Zahlungsart = "BAR";
                }
                else if (rb_Zahlung_ueberweisung.Checked)
                {
                    rechnung.Zahlungsart = "ÜBERW";
                }
            }
            else
            {
                rechnung.Zahlungsstatus = Zahlungsstatus.OFFEN;
            }
            rechnung.Leistungen = leistungs;
            return rechnung;
        }
        private void CopieDataToMask(DataTable datatable)
        {
            //Log
            Log.Information("Rechnung wurde gesucht -> \n" + JsonConvert.SerializeObject(datatable));

            // populate
            PopulateRechnung(datatable);
            PopulateStornos(feld_Rechnungsnummer.Texts);

            //***Enable Vorschau***//
            pbVorschau.Enabled = true;
            RECHNUNG_BEARBEITEN = true;
        }
        private void PopulateRechnung(DataTable datatable)
        {
            DataRow row = datatable.Rows[0];
            feld_ID.Texts = row["RECHNUNGID"].ToString();
            feld_Rechnungsnummer.Texts = (string)row["RECHNUNGSNUMMER"];
            cmb_Kunde.Text = (string)row["NAME"];
            feld_Leistungsbeginn.Texts = row["LEISTUNGSBEGINN"].ToString();
            feld_Leistungsende.Texts = row["LEISTUNGSBEGINN"].ToString();
            feld_Erstellungsdatum.Texts = row["ERSTELLUNGSDATUM"].ToString();
            feld_Rechnungsdatum.Texts = row["RECHNUNGSDATUM"].ToString();
            feld_Druckdatum.Texts = row["DRUCKDATUM"].ToString();
            feld_Bezahlt_Am.Texts = row["BEZAHLT_AM"].ToString();
            DateTime zahlungsziel = (DateTime)row["ZAHLUNGSZIEL"];
            feld_Kurzbezeichnung.Texts = (string)row["R_BEZEICHNUNG"];
            checkBox_Rechnung_beglichen.Checked = (bool)row["BEZAHLT"];
            string zahlungsart = (string)row["ZAHLUNGSART"];
            feld_Bezahlt_Am.Enabled = false;
            feld_Notizen.Texts = (string)row["NOTIZEN"];
            string status = (string)row["STATUS"];
            string waehrung = (string)row["WAEHRUNG"];
            int restTage = (zahlungsziel.Date - DateTime.Today.Date).Days;

            num_Zahlungsziel.Text = restTage.ToString();
            cmb_Währung.Text = waehrung;
            // Rechnungsstatus
            if (checkBox_Rechnung_beglichen.Checked)
            {
                Label_Status.BackColor = Color.MediumSeaGreen;
                if (!string.IsNullOrEmpty(zahlungsart))
                {
                    if (zahlungsart.Equals("BAR"))
                    {
                        rb_Zahlung_bar.Checked = true;
                    }
                    else if (zahlungsart.Equals("ÜBERW"))
                    {
                        rb_Zahlung_ueberweisung.Checked = true;
                    }
                }
            }
            else
            {
                Prozessstatus prozess = (Prozessstatus)Enum.Parse(typeof(Prozessstatus), status);
                if (prozess.Equals(Prozessstatus.ERSTELLT))
                {
                    Label_Status.BackColor = Color.Transparent;
                }
                else if ((prozess.Equals(Prozessstatus.GEDRUCKT) || prozess.Equals(Prozessstatus.VERSANDT))
                    && restTage < 0)
                {
                    Label_Status.BackColor = Color.Red;
                }
                else if (prozess.Equals(Prozessstatus.GEDRUCKT))
                {
                    Label_Status.BackColor = Color.DeepSkyBlue;
                }
                else if (prozess.Equals(Prozessstatus.VERSANDT))
                {
                    Label_Status.BackColor = Color.DarkOrange;
                }
            }

            dataGrid.Rows.Clear();
            DataTable dtLeistungen = DataAccessLayer.getLeistungenByRechnungsnummer(feld_Rechnungsnummer.Texts);
            //DataTable dt = initilizeDataTableLeistung();
            for (int j = 0; j < dtLeistungen.Rows.Count; j++)
            {
                string ID = dtLeistungen.Rows[j]["ID"].ToString().Trim();
                string Pos = dtLeistungen.Rows[j]["Pos"].ToString().Trim();
                string Bezeichnung = dtLeistungen.Rows[j]["Bezeichnung"].ToString().Trim();
                string Einheit = dtLeistungen.Rows[j]["Einheit"].ToString().Trim();
                int Menge = int.Parse(dtLeistungen.Rows[j]["Menge"].ToString().Trim());
                int Steuersatz = int.Parse(dtLeistungen.Rows[j]["Steuersatz"].ToString().Trim());
                var Brutto = dtLeistungen.Rows[j]["Brutto"].ToString().Trim();
                var Steuer = dtLeistungen.Rows[j]["Steuer"].ToString().Trim();
                var Netto = dtLeistungen.Rows[j]["Netto"].ToString().Trim();
                string Beschreibung = dtLeistungen.Rows[j]["Beschreibung"].ToString().Trim();

                dataGrid.Rows.Add(ID, Pos, Bezeichnung, Einheit, Menge, Steuersatz, Brutto, Steuer, Netto, Beschreibung);
            }
        }
        private void PopulateStornos(string bezugsrechnung)
        {
            DataTable dtStorno = DataAccessLayer.queryStornosByBezugsrechnung(bezugsrechnung);
            DataGridStorno.BuildTreeRecursive(dtStorno, new[] { "STORNONUMMER" }, "STORNO-Nummer");
        }
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            feld_Leistung_Bez.Texts = dataGrid.Rows[e.RowIndex].Cells["Bezeichnung"].Value.ToString();
            cmb_Leistung_Einheit.Text = dataGrid.Rows[e.RowIndex].Cells["Einheit"].Value.ToString();
            num_Leistung_Menge.Texts = dataGrid.Rows[e.RowIndex].Cells["Menge"].Value.ToString();
            cmb_Leistung_Steursatz.Text = dataGrid.Rows[e.RowIndex].Cells["Steuersatz"].Value.ToString();
            feld_Leistung_Brutto.Texts = dataGrid.Rows[e.RowIndex].Cells["Brutto"].Value.ToString();
            feld_Leistung_steuern.Texts = dataGrid.Rows[e.RowIndex].Cells["Steuer"].Value.ToString();
            feld_Leistung_Netto.Texts = dataGrid.Rows[e.RowIndex].Cells["Netto"].Value.ToString();
            feld_Leistung_Beschreibung.Texts = (string)dataGrid.Rows[e.RowIndex].Cells["Beschreibung"].Value;
        }
        private void Delete_Leistung(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (!User.Rechte.RECHNUNG_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung eine Rechnung zu bearbeiten!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
                return;
            }
            int index = e.Row.Index;
            int leistung_id;
            string pos = dataGrid.Rows[index].Cells["Pos"].Value.ToString();
            DialogResult result = MessageBox.Show("Position " + pos + " löschen?", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
                bool ok = int.TryParse(dataGrid.Rows[index].Cells["ID"].Value.ToString(), out leistung_id);
                if (ok)
                {
                    Leistung leistung = new Leistung();
                    leistung.Id = leistung_id;
                    DataAccessLayer.delete_LeistungByID(leistung);
                }
            }

        }
        private void Leistung_Deleted(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = 0; i < dataGrid.RowCount; i++)
            {
                int id = int.Parse(dataGrid.Rows[i].Cells["ID"].Value.ToString());
                dataGrid.Rows[i].Cells["Pos"].Value = i + 1;
                Leistung uLeistung = new Leistung();
                uLeistung.Id = id;
                uLeistung.Pos = i + 1;
                DataAccessLayer.update_Leistungsposition(uLeistung);
            }
        }
        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabPage page = tabControl.TabPages[e.Index];
            Rectangle rect = e.Bounds;
            // Standardfarbe
            Color textColor = Color.Black;

            if (searchModeEnabled && !RECHNUNG_BEARBEITEN)
            {
                // Falls TabPage deaktiviert → grau
                if (page == tabStorno)
                {
                    textColor = Color.Gray;
                }
                else if (page == tabLeistungen)
                {
                    textColor = Color.Gray;
                }
            }
            // Hintergrund
            Color backColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? Color.LightSkyBlue
                : Color.LightGray;

            using (Brush brush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            // Schrift größer machen
            using (Font biggerFont = new Font(e.Font.FontFamily, e.Font.Size + 4, FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    page.Text,
                    biggerFont,
                    rect,
                    textColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        }
        private void TabControl_Selected(object sender, TabControlEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;

            //***********
            //  Buttons aktivieren - Deaktivieren
            //***********
            if (current.Name.Equals(tabRechnung.Name))
            {
                pbOk.ReadOnly = false;
            }
            else
            {
                pbOk.ReadOnly = true;
            }

            if (current.Name.Equals(tabLeistungen.Name))
            {
                pbDrucken.Hide();
                pbMailen.Hide();
                pbVorschau.Hide();
            }
            else
            {
                pbDrucken.Show();
                pbMailen.Show();
                pbVorschau.Show();
            }
        }
        private void EnablePrintTool(bool enable)
        {
            pbDrucken.Enabled = enable;
            pbMailen.Enabled = enable;
            pbVorschau.Enabled = enable;
        }
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;

            // wenn das Suchmodus aktiviert ist darf nicht zu Leistung/Storno Tab Wechseln
            if (searchModeEnabled && !RECHNUNG_BEARBEITEN)
            {
                if (current.Equals(tabLeistungen) || current.Equals(tabStorno))
                    e.Cancel = true;
                return;
            }
            else if (!searchModeEnabled)
            {
                if (current.Equals(tabStorno))
                    e.Cancel = true;
                return;
            }

        }
        private void BezahltStateChanged(object sender, EventArgs e)
        {
            if (checkBox_Rechnung_beglichen.Checked)
            {
                feld_Bezahlt_Am.Enabled = true;
                feld_Bezahlt_Am.Texts = DateTime.Today.ToString();
            }
            else
            {
                feld_Bezahlt_Am.Enabled = false;
                feld_Bezahlt_Am.ResetText();
            }
        }
        private void brutto_Berechnung(object sender, EventArgs e)
        {
            var t = sender as MyFieldText;
            if (!t.Focused)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(cmb_Leistung_Steursatz.Text) ||
                string.IsNullOrWhiteSpace(feld_Leistung_Netto.Text))
            {
                return;
            }
            int Steuersatz = int.Parse(cmb_Leistung_Steursatz.Text);

            float mwst = ((100F + Steuersatz) / 100F);

            double Netto = double.Parse(feld_Leistung_Netto.Text);
            feld_Leistung_Brutto.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(Netto * mwst));
            double steuer = double.Parse(feld_Leistung_Brutto.Text) - double.Parse(feld_Leistung_Netto.Text);
            feld_Leistung_steuern.Text = string.Format("{0:#,##0.00}", steuer);
        }
        private void netto_Berechnung(object sender, EventArgs e)
        {
            var t = sender as MyFieldText;
            if (!t.Focused)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(cmb_Leistung_Steursatz.Text) ||
                string.IsNullOrWhiteSpace(feld_Leistung_Brutto.Text))
            {
                return;
            }
            int Steuersatz = int.Parse(cmb_Leistung_Steursatz.Text);

            float mwst = ((100F + Steuersatz) / 100F);

            double Brutto = double.Parse(feld_Leistung_Brutto.Text);
            feld_Leistung_Netto.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(Brutto / mwst));
            double steuer = double.Parse(feld_Leistung_Brutto.Text) - double.Parse(feld_Leistung_Netto.Text);
            feld_Leistung_steuern.Text = string.Format("{0:#,##0.00}", steuer);
        }
        private void leistungsfeldFormatieren(object sender, EventArgs e)
        {
            MyFieldText box = sender as MyFieldText;
            if (string.IsNullOrWhiteSpace(box.Text))
            {
                feld_Leistung_Brutto.Text = "0";
                feld_Leistung_steuern.Text = "0";
                feld_Leistung_Netto.Text = "0";
            }
            feld_Leistung_Brutto.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(feld_Leistung_Brutto.Text));
            feld_Leistung_steuern.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(feld_Leistung_steuern.Text));
            feld_Leistung_Netto.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(feld_Leistung_Netto.Text));
        }
        private void steuersatz_wechsel(object sender, EventArgs e)
        {
            brutto_Berechnung(feld_Leistung_Netto, e);
        }
        private void InitializeFieldsLeistung()
        {
            feld_Leistung_Bez.Text = "";

            num_Leistung_Menge.Text = "1";

            feld_Leistung_Brutto.Text = string.Format("{0:#,##0.00}", 0d);
            feld_Leistung_steuern.Text = string.Format("{0:#,##0.00}", 0d);
            feld_Leistung_Netto.Text = string.Format("{0:#,##0.00}", 0d);
            feld_Leistung_Beschreibung.Text = "";
        }
        private bool Rechnung_Plausibility()
        {
            if (string.IsNullOrWhiteSpace(feld_Kurzbezeichnung.Texts))
            {
                errorProvider.SetError(feld_Kurzbezeichnung, "Rechnungsbezeichnung eingeben!!!");

                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Erstellungsdatum.Texts))
            {
                errorProvider.SetError(feld_Erstellungsdatum, "Erstellungsdatum der Rechnung fehlt!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Rechnungsdatum.Texts))
            {
                errorProvider.SetError(feld_Rechnungsdatum, "Rechnungsdatum eingeben!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Leistungsbeginn.Texts))
            {
                errorProvider.SetError(feld_Leistungsbeginn, "Leistungsbeginn eingeben!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Leistungsende.Texts))
            {
                errorProvider.SetError(feld_Leistungsende, "Leistungsende eingeben!!!");
                return false;
            }
            if (DateTime.Parse(feld_Leistungsbeginn.Texts) > DateTime.Parse(feld_Leistungsende.Texts))
            {
                errorProvider.SetError(feld_Leistungsbeginn, "Leistungszeitraum passt nicht.\nLeistungsbeginn ist größer als Leistungsende");
                errorProvider.SetError(feld_Leistungsende, "Leistungszeitraum passt nicht.\nLeistungsende ist kleiner als Leistungsbeginn");
                return false;
            }
            if (!cmb_Währung.Items.Contains(cmb_Währung.Text))
            {
                errorProvider.SetError(cmb_Währung, "Die eingegebene Währung ist in der Liste nicht vorhanden");
                return false;
            }
            if (string.IsNullOrWhiteSpace(num_Zahlungsziel.Text))
            {
                errorProvider.SetError(num_Zahlungsziel, "Zahlungsziel (in Tage) eingeben!!!");
                return false;
            }
            if (cmb_Kunde.FindStringExact(cmb_Kunde.Text) < 0)
            {
                errorProvider.SetError(cmb_Kunde, "Der eingegebene Kunde ist in der Liste nicht vorhanden");
                return false;
            }
            if (checkBox_Rechnung_beglichen.Checked)
            {
                if (string.IsNullOrWhiteSpace(feld_Bezahlt_Am.Texts))
                {
                    errorProvider.SetError(feld_Bezahlt_Am, "Zahlungsdatum eingeben");
                    return false;
                }
                if (!rb_Zahlung_bar.Checked && !rb_Zahlung_ueberweisung.Checked)
                {
                    errorProvider.SetError(rb_Zahlung_bar, "Zahlungsart auswählen");
                    return false;
                }
            }
            if (dataGrid.Rows.Count < 1)
            {
                errorProvider.SetError(pbDrucken, "Es sind noch keine Leistungen vorhanden");
                return false;
            }
            return true;
        }
        private bool Leistung_Plausibility()
        {
            //**** prüfung der Daten
            if (string.IsNullOrWhiteSpace(feld_Leistung_Bez.Texts))
            {
                errorProvider.SetError(feld_Leistung_Bez, "Leistungsbezeichnung eingeben!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmb_Leistung_Einheit.Text))
            {
                errorProvider.SetError(cmb_Leistung_Einheit, "Eine Einheit auswählen!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(num_Leistung_Menge.Texts))
            {
                errorProvider.SetError(num_Leistung_Menge, "Menge eingeben!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cmb_Leistung_Steursatz.Text))
            {
                errorProvider.SetError(cmb_Leistung_Steursatz, "Steuersatz auswählen!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Leistung_Beschreibung.Texts))
            {
            }
            if (string.IsNullOrWhiteSpace(feld_Leistung_Brutto.Texts))
            {
                errorProvider.SetError(feld_Leistung_Brutto, "Brutto-Betrag fehlt!!!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Leistung_Netto.Texts))
            {
                errorProvider.SetError(feld_Leistung_Netto, "Netto-Betrag fehlt!!!");
                return false;
            }

            return true;
        }
        private bool saveRechnung()
        {
            RechnungHelper rechnung = CopieDataFromMask();
            if (rechnung.Id > 0)
            {
                // Update Rechnung
                if (!User.Rechte.RECHNUNG_BEARBEITEN())
                {
                    MessageBox.Show("Kein Berechtigung Rechnungensdaten zu bearbeiten!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return false;
                }
                DialogResult result = MessageBox.Show("Rechnungsdaten geändert.\nÄnderung übernehmen?", "Frage!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.No))
                {
                    //nicht bestätigt -> nicht speichern, aber Daten in der Maske behalten
                    return false;
                }
                Log.Information("Rechnung wurde aktualisiert -> \n" + JsonConvert.SerializeObject(rechnung));
                DataAccessLayer.update_Rechnung(rechnung);

                RECHNUNG_BEARBEITEN = false;
                return true;
            }
            else
            {
                if (!User.Rechte.RECHNUNG_ERSTELLEN())
                {
                    MessageBox.Show("Kein Recht eine Rechnung zu erstellen!\nAdmin kontaktieren");
                    return false;
                }
                rechnung.Zahlungsstatus = Zahlungsstatus.OFFEN;
                rechnung.Prozessstatus = Prozessstatus.ERSTELLT;

                DialogResult result = MessageBox.Show("eine Rechnungsnummer wird generiert und kann nicht mehr zurückgesetzt werden.\nRechnung erstellen?",
                    "Info!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result.Equals(DialogResult.Yes))
                {
                    DataAccessLayer.Insert_Rechnung(rechnung);
                    Log.Information("Rechnung wurde erstellt ->\n " + JsonConvert.SerializeObject(rechnung));

                    return true;
                }
            }
            return false;
        }
        private bool saveLeistung()
        {
            // daten in dataGrid einfügen
            dataGrid.Rows.Add(0, dataGrid.Rows.Count + 1, feld_Leistung_Bez.Texts, cmb_Leistung_Einheit.Text,
                num_Leistung_Menge.Texts, cmb_Leistung_Steursatz.Text, feld_Leistung_Brutto.Texts,
                feld_Leistung_steuern.Texts, feld_Leistung_Netto.Texts, feld_Leistung_Beschreibung.Texts);

            return true;
        }
        private void Report_Show(object sender, EventArgs e)
        {
            string tabName = getCurrentTabName();
            if (tabName.Equals(tabStorno.Name))
            {
                string stornonummer = getCurrentDataGridStornoRow();
                if (!string.IsNullOrEmpty(stornonummer))
                {
                    StornoRechnungsReportViewer stornoRechnungsReport = new StornoRechnungsReportViewer();
                    stornoRechnungsReport.PreviewReport(stornonummer);
                }
                else
                {
                    MessageBox.Show("Keine 'Head'-Zeile ist in der Tabelle ausgewählt");
                }
            }
            else
            {
                RechnungsReportViewer reportViewer = new RechnungsReportViewer();
                reportViewer.PreviewReport(feld_Rechnungsnummer.Texts);
            }
        }
        private void Report_Print(object sender, EventArgs e)
        {

            string tabName = getCurrentTabName();
            Enabled = false;
            if (tabName.Equals(tabStorno.Name))
            {
                string stornonummer = getCurrentDataGridStornoRow();
                if (!string.IsNullOrEmpty(stornonummer))
                {
                    StornoRechnungsReportViewer stornoRechnungsReport = new StornoRechnungsReportViewer();
                    stornoRechnungsReport.PrintReport(stornonummer);
                    // die erzeugte Datei in das Archiv hängen
                    string file = stornoRechnungsReport.FilePath;
                    FileArchiv.DateiArchivieren(file);
                }
                else
                {
                    MessageBox.Show("Keine Zeile ist in der Tabelle ausgewählt");
                }
            }
            else
            {
                feld_Druckdatum.Text = DateTime.Now.Date.ToString();

                //****report*****
                RechnungsReportViewer reportViewer = new RechnungsReportViewer();
                reportViewer.PrintReport(feld_Rechnungsnummer.Texts);
                // die erzeugte Datei in das Archiv hängen
                string file = reportViewer.FilePath;
                FileArchiv.DateiArchivieren(file);
            }
            Cursor = Cursors.Default;
            Enabled = true;
        }
        private void Report_Mail(object sender, EventArgs e)
        {
            Enabled = false;
            Cursor = Cursors.WaitCursor;

            string tabName = getCurrentTabName();
            Enabled = false;
            if (tabName.Equals(tabStorno.Name))
            {
                string stornonummer = getCurrentDataGridStornoRow();
                if (!string.IsNullOrEmpty(stornonummer))
                {
                    StornoRechnungsReportViewer stornoRechnungsReport = new StornoRechnungsReportViewer();
                    stornoRechnungsReport.mailReport(stornonummer);
                    // die erzeugte Datei in das Archiv hängen
                    string file = stornoRechnungsReport.FilePath;
                    FileArchiv.DateiArchivieren(file);
                }
                else
                {
                    MessageBox.Show("Keine Zeile ist in der Tabelle ausgewählt");
                }
            }
            else
            {
                //****datum des Druckens setzen
                feld_Druckdatum.Text = DateTime.Now.Date.ToString();

                //****report*****
                RechnungsReportViewer reportViewer = new RechnungsReportViewer();
                reportViewer.mailReport(feld_Rechnungsnummer.Text);
                // die erzeugte Datei in das Archiv hängen
                string file = reportViewer.FilePath;
                FileArchiv.DateiArchivieren(file);
            }
            Cursor = Cursors.Default;
            Enabled = true;
        }
        private string getCurrentTabName()
        {
            if (tabControl != null)
            {
                TabPage current = tabControl.SelectedTab;
                if (current != null)
                {
                    return current.Name;
                }
            }
            return "";
        }
        private void stornoErzeugen(object sender, EventArgs e)
        {
            if (!User.Rechte.RECHNUNG_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung Stornorechnung zu erzeugen!");
                return;
            }
            Maske_StornoRechnung stornoRechnung = new Maske_StornoRechnung(feld_Rechnungsnummer.Texts, cmb_Kunde.Text);
            stornoRechnung.ShowDialog();
            // nachdem Erzeugen, Tabelle aktualisieren 
            PopulateStornos(feld_Rechnungsnummer.Texts);
        }
        private string getCurrentDataGridStornoRow()
        {
            if (DataGridStorno.CurrentRow != null)
            {
                var row = DataGridStorno.CurrentRow;
                // Beispiel: Wert aus Spalte "Name" auslesen
                var stornonummer = row.Cells["STORNO-NUMMER"].Value.ToString();

                return stornonummer;
            }
            return "";
        }
    }
}