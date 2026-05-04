
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            //***Menu****//
            mainMenu = new System.Windows.Forms.MainMenu();
            menu_Option = new MenuItem();
            menu_Kurs = new MenuItem();
            menu_Einstellungen = new MenuItem();
            menu_Firma = new MenuItem();
            menu_Formular = new MenuItem();
            menu_Konfiguration = new MenuItem();
            label1 = new Label();
            statusStrip = new StatusStrip();
            statusStrip.SuspendLayout();
            SuspendLayout();

            // Set the caption of the menu items.
            menu_Option.Text = "&Option";
            menu_Option.Shortcut = Shortcut.CtrlO;
            menu_Option.ShowShortcut = true;
            menu_Option.BarBreak = false;
            menu_Option.PerformClick();
            menu_Option.Visible = Enable_MenuOption(menu_Option);
            /**/
            menu_Kurs.Text = "&Kurs/Preise";
            menu_Kurs.Shortcut = Shortcut.CtrlK;
            menu_Kurs.ShowShortcut = true;
            menu_Kurs.BarBreak = false;
            menu_Kurs.Visible = Enable_MenuKurs(menu_Kurs);
            /**/
            menu_Einstellungen.Text = "&Einstellungen";
            menu_Einstellungen.Shortcut = Shortcut.CtrlE;
            menu_Einstellungen.ShowShortcut = true;
            menu_Einstellungen.BarBreak = false;
            menu_Einstellungen.Visible = Enable_MenuEinstellungen(menu_Einstellungen);
            /**/
            menu_Firma.Text = "&Firma";
            menu_Firma.Shortcut = Shortcut.CtrlF;
            menu_Firma.ShowShortcut = true;
            menu_Firma.BarBreak = false;
            menu_Firma.Visible = Enable_MenuFirma(menu_Firma);
            /**/
            menu_Formular.Text = "&Formular";
            menu_Formular.Shortcut = Shortcut.CtrlR;
            menu_Formular.ShowShortcut = true;
            menu_Formular.BarBreak = false;
            menu_Formular.Visible = Enable_MenuFormular(menu_Formular);
            /**/
            menu_Konfiguration.Text = "&Konfiguration";
            menu_Konfiguration.Shortcut = Shortcut.CtrlG;
            menu_Konfiguration.ShowShortcut = true;
            menu_Konfiguration.BarBreak = false;
            menu_Konfiguration.Visible = Enable_MenuKonfiguration(menu_Konfiguration);
            /**/
            mainMenu.MenuItems.Add(menu_Option);
            mainMenu.MenuItems.Add(menu_Kurs);
            mainMenu.MenuItems.Add(menu_Konfiguration);
            mainMenu.MenuItems.Add(menu_Einstellungen);
            mainMenu.MenuItems.Add(menu_Formular);
            mainMenu.MenuItems.Add(menu_Firma);

            // Assign mainMenu1 to the form.
            Menu = mainMenu;



            this.pMyLogo = new PictureBox();
            this.pbSpeichern = new myPbSave();
            this.pbAbbrechen = new myPbClose();
            this.tFirmenname = new Label();
            this.tAddresse = new Label();
            this.tWebseite = new MyWebseite();
            this.tVerbindung = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.pMyLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pMyLogo
            // 
            this.pMyLogo.Image = Properties.Resources.toro;
            this.pMyLogo.Location = new System.Drawing.Point(11, 2);
            this.pMyLogo.Name = "pMyLogo";
            this.pMyLogo.Size = new System.Drawing.Size(78, 83);
            this.pMyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pMyLogo.TabIndex = 20;
            this.pMyLogo.TabStop = false;
            // 
            // pbSpeichern
            // 
            this.pbSpeichern.Location = new System.Drawing.Point(325, 12);
            pbSpeichern.Enabled = false;
            // 
            // pbAbbrechen
            // 
            this.pbAbbrechen.Location = new System.Drawing.Point(512, 12);
            // 
            // tFirmenname
            // 
            this.tFirmenname.AutoSize = true;
            this.tFirmenname.BackColor = System.Drawing.Color.Transparent;
            this.tFirmenname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tFirmenname.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.tFirmenname.ForeColor = System.Drawing.Color.Red;
            this.tFirmenname.Location = new System.Drawing.Point(99, 12);
            this.tFirmenname.Name = "tFirmenname";
            this.tFirmenname.Size = new System.Drawing.Size(203, 26);
            this.tFirmenname.TabIndex = 21;
            this.tFirmenname.Text = Firma.Name;
            // 
            // tAddresse
            // 
            this.tAddresse.BackColor = System.Drawing.Color.Transparent;
            this.tAddresse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tAddresse.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.tAddresse.ForeColor = System.Drawing.Color.DimGray;
            this.tAddresse.Location = new System.Drawing.Point(98, 41);
            this.tAddresse.Name = "tAddresse";
            this.tAddresse.Size = new System.Drawing.Size(204, 38);
            this.tAddresse.TabIndex = 33;
            this.tAddresse.Text = Firma.Adresse;
            // 
            // tWebseite
            //
            this.tWebseite.Location = new System.Drawing.Point(96, 82);
            //
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(688, 110);
            this.label1.TabIndex = 44;
            // 
            // tVerbindung
            // 
            this.tVerbindung.Font = new System.Drawing.Font("Arial", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tVerbindung.Location = new System.Drawing.Point(12, 86);
            this.tVerbindung.Name = "tVerbindung";
            this.tVerbindung.Size = new System.Drawing.Size(77, 15);
            this.tVerbindung.TabIndex = 40;
            this.tVerbindung.Text = "Verbindung";
            this.tVerbindung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // statusStrip1
            // 
            statusStrip.Name = "statusStrip";
            statusStrip.Text = "statusStrip";
            statusStrip.BringToFront();
            statusStrip.Dock = DockStyle.None;
            statusStrip.Location = new System.Drawing.Point(0, 370);
            statusStrip.Size = new Size(698, 22);

            lblMitarbeiter = new ToolStripStatusLabel("  eingeloggt: " + Anmeldung.Vorname + " " + Anmeldung.Nachname);
            lblSpacer1 = new ToolStripStatusLabel { Spring = true };
            lblDatenbank = new ToolStripStatusLabel("Datenbank: " +Database.DatabaseName);
            lblSpacer2 = new ToolStripStatusLabel { Spring = true };
            lblTime = new ToolStripStatusLabel();

            lblMitarbeiter.ForeColor = Color.Gray;
            lblDatenbank.ForeColor = Color.Gray;
            lblTime.ForeColor = Color.Gray;

            statusStrip.Items.Add(lblDatenbank);
            statusStrip.Items.Add(lblSpacer1);
            statusStrip.Items.Add(lblMitarbeiter);
            statusStrip.Items.Add(lblSpacer2);
            statusStrip.Items.Add(lblTime);
            
            // Timer für Uhrzeit
            timer = new Timer();
            timer.Interval = 1000; // 1 Sekunde
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(null, null); // initiale Anzeige
            // 
            // MyFightClubChemnitz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.pbAbbrechen;
            this.ShowIcon = true;
            this.ClientSize = new System.Drawing.Size(698, 395);
            this.Controls.Add(this.tVerbindung);
            this.Controls.Add(this.pMyLogo);
            this.Controls.Add(this.pbSpeichern);
            this.Controls.Add(this.pbAbbrechen);
            this.Controls.Add(this.tFirmenname);
            this.Controls.Add(this.tAddresse);
            this.Controls.Add(this.tWebseite);
            this.Controls.Add(label1);
            this.Controls.Add(statusStrip);
            //this.Controls.Add(MyGroupBox);
            ImeMode = ImeMode.Off;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = _name();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = _name();
            this.Load += new System.EventHandler(this._Load);
            Icon = Properties.Resources.Logo_ico;
            ((System.ComponentModel.ISupportInitialize)(this.pMyLogo)).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        // Create empty menu item objects.
        System.Windows.Forms.MainMenu mainMenu;
        MenuItem menu_Option;
        MenuItem menu_Kurs;
        MenuItem menu_Einstellungen;
        MenuItem menu_Firma;
        MenuItem menu_Formular;
        MenuItem menu_Konfiguration;
        private Label tFirmenname;
        private Label tAddresse;
        private MyWebseite tWebseite;
        private PictureBox pMyLogo;
        private myPbSave pbSpeichern;
        private myPbClose pbAbbrechen;
        private System.Windows.Forms.Label tVerbindung;
        private Label label1;
        public StatusStrip statusStrip;
        private ToolStripStatusLabel lblMitarbeiter;
        private ToolStripStatusLabel lblSpacer1;
        private ToolStripStatusLabel lblDatenbank;
        private ToolStripStatusLabel lblSpacer2;
        private ToolStripStatusLabel lblTime;
        private Timer timer;
    }

}
