using MyControls;
using Serilog;
using System;

namespace FCC_Verwaltungssystem
{
    public class Maske_Stammdaten : MyForm
    {
        private MyGroupBox myGroupBox2;
        private MyFieldText feld_BIC;
        private MyFieldText feld_IBAN;
        private MyFieldText feld_Kontoinhaber;
        private MyFieldText feld_Bankbezeichnung;
        private MyGroupBox myGroupBox1;
        private MyFieldText feld_Tel;
        private MyFieldText feld_UmsatzsteuerID;
        private MyFieldText feld_Adresse;
        private MyFieldText feld_Geschaeftsfuehrer;
        private MyFieldText feld_Email;
        private MyFieldText feld_Webseite;
        private MyFieldText feld_Name;

        protected override string _name()
        {
            return "Stammdaten";
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv dokument)
        {
            dokument.IdColumn = User.ID;
            dokument.TableName = "STAMMDATEN";

            return dokument;
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            feld_Name.Texts = Firma.Name;
            feld_Adresse.Texts = Firma.Adresse;
            feld_Geschaeftsfuehrer.Texts = Firma.G_Fuehrer;
            feld_Webseite.Texts = Firma.Webseite;
            feld_Email.Texts = Firma.Mail;
            feld_Tel.Texts = Firma.Tel;
            feld_UmsatzsteuerID.Texts = Firma.UmsatzsteuerID;
            feld_Bankbezeichnung.Texts = Firma.Bankbezeichnung;
            feld_Kontoinhaber.Texts = Firma.Kontoinhaber;
            feld_IBAN.Texts = Firma.Iban;
            feld_BIC.Texts = Firma.Bic;

            Focus();
            //Log
            Log.Information("Stammdaten geladen -> \n" + feld_Name.Texts + "\n" + feld_Adresse.Texts + "\n" +
                feld_Geschaeftsfuehrer.Texts + "\n" + feld_Webseite.Texts + "\n" + feld_Email.Texts + "\n" +
                feld_Tel.Texts + "\n" + feld_UmsatzsteuerID.Texts + "\n" + feld_Bankbezeichnung.Texts + "\n" +
                feld_Kontoinhaber.Texts + "\n" + feld_IBAN.Texts + "\n" + feld_BIC.Texts);
            return false;
        }
        protected override bool _Save()
        {
            Firma.Name = feld_Name.Texts;
            Firma.Adresse = feld_Adresse.Texts;
            Firma.G_Fuehrer = feld_Geschaeftsfuehrer.Texts;
            Firma.Webseite = feld_Webseite.Texts;
            Firma.Mail = feld_Email.Texts;
            Firma.Tel = feld_Tel.Texts;
            Firma.UmsatzsteuerID = feld_UmsatzsteuerID.Texts;
            Firma.Bankbezeichnung = feld_Bankbezeichnung.Texts;
            Firma.Kontoinhaber = feld_Kontoinhaber.Texts;
            Firma.Iban = feld_IBAN.Texts;
            Firma.Bic = feld_BIC.Texts;

            DataAccessLayer.Update_FirmaStammdaten();

            //Log
            Log.Information("Stammdaten aktualisiert -> \n" + feld_Name.Texts + "\n" + feld_Adresse.Texts + "\n" +
                feld_Geschaeftsfuehrer.Texts + "\n" + feld_Webseite.Texts + "\n" + feld_Email.Texts + "\n" +
                feld_Tel.Texts + "\n" + feld_UmsatzsteuerID.Texts + "\n" + feld_Bankbezeichnung.Texts + "\n" +
                feld_Kontoinhaber.Texts + "\n" + feld_IBAN.Texts + "\n" + feld_BIC.Texts);

            return _Populate();
        }
        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_Stammdaten));
            this.feld_Name = new MyControls.MyFieldText();
            this.feld_Adresse = new MyControls.MyFieldText();
            this.feld_Geschaeftsfuehrer = new MyControls.MyFieldText();
            this.feld_Webseite = new MyControls.MyFieldText();
            this.feld_Email = new MyControls.MyFieldText();
            this.feld_UmsatzsteuerID = new MyControls.MyFieldText();
            this.feld_Tel = new MyControls.MyFieldText();
            this.myGroupBox1 = new MyControls.MyGroupBox();
            this.myGroupBox2 = new MyControls.MyGroupBox();
            this.feld_BIC = new MyControls.MyFieldText();
            this.feld_IBAN = new MyControls.MyFieldText();
            this.feld_Kontoinhaber = new MyControls.MyFieldText();
            this.feld_Bankbezeichnung = new MyControls.MyFieldText();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            this.myGroupBox2.SuspendLayout();
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
            this.BorderBody.Controls.Add(this.myGroupBox2);
            this.BorderBody.Controls.Add(this.myGroupBox1);
            // 
            // feld_Name
            // 
            this.feld_Name.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Name.Location = new System.Drawing.Point(9, 32);
            this.feld_Name.Name = "feld_Name";
            this.feld_Name.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Name.PlaceholderText = "Firmenname";
            this.feld_Name.Size = new System.Drawing.Size(763, 27);
            this.feld_Name.TabIndex = 0;
            this.feld_Name.Text = "Firmenname";
            this.feld_Name.Texts = "";
            // 
            // feld_Adresse
            // 
            this.feld_Adresse.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Adresse.Location = new System.Drawing.Point(9, 69);
            this.feld_Adresse.Name = "feld_Adresse";
            this.feld_Adresse.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Adresse.PlaceholderText = "Adresse";
            this.feld_Adresse.Size = new System.Drawing.Size(763, 27);
            this.feld_Adresse.TabIndex = 1;
            this.feld_Adresse.Text = "Adresse";
            this.feld_Adresse.Texts = "";
            // 
            // feld_Geschaeftsfuehrer
            // 
            this.feld_Geschaeftsfuehrer.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Geschaeftsfuehrer.Location = new System.Drawing.Point(9, 104);
            this.feld_Geschaeftsfuehrer.Name = "feld_Geschaeftsfuehrer";
            this.feld_Geschaeftsfuehrer.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Geschaeftsfuehrer.PlaceholderText = "Geschäftsführer(n)";
            this.feld_Geschaeftsfuehrer.Size = new System.Drawing.Size(763, 27);
            this.feld_Geschaeftsfuehrer.TabIndex = 2;
            this.feld_Geschaeftsfuehrer.Text = "Geschäftsführer(n)";
            this.feld_Geschaeftsfuehrer.Texts = "";
            // 
            // feld_Webseite
            // 
            this.feld_Webseite.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Webseite.Location = new System.Drawing.Point(9, 137);
            this.feld_Webseite.Name = "feld_Webseite";
            this.feld_Webseite.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Webseite.PlaceholderText = "Webseite";
            this.feld_Webseite.Size = new System.Drawing.Size(389, 27);
            this.feld_Webseite.TabIndex = 4;
            this.feld_Webseite.Text = "Webseite";
            this.feld_Webseite.Texts = "";
            // 
            // feld_Email
            // 
            this.feld_Email.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Email.Location = new System.Drawing.Point(9, 174);
            this.feld_Email.Name = "feld_Email";
            this.feld_Email.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Email.PlaceholderText = "E-Mail";
            this.feld_Email.Size = new System.Drawing.Size(389, 27);
            this.feld_Email.TabIndex = 5;
            this.feld_Email.Text = "E-Mail";
            this.feld_Email.Texts = "";
            // 
            // feld_UmsatzsteuerID
            // 
            this.feld_UmsatzsteuerID.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_UmsatzsteuerID.Location = new System.Drawing.Point(9, 244);
            this.feld_UmsatzsteuerID.Name = "feld_UmsatzsteuerID";
            this.feld_UmsatzsteuerID.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_UmsatzsteuerID.PlaceholderText = "UmsatzsteuerID";
            this.feld_UmsatzsteuerID.Size = new System.Drawing.Size(389, 27);
            this.feld_UmsatzsteuerID.TabIndex = 7;
            this.feld_UmsatzsteuerID.Text = "UmsatzsteuerID";
            this.feld_UmsatzsteuerID.Texts = "";
            // 
            // feld_Tel
            // 
            this.feld_Tel.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Tel.Location = new System.Drawing.Point(9, 207);
            this.feld_Tel.Name = "feld_Tel";
            this.feld_Tel.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Tel.PlaceholderText = "Tel";
            this.feld_Tel.Size = new System.Drawing.Size(389, 27);
            this.feld_Tel.TabIndex = 6;
            this.feld_Tel.Text = "Tel";
            this.feld_Tel.Texts = "";
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox1.BorderThickness = 1;
            this.myGroupBox1.Controls.Add(this.feld_Name);
            this.myGroupBox1.Controls.Add(this.feld_Tel);
            this.myGroupBox1.Controls.Add(this.feld_UmsatzsteuerID);
            this.myGroupBox1.Controls.Add(this.feld_Adresse);
            this.myGroupBox1.Controls.Add(this.feld_Geschaeftsfuehrer);
            this.myGroupBox1.Controls.Add(this.feld_Email);
            this.myGroupBox1.Controls.Add(this.feld_Webseite);
            this.myGroupBox1.Location = new System.Drawing.Point(14, 19);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(782, 289);
            this.myGroupBox1.TabIndex = 8;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "Anschrift";
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox2.BorderThickness = 1;
            this.myGroupBox2.Controls.Add(this.feld_BIC);
            this.myGroupBox2.Controls.Add(this.feld_IBAN);
            this.myGroupBox2.Controls.Add(this.feld_Kontoinhaber);
            this.myGroupBox2.Controls.Add(this.feld_Bankbezeichnung);
            this.myGroupBox2.Location = new System.Drawing.Point(14, 314);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Size = new System.Drawing.Size(782, 165);
            this.myGroupBox2.TabIndex = 9;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "Finanzen";
            // 
            // feld_BIC
            // 
            this.feld_BIC.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_BIC.Location = new System.Drawing.Point(9, 122);
            this.feld_BIC.Name = "feld_BIC";
            this.feld_BIC.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_BIC.PlaceholderText = "BIC";
            this.feld_BIC.Size = new System.Drawing.Size(385, 27);
            this.feld_BIC.TabIndex = 11;
            this.feld_BIC.Text = "BIC";
            this.feld_BIC.Texts = "";
            // 
            // feld_IBAN
            // 
            this.feld_IBAN.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_IBAN.Location = new System.Drawing.Point(9, 87);
            this.feld_IBAN.Name = "feld_IBAN";
            this.feld_IBAN.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_IBAN.PlaceholderText = "IBAN";
            this.feld_IBAN.Size = new System.Drawing.Size(385, 27);
            this.feld_IBAN.TabIndex = 10;
            this.feld_IBAN.Text = "IBAN";
            this.feld_IBAN.Texts = "";
            // 
            // feld_Kontoinhaber
            // 
            this.feld_Kontoinhaber.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Kontoinhaber.Location = new System.Drawing.Point(9, 52);
            this.feld_Kontoinhaber.Name = "feld_Kontoinhaber";
            this.feld_Kontoinhaber.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Kontoinhaber.PlaceholderText = "Kontoinhaber";
            this.feld_Kontoinhaber.Size = new System.Drawing.Size(385, 27);
            this.feld_Kontoinhaber.TabIndex = 9;
            this.feld_Kontoinhaber.Text = "Kontoinhaber";
            this.feld_Kontoinhaber.Texts = "";
            // 
            // feld_Bankbezeichnung
            // 
            this.feld_Bankbezeichnung.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Bankbezeichnung.Location = new System.Drawing.Point(9, 19);
            this.feld_Bankbezeichnung.Name = "feld_Bankbezeichnung";
            this.feld_Bankbezeichnung.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Bankbezeichnung.PlaceholderText = "Bankbezeichnung";
            this.feld_Bankbezeichnung.Size = new System.Drawing.Size(385, 27);
            this.feld_Bankbezeichnung.TabIndex = 8;
            this.feld_Bankbezeichnung.Text = "Bankbezeichnung";
            this.feld_Bankbezeichnung.Texts = "";
            this.BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Stammdaten_ico;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            this.myGroupBox2.ResumeLayout(false);
            this.myGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}