using MyControls;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_KonfigKalendar : MyForm
    {
        private MyLabel myLabel1;
        private MyGroupBox myGroupBox2;
        private MyDuseCheckBox checkBox_islamische_F;
        private MyDuseCheckBox checkBox_Feiertage_in_D;
        private MyDuseCheckBox checkBox_geloeschte_Termine;
        private MyGroupBox myGroupBox1;
        private MyFieldText feld_SecretClient;
        private MyPushButton pbOpenFileD;
        private MyFieldText feld_Mail;

        protected override string _name()
        {
            return "Kalendar-Einstellungen";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            //*** query Daten anhand der User_ID in User-Klasse (static)
            DataTable dataTable = DataAccessLayer.queryKalenderEinstellungen();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                feld_Mail.Texts = (string)row["EMAIL"];
                feld_SecretClient.Texts = (string)row["SECRET_PATH"];
                checkBox_geloeschte_Termine.Checked = (bool)row["GELOESCHTE_TERMINE"];
                checkBox_Feiertage_in_D.Checked = (bool)row["FEIERTAGE"];
                checkBox_islamische_F.Checked = (bool)row["ISLAMISCHE_FEIERTAGE"];
            }
            return true;
        }
        protected override bool _PlausibleBevorSave()
        {

            if (string.IsNullOrWhiteSpace(feld_Mail.Texts))
            {
                errorProvider.SetError(feld_Mail, "Die E-Mail darf nicht leer sein!");
                return false;
            }

            if (!CheckValidate.ValidateEmail(feld_Mail.Texts))
            {
                errorProvider.SetError(feld_Mail, "Dies ist keine gültige EMail adresse!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_SecretClient.Texts))
            {
                errorProvider.SetError(feld_SecretClient, "Das Passwort darf nicht leer sein!");
                return false;
            }
            return true;
        }
        protected override bool _Save()
        {
            string mail = feld_Mail.Texts;
            string path = feld_SecretClient.Texts;
            bool geloeschte_termine = checkBox_geloeschte_Termine.Checked;
            bool feiertage_d = checkBox_Feiertage_in_D.Checked;
            bool i_feiertage = checkBox_islamische_F.Checked;

            DataAccessLayer.Update_KalendarEinstellungen(mail, path, geloeschte_termine, feiertage_d, i_feiertage);
            return true;
        }
        protected override void _AfterSave()
        {
            _Populate();
        }
        private void pbOpenFileD_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Json files (*.json)|*.json";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    feld_SecretClient.Text = openFileDialog.FileName;
                }
            }
        }
        protected override void _InitializeComponent()
        {
            this.myGroupBox1 = new MyControls.MyGroupBox();
            this.feld_Mail = new MyControls.MyFieldText();
            this.feld_SecretClient = new MyControls.MyFieldText();
            this.myGroupBox2 = new MyControls.MyGroupBox();
            this.myLabel1 = new MyControls.MyLabel();
            this.checkBox_geloeschte_Termine = new MyControls.MyDuseCheckBox();
            this.checkBox_Feiertage_in_D = new MyControls.MyDuseCheckBox();
            this.checkBox_islamische_F = new MyControls.MyDuseCheckBox();
            this.pbOpenFileD = new MyControls.MyPushButton();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            this.myGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.myLabel1);
            this.BorderBody.Controls.Add(this.myGroupBox2);
            this.BorderBody.Controls.Add(this.myGroupBox1);
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox1.BorderThickness = 1;
            this.myGroupBox1.Controls.Add(this.pbOpenFileD);
            this.myGroupBox1.Controls.Add(this.feld_SecretClient);
            this.myGroupBox1.Controls.Add(this.feld_Mail);
            this.myGroupBox1.Location = new System.Drawing.Point(8, 19);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(788, 109);
            this.myGroupBox1.TabIndex = 0;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "Zugangsdaten";
            // 
            // feld_Mail
            // 
            this.feld_Mail.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Mail.Location = new System.Drawing.Point(6, 24);
            this.feld_Mail.Name = "feld_Mail";
            this.feld_Mail.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Mail.PlaceholderText = "E-Mail";
            this.feld_Mail.Size = new System.Drawing.Size(776, 27);
            this.feld_Mail.TabIndex = 0;
            this.feld_Mail.Text = "E-Mail";
            this.feld_Mail.Texts = "";
            // 
            // feld_SecretClient
            // 
            this.feld_SecretClient.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_SecretClient.Location = new System.Drawing.Point(6, 57);
            this.feld_SecretClient.Name = "feld_SecretClient";
            this.feld_SecretClient.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_SecretClient.PlaceholderText = "Client-Secret (.JSON)";
            this.feld_SecretClient.Size = new System.Drawing.Size(776, 27);
            this.feld_SecretClient.TabIndex = 1;
            this.feld_SecretClient.Text = "Client-Secret (.JSON)";
            this.feld_SecretClient.Texts = "";
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox2.BorderThickness = 1;
            this.myGroupBox2.Controls.Add(this.checkBox_islamische_F);
            this.myGroupBox2.Controls.Add(this.checkBox_Feiertage_in_D);
            this.myGroupBox2.Controls.Add(this.checkBox_geloeschte_Termine);
            this.myGroupBox2.Location = new System.Drawing.Point(8, 163);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Size = new System.Drawing.Size(206, 108);
            this.myGroupBox2.TabIndex = 1;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "Termine";
            // 
            // myLabel1
            // 
            this.myLabel1.AutoSize = true;
            this.myLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel1.ForeColor = System.Drawing.Color.CadetBlue;
            this.myLabel1.Location = new System.Drawing.Point(4, 137);
            this.myLabel1.Name = "myLabel1";
            this.myLabel1.Size = new System.Drawing.Size(68, 19);
            this.myLabel1.TabIndex = 2;
            this.myLabel1.Text = "Ansicht...";
            this.myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_geloeschte_Termine
            // 
            this.checkBox_geloeschte_Termine.Location = new System.Drawing.Point(15, 19);
            this.checkBox_geloeschte_Termine.Name = "checkBox_geloeschte_Termine";
            this.checkBox_geloeschte_Termine.Size = new System.Drawing.Size(147, 23);
            this.checkBox_geloeschte_Termine.TabIndex = 0;
            this.checkBox_geloeschte_Termine.Text = "gelöschte Termine";
            // 
            // checkBox_Feiertage_in_D
            // 
            this.checkBox_Feiertage_in_D.Location = new System.Drawing.Point(15, 77);
            this.checkBox_Feiertage_in_D.Name = "checkBox_Feiertage_in_D";
            this.checkBox_Feiertage_in_D.Size = new System.Drawing.Size(189, 23);
            this.checkBox_Feiertage_in_D.TabIndex = 1;
            this.checkBox_Feiertage_in_D.Text = "Feiertage in Deutschland";
            // 
            // checkBox_islamische_F
            // 
            this.checkBox_islamische_F.Location = new System.Drawing.Point(15, 48);
            this.checkBox_islamische_F.Name = "checkBox_islamische_F";
            this.checkBox_islamische_F.Size = new System.Drawing.Size(161, 23);
            this.checkBox_islamische_F.TabIndex = 2;
            this.checkBox_islamische_F.Text = "Islamische Feiertage";
            // 
            // pbOpenFileD
            // 
            this.pbOpenFileD.BackColor = System.Drawing.Color.Transparent;
            this.pbOpenFileD.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbOpenFileD.FlatAppearance.BorderSize = 2;
            this.pbOpenFileD.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbOpenFileD.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbOpenFileD.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbOpenFileD.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbOpenFileD.Location = new System.Drawing.Point(747, 57);
            this.pbOpenFileD.Name = "pbOpenFileD";
            this.pbOpenFileD.Size = new System.Drawing.Size(35, 27);
            this.pbOpenFileD.TabIndex = 2;
            this.pbOpenFileD.Text = "...";
            this.pbOpenFileD.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.pbOpenFileD.UseVisualStyleBackColor = false;
            pbOpenFileD.Click += pbOpenFileD_Click;
            this.BorderBody.ResumeLayout(false);
            this.BorderBody.PerformLayout();
            Icon = Properties.Resources.Konfig_rad_ico;
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