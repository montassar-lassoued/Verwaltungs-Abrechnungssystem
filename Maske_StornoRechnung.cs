using MyControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_StornoRechnung : MyForm
    {
        private MyLabel myLabel16;
        private RowMergeView dataGrid_StornoLeistungen;
        private MyLabel myLabel15;
        private MyFieldDate feld_StornoDatum;
        private MyDuseFieldText feld_Storno_Bez;
        private MyDuseFieldText feld_Bezugsrechnung;
        private MyDuseFieldText feld_kunde;
        private MyDuseFieldText feld_StornoRechnungsNummer;
        private MyLabel myLabel2;
        private MyLabel myLabel1;
        private string Rechnungsnummer;
        private string Kundenname;

        public Maske_StornoRechnung(string bezugRechnung, string _kundenname)
        {
            Rechnungsnummer = bezugRechnung;
            Kundenname = _kundenname;
        }
        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_StornoRechnung));
            myLabel16 = new MyLabel();
            dataGrid_StornoLeistungen = new RowMergeView();
            myLabel15 = new MyLabel();
            feld_StornoDatum = new MyFieldDate();
            feld_Storno_Bez = new MyDuseFieldText();
            feld_StornoRechnungsNummer = new MyDuseFieldText();
            feld_kunde = new MyDuseFieldText();
            feld_Bezugsrechnung = new MyDuseFieldText();
            myLabel1 = new MyLabel();
            myLabel2 = new MyLabel();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGrid_StornoLeistungen)).BeginInit();
            SuspendLayout();
            // 
            // pbOk
            // 
            pbOk.BackColor = Color.Transparent;
            pbOk.BackgroundImageLayout = ImageLayout.None;
            pbOk.FlatAppearance.BorderSize = 2;
            pbOk.FlatAppearance.MouseDownBackColor = Color.Lime;
            pbOk.FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbOk.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            pbOk.Image = ((Image)(resources.GetObject("pbOk.Image")));
            pbOk.ImageAlign = ContentAlignment.TopLeft;
            pbOk.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbOk.UseVisualStyleBackColor = false;
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(myLabel2);
            BorderBody.Controls.Add(feld_Bezugsrechnung);
            BorderBody.Controls.Add(myLabel1);
            BorderBody.Controls.Add(feld_kunde);
            BorderBody.Controls.Add(myLabel16);
            BorderBody.Controls.Add(dataGrid_StornoLeistungen);
            BorderBody.Controls.Add(myLabel15);
            BorderBody.Controls.Add(feld_StornoDatum);
            BorderBody.Controls.Add(feld_Storno_Bez);
            BorderBody.Controls.Add(feld_StornoRechnungsNummer);
            // 
            // myLabel16
            // 
            myLabel16.AutoSize = true;
            myLabel16.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel16.ForeColor = Color.Black;
            myLabel16.Location = new Point(23, 136);
            myLabel16.Name = "myLabel16";
            myLabel16.Size = new Size(76, 19);
            myLabel16.TabIndex = 16;
            myLabel16.Text = "Positionen";
            myLabel16.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dataGrid_StornoLeistungen
            // 
            dataGrid_StornoLeistungen.AllowUserToAddRows = false;
            dataGrid_StornoLeistungen.AllowUserToDeleteRows = false;
            dataGrid_StornoLeistungen.Location = new Point(26, 158);
            dataGrid_StornoLeistungen.Name = "dataGrid_StornoLeistungen";
            dataGrid_StornoLeistungen.Size = new Size(757, 334);
            dataGrid_StornoLeistungen.CellContentClick += DataGrid_StornoLeistungen_CellContentClick;
            dataGrid_StornoLeistungen.CurrentCellDirtyStateChanged += dataGrid_StornoLeistungen_CurrentCellDirtyStateChanged;
            //dataGrid_StornoLeistungen.CellFormatting += dataGrid_StornoLeistungen_CellFormatting;
            dataGrid_StornoLeistungen.DataBindingComplete += dataGrid_StornoLeistungen_DataBindingComplete;
            // 
            // myLabel15
            // 
            myLabel15.AutoSize = true;
            myLabel15.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel15.ForeColor = Color.Black;
            myLabel15.Location = new Point(23, 84);
            myLabel15.Name = "myLabel15";
            myLabel15.Size = new Size(99, 19);
            myLabel15.TabIndex = 14;
            myLabel15.Text = "Storno-Datum";
            myLabel15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // feld_StornoDatum
            // 
            feld_StornoDatum.BackColor = SystemColors.ControlLightLight;
            feld_StornoDatum.Location = new Point(26, 106);
            feld_StornoDatum.Name = "feld_StornoDatum";
            feld_StornoDatum.PlaceholderColor = Color.DarkGray;
            feld_StornoDatum.PlaceholderText = "";
            feld_StornoDatum.Size = new Size(203, 27);
            feld_StornoDatum.TabIndex = 13;
            feld_StornoDatum.TextAlign = HorizontalAlignment.Center;
            feld_StornoDatum.Texts = "";
            feld_StornoDatum.ToolTip = " ";
            feld_StornoDatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Storno_Bez
            // 
            feld_Storno_Bez.ForeColor = Color.DarkGray;
            feld_Storno_Bez.Location = new Point(26, 50);
            feld_Storno_Bez.Name = "feld_Storno_Bez";
            feld_Storno_Bez.PlaceholderColor = Color.DarkGray;
            feld_Storno_Bez.PlaceholderText = " Storno-Bezeichnung";
            feld_Storno_Bez.Size = new Size(578, 27);
            feld_Storno_Bez.TabIndex = 11;
            feld_Storno_Bez.Text = " Storno-Bezeichnung";
            feld_Storno_Bez.Texts = "";
            // 
            // feld_StornoRechnungsNummer
            // 
            feld_StornoRechnungsNummer.ForeColor = Color.DarkGray;
            feld_StornoRechnungsNummer.Location = new Point(610, 50);
            feld_StornoRechnungsNummer.Name = "feld_StornoRechnungsNummer";
            feld_StornoRechnungsNummer.PlaceholderColor = Color.DarkGray;
            feld_StornoRechnungsNummer.PlaceholderText = " Storno-Nummer";
            feld_StornoRechnungsNummer.ReadOnly = true;
            feld_StornoRechnungsNummer.Size = new Size(173, 27);
            feld_StornoRechnungsNummer.TabIndex = 12;
            feld_StornoRechnungsNummer.Text = " Storno-Nummer";
            feld_StornoRechnungsNummer.Texts = "";
            // 
            // feld_kunde
            // 
            feld_kunde.Enabled = false;
            feld_kunde.ForeColor = Color.Black;
            feld_kunde.Location = new Point(235, 106);
            feld_kunde.Name = "feld_kunde";
            feld_kunde.PlaceholderColor = Color.DarkGray;
            feld_kunde.ReadOnly = true;
            feld_kunde.Size = new Size(369, 27);
            feld_kunde.TabIndex = 17;
            feld_kunde.Texts = "";
            // 
            // feld_Bezugsrechnung
            // 
            feld_Bezugsrechnung.Enabled = false;
            feld_Bezugsrechnung.ForeColor = Color.Black;
            feld_Bezugsrechnung.Location = new Point(610, 106);
            feld_Bezugsrechnung.Name = "feld_Bezugsrechnung";
            feld_Bezugsrechnung.PlaceholderColor = Color.DarkGray;
            feld_Bezugsrechnung.ReadOnly = true;
            feld_Bezugsrechnung.Size = new Size(172, 27);
            feld_Bezugsrechnung.TabIndex = 19;
            feld_Bezugsrechnung.Texts = "";
            // 
            // myLabel1
            // 
            myLabel1.AutoSize = true;
            myLabel1.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel1.ForeColor = Color.Black;
            myLabel1.Location = new Point(231, 84);
            myLabel1.Name = "myLabel1";
            myLabel1.Size = new Size(49, 19);
            myLabel1.TabIndex = 18;
            myLabel1.Text = "Kunde";
            myLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // myLabel2
            // 
            myLabel2.AutoSize = true;
            myLabel2.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myLabel2.ForeColor = Color.Black;
            myLabel2.Location = new Point(606, 84);
            myLabel2.Name = "myLabel2";
            myLabel2.Size = new Size(117, 19);
            myLabel2.TabIndex = 20;
            myLabel2.Text = "Rechnungsbezug";
            myLabel2.TextAlign = ContentAlignment.MiddleCenter;
            BorderBody.ResumeLayout(false);
            BorderBody.PerformLayout();
            Icon = Properties.Resources.StornoR_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGrid_StornoLeistungen)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        protected override string _name()
        {
            return "StornoRechnung";
        }
        protected override void _OnLoad(EventArgs e)
        {
            feld_StornoRechnungsNummer.Enabled = false;
            _Populate();
        }
        protected override bool _Populate()
        {
            feld_StornoRechnungsNummer.Texts = NummernkreisManager.GetAktuelleStornoNummer();
            feld_Bezugsrechnung.Texts = Rechnungsnummer;
            feld_kunde.Texts = Kundenname;
            feld_StornoDatum.Texts = DateTime.Now.Date.ToString();

            DataTable stornoL_DT = DataAccessLayer.queryStornoLeistungenByBezugsrechnung(Rechnungsnummer);

            DataColumn col = new DataColumn("STORNIEREN", typeof(bool));
            col.DefaultValue = false;
            col.ReadOnly = false;
            stornoL_DT.Columns.Add(col); // Erst hier gehört sie zur Tabelle
            col.SetOrdinal(0);

            dataGrid_StornoLeistungen.DataSource = stornoL_DT;

            dataGrid_StornoLeistungen.Columns["STATUS"].Visible = false;
            dataGrid_StornoLeistungen.Columns["ID"].Visible = false;
            dataGrid_StornoLeistungen.ClearSelection();
            return true;
        }
        protected override bool _PlausibleBevorSave()
        {
            return true;
        }
        protected override bool _Save()
        {
            List<Leistung> leistungs = new List<Leistung>();
            for (int i = 0; i < dataGrid_StornoLeistungen.RowCount; i++)
            {
                Leistung leistung = new Leistung();
                var storno = dataGrid_StornoLeistungen.Rows[i].Cells["STORNIEREN"].Value;
                if (storno != null && storno != DBNull.Value && (bool)storno == true)
                {
                    leistung.Pos = int.Parse(dataGrid_StornoLeistungen.Rows[i].Cells["Pos"].Value.ToString());
                    leistung.Bezeichnung = (string)dataGrid_StornoLeistungen.Rows[i].Cells["BEZEICHNUNG"].Value;
                    leistung.Einheit = dataGrid_StornoLeistungen.Rows[i].Cells["Einheit"].Value.ToString();
                    leistung.Menge = int.Parse(dataGrid_StornoLeistungen.Rows[i].Cells["Menge"].Value.ToString());
                    leistung.Steuersatz = -1 * int.Parse(dataGrid_StornoLeistungen.Rows[i].Cells["Steuersatz"].Value.ToString());
                    leistung.Brutto = -1 * float.Parse(dataGrid_StornoLeistungen.Rows[i].Cells["Brutto"].Value.ToString());
                    leistung.Steuer = -1 * float.Parse(dataGrid_StornoLeistungen.Rows[i].Cells["Steuer"].Value.ToString());
                    leistung.Netto = -1 * float.Parse(dataGrid_StornoLeistungen.Rows[i].Cells["Netto"].Value.ToString());
                    leistung.Beschreibung = (string)dataGrid_StornoLeistungen.Rows[i].Cells["Beschreibung"].Value;

                    leistungs.Add(leistung);
                }
            }
            /*prüfen, ob hier Leistungen zu stornieren sind*/
            if (leistungs.Count == 0)
            {
                MessageBox.Show("Es sind Positionen ausgewählt!\nStornorechnung kann nicht erzeugt werden", "Info!!");
                return false;
            }


            StornoRechnung rechnung = new StornoRechnung();
            rechnung.StornoNummer = feld_StornoRechnungsNummer.Texts;
            rechnung.Kurzbez = feld_Storno_Bez.Texts;
            rechnung.StornoDatum = DateTime.Parse(feld_StornoDatum.Texts);
            rechnung.BezugRechnungsnummer = Rechnungsnummer;

            rechnung.Leistungen = leistungs;

            DataAccessLayer.Insert_StornoRechnung(rechnung);

            MyMessageBox messageBox = new MyMessageBox("Stornorechnung wurde erzeugt.\nAusdrucken oder eine Kopie an den Kunden send?");
            messageBox.ShowDialog();
            string result = messageBox.Result;

            StornoRechnungsReportViewer stornoRechnungsReport = new StornoRechnungsReportViewer();
            if (result.Equals("MAIL"))
            {
                stornoRechnungsReport.mailReport(feld_StornoRechnungsNummer.Text);
            }
            else if (result.Equals("DRUCK"))
            {
                stornoRechnungsReport.PrintReport(feld_StornoRechnungsNummer.Text);
            }
            else
            {
                stornoRechnungsReport.PreviewReport(feld_StornoRechnungsNummer.Text);
            }
            return true;
        }
        protected override void _AfterSave()
        {
        }
        private void DataGrid_StornoLeistungen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid_StornoLeistungen.Columns[e.ColumnIndex].Name == "Stornieren")
            {
                bool storniert = (bool)dataGrid_StornoLeistungen.Rows[e.RowIndex].Cells["Stornieren"].Value;
                if (storniert)
                {
                    dataGrid_StornoLeistungen.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                }
                else
                {
                    dataGrid_StornoLeistungen.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGrid_StornoLeistungen.DefaultCellStyle.BackColor;
                }
            }
        }
        private void dataGrid_StornoLeistungen_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGrid_StornoLeistungen.IsCurrentCellDirty)
            {
                dataGrid_StornoLeistungen.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dataGrid_StornoLeistungen_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGrid_StornoLeistungen.Columns[e.ColumnIndex].Name == "STATUS")
            {
                if (e.Value?.ToString() == "STORNIERT")
                {
                    dataGrid_StornoLeistungen.Rows[e.RowIndex].Cells["STORNIEREN"].Value = true;
                    dataGrid_StornoLeistungen.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Orange;
                    dataGrid_StornoLeistungen.Rows[e.RowIndex].ReadOnly = true;
                }
                else
                {
                    dataGrid_StornoLeistungen.Rows[e.RowIndex].Cells["STORNIEREN"].ReadOnly = false;
                }
            }
        }
        private void dataGrid_StornoLeistungen_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGrid_StornoLeistungen.Rows)
            {
                if (row.Cells["STATUS"].Value?.ToString() == "STORNIERT")
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    row.ReadOnly = true; // ganze Zeile nicht editierbar
                }
                else
                {
                    row.ReadOnly = true; // ganze Zeile nicht editierbar
                    row.Cells["STORNIEREN"].ReadOnly = false; // nur Checkbox bleibt editierbar
                }
            }
        }
    }
}
