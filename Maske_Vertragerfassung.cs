using MyControls;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Vertragerfassung : MyForm
    {
        MenuItem menuItem_vertrag;
        MenuItem item_loeschen;
        MenuItem item_aktivieren;
        MenuItem item_pausieren;
        MenuItem item_kuendigen;
        bool menuItemEnabled = false;
        private MyGroupBox GroupBox_Ansprechpartner;
        private MyGroupBox myGroupBox2;
        private MyDuseFieldText feld_Handy_V;
        private MyDuseFieldText feld_Name_V;
        private MyDuseFieldText feld_Ort_V;
        private MyDuseFieldText feld_Nachname_V;
        private MyDuseNumericField feld_PLZ_V;
        private MyDuseFieldDate feld_Geburtsdatum_V;
        private MyDuseFieldText feld_Srasse_V;
        private MyDuseCheckBox checkbox_Minderjaehrige;
        private MyDuseRadioButton checkbox_Maennlich;
        private MyDuseRadioButton checkbox_Weiblich;
        private MyDuseFieldText feld_Handy;
        private MyDuseFieldText feld_Ort;
        private MyDuseNumericField feld_PLZ;
        private MyDuseFieldText feld_Straße;
        private MyDuseFieldDate feld_Geburtsdatum;
        private MyDuseFieldText feld_Nachname;
        private MyDuseFieldText feld_Name;
        private MyDuseRichTextBox feld_Anmerkung;
        private MyDuseFieldDate feld_Vertragsbeginn;
        private MyGroupBox myGroupBox3;
        private MyDuseRadioButton checkbox_Kurs_Kinder;
        private MyDuseRadioButton checkbox_Kurs_Erwachsene;
        private MyGroupBox myGroupBox1;
        private MyCheckedListBox checkedListKurse;
        private Label label1;
        private FlowLayoutPanel pnlBetrag;
        private MyGroupBox GroupBox_Mitglied;
        private MyFieldText feld_id;
        private bool bSearchModeEnabled;
        Dictionary<string, decimal> gespeicherteBeträge = new Dictionary<string, decimal>();

        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Vertragerfassung));
            menuItem_vertrag = new MenuItem();
            GroupBox_Mitglied = new MyGroupBox();
            checkbox_Minderjaehrige = new MyDuseCheckBox();
            checkbox_Maennlich = new MyDuseRadioButton();
            checkbox_Weiblich = new MyDuseRadioButton();
            feld_Handy = new MyDuseFieldText();
            feld_Ort = new MyDuseFieldText();
            feld_PLZ = new MyDuseNumericField();
            feld_Straße = new MyDuseFieldText();
            feld_Geburtsdatum = new MyDuseFieldDate();
            feld_Nachname = new MyDuseFieldText();
            feld_Name = new MyDuseFieldText();
            GroupBox_Ansprechpartner = new MyGroupBox();
            feld_Handy_V = new MyDuseFieldText();
            feld_Name_V = new MyDuseFieldText();
            feld_Ort_V = new MyDuseFieldText();
            feld_Nachname_V = new MyDuseFieldText();
            feld_PLZ_V = new MyDuseNumericField();
            feld_Geburtsdatum_V = new MyDuseFieldDate();
            feld_Srasse_V = new MyDuseFieldText();
            myGroupBox2 = new MyGroupBox();
            label1 = new Label();
            feld_Anmerkung = new MyDuseRichTextBox();
            feld_Vertragsbeginn = new MyDuseFieldDate();
            myGroupBox3 = new MyGroupBox();
            checkbox_Kurs_Kinder = new MyDuseRadioButton();
            checkbox_Kurs_Erwachsene = new MyDuseRadioButton();
            myGroupBox1 = new MyGroupBox();
            checkedListKurse = new MyCheckedListBox();
            pnlBetrag = new FlowLayoutPanel();
            feld_id = new MyFieldText();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            GroupBox_Mitglied.SuspendLayout();
            GroupBox_Ansprechpartner.SuspendLayout();
            myGroupBox2.SuspendLayout();
            myGroupBox3.SuspendLayout();
            myGroupBox1.SuspendLayout();
            pnlBetrag.SuspendLayout();
            SuspendLayout();
            // Set the caption of the menu items.
            menuItem_vertrag.Text = "&Vertrag";
            menuItem_vertrag.Shortcut = Shortcut.CtrlV;
            menuItem_vertrag.ShowShortcut = true;
            menuItem_vertrag.BarBreak = false;
            menuItem_vertrag.PerformClick();

            item_loeschen = new MenuItem();
            item_loeschen.Text = "Löschen";
            item_loeschen.Shortcut = Shortcut.CtrlL;
            item_loeschen.ShowShortcut = true;
            item_loeschen.BarBreak = false;
            item_loeschen.Click += new System.EventHandler(vertrag_loeschen);
            item_loeschen.Enabled = false;


            item_aktivieren = new MenuItem();
            item_aktivieren.Text = "Reaktivieren";
            item_aktivieren.Shortcut = Shortcut.CtrlR;
            item_aktivieren.ShowShortcut = true;
            item_aktivieren.BarBreak = false;
            item_aktivieren.Click += new System.EventHandler(vertrag_reaktivieren);
            item_aktivieren.Enabled = false;


            item_pausieren = new MenuItem();
            item_pausieren.Text = "Pausieren";
            item_pausieren.Shortcut = Shortcut.CtrlP;
            item_pausieren.ShowShortcut = true;
            item_pausieren.BarBreak = false;
            item_pausieren.Click += new System.EventHandler(Vertrag_pausieren);
            item_pausieren.Enabled = false;


            item_kuendigen = new MenuItem();
            item_kuendigen.Text = "Kündigen";
            item_kuendigen.Shortcut = Shortcut.CtrlK;
            item_kuendigen.ShowShortcut = true;
            item_kuendigen.BarBreak = false;
            item_kuendigen.Click += new System.EventHandler(Vertrag_Kuendigen);
            item_kuendigen.Enabled = false;

            //
            //Menü einfügen
            menuItem_vertrag.MenuItems.Add(item_pausieren);
            menuItem_vertrag.MenuItems.Add(item_aktivieren);
            menuItem_vertrag.MenuItems.Add(item_kuendigen);
            menuItem_vertrag.MenuItems.Add(item_loeschen);
            mainMenu.MenuItems.Add(menuItem_vertrag);
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
            BorderBody.Controls.Add(myGroupBox2);
            BorderBody.Controls.Add(GroupBox_Ansprechpartner);
            BorderBody.Controls.Add(GroupBox_Mitglied);
            // 
            // GroupBox_Mitglied
            // 
            GroupBox_Mitglied.BorderColor = Color.DimGray;
            GroupBox_Mitglied.BorderThickness = 1;
            GroupBox_Mitglied.Controls.Add(checkbox_Minderjaehrige);
            GroupBox_Mitglied.Controls.Add(checkbox_Maennlich);
            GroupBox_Mitglied.Controls.Add(checkbox_Weiblich);
            GroupBox_Mitglied.Controls.Add(feld_Handy);
            GroupBox_Mitglied.Controls.Add(feld_Ort);
            GroupBox_Mitglied.Controls.Add(feld_PLZ);
            GroupBox_Mitglied.Controls.Add(feld_Straße);
            GroupBox_Mitglied.Controls.Add(feld_Geburtsdatum);
            GroupBox_Mitglied.Controls.Add(feld_Nachname);
            GroupBox_Mitglied.Controls.Add(feld_Name);
            GroupBox_Mitglied.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            GroupBox_Mitglied.Location = new Point(7, 24);
            GroupBox_Mitglied.Name = "GroupBox_Mitglied";
            GroupBox_Mitglied.Size = new Size(393, 262);
            GroupBox_Mitglied.TabIndex = 0;
            GroupBox_Mitglied.TabStop = false;
            GroupBox_Mitglied.Text = "Mitglied";
            // 
            // checkbox_Minderjaehrige
            // 
            checkbox_Minderjaehrige.Location = new Point(270, 95);
            checkbox_Minderjaehrige.Name = "checkbox_Minderjaehrige";
            checkbox_Minderjaehrige.Size = new Size(112, 23);
            checkbox_Minderjaehrige.TabIndex = 9;
            checkbox_Minderjaehrige.Text = "Minderjährig";
            checkbox_Minderjaehrige.Click += Checkbox_Minderjaehrige_CheckStateChanged;
            // 
            // checkbox_Maennlich
            // 
            checkbox_Maennlich.Location = new Point(219, 228);
            checkbox_Maennlich.Name = "checkbox_Maennlich";
            checkbox_Maennlich.Size = new Size(88, 23);
            checkbox_Maennlich.TabIndex = 8;
            checkbox_Maennlich.TabStop = true;
            checkbox_Maennlich.Text = "Männlich";
            // 
            // checkbox_Weiblich
            // 
            checkbox_Weiblich.Location = new Point(12, 228);
            checkbox_Weiblich.Name = "checkbox_Weiblich";
            checkbox_Weiblich.Size = new Size(83, 23);
            checkbox_Weiblich.TabIndex = 7;
            checkbox_Weiblich.TabStop = true;
            checkbox_Weiblich.Text = "Weiblich";
            // 
            // feld_Handy
            // 
            feld_Handy.ForeColor = Color.DarkGray;
            feld_Handy.Location = new Point(12, 192);
            feld_Handy.Name = "feld_Handy";
            feld_Handy.PlaceholderColor = Color.DarkGray;
            feld_Handy.PlaceholderText = " Handy";
            feld_Handy.Size = new Size(370, 27);
            feld_Handy.TabIndex = 6;
            feld_Handy.Text = " Handy";
            feld_Handy.Texts = "";
            // 
            // feld_Ort
            // 
            feld_Ort.ForeColor = Color.DarkGray;
            feld_Ort.Location = new Point(219, 159);
            feld_Ort.Name = "feld_Ort";
            feld_Ort.PlaceholderColor = Color.DarkGray;
            feld_Ort.PlaceholderText = " Ort";
            feld_Ort.Size = new Size(163, 27);
            feld_Ort.TabIndex = 5;
            feld_Ort.Text = " Ort";
            feld_Ort.Texts = "";
            // 
            // feld_PLZ
            // 
            feld_PLZ.BackColor = SystemColors.ControlLightLight;
            feld_PLZ.ForeColor = Color.DarkGray;
            feld_PLZ.Location = new Point(12, 159);
            feld_PLZ.Name = "feld_PLZ";
            feld_PLZ.PlaceholderColor = Color.DarkGray;
            feld_PLZ.PlaceholderText = "PLZ";
            feld_PLZ.Size = new Size(200, 27);
            feld_PLZ.TabIndex = 4;
            feld_PLZ.Text = "PLZ";
            feld_PLZ.Texts = "";
            // 
            // feld_Straße
            // 
            feld_Straße.ForeColor = Color.DarkGray;
            feld_Straße.Location = new Point(12, 126);
            feld_Straße.Name = "feld_Straße";
            feld_Straße.PlaceholderColor = Color.DarkGray;
            feld_Straße.PlaceholderText = " Straße";
            feld_Straße.Size = new Size(370, 27);
            feld_Straße.TabIndex = 3;
            feld_Straße.Text = " Straße";
            feld_Straße.Texts = "";
            // 
            // feld_Geburtsdatum
            // 
            feld_Geburtsdatum.Location = new Point(12, 93);
            feld_Geburtsdatum.Name = "feld_Geburtsdatum";
            feld_Geburtsdatum.PlaceholderColor = Color.DarkGray;
            feld_Geburtsdatum.PlaceholderText = "";
            feld_Geburtsdatum.Size = new Size(203, 27);
            feld_Geburtsdatum.TabIndex = 2;
            feld_Geburtsdatum.TextAlign = HorizontalAlignment.Center;
            feld_Geburtsdatum.Texts = "";
            feld_Geburtsdatum.ToolTip = " Geburtstag";
            feld_Geburtsdatum.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Nachname
            // 
            feld_Nachname.ForeColor = Color.DarkGray;
            feld_Nachname.Location = new Point(12, 60);
            feld_Nachname.Name = "feld_Nachname";
            feld_Nachname.PlaceholderColor = Color.DarkGray;
            feld_Nachname.PlaceholderText = " Nachname";
            feld_Nachname.Size = new Size(370, 27);
            feld_Nachname.TabIndex = 1;
            feld_Nachname.Text = " Nachname";
            feld_Nachname.Texts = "";
            // 
            // feld_Name
            // 
            feld_Name.ForeColor = Color.DarkGray;
            feld_Name.Location = new Point(12, 27);
            feld_Name.Name = "feld_Name";
            feld_Name.PlaceholderColor = Color.DarkGray;
            feld_Name.PlaceholderText = " Vorname";
            feld_Name.Size = new Size(370, 27);
            feld_Name.TabIndex = 0;
            feld_Name.Text = " Vorname";
            feld_Name.Texts = "";
            // 
            // GroupBox_Ansprechpartner
            // 
            GroupBox_Ansprechpartner.BorderColor = Color.DimGray;
            GroupBox_Ansprechpartner.BorderThickness = 1;
            GroupBox_Ansprechpartner.Controls.Add(feld_Handy_V);
            GroupBox_Ansprechpartner.Controls.Add(feld_Name_V);
            GroupBox_Ansprechpartner.Controls.Add(feld_Ort_V);
            GroupBox_Ansprechpartner.Controls.Add(feld_Nachname_V);
            GroupBox_Ansprechpartner.Controls.Add(feld_PLZ_V);
            GroupBox_Ansprechpartner.Controls.Add(feld_Geburtsdatum_V);
            GroupBox_Ansprechpartner.Controls.Add(feld_Srasse_V);
            GroupBox_Ansprechpartner.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            GroupBox_Ansprechpartner.Location = new Point(407, 24);
            GroupBox_Ansprechpartner.Name = "GroupBox_Ansprechpartner";
            GroupBox_Ansprechpartner.Size = new Size(391, 262);
            GroupBox_Ansprechpartner.TabIndex = 1;
            GroupBox_Ansprechpartner.TabStop = false;
            GroupBox_Ansprechpartner.Text = "Gesetzlicher Verteter";
            // 
            // feld_Handy_V
            // 
            feld_Handy_V.ForeColor = Color.DarkGray;
            feld_Handy_V.Location = new Point(13, 192);
            feld_Handy_V.Name = "feld_Handy_V";
            feld_Handy_V.PlaceholderColor = Color.DarkGray;
            feld_Handy_V.PlaceholderText = " Handy";
            feld_Handy_V.Size = new Size(370, 27);
            feld_Handy_V.TabIndex = 16;
            feld_Handy_V.Text = " Handy";
            feld_Handy_V.Texts = "";
            // 
            // feld_Name_V
            // 
            feld_Name_V.ForeColor = Color.DarkGray;
            feld_Name_V.Location = new Point(13, 27);
            feld_Name_V.Name = "feld_Name_V";
            feld_Name_V.PlaceholderColor = Color.DarkGray;
            feld_Name_V.PlaceholderText = " Vorname";
            feld_Name_V.Size = new Size(370, 27);
            feld_Name_V.TabIndex = 10;
            feld_Name_V.Text = " Vorname";
            feld_Name_V.Texts = "";
            // 
            // feld_Ort_V
            // 
            feld_Ort_V.ForeColor = Color.DarkGray;
            feld_Ort_V.Location = new Point(220, 159);
            feld_Ort_V.Name = "feld_Ort_V";
            feld_Ort_V.PlaceholderColor = Color.DarkGray;
            feld_Ort_V.PlaceholderText = " Ort";
            feld_Ort_V.Size = new Size(163, 27);
            feld_Ort_V.TabIndex = 15;
            feld_Ort_V.Text = " Ort";
            feld_Ort_V.Texts = "";
            // 
            // feld_Nachname_V
            // 
            feld_Nachname_V.ForeColor = Color.DarkGray;
            feld_Nachname_V.Location = new Point(13, 60);
            feld_Nachname_V.Name = "feld_Nachname_V";
            feld_Nachname_V.PlaceholderColor = Color.DarkGray;
            feld_Nachname_V.PlaceholderText = " Nachname";
            feld_Nachname_V.Size = new Size(370, 27);
            feld_Nachname_V.TabIndex = 11;
            feld_Nachname_V.Text = " Nachname";
            feld_Nachname_V.Texts = "";
            // 
            // feld_PLZ_V
            // 
            feld_PLZ_V.BackColor = SystemColors.ControlLightLight;
            feld_PLZ_V.ForeColor = Color.DarkGray;
            feld_PLZ_V.Location = new Point(13, 159);
            feld_PLZ_V.Name = "feld_PLZ_V";
            feld_PLZ_V.PlaceholderColor = Color.DarkGray;
            feld_PLZ_V.PlaceholderText = "PLZ";
            feld_PLZ_V.Size = new Size(200, 27);
            feld_PLZ_V.TabIndex = 14;
            feld_PLZ_V.Text = "PLZ";
            feld_PLZ_V.Texts = "";
            // 
            // feld_Geburtsdatum_V
            // 
            feld_Geburtsdatum_V.Location = new Point(13, 93);
            feld_Geburtsdatum_V.Name = "feld_Geburtsdatum_V";
            feld_Geburtsdatum_V.PlaceholderColor = Color.DarkGray;
            feld_Geburtsdatum_V.PlaceholderText = "";
            feld_Geburtsdatum_V.Size = new Size(203, 27);
            feld_Geburtsdatum_V.TabIndex = 12;
            feld_Geburtsdatum_V.TextAlign = HorizontalAlignment.Center;
            feld_Geburtsdatum_V.Texts = "";
            feld_Geburtsdatum_V.ToolTip = "  Geburtstag";
            feld_Geburtsdatum_V.ValidatingType = typeof(System.DateTime);
            // 
            // feld_Srasse_V
            // 
            feld_Srasse_V.ForeColor = Color.DarkGray;
            feld_Srasse_V.Location = new Point(13, 126);
            feld_Srasse_V.Name = "feld_Srasse_V";
            feld_Srasse_V.PlaceholderColor = Color.DarkGray;
            feld_Srasse_V.PlaceholderText = " Straße";
            feld_Srasse_V.Size = new Size(370, 27);
            feld_Srasse_V.TabIndex = 13;
            feld_Srasse_V.Text = " Straße";
            feld_Srasse_V.Texts = "";
            // 
            // myGroupBox2
            // 
            myGroupBox2.BorderColor = Color.DimGray;
            myGroupBox2.BorderThickness = 1;
            myGroupBox2.Controls.Add(label1);
            myGroupBox2.Controls.Add(feld_Anmerkung);
            myGroupBox2.Controls.Add(feld_Vertragsbeginn);
            myGroupBox2.Controls.Add(myGroupBox3);
            myGroupBox2.Controls.Add(myGroupBox1);
            myGroupBox2.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            myGroupBox2.Location = new Point(8, 286);
            myGroupBox2.Name = "myGroupBox2";
            myGroupBox2.Size = new Size(789, 202);
            myGroupBox2.TabIndex = 1;
            myGroupBox2.TabStop = false;
            myGroupBox2.Text = "Mitgliedschaft";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            label1.ForeColor = Color.Coral;
            label1.Location = new Point(579, 42);
            label1.Name = "label1";
            label1.Size = new Size(98, 15);
            label1.TabIndex = 8;
            label1.Text = "(Vertragsbeginn)";
            // 
            // Anmerkung
            // 
            feld_Anmerkung.Location = new Point(386, 109);
            feld_Anmerkung.Name = "Anmerkung";
            feld_Anmerkung.PlaceholderColor = Color.DarkGray;
            feld_Anmerkung.PlaceholderText = " Anmerkung";
            feld_Anmerkung.Size = new Size(396, 82);
            feld_Anmerkung.TabIndex = 7;
            feld_Anmerkung.Text = " Anmerkung";
            feld_Anmerkung.Texts = "";
            // 
            // feld_Vertragsbeginn
            // 
            feld_Vertragsbeginn.Location = new Point(579, 66);
            feld_Vertragsbeginn.Name = "feld_Vertragsbeginn";
            feld_Vertragsbeginn.PlaceholderColor = Color.DarkGray;
            feld_Vertragsbeginn.PlaceholderText = "";
            feld_Vertragsbeginn.Size = new Size(203, 27);
            feld_Vertragsbeginn.TabIndex = 5;
            feld_Vertragsbeginn.TextAlign = HorizontalAlignment.Center;
            feld_Vertragsbeginn.Texts = "";
            feld_Vertragsbeginn.ToolTip = "Vertragsbeginn";
            feld_Vertragsbeginn.ValidatingType = typeof(System.DateTime);
            // 
            // myGroupBox3
            // 
            myGroupBox3.BorderColor = Color.Coral;
            myGroupBox3.BorderThickness = 1;
            myGroupBox3.Controls.Add(checkbox_Kurs_Kinder);
            myGroupBox3.Controls.Add(checkbox_Kurs_Erwachsene);
            myGroupBox3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myGroupBox3.Location = new Point(386, 27);
            myGroupBox3.Name = "myGroupBox3";
            myGroupBox3.Size = new Size(187, 76);
            myGroupBox3.TabIndex = 4;
            myGroupBox3.TabStop = false;
            myGroupBox3.Text = "Kategorie";
            // 
            // checkbox_Kind
            // 
            checkbox_Kurs_Kinder.Location = new Point(16, 20);
            checkbox_Kurs_Kinder.Name = "checkbox_Kind";
            checkbox_Kurs_Kinder.Size = new Size(150, 23);
            checkbox_Kurs_Kinder.TabIndex = 1;
            checkbox_Kurs_Kinder.Text = "Kinder (7-12 Jahre)";
            // 
            // checkbox_Erwachsene
            // 
            checkbox_Kurs_Erwachsene.Location = new Point(16, 47);
            checkbox_Kurs_Erwachsene.Name = "checkbox_Erwachsene";
            checkbox_Kurs_Erwachsene.Size = new Size(171, 23);
            checkbox_Kurs_Erwachsene.TabIndex = 2;
            checkbox_Kurs_Erwachsene.Text = "Erwachsene >12 Jahre";
            // 
            // myGroupBox1
            // 
            myGroupBox1.BorderColor = Color.Coral;
            myGroupBox1.BorderThickness = 1;
            myGroupBox1.Controls.Add(pnlBetrag);
            myGroupBox1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            myGroupBox1.Location = new Point(11, 27);
            myGroupBox1.Name = "myGroupBox1";
            myGroupBox1.Size = new Size(369, 164);
            myGroupBox1.TabIndex = 3;
            myGroupBox1.TabStop = false;
            myGroupBox1.Text = "Kurs";
            // 
            // checkedListKurse
            // 
            checkedListKurse.BackColor = SystemColors.Control;
            checkedListKurse.BorderStyle = BorderStyle.None;
            checkedListKurse.CheckOnClick = true;
            checkedListKurse.Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            checkedListKurse.FormattingEnabled = true;
            checkedListKurse.Location = new Point(3, 3);
            checkedListKurse.MultiColumn = true;
            checkedListKurse.Name = "checkedListKurse";
            checkedListKurse.Size = new Size(219, 132);
            checkedListKurse.TabIndex = 0;
            checkedListKurse.ItemCheck += ClbKurse_ItemCheck;
            // 
            // pnlBetrag
            // 
            pnlBetrag.AutoScroll = true;
            pnlBetrag.Controls.Add(checkedListKurse);
            pnlBetrag.Dock = DockStyle.Fill;
            pnlBetrag.FlowDirection = FlowDirection.TopDown;
            pnlBetrag.Location = new Point(3, 17);
            pnlBetrag.Name = "pnlBetrag";
            pnlBetrag.Size = new Size(363, 144);
            pnlBetrag.TabIndex = 0;
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Vertrag_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            GroupBox_Mitglied.ResumeLayout(false);
            GroupBox_Mitglied.PerformLayout();
            GroupBox_Ansprechpartner.ResumeLayout(false);
            GroupBox_Ansprechpartner.PerformLayout();
            myGroupBox2.ResumeLayout(false);
            myGroupBox2.PerformLayout();
            myGroupBox3.ResumeLayout(false);
            myGroupBox3.PerformLayout();
            myGroupBox1.ResumeLayout(false);
            pnlBetrag.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }
        protected override string _name()
        {
            return "Vertrag erfassen";
        }
        protected override void _OnLoad(EventArgs e)
        {
            GroupBox_Ansprechpartner.Enabled = false;
            feld_Vertragsbeginn.Texts = DateTime.Today.ToString();
            DataTable dt = DataAccessLayer.Query_Kurs();
            checkedListKurse.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                // Tuple oder CustomObject, damit man ID später bekommt
                checkedListKurse.Items.Add(new ListItem
                {
                    ID = (long)row["ID"],
                    BEZ = row["Name"].ToString(),
                    BETRAG = (Decimal)row["BETRAG"]
                });
            }
        }
        protected override bool _Populate()
        {
            bool populate = false;
            // Daten in einem Objekt merken
            Vertrag vertrag = CopieDataFromMask();
            DataTable dt = DataAccessLayer.Query_Vertrag(vertrag);
            if (dt == null || dt.Rows.Count < 1)
            {
                MessageBox.Show("Es wurde keine Rechnung mit diesen " +
                    "Daten gefunden", "Info!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return populate;
            }
            else
            {
                gespeicherteBeträge.Clear();
                string ID = "";
                string Vorname = "";
                string Nachname = "";
                string Geburtsdatum = "";
                string Strasse = "";
                string Plz = "";
                string Ort = "";
                string Handy = "";
                string Geschlecht = "";
                string Minderjaehriger = "";
                string Vertragsbeginn = "";
                string Status = "";
                string Geandert_am = "";
                string Kategorie = "";
                string Vertreter_Vorname = "";
                string Vertreter_Nachname = "";
                string Vertreter_Geburtsdatum = "";
                string Vertreter_Strasse = "";
                string Vertreter_Plz = "";
                string Vertreter_Ort = "";
                string Vertreter_Handy = "";
                string Anmerkung = "";
                string MITGLIED_ID = "";
                if (dt.Rows.Count == 1)
                {
                    DataRow row = dt.Rows[0];
                    ID = row["ID"].ToString();
                    Vorname = row["Vorname"].ToString();
                    Nachname = row["Nachname"].ToString();
                    Geburtsdatum = row["Geburtsdatum"].ToString();
                    Strasse = row["Strasse"].ToString();
                    Plz = row["Plz"].ToString();
                    Ort = row["Ort"].ToString();
                    Handy = row["Handy"].ToString();
                    Geschlecht = row["Geschlecht"].ToString();
                    Minderjaehriger = row["Minderjaehriger"].ToString();
                    Vertragsbeginn = row["Vertragsbeginn"].ToString();
                    Status = row["Status"].ToString();
                    Geandert_am = row["Geandert_am"].ToString();
                    Kategorie = row["Kategorie"].ToString();
                    Vertreter_Vorname = row["Vertreter_Vorname"].ToString();
                    Vertreter_Nachname = row["Vertreter_Nachname"].ToString();
                    Vertreter_Geburtsdatum = row["Vertreter_Geburtsdatum"].ToString();
                    Vertreter_Strasse = row["Vertreter_Strasse"].ToString();
                    Vertreter_Plz = row["Vertreter_Plz"].ToString();
                    Vertreter_Ort = row["Vertreter_Ort"].ToString();
                    Vertreter_Handy = row["Vertreter_Handy"].ToString();
                    Anmerkung = row["Anmerkung"].ToString();
                    MITGLIED_ID = row["MITGLIED_ID"].ToString();

                    DataTable kurs_dt = DataAccessLayer.GetKurseByVertragID(int.Parse(ID));
                    foreach (DataRow row1 in kurs_dt.Rows)
                    {
                        string kursname = row1["NAME"].ToString();
                        decimal betrag = (decimal)row1["BETRAG"];
                        gespeicherteBeträge.Add(kursname, betrag);
                        ListItem listItem = new ListItem();
                        listItem.BETRAG = (decimal)row1["BETRAG"];
                        listItem.BEZ = row1["NAME"].ToString();
                        listItem.ID = (long)row1["ID"];
                        int index = checkedListKurse.Items.IndexOf(listItem);
                        if (index >= 0)
                            checkedListKurse.SetItemChecked(index, true);
                    }

                    populate = true;
                }
                else
                {
                    MyTable myTable = new MyTable(dt);
                    myTable.ShowDialog();
                    if (myTable.DataSelected())
                    {
                        var result = myTable.GetSelectedRow();
                        ID = result.Find(l => l.Key == "ID").Value;
                        Vorname = result.Find(l => l.Key == "Vorname").Value;
                        Nachname = result.Find(l => l.Key == "Nachname").Value;
                        Geburtsdatum = result.Find(l => l.Key == "Geburtsdatum").Value;
                        Strasse = result.Find(l => l.Key == "Strasse").Value;
                        Plz = result.Find(l => l.Key == "Plz").Value;
                        Ort = result.Find(l => l.Key == "Ort").Value;
                        Handy = result.Find(l => l.Key == "Handy").Value;
                        Geschlecht = result.Find(l => l.Key == "Geschlecht").Value;
                        Minderjaehriger = result.Find(l => l.Key == "Minderjaehriger").Value;
                        Vertragsbeginn = result.Find(l => l.Key == "Vertragsbeginn").Value;
                        Status = result.Find(l => l.Key == "Status").Value;
                        Geandert_am = result.Find(l => l.Key == "Geandert_am").Value;
                        Kategorie = result.Find(l => l.Key == "Kategorie").Value;
                        Vertreter_Vorname = result.Find(l => l.Key == "Vertreter_Vorname").Value;
                        Vertreter_Nachname = result.Find(l => l.Key == "Vertreter_Nachname").Value;
                        Vertreter_Geburtsdatum = result.Find(l => l.Key == "Vertreter_Geburtsdatum").Value;
                        Vertreter_Strasse = result.Find(l => l.Key == "Vertreter_Strasse").Value;
                        Vertreter_Plz = result.Find(l => l.Key == "Vertreter_Plz").Value;
                        Vertreter_Ort = result.Find(l => l.Key == "Vertreter_Ort").Value;
                        Vertreter_Handy = result.Find(l => l.Key == "Vertreter_Handy").Value;
                        Anmerkung = result.Find(l => l.Key == "Anmerkung").Value;
                        MITGLIED_ID = result.Find(l => l.Key == "MITGLIED_ID").Value;

                        DataTable kurs_dt = DataAccessLayer.GetKurseByVertragID(int.Parse(ID));
                        foreach (DataRow row1 in kurs_dt.Rows)
                        {
                            string kursname = row1["NAME"].ToString();
                            decimal betrag = (decimal)row1["BETRAG"];
                            gespeicherteBeträge.Add(kursname, betrag);

                            int index = -1;
                            for (int i = 0; i < checkedListKurse.Items.Count; i++)
                            {
                                if (checkedListKurse.Items[i] is ListItem li && li.ID == (long)row1["ID"])
                                {
                                    index = i;
                                    break;
                                }
                            }
                            checkedListKurse.SetItemChecked(index, true);
                        }
                        populate = true;
                    }
                }
                if (populate)
                {
                    feld_id.Texts = ID;
                    feld_Handy_V.Texts = Vertreter_Handy;
                    feld_Handy.Texts = Handy;
                    feld_Name_V.Texts = Vertreter_Vorname;
                    feld_Name.Texts = Vorname;
                    feld_Nachname_V.Texts = Vertreter_Nachname;
                    feld_Nachname.Texts = Nachname;
                    feld_Ort_V.Texts = Vertreter_Ort;
                    feld_Ort.Texts = Ort;
                    feld_Srasse_V.Texts = Vertreter_Strasse;
                    feld_Straße.Texts = Strasse;
                    feld_PLZ_V.Texts = Vertreter_Plz;
                    feld_PLZ.Texts = Plz;
                    feld_Geburtsdatum_V.Texts = Vertreter_Geburtsdatum;
                    feld_Geburtsdatum.Texts = Geburtsdatum;
                    feld_Vertragsbeginn.Texts = Vertragsbeginn;
                    checkbox_Minderjaehrige.Checked = Minderjaehriger.Equals("True") ? true : false;
                    checkbox_Maennlich.Checked = Geschlecht.Equals("M") ? true : false;
                    checkbox_Weiblich.Checked = Geschlecht.Equals("W") ? true : false;
                    checkbox_Kurs_Kinder.Checked = Kategorie.Equals("KIND") ? true : false;
                    checkbox_Kurs_Erwachsene.Checked = Kategorie.Equals("ERWACHSENE") ? true : false;
                    feld_Anmerkung.Texts = Anmerkung;

                    if (string.IsNullOrEmpty(Status))
                    {
                        Status = StatusVertrag.ACTIVE.ToString();
                    }
                    StatusVertrag sv = (StatusVertrag)Enum.Parse(typeof(StatusVertrag), Status);
                    if (sv.Equals(StatusVertrag.ACTIVE))
                    {
                        MenuVertragEnable(true, false, true, true);
                    }
                    else if (sv.Equals(StatusVertrag.PAUSIERT))
                    {
                        MenuVertragEnable(true, true, false, true);
                    }
                    else if (sv.Equals(StatusVertrag.GEKUENDIGT))
                    {
                        MenuVertragEnable(true, true, false, false);
                    }
                }
            }
            return populate;
        }
        protected override bool _PlausibleBevorSave()
        {
            if (string.IsNullOrWhiteSpace(feld_Name.Texts))
            {
                errorProvider.SetError(feld_Name, "Vorname des Mitglieds eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Nachname.Texts))
            {
                errorProvider.SetError(feld_Nachname, "Nachname des Mitglieds eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Geburtsdatum.Texts))
            {
                errorProvider.SetError(feld_Geburtsdatum, "Geburtsdatum des Mitglieds eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Straße.Texts))
            {
                errorProvider.SetError(feld_Straße, "Adresse des Mitgliedseintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_PLZ.Texts))
            {
                errorProvider.SetError(feld_PLZ, "PLZ ist ein Pflichtfeld");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Ort.Texts))
            {
                errorProvider.SetError(feld_Ort, "Ort ist ein Pflichtfeld");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Handy.Texts))
            {
                errorProvider.SetError(feld_Handy, "Handy-Nummer des Mitglieds ist ein Pflichtfeld");
                return false;
            }
            if (!(checkbox_Maennlich.Checked || checkbox_Weiblich.Checked))
            {
                errorProvider.SetError(checkbox_Maennlich, "Geschlecht des Mitglieds ist ein Pflichtfeld");
                return false;
            }
            if (!(checkbox_Kurs_Erwachsene.Checked || checkbox_Kurs_Kinder.Checked))
            {
                errorProvider.SetError(checkbox_Kurs_Erwachsene, "Bitte eine Mitgliedschaft auswählen");
                return false;
            }
            if (!checkbox_Minderjaehrige.Checked)
            {
                return true;
            }
            if (string.IsNullOrWhiteSpace(feld_Name_V.Texts))
            {
                errorProvider.SetError(feld_Name_V, "Vorname des Vertreters eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Nachname_V.Texts))
            {
                errorProvider.SetError(feld_Nachname_V, "Nachname des Vertreters eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Geburtsdatum_V.Texts))
            {
                errorProvider.SetError(feld_Geburtsdatum_V, "Geburtsdatum des Vertreters eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Srasse_V.Texts))
            {
                errorProvider.SetError(feld_Srasse_V, "Adresse des Vertreters eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_PLZ_V.Texts))
            {
                errorProvider.SetError(feld_PLZ_V, "Plz des Vertreters eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Ort_V.Texts))
            {
                errorProvider.SetError(feld_Ort_V, "Ort des Vertreters eintragen");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_Handy_V.Texts))
            {
                errorProvider.SetError(feld_Handy_V, "Handy-Nummer des Vertreters ist ein Pflichtfeld");
                return false;
            }
            var ausgewaehlteKurse = GetSelectedKurseMitBetrag();
            if (ausgewaehlteKurse.Count < 1)
            {
                errorProvider.SetError(checkedListKurse, "Es wurde keinen Kurs ausgewählt!");
                return false;
            }
            return true;
        }
        protected override bool _Save()
        {
            // Daten in einem Objekt merken
            Vertrag vertrag = CopieDataFromMask();
            if (string.IsNullOrEmpty(feld_id.Texts))
            {
                //*** Vertrag in die Datenbank schreiben
                DataAccessLayer.Insert_Vertrag(vertrag);
            }
            else
            {
                DataAccessLayer.Update_Vertrag(vertrag);
            }
            return true;
        }
        protected override bool _Enable_MenuOption(MenuItem menu_Option)
        {
            return true;
        }
        protected override bool _EnableSearchModeMenu()
        {
            return true;
        }
        protected override void _SearchModeEnabled()
        {
            bSearchModeEnabled = true;
            feld_id.Texts = "";
            feld_Vertragsbeginn.Texts = "";
            MenuVertragEnable(false, false, false, false);
        }
        protected override void _SearchModeDisabled()
        {
            bSearchModeEnabled = false;
            feld_id.Texts = "";
            feld_Vertragsbeginn.Texts = DateTime.Now.Date.ToString();
            MenuVertragEnable(false, false, false, false);
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv document)
        {
            document.IdColumn = string.IsNullOrEmpty(feld_id.Texts) ? 0 : long.Parse(feld_id.Texts);
            document.TableName = "VERTRAG";
            return base._DocumentArchivData(document);
        }
        private Vertrag CopieDataFromMask()
        {
            /**Vertreter-Daten**/
            VertreterMitglied vertreter = new VertreterMitglied();
            if (checkbox_Minderjaehrige.Checked)
            {
                vertreter.Vorname = feld_Name_V.Texts;
                vertreter.Nachname = feld_Nachname_V.Texts;
                vertreter.Geburtsdatum = string.IsNullOrEmpty(feld_Geburtsdatum_V.Texts) ? (DateTime?)null : DateTime.Parse(feld_Geburtsdatum_V.Texts);
                vertreter.Strasse = feld_Srasse_V.Texts;
                vertreter.Plz = feld_PLZ_V.Texts;
                vertreter.Ort = feld_Ort_V.Texts;
                vertreter.Handy = feld_Handy_V.Texts;
            }

            /**Mitglied-Daten**/
            Mitglied mitglied = new Mitglied();
            mitglied.Vorname = feld_Name.Texts;
            mitglied.Nachname = feld_Nachname.Texts;
            mitglied.Geburtsdatum = string.IsNullOrEmpty(feld_Geburtsdatum.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Geburtsdatum.Texts);
            mitglied.Strasse = feld_Straße.Texts;
            mitglied.Plz = feld_PLZ.Texts;
            mitglied.Ort = feld_Ort.Texts;
            mitglied.Handy = feld_Handy.Texts;
            if (checkbox_Maennlich.Checked)
            {
                mitglied.Geschlecht = "M";
            }
            else if (checkbox_Weiblich.Checked)
            {
                mitglied.Geschlecht = "W";
            }
            mitglied.Miderjaehrige = checkbox_Minderjaehrige.Checked;
            //***vertreterdaten einfügen
            mitglied.Vertreter = vertreter;

            /**Vertragsdaten**/
            Vertrag vertrag = new Vertrag();
            vertrag.Id = string.IsNullOrEmpty(feld_id.Texts) ? 0 : int.Parse(feld_id.Texts);
            vertrag.Beginn = string.IsNullOrEmpty(feld_Vertragsbeginn.Texts) ?
                (DateTime?)null : DateTime.Parse(feld_Vertragsbeginn.Texts);

            if (checkbox_Kurs_Kinder.Checked)
            {
                vertrag.Kategorie = KategorieVertrag.KIND;
            }
            else if (checkbox_Kurs_Erwachsene.Checked)
            {
                vertrag.Kategorie = KategorieVertrag.ERWACHSENE;
            }
            vertrag.Anmerkung = feld_Anmerkung.Texts;
            /**Mitglied-daten einfüegn**/
            vertrag.Mitglied = mitglied;

            List<Kurs> kurse = new List<Kurs>();
            var ausgewaehlteKurse = GetSelectedKurseMitBetrag();
            foreach (var kvp in ausgewaehlteKurse)
            {
                Kurs kurs = new Kurs();
                kurs.Id = DataAccessLayer.Get_KursIdByName(kvp.Key);
                kurs.Name = kvp.Key;
                kurs.Betrag = int.Parse(kvp.Value.ToString());

                kurse.Add(kurs);
            }

            /**Kursdaten einfügen**/
            vertrag.Kurse = kurse;

            return vertrag;
        }
        private void UpdateBeitragFelder()
        {
            // Entferne alte Felder, aber nicht die CheckedListBox
            foreach (Control c in pnlBetrag.Controls.Cast<Control>().Where(c => c.Tag != null && c.Tag.ToString().StartsWith("BEITRAG_")).ToList())
                pnlBetrag.Controls.Remove(c);

            int itemHeight = checkedListKurse.ItemHeight;

            for (int i = 0; i < checkedListKurse.Items.Count; i++)
            {
                if (checkedListKurse.GetItemChecked(i))
                {
                    string kurs = checkedListKurse.Items[i].ToString();
                    Decimal betrag = 0;
                    if (checkedListKurse.Items[i] is ListItem li)
                    {
                        betrag = li.BETRAG;
                    }
                    // Falls gespeicherte Werte vorhanden sind, überschreiben
                    if (gespeicherteBeträge != null && gespeicherteBeträge.ContainsKey(kurs))
                        betrag = gespeicherteBeträge[kurs];

                    // Label
                    var lbl = new Label
                    {
                        Text = kurs + "-Betrag",
                        //Width = 80,
                        AutoSize = true,
                        Height = itemHeight,
                        //TextAlign = ContentAlignment.MiddleRight,
                        Left = checkedListKurse.Right + 5,
                        Top = i * itemHeight,
                        Tag = "BEITRAG_" + kurs
                    };


                    var txt = new TextBox
                    {
                        Text = betrag.ToString(),
                        Width = 100,
                        Height = itemHeight - 4,
                        Left = lbl.Right + 5,
                        Top = i * itemHeight + 2,
                        Tag = "BEITRAG_" + kurs
                    };

                    if (!User.Rechte.BEITRAG_AENDERN())
                        txt.Enabled = false;

                    pnlBetrag.Controls.Add(lbl);
                    pnlBetrag.Controls.Add(txt);
                }
            }
        }
        // EventHandler für ItemCheck
        private void ClbKurse_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Wir warten, bis der Event abgeschlossen ist, daher Delay:
            BeginInvoke((MethodInvoker)delegate { UpdateBeitragFelder(); });
        }
        private Dictionary<string, decimal> GetSelectedKurseMitBetrag()
        {
            var result = new Dictionary<string, decimal>();

            foreach (var txtBox in pnlBetrag.Controls.OfType<TextBox>())
            {
                string tag = txtBox.Tag?.ToString();
                if (tag == null || !tag.StartsWith("BEITRAG_")) continue;

                string kurs = tag.Substring("BEITRAG_".Length);

                if (decimal.TryParse(txtBox.Text, out decimal betrag))
                    result[kurs] = betrag;
                else
                    result[kurs] = 0;
            }

            return result;
        }
        private void Checkbox_Minderjaehrige_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkbox_Minderjaehrige.Checked)
            {
                GroupBox_Ansprechpartner.Enabled = true;
            }
            else
            {
                GroupBox_Ansprechpartner.Enabled = false;
            }
        }
        private void vertrag_loeschen(object sender, EventArgs e)
        {
            if (!User.Rechte.VERTRAG_LOESCHEN())
            {
                MessageBox.Show("Keine Berechtigung den Vertrag zu löschen-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result = MessageBox.Show("Vertrag endgültig löschen?", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.Yes))
            {
                Vertrag vertrag = CopieDataFromMask();
                try
                {
                    DataAccessLayer.Delete_Vertrag(vertrag);
                    MessageBox.Show("Vertrag mit der ID " + vertrag.Id + " wurde 'gelöscht'");

                    //Log
                    Log.Information("Vertrag wurde gelöscht -> \n" + JsonConvert.SerializeObject(vertrag) + "" +
                        "\n Angemeldet-> " + User.Anmeldename + "Name: " + User.Vorname + "Nachname: " + User.Nachname);

                    item_SearchMode.PerformClick();
                }
                catch (Exception exp)
                {
                    Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
                }
            }
        }
        private void Vertrag_pausieren(object sender, EventArgs e)
        {
            if (!User.Rechte.VERTRAG_PAUSIEREN())
            {
                MessageBox.Show("Keine Berechtigung den Vertrag zu pausieren-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result = MessageBox.Show("Vertrag pausieren?", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.Yes))
            {
                Vertrag vertrag = CopieDataFromMask();
                try
                {
                    vertrag.Status = StatusVertrag.PAUSIERT;
                    vertrag.Status_geaendert_am = DateTime.Now.Date;
                    DataAccessLayer.Update_StatusVertrag(vertrag);
                    MessageBox.Show("Vertrag mit der ID " + vertrag.Id + " wurde 'pausiert'");
                    //Log
                    Log.Information("Vertrag wurde pausiet -> \n" + JsonConvert.SerializeObject(vertrag) + "" +
                        "\n Angemeldet-> " + User.Anmeldename + "Name: " + User.Vorname + "Nachname: " + User.Nachname);
                }
                catch (Exception exp)
                {
                    Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
                }
            }
        }
        private void Vertrag_Kuendigen(object sender, EventArgs e)
        {
            if (!User.Rechte.VERTRAG_PAUSIEREN())
            {
                MessageBox.Show("Keine Berechtigung den Vertrag zu kündigen-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result = MessageBox.Show("Vertrag kündigen?", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.Yes))
            {
                Vertrag vertrag = CopieDataFromMask();
                try
                {
                    vertrag.Status = StatusVertrag.GEKUENDIGT;
                    vertrag.Status_geaendert_am = DateTime.Now.Date;
                    DataAccessLayer.Update_StatusVertrag(vertrag);
                    MessageBox.Show("Vertrag mit der ID " + vertrag.Id + " wurde 'gekündigt'");
                    //Log
                    Log.Information("Vertrag wurde gekündigt -> \n" + JsonConvert.SerializeObject(vertrag) + "" +
                        "\n Angemeldet-> " + User.Anmeldename + "Name: " + User.Vorname + "Nachname: " + User.Nachname);
                }
                catch (Exception exp)
                {
                    Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
                }
            }
        }
        private void vertrag_reaktivieren(object sender, System.EventArgs e)
        {
            if (!User.Rechte.VERTRAG_REAKTIVIEREN())
            {
                MessageBox.Show("Keine Berechtigung den Vertrag zu aktivieren-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result = MessageBox.Show("Vertrag reaktivieren?", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result.Equals(DialogResult.Yes))
            {
                Vertrag vertrag = CopieDataFromMask();
                try
                {
                    vertrag.Status = StatusVertrag.ACTIVE;
                    vertrag.Status_geaendert_am = DateTime.Now.Date;
                    DataAccessLayer.Update_StatusVertrag(vertrag);
                    MessageBox.Show("Vertrag mit der ID " + vertrag.Id + " wurde 'aktiviert'");

                    //Log
                    Log.Information("Vertrag wurde reaktiviert -> \n" + JsonConvert.SerializeObject(vertrag) + "" +
                        "\n Angemeldet-> " + User.Anmeldename + "Name: " + User.Vorname + "Nachname: " + User.Nachname);
                }
                catch (Exception exp)
                {
                    Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
                }
            }
        }
        private void MenuVertragEnable(bool loeschen, bool aktivieren, bool pausieren, bool kuendigen)
        {
            item_loeschen.Enabled = loeschen;
            item_aktivieren.Enabled = aktivieren;
            item_pausieren.Enabled = pausieren;
            item_kuendigen.Enabled = kuendigen;
        }
    }



    // Hilfsklasse für Anzeige
    public class ListItem
    {
        public long ID { get; set; }
        public string BEZ { get; set; }
        public Decimal BETRAG { get; set; }

        public override string ToString()
        {
            return BEZ; // wird in CheckedListBox angezeigt
        }
    }
}