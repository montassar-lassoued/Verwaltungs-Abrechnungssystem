using MyControls;
using System;
using System.Data;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_KonfigMail : MyForm
    {
        private GroupBox groupBox1;
        private MyFieldText feld_passwort;
        private MyFieldText feld_Mail_SMTP;
        private MyFieldText feld_host;
        private MyNumericField feld_port;
        private MyDuseCheckBox checkBox_SSL;
        private MyDuseCheckBox checkBox_SInfo;
        private GroupBox groupBox2;
        private MyRadioButton rbOUTLOOK;
        private MyDuseCheckBox checkBox_DisplayOutlook;
        private MyFieldText feld_Mail_outlook;
        //private MyDuseCheckBox myDuseCheckBox1;
        //private MyDuseCheckBox myDuseCheckBox2;
        private MyRadioButton rbSMTP;

        protected override string _name()
        {
            return "Mail-Konfiguration";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
            Focus();
        }
        protected override bool _Populate()
        {
            DataTable dataTable = DataAccessLayer.query_MailEinstellungen();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                string email_smtp = (string)row["EMAIL_SMTP"];
                string psw_smtp = (string)row["PASSWORT_SMTP"];
                int port_smtp = (int)row["PORT"];
                string host_smtp = (string)row["HOST"];
                bool ssl_smtp = (bool)row["SSL"];
                bool sInfo_smtp = (bool)row["STANDARD_INFO"];
                string email_outl = (string)row["EMAIL_OUT"];
                bool display_outl = (bool)row["DISPLAY_OUT"];
                string aktive_Einstellung = (string)row["AKTIV_EINST"];

                if (aktive_Einstellung.Equals("SMTP"))
                {
                    //InvokeOnClick(rbSMTP, EventArgs.Empty);
                    rbSMTP.Checked = true;
                }
                if (aktive_Einstellung.Equals("OUTLK"))
                {
                    //InvokeOnClick(rbOUTLOOK, EventArgs.Empty);
                    rbOUTLOOK.Checked = true;
                }

                feld_Mail_SMTP.Texts = email_smtp;
                feld_passwort.Texts = psw_smtp;
                feld_port.Texts = port_smtp.ToString();
                feld_host.Texts = host_smtp;
                checkBox_SSL.Checked = ssl_smtp;
                checkBox_SInfo.Checked = sInfo_smtp;

                feld_Mail_outlook.Texts = email_outl;
                checkBox_DisplayOutlook.Checked = display_outl;

                rbCheckedStateChanged(rbOUTLOOK, EventArgs.Empty);
            }
            return true;
        }
        protected override bool _PlausibleBevorSave()
        {
            if (rbOUTLOOK.Checked && !OUTLOOKData_OK())
            {
                return false;
            }
            else if (rbSMTP.Checked && !SMTPData_OK())
            {
                return false;
            }
            return true;
        }
        protected override bool _Save()
        {
            if (rbOUTLOOK.Checked)
            {
                int port = int.Parse(string.IsNullOrEmpty(feld_port.Texts) ? "0" : feld_port.Texts);
                DataAccessLayer.update_MailEinstellungen(feld_Mail_SMTP.Texts, feld_passwort.Texts, port,
                    feld_host.Texts, checkBox_SSL.Checked, checkBox_SInfo.Checked, feld_Mail_outlook.Texts, checkBox_DisplayOutlook.Checked, "OUTLK");
            }
            else if (rbSMTP.Checked)
            {
                int port = int.Parse(feld_port.Texts);
                DataAccessLayer.update_MailEinstellungen(feld_Mail_SMTP.Texts, feld_passwort.Texts, port,
                    feld_host.Texts, checkBox_SSL.Checked, checkBox_SInfo.Checked, feld_Mail_outlook.Texts, checkBox_DisplayOutlook.Checked, "SMTP");
            }
            return true;
        }
        protected override void _AfterSave()
        {
            _Populate();
        }
        private bool SMTPData_OK()
        {
            if (!CheckValidate.ValidateEmail(feld_Mail_SMTP.Texts))
            {
                errorProvider.SetError(feld_Mail_SMTP, "E-Mail Adresse ist fehlerhaft.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_passwort.Texts))
            {
                errorProvider.SetError(feld_passwort, "Passwort darf nicht leer sein.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_port.Texts))
            {
                errorProvider.SetError(feld_port, "Port-Nummer ist fehlerhaft.");
                return false;
            }
            int rslt;
            if (!int.TryParse(feld_port.Texts, out rslt))
            {
                errorProvider.SetError(feld_port, "Port-Nummer ist fehlerhaft.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(feld_host.Texts))
            {
                errorProvider.SetError(feld_host, "Hostname ist fehlerhaft.");
                return false;
            }
            return true;
        }
        private bool OUTLOOKData_OK()
        {
            if (!CheckValidate.ValidateEmail(feld_Mail_outlook.Texts))
            {
                errorProvider.SetError(feld_Mail_outlook, "E-Mail Adresse ist fehlerhaft.");
                return false;
            }
            return true;
        }
        protected override void _InitializeComponent()
        {
            groupBox1 = new GroupBox();
            checkBox_SInfo = new MyDuseCheckBox();
            checkBox_SSL = new MyDuseCheckBox();
            feld_host = new MyFieldText();
            feld_port = new MyNumericField();
            feld_passwort = new MyFieldText();
            feld_Mail_SMTP = new MyFieldText();
            rbSMTP = new MyRadioButton();
            groupBox2 = new GroupBox();
            checkBox_DisplayOutlook = new MyDuseCheckBox();
            feld_Mail_outlook = new MyFieldText();
            rbOUTLOOK = new MyRadioButton();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(rbOUTLOOK);
            BorderBody.Controls.Add(groupBox2);
            BorderBody.Controls.Add(rbSMTP);
            BorderBody.Controls.Add(groupBox1);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox_SInfo);
            groupBox1.Controls.Add(checkBox_SSL);
            groupBox1.Controls.Add(feld_host);
            groupBox1.Controls.Add(feld_port);
            groupBox1.Controls.Add(feld_passwort);
            groupBox1.Controls.Add(feld_Mail_SMTP);
            groupBox1.Location = new System.Drawing.Point(16, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(771, 256);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            // 
            // checkBox_SInfo
            // 
            checkBox_SInfo.Location = new System.Drawing.Point(18, 213);
            checkBox_SInfo.Name = "checkBox_SInfo";
            checkBox_SInfo.Size = new System.Drawing.Size(309, 23);
            checkBox_SInfo.TabIndex = 0;
            checkBox_SInfo.Text = "Standardanmeldeinformationen verwenden";
            // 
            // checkBox_SSL
            // 
            checkBox_SSL.Location = new System.Drawing.Point(18, 184);
            checkBox_SSL.Name = "checkBox_SSL";
            checkBox_SSL.Size = new System.Drawing.Size(117, 23);
            checkBox_SSL.TabIndex = 1;
            checkBox_SSL.Text = "SSL aktivieren";
            // 
            // feld_host
            // 
            feld_host.ForeColor = System.Drawing.Color.DarkGray;
            feld_host.Location = new System.Drawing.Point(18, 138);
            feld_host.Name = "feld_host";
            feld_host.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_host.PlaceholderText = "   Host";
            feld_host.Size = new System.Drawing.Size(742, 27);
            feld_host.TabIndex = 2;
            feld_host.Text = "   Host";
            feld_host.Texts = "";
            // 
            // feld_port
            // 
            feld_port.BackColor = System.Drawing.SystemColors.ControlLightLight;
            feld_port.ForeColor = System.Drawing.Color.DarkGray;
            feld_port.Location = new System.Drawing.Point(18, 100);
            feld_port.Name = "feld_port";
            feld_port.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_port.PlaceholderText = "   Port";
            feld_port.Size = new System.Drawing.Size(742, 27);
            feld_port.TabIndex = 3;
            feld_port.Text = "   Port";
            feld_port.TextAlign = HorizontalAlignment.Center;
            feld_port.Texts = "";
            // 
            // feld_passwort
            // 
            feld_passwort.ForeColor = System.Drawing.Color.DarkGray;
            feld_passwort.IsPasswordChar = true;
            feld_passwort.Location = new System.Drawing.Point(18, 63);
            feld_passwort.Name = "feld_passwort";
            feld_passwort.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_passwort.PlaceholderText = "   Password Credential";
            feld_passwort.Size = new System.Drawing.Size(742, 27);
            feld_passwort.TabIndex = 4;
            feld_passwort.Text = "   Password Credential";
            feld_passwort.Texts = "";
            // 
            // feld_Mail_SMTP
            // 
            feld_Mail_SMTP.ForeColor = System.Drawing.Color.DarkGray;
            feld_Mail_SMTP.Location = new System.Drawing.Point(18, 29);
            feld_Mail_SMTP.Name = "feld_Mail_SMTP";
            feld_Mail_SMTP.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Mail_SMTP.PlaceholderText = "   EMail-Adresse";
            feld_Mail_SMTP.Size = new System.Drawing.Size(742, 27);
            feld_Mail_SMTP.TabIndex = 5;
            feld_Mail_SMTP.Text = "   EMail-Adresse";
            feld_Mail_SMTP.Texts = "";
            // 
            // rbSMTP
            // 
            rbSMTP.Location = new System.Drawing.Point(16, 11);
            rbSMTP.Name = "rbSMTP";
            rbSMTP.Size = new System.Drawing.Size(64, 23);
            rbSMTP.TabIndex = 12;
            rbSMTP.TabStop = true;
            rbSMTP.Text = "SMTP";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBox_DisplayOutlook);
            groupBox2.Controls.Add(feld_Mail_outlook);
            groupBox2.Location = new System.Drawing.Point(16, 294);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(771, 195);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            // 
            // checkBox_DisplayOutlook
            // 
            checkBox_DisplayOutlook.Location = new System.Drawing.Point(17, 81);
            checkBox_DisplayOutlook.Name = "checkBox_DisplayOutlook";
            checkBox_DisplayOutlook.Size = new System.Drawing.Size(239, 23);
            checkBox_DisplayOutlook.TabIndex = 0;
            checkBox_DisplayOutlook.Text = "Outlook in neuem Fesnter öffnen";
            // 
            // feld_Mail_outlook
            // 
            feld_Mail_outlook.ForeColor = System.Drawing.Color.DarkGray;
            feld_Mail_outlook.Location = new System.Drawing.Point(18, 39);
            feld_Mail_outlook.Name = "feld_Mail_outlook";
            feld_Mail_outlook.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Mail_outlook.PlaceholderText = "   EMail-Adresse";
            feld_Mail_outlook.Size = new System.Drawing.Size(741, 27);
            feld_Mail_outlook.TabIndex = 1;
            feld_Mail_outlook.Text = "   EMail-Adresse";
            feld_Mail_outlook.Texts = "";
            // 
            // rbOUTLOOK
            // 
            rbOUTLOOK.Location = new System.Drawing.Point(16, 288);
            rbOUTLOOK.Name = "rbOUTLOOK";
            rbOUTLOOK.Size = new System.Drawing.Size(89, 23);
            rbOUTLOOK.TabIndex = 10;
            rbOUTLOOK.TabStop = true;
            rbOUTLOOK.Text = "OUTLOOK";
            BorderBody.ResumeLayout(false);
            BorderBody.PerformLayout();
            Icon = Properties.Resources.Konfig_rad_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        private void rbCheckedStateChanged(object sender, EventArgs e)
        {
            if (rbOUTLOOK.Checked)
            {
                feld_Mail_SMTP.Enabled = false;
                feld_passwort.Enabled = false;
                feld_host.Enabled = false;
                feld_port.Enabled = false;
                checkBox_SSL.Enabled = false;
                checkBox_SInfo.Enabled = false;

                feld_Mail_outlook.Enabled = true;
                feld_Mail_outlook.ReadOnly = false;
                checkBox_DisplayOutlook.Enabled = true;
            }
            else
            {
                feld_Mail_outlook.Enabled = false;
                checkBox_DisplayOutlook.Enabled = false;

                feld_Mail_SMTP.Enabled = true;
                feld_passwort.Enabled = true;
                feld_host.Enabled = true;
                feld_port.Enabled = true;
                checkBox_SSL.Enabled = true;
                checkBox_SInfo.Enabled = true;
                feld_Mail_SMTP.ReadOnly = false;
                feld_passwort.ReadOnly = false;
                feld_host.ReadOnly = false;
                feld_port.ReadOnly = false;
            }
        }
    }
}