using Microsoft.WindowsAPICodePack.Dialogs;
using MyControls;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    internal class Maske_KonfigNummernkreis : MyForm
    {
        private MyGroupBox myGroupBox2;
        private MyPushButton pb_Explorer;
        private MyLabel myLabel3;
        private MyFieldText feld_Rechnung_Speicher_Url;
        private MyLabel myLabel2;
        private MyPushButton pbKreisNr_Zuruecksetzen;
        private MyLabel myLabel1;
        private MyFieldText feld_AktuelleRechJahr;
        private MyFieldText feld_AktuelleRechNr;
        private MyDuseCheckBox checkBox_MwSt;
        private Label label1;
        private Label label2;
        private Label label3;
        private MyPushButton pbKreisNrStorno_Zuruecksetzen;
        private MyPushButton pb_StornoExplorer;
        private MyFieldText feld_AktuelleStornoNr;
        private MyLabel myLabel4;
        private MyFieldText feld_AktuelleStornoJahr;
        private MyFieldText feld_Storno_Speicher_Url;
        private MyLabel myLabel6;
        private MyLabel myLabel5;
        private MyGroupBox myGroupBox1;


        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_KonfigNummernkreis));
            myGroupBox1 = new MyGroupBox();
            label1 = new Label();
            checkBox_MwSt = new MyDuseCheckBox();
            pb_Explorer = new MyPushButton();
            myLabel3 = new MyLabel();
            feld_Rechnung_Speicher_Url = new MyFieldText();
            myLabel2 = new MyLabel();
            pbKreisNr_Zuruecksetzen = new MyPushButton();
            myLabel1 = new MyLabel();
            feld_AktuelleRechJahr = new MyFieldText();
            feld_AktuelleRechNr = new MyFieldText();
            myGroupBox2 = new MyGroupBox();
            label2 = new Label();
            label3 = new Label();
            pb_StornoExplorer = new MyPushButton();
            myLabel4 = new MyLabel();
            feld_Storno_Speicher_Url = new MyFieldText();
            myLabel5 = new MyLabel();
            pbKreisNrStorno_Zuruecksetzen = new MyPushButton();
            myLabel6 = new MyLabel();
            feld_AktuelleStornoJahr = new MyFieldText();
            feld_AktuelleStornoNr = new MyFieldText();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            myGroupBox1.SuspendLayout();
            myGroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // pbOk
            // 
            pbOk.BackColor = System.Drawing.Color.Transparent;
            pbOk.BackgroundImageLayout = ImageLayout.None;
            pbOk.FlatAppearance.BorderSize = 2;
            pbOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbOk.Image = ((System.Drawing.Image)(resources.GetObject("pbOk.Image")));
            pbOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbOk.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbOk.UseVisualStyleBackColor = false;
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(myGroupBox2);
            BorderBody.Controls.Add(myGroupBox1);
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(label2);
            myGroupBox1.Controls.Add(label1);
            myGroupBox1.Controls.Add(checkBox_MwSt);
            myGroupBox1.Controls.Add(pb_Explorer);
            myGroupBox1.Controls.Add(myLabel3);
            myGroupBox1.Controls.Add(feld_Rechnung_Speicher_Url);
            myGroupBox1.Controls.Add(myLabel2);
            myGroupBox1.Controls.Add(pbKreisNr_Zuruecksetzen);
            myGroupBox1.Controls.Add(myLabel1);
            myGroupBox1.Controls.Add(feld_AktuelleRechJahr);
            myGroupBox1.Controls.Add(feld_AktuelleRechNr);
            myGroupBox1.Location = new System.Drawing.Point(9, 18);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new System.Drawing.Size(788, 240);
            myGroupBox1.TabIndex = 0;
            myGroupBox1.TabStop = false;
            myGroupBox1.Text = "Rechnung";
            // 
            // label1
            // 
            label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            label1.Location = new System.Drawing.Point(569, 36);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(210, 72);
            label1.TabIndex = 9;
            label1.Text = "Hinweis: Beim Zurücksetzen wird der Nummernkreis neu gestartet.\nDer alte Stand ka" +
    "nn nicht wiederhergestellt werden.\nManuelle Eingriffe sind ausgeschlossen.";
            // 
            // checkBox_MwSt
            // 
            checkBox_MwSt.Location = new System.Drawing.Point(32, 162);
            checkBox_MwSt.Name = "checkBox_MwSt";
            checkBox_MwSt.Size = new System.Drawing.Size(305, 23);
            checkBox_MwSt.TabIndex = 8;
            checkBox_MwSt.Text = "Mehrwertsteuer (MwSt.) auf der Rechnung";
            // 
            // pb_Explorer
            // 
            pb_Explorer.BackColor = System.Drawing.Color.Transparent;
            pb_Explorer.BackgroundImageLayout = ImageLayout.None;
            pb_Explorer.FlatAppearance.BorderSize = 2;
            pb_Explorer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pb_Explorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pb_Explorer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pb_Explorer.ForeColor = System.Drawing.Color.Black;
            pb_Explorer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pb_Explorer.Location = new System.Drawing.Point(521, 114);
            pb_Explorer.Name = "pb_Explorer";
            pb_Explorer.ReadOnly = false;
            pb_Explorer.Size = new System.Drawing.Size(36, 27);
            pb_Explorer.TabIndex = 7;
            pb_Explorer.Text = "...";
            pb_Explorer.TextImageRelation = TextImageRelation.ImageBeforeText;
            pb_Explorer.UseVisualStyleBackColor = false;
            pb_Explorer.Click += Pb_Explorer_Click;
            // 
            // myLabel3
            // 
            myLabel3.AutoSize = true;
            myLabel3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel3.ForeColor = System.Drawing.Color.Black;
            myLabel3.Location = new System.Drawing.Point(28, 92);
            myLabel3.Name = "myLabel3";
            myLabel3.Size = new System.Drawing.Size(142, 19);
            myLabel3.TabIndex = 6;
            myLabel3.Text = "Ablageort für Kopien";
            myLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_Rechnung_Speicher_Url
            // 
            feld_Rechnung_Speicher_Url.ForeColor = System.Drawing.Color.Black;
            feld_Rechnung_Speicher_Url.Location = new System.Drawing.Point(32, 114);
            feld_Rechnung_Speicher_Url.Name = "feld_Rechnung_Speicher_Url";
            feld_Rechnung_Speicher_Url.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Rechnung_Speicher_Url.Size = new System.Drawing.Size(488, 27);
            feld_Rechnung_Speicher_Url.TabIndex = 5;
            feld_Rechnung_Speicher_Url.Texts = "";
            // 
            // myLabel2
            // 
            myLabel2.AutoSize = true;
            myLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel2.ForeColor = System.Drawing.Color.Black;
            myLabel2.Location = new System.Drawing.Point(28, 33);
            myLabel2.Name = "myLabel2";
            myLabel2.Size = new System.Drawing.Size(190, 19);
            myLabel2.TabIndex = 4;
            myLabel2.Text = "Nächste Rechnungsnummer";
            myLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbKreisNr_Zuruecksetzen
            // 
            pbKreisNr_Zuruecksetzen.BackColor = System.Drawing.Color.Transparent;
            pbKreisNr_Zuruecksetzen.BackgroundImageLayout = ImageLayout.None;
            pbKreisNr_Zuruecksetzen.FlatAppearance.BorderSize = 2;
            pbKreisNr_Zuruecksetzen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbKreisNr_Zuruecksetzen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbKreisNr_Zuruecksetzen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbKreisNr_Zuruecksetzen.ForeColor = System.Drawing.Color.Black;
            pbKreisNr_Zuruecksetzen.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbKreisNr_Zuruecksetzen.Location = new System.Drawing.Point(339, 55);
            pbKreisNr_Zuruecksetzen.Name = "pbKreisNr_Zuruecksetzen";
            pbKreisNr_Zuruecksetzen.ReadOnly = false;
            pbKreisNr_Zuruecksetzen.Size = new System.Drawing.Size(218, 27);
            pbKreisNr_Zuruecksetzen.TabIndex = 3;
            pbKreisNr_Zuruecksetzen.Text = "Nummernkreis zurücksetzen";
            pbKreisNr_Zuruecksetzen.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbKreisNr_Zuruecksetzen.UseVisualStyleBackColor = false;
            pbKreisNr_Zuruecksetzen.Click += PbKreisNr_Zuruecksetzen_Click;
            // 
            // myLabel1
            // 
            myLabel1.AutoSize = true;
            myLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel1.ForeColor = System.Drawing.Color.Black;
            myLabel1.Location = new System.Drawing.Point(207, 58);
            myLabel1.Name = "myLabel1";
            myLabel1.Size = new System.Drawing.Size(14, 19);
            myLabel1.TabIndex = 2;
            myLabel1.Text = "-";
            myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_AktuelleRechJahr
            // 
            feld_AktuelleRechJahr.ForeColor = System.Drawing.Color.Black;
            feld_AktuelleRechJahr.Location = new System.Drawing.Point(220, 55);
            feld_AktuelleRechJahr.Name = "feld_AktuelleRechJahr";
            feld_AktuelleRechJahr.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_AktuelleRechJahr.Size = new System.Drawing.Size(102, 27);
            feld_AktuelleRechJahr.TabIndex = 1;
            feld_AktuelleRechJahr.Texts = "";
            feld_AktuelleRechJahr.ReadOnly = true;
            // 
            // feld_AktuelleRechNr
            // 
            feld_AktuelleRechNr.ForeColor = System.Drawing.Color.Black;
            feld_AktuelleRechNr.Location = new System.Drawing.Point(32, 55);
            feld_AktuelleRechNr.Name = "feld_AktuelleRechNr";
            feld_AktuelleRechNr.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_AktuelleRechNr.Size = new System.Drawing.Size(172, 27);
            feld_AktuelleRechNr.TabIndex = 0;
            feld_AktuelleRechNr.Texts = "";
            feld_AktuelleRechNr.ReadOnly = true;
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(label3);
            myGroupBox2.Controls.Add(pbKreisNrStorno_Zuruecksetzen);
            myGroupBox2.Controls.Add(pb_StornoExplorer);
            myGroupBox2.Controls.Add(feld_AktuelleStornoNr);
            myGroupBox2.Controls.Add(myLabel4);
            myGroupBox2.Controls.Add(feld_AktuelleStornoJahr);
            myGroupBox2.Controls.Add(feld_Storno_Speicher_Url);
            myGroupBox2.Controls.Add(myLabel6);
            myGroupBox2.Controls.Add(myLabel5);
            myGroupBox2.Location = new System.Drawing.Point(9, 267);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new System.Drawing.Size(788, 210);
            myGroupBox2.TabIndex = 1;
            myGroupBox2.TabStop = false;
            myGroupBox2.Text = "Storonorechnung";
            // 
            // label2
            // 
            label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            label2.Location = new System.Drawing.Point(343, 167);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(314, 40);
            label2.TabIndex = 10;
            label2.Text = "Hinweis: Wenn diese Option aktiviert ist, wird die Rechnung mit Mehrwertsteuer au" +
    "sgewiesen (Netto, Steuer und Brutto).\n Ansonsten wird nur der Gesamtbetrag ohne " +
    "Steuer dargestellt.";
            // 
            // label3
            // 
            label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            label3.Location = new System.Drawing.Point(569, 55);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(210, 72);
            label3.TabIndex = 19;
            label3.Text = "Hinweis: Beim Zurücksetzen wird der Nummernkreis neu gestartet.\nDer alte Stand ka" +
    "nn nicht wiederhergestellt werden.\nManuelle Eingriffe sind ausgeschlossen.";
            // 
            // pb_StornoExplorer
            // 
            pb_StornoExplorer.BackColor = System.Drawing.Color.Transparent;
            pb_StornoExplorer.BackgroundImageLayout = ImageLayout.None;
            pb_StornoExplorer.FlatAppearance.BorderSize = 2;
            pb_StornoExplorer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pb_StornoExplorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pb_StornoExplorer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pb_StornoExplorer.ForeColor = System.Drawing.Color.Black;
            pb_StornoExplorer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pb_StornoExplorer.Location = new System.Drawing.Point(521, 133);
            pb_StornoExplorer.Name = "pb_StornoExplorer";
            pb_StornoExplorer.ReadOnly = false;
            pb_StornoExplorer.Size = new System.Drawing.Size(36, 27);
            pb_StornoExplorer.TabIndex = 18;
            pb_StornoExplorer.Text = "...";
            pb_StornoExplorer.TextImageRelation = TextImageRelation.ImageBeforeText;
            pb_StornoExplorer.UseVisualStyleBackColor = false;
            pb_StornoExplorer.Click += Pb_Explorer_Click;
            // 
            // myLabel4
            // 
            myLabel4.AutoSize = true;
            myLabel4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel4.ForeColor = System.Drawing.Color.Black;
            myLabel4.Location = new System.Drawing.Point(28, 111);
            myLabel4.Name = "myLabel4";
            myLabel4.Size = new System.Drawing.Size(142, 19);
            myLabel4.TabIndex = 17;
            myLabel4.Text = "Ablageort für Kopien";
            myLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_Storno_Speicher_Url
            // 
            feld_Storno_Speicher_Url.ForeColor = System.Drawing.Color.Black;
            feld_Storno_Speicher_Url.Location = new System.Drawing.Point(32, 133);
            feld_Storno_Speicher_Url.Name = "feld_Storno_Speicher_Url";
            feld_Storno_Speicher_Url.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Storno_Speicher_Url.Size = new System.Drawing.Size(488, 27);
            feld_Storno_Speicher_Url.TabIndex = 16;
            feld_Storno_Speicher_Url.Texts = "";

            // 
            // myLabel5
            // 
            myLabel5.AutoSize = true;
            myLabel5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel5.ForeColor = System.Drawing.Color.Black;
            myLabel5.Location = new System.Drawing.Point(28, 52);
            myLabel5.Name = "myLabel5";
            myLabel5.Size = new System.Drawing.Size(161, 19);
            myLabel5.TabIndex = 15;
            myLabel5.Text = "Nächste Stornonummer";
            myLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbKreisNrStorno_Zuruecksetzen
            // 
            pbKreisNrStorno_Zuruecksetzen.BackColor = System.Drawing.Color.Transparent;
            pbKreisNrStorno_Zuruecksetzen.BackgroundImageLayout = ImageLayout.None;
            pbKreisNrStorno_Zuruecksetzen.FlatAppearance.BorderSize = 2;
            pbKreisNrStorno_Zuruecksetzen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbKreisNrStorno_Zuruecksetzen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbKreisNrStorno_Zuruecksetzen.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbKreisNrStorno_Zuruecksetzen.ForeColor = System.Drawing.Color.Black;
            pbKreisNrStorno_Zuruecksetzen.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbKreisNrStorno_Zuruecksetzen.Location = new System.Drawing.Point(339, 74);
            pbKreisNrStorno_Zuruecksetzen.Name = "pbKreisNrStorno_Zuruecksetzen";
            pbKreisNrStorno_Zuruecksetzen.ReadOnly = false;
            pbKreisNrStorno_Zuruecksetzen.Size = new System.Drawing.Size(218, 27);
            pbKreisNrStorno_Zuruecksetzen.TabIndex = 14;
            pbKreisNrStorno_Zuruecksetzen.Text = "Nummernkreis zurücksetzen";
            pbKreisNrStorno_Zuruecksetzen.TextImageRelation = TextImageRelation.ImageBeforeText;
            pbKreisNrStorno_Zuruecksetzen.UseVisualStyleBackColor = false;
            pbKreisNrStorno_Zuruecksetzen.Click += PbKreisNr_Zuruecksetzen_Click;
            // 
            // myLabel6
            // 
            myLabel6.AutoSize = true;
            myLabel6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel6.ForeColor = System.Drawing.Color.Black;
            myLabel6.Location = new System.Drawing.Point(207, 77);
            myLabel6.Name = "myLabel6";
            myLabel6.Size = new System.Drawing.Size(14, 19);
            myLabel6.TabIndex = 13;
            myLabel6.Text = "-";
            myLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_AktuelleStornoJahr
            // 
            feld_AktuelleStornoJahr.ForeColor = System.Drawing.Color.Black;
            feld_AktuelleStornoJahr.Location = new System.Drawing.Point(220, 74);
            feld_AktuelleStornoJahr.Name = "feld_AktuelleStornoJahr";
            feld_AktuelleStornoJahr.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_AktuelleStornoJahr.Size = new System.Drawing.Size(102, 27);
            feld_AktuelleStornoJahr.TabIndex = 12;
            feld_AktuelleStornoJahr.Texts = "";
            feld_AktuelleStornoJahr.ReadOnly = true;
            // 
            // feld_AktuelleStornoNr
            // 
            feld_AktuelleStornoNr.ForeColor = System.Drawing.Color.Black;
            feld_AktuelleStornoNr.Location = new System.Drawing.Point(32, 74);
            feld_AktuelleStornoNr.Name = "feld_AktuelleStornoNr";
            feld_AktuelleStornoNr.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_AktuelleStornoNr.Size = new System.Drawing.Size(172, 27);
            feld_AktuelleStornoNr.TabIndex = 11;
            feld_AktuelleStornoNr.Texts = "";
            feld_AktuelleStornoNr.ReadOnly = true;

            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Konfig_rad_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            myGroupBox1.ResumeLayout(false);
            myGroupBox1.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        protected override string _name()
        {
            return "Nummernkreis";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            PopulateRechnungsdaten();
            PopulateStornodaten();
            return true;
        }
        private void PopulateStornodaten()
        {
            DataTable dataTable_nummerkreis = DataAccessLayer.Get_AktStornoNummer();
            if (dataTable_nummerkreis != null && dataTable_nummerkreis.Rows.Count > 0)
            {
                DataRow row = dataTable_nummerkreis.Rows[0];
                string rechnungsnummer = (string)row["NUMMER"];
                int rechnungsjahr = (int)row["JAHR"];

                feld_AktuelleStornoNr.Texts = rechnungsnummer;
                feld_AktuelleStornoJahr.Texts = rechnungsjahr.ToString();
            }
            Formular formular = new Formular(Globals.FORMULAR_STORNO_RECHNUNG);
            DataTable datatable_rechnung = DataAccessLayer.queryFormular(formular);
            if (datatable_rechnung != null && datatable_rechnung.Rows.Count > 0)
            {
                DataRow rowf = datatable_rechnung.Rows[0];
                string ordner = rowf.Field<string>("ABLAGEORT");

                feld_Storno_Speicher_Url.Texts = ordner;
            }
        }
        private void PopulateRechnungsdaten()
        {
            DataTable dataTable_nummerkreis = DataAccessLayer.Get_AktRechnungNummer();
            if (dataTable_nummerkreis != null && dataTable_nummerkreis.Rows.Count > 0)
            {
                DataRow row = dataTable_nummerkreis.Rows[0];
                string rechnungsnummer = (string)row["NUMMER"];
                int rechnungsjahr = (int)row["JAHR"];

                feld_AktuelleRechNr.Texts = rechnungsnummer;
                feld_AktuelleRechJahr.Texts = rechnungsjahr.ToString();
            }
            Formular formular = new Formular(Globals.FORMULAR_RECHNUNG);
            DataTable datatable_rechnung = DataAccessLayer.queryFormular(formular);
            if (datatable_rechnung != null && datatable_rechnung.Rows.Count > 0)
            {
                DataRow rowf = datatable_rechnung.Rows[0];
                object bMwst = rowf["MWST"];
                string ordner = rowf.Field<string>("ABLAGEORT");

                feld_Rechnung_Speicher_Url.Texts = ordner;

                if (bMwst == DBNull.Value)
                {
                    checkBox_MwSt.Checked = false;
                }
                else
                {
                    checkBox_MwSt.Checked = (bool)bMwst;
                }
            }
        }
        protected override bool _PlausibleBevorSave()
        {
            if (string.IsNullOrEmpty(feld_Rechnung_Speicher_Url.Texts))
            {
                errorProvider.SetError(feld_Rechnung_Speicher_Url, "Bitte Ablageort für Rechnungskopien eingeben");
                return false;
            }
            if (string.IsNullOrEmpty(feld_Storno_Speicher_Url.Texts))
            {
                errorProvider.SetError(feld_Rechnung_Speicher_Url, "Bitte Ablageort für Stornokopien eingeben");
                return false;
            }
            return true;
        }
        protected override bool _Save()
        {
            Formular formular_rechnung = new Formular(Globals.FORMULAR_RECHNUNG);
            formular_rechnung.BMwst = checkBox_MwSt.Checked;
            formular_rechnung.SpeicherOrdner = feld_Rechnung_Speicher_Url.Text;

            DataAccessLayer.Update_AblageortFormular(formular_rechnung);

            Formular formular_storno = new Formular(Globals.FORMULAR_STORNO_RECHNUNG);
            formular_storno.BMwst = checkBox_MwSt.Checked;
            formular_storno.SpeicherOrdner = feld_Storno_Speicher_Url.Text;

            DataAccessLayer.Update_AblageortFormular(formular_storno);

            return true;
        }
        protected override void _AfterSave()
        {
            _Populate();
        }
        private void Pb_Explorer_Click(object sender, EventArgs e)
        {
            var pb = sender as MyPushButton;
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (pb.Name.Equals(pb_Explorer.Name))
                {
                    feld_Rechnung_Speicher_Url.Texts = dialog.FileName;
                }
                else if (pb.Name.Equals(pb_StornoExplorer.Name))
                {
                    feld_Storno_Speicher_Url.Texts = dialog.FileName;
                }
            }
        }
        private void PbKreisNr_Zuruecksetzen_Click(object sender, EventArgs e)
        {
            var pb = sender as MyPushButton;
            if (pb.Name.Equals(pbKreisNr_Zuruecksetzen.Name))
            {
                DialogResult result = MessageBox.Show("Rechnungsnummerkreis zurücksetzen?", "Warnung!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataAccessLayer.Rechnungnummerkreis_Zuruecksetzen();
                    MessageBox.Show("Rechnungsnummerkreis ist erfogreich zurückgesetzt", "info!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Populate();
                }
            }
            else if (pb.Name.Equals(pbKreisNrStorno_Zuruecksetzen.Name))
            {
                DialogResult result = MessageBox.Show("Stornonummerkreis zurücksetzen?", "Warnung!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DataAccessLayer.Stornonummerkreis_Zuruecksetzen();
                    MessageBox.Show("Stornonummerkreis ist erfogreich zurückgesetzt", "info!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Populate();
                }
            }

        }
    }
}