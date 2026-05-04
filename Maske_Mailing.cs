using MyControls;
using Serilog;
using System;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_Mailing : MyForm
    {
        private MyGroupBox myGroupBox4;
        private MyDuseRichTextBox feld_Body;
        private MyGroupBox myGroupBox3;
        private MyFieldText feld_Betreff;
        private MyGroupBox myGroupBox2;
        private MyLabel myLabel4;
        private MyFieldText feld_Mail_Bcc;
        private MyLabel myLabel3;
        private MyFieldText feld_Mail_Cc;
        private MyLabel myLabel2;
        private MyFieldText feld_Mail_An;
        private MyGroupBox myGroupBox1;
        private MyLabel myLabel1;
        private MyFieldText feld_Mail_Von;
        private EMailData eMailData = new EMailData();
        private string rechnungsnummer;

        protected override void _InitializeComponent()
        {
            this.feld_Mail_Von = new MyControls.MyFieldText();
            this.myLabel1 = new MyControls.MyLabel();
            this.myGroupBox1 = new MyControls.MyGroupBox();
            this.myGroupBox2 = new MyControls.MyGroupBox();
            this.myLabel2 = new MyControls.MyLabel();
            this.feld_Mail_An = new MyControls.MyFieldText();
            this.myLabel3 = new MyControls.MyLabel();
            this.feld_Mail_Cc = new MyControls.MyFieldText();
            this.myLabel4 = new MyControls.MyLabel();
            this.feld_Mail_Bcc = new MyControls.MyFieldText();
            this.myGroupBox3 = new MyControls.MyGroupBox();
            this.feld_Betreff = new MyControls.MyFieldText();
            this.feld_Body = new MyControls.MyDuseRichTextBox();
            this.myGroupBox4 = new MyControls.MyGroupBox();
            this.BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.myGroupBox1.SuspendLayout();
            this.myGroupBox2.SuspendLayout();
            this.myGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BorderBody
            // 
            this.BorderBody.Controls.Add(this.myGroupBox4);
            this.BorderBody.Controls.Add(this.feld_Body);
            this.BorderBody.Controls.Add(this.myGroupBox3);
            this.BorderBody.Controls.Add(this.myGroupBox2);
            this.BorderBody.Controls.Add(this.myGroupBox1);
            // 
            // feld_Mail_Von
            // 
            this.feld_Mail_Von.Location = new System.Drawing.Point(49, 28);
            this.feld_Mail_Von.Name = "feld_Mail_Von";
            this.feld_Mail_Von.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Mail_Von.Size = new System.Drawing.Size(725, 27);
            this.feld_Mail_Von.TabIndex = 0;
            this.feld_Mail_Von.Texts = "";
            // 
            // myLabel1
            // 
            this.myLabel1.AutoSize = true;
            this.myLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel1.ForeColor = System.Drawing.Color.Black;
            this.myLabel1.Location = new System.Drawing.Point(6, 31);
            this.myLabel1.Name = "myLabel1";
            this.myLabel1.Size = new System.Drawing.Size(37, 19);
            this.myLabel1.TabIndex = 1;
            this.myLabel1.Text = "Von:";
            this.myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox1.BorderThickness = 1;
            this.myGroupBox1.Controls.Add(this.myLabel1);
            this.myGroupBox1.Controls.Add(this.feld_Mail_Von);
            this.myGroupBox1.Location = new System.Drawing.Point(8, 17);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(788, 75);
            this.myGroupBox1.TabIndex = 2;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "Absender";
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox2.BorderThickness = 1;
            this.myGroupBox2.Controls.Add(this.myLabel4);
            this.myGroupBox2.Controls.Add(this.feld_Mail_Bcc);
            this.myGroupBox2.Controls.Add(this.myLabel3);
            this.myGroupBox2.Controls.Add(this.feld_Mail_Cc);
            this.myGroupBox2.Controls.Add(this.myLabel2);
            this.myGroupBox2.Controls.Add(this.feld_Mail_An);
            this.myGroupBox2.Location = new System.Drawing.Point(8, 98);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Size = new System.Drawing.Size(788, 147);
            this.myGroupBox2.TabIndex = 3;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "Empfänger";
            // 
            // myLabel2
            // 
            this.myLabel2.AutoSize = true;
            this.myLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel2.ForeColor = System.Drawing.Color.Black;
            this.myLabel2.Location = new System.Drawing.Point(6, 32);
            this.myLabel2.Name = "myLabel2";
            this.myLabel2.Size = new System.Drawing.Size(30, 19);
            this.myLabel2.TabIndex = 3;
            this.myLabel2.Text = "An:";
            this.myLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_Mail_An
            // 
            this.feld_Mail_An.Location = new System.Drawing.Point(49, 29);
            this.feld_Mail_An.Name = "feld_Mail_An";
            this.feld_Mail_An.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Mail_An.Size = new System.Drawing.Size(725, 27);
            this.feld_Mail_An.TabIndex = 2;
            this.feld_Mail_An.Texts = "";
            // 
            // myLabel3
            // 
            this.myLabel3.AutoSize = true;
            this.myLabel3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel3.ForeColor = System.Drawing.Color.Black;
            this.myLabel3.Location = new System.Drawing.Point(6, 65);
            this.myLabel3.Name = "myLabel3";
            this.myLabel3.Size = new System.Drawing.Size(28, 19);
            this.myLabel3.TabIndex = 5;
            this.myLabel3.Text = "Cc:";
            this.myLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_Mail_Cc
            // 
            this.feld_Mail_Cc.Location = new System.Drawing.Point(49, 62);
            this.feld_Mail_Cc.Name = "feld_Mail_Cc";
            this.feld_Mail_Cc.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Mail_Cc.Size = new System.Drawing.Size(725, 27);
            this.feld_Mail_Cc.TabIndex = 4;
            this.feld_Mail_Cc.Texts = "";
            // 
            // myLabel4
            // 
            this.myLabel4.AutoSize = true;
            this.myLabel4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myLabel4.ForeColor = System.Drawing.Color.Black;
            this.myLabel4.Location = new System.Drawing.Point(6, 98);
            this.myLabel4.Name = "myLabel4";
            this.myLabel4.Size = new System.Drawing.Size(36, 19);
            this.myLabel4.TabIndex = 7;
            this.myLabel4.Text = "Bcc:";
            this.myLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_Mail_Bcc
            // 
            this.feld_Mail_Bcc.Location = new System.Drawing.Point(49, 95);
            this.feld_Mail_Bcc.Name = "feld_Mail_Bcc";
            this.feld_Mail_Bcc.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Mail_Bcc.Size = new System.Drawing.Size(725, 27);
            this.feld_Mail_Bcc.TabIndex = 6;
            this.feld_Mail_Bcc.Texts = "";
            // 
            // myGroupBox3
            // 
            this.myGroupBox3.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox3.BorderThickness = 1;
            this.myGroupBox3.Controls.Add(this.feld_Betreff);
            this.myGroupBox3.Location = new System.Drawing.Point(8, 252);
            this.myGroupBox3.Name = "myGroupBox3";
            this.myGroupBox3.Size = new System.Drawing.Size(788, 64);
            this.myGroupBox3.TabIndex = 4;
            this.myGroupBox3.TabStop = false;
            this.myGroupBox3.Text = "Betreff";
            // 
            // feld_Betreff
            // 
            this.feld_Betreff.Location = new System.Drawing.Point(49, 22);
            this.feld_Betreff.Name = "feld_Betreff";
            this.feld_Betreff.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Betreff.Size = new System.Drawing.Size(725, 27);
            this.feld_Betreff.TabIndex = 2;
            this.feld_Betreff.Texts = "";
            // 
            // feld_Body
            // 
            this.feld_Body.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.feld_Body.ForeColor = System.Drawing.Color.DarkGray;
            this.feld_Body.Location = new System.Drawing.Point(8, 328);
            this.feld_Body.Name = "feld_Body";
            this.feld_Body.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Body.PlaceholderText = "  Inhalt";
            this.feld_Body.Size = new System.Drawing.Size(788, 95);
            this.feld_Body.TabIndex = 5;
            this.feld_Body.Text = "  Inhalt";
            this.feld_Body.Texts = "";
            // 
            // myGroupBox4
            // 
            this.myGroupBox4.BorderColor = System.Drawing.Color.DimGray;
            this.myGroupBox4.BorderThickness = 1;
            this.myGroupBox4.Location = new System.Drawing.Point(8, 435);
            this.myGroupBox4.Name = "myGroupBox4";
            this.myGroupBox4.Size = new System.Drawing.Size(787, 64);
            this.myGroupBox4.TabIndex = 6;
            this.myGroupBox4.TabStop = false;
            this.myGroupBox4.Text = "Anhang";
            this.BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Konfig_rad_ico;
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            this.myGroupBox2.ResumeLayout(false);
            this.myGroupBox2.PerformLayout();
            this.myGroupBox3.ResumeLayout(false);
            this.myGroupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected override string _name()
        {
            return "E-Mail";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            feld_Mail_Von.Text = eMailData.Von;
            feld_Mail_An.Text = eMailData.An;
            feld_Mail_Cc.Text = eMailData.Cc;
            feld_Mail_Bcc.Text = eMailData.Bcc;
            feld_Betreff.Text = eMailData.Betreff;
            feld_Body.Text = eMailData.Body;
            if (!string.IsNullOrWhiteSpace(eMailData.Anhang1))
            {
                Label lbl = new Label();
                lbl.Text = eMailData.Anhang1_Name;
                lbl.AutoSize = true;
                lbl.Location = new System.Drawing.Point(6, 20);
                lbl.ForeColor = Color.Red;
                lbl.Cursor = Cursors.Hand;
                lbl.DoubleClick += Click_Anhang;
                myGroupBox4.Controls.Add(lbl);
            }
            return true;
        }
        protected override bool _Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(feld_Mail_An.Text))
                {
                    MessageBox.Show("Empfanger-Adresse darf nicht leer sein!", "Error!!");

                    return false;
                }
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(feld_Mail_Von.Text);
                message.To.Add(new MailAddress(feld_Mail_An.Text));
                if (!string.IsNullOrWhiteSpace(feld_Mail_Cc.Text))
                {
                    message.CC.Add(new MailAddress(feld_Mail_Cc.Text));
                }
                if (!string.IsNullOrWhiteSpace(feld_Mail_Bcc.Text))
                {
                    message.Bcc.Add(new MailAddress(feld_Mail_Bcc.Text));
                }

                message.Subject = feld_Betreff.Text;
                message.Body = feld_Body.Text;

                System.Net.Mail.Attachment attachment = new Attachment(eMailData.Anhang1);
                attachment.Name = eMailData.Anhang1_Name;
                message.Attachments.Add(attachment);

                smtp.Port = eMailData.Port;
                smtp.Host = eMailData.Host;
                smtp.EnableSsl = eMailData.Ssl;
                smtp.UseDefaultCredentials = eMailData.Standard_info_verwenden;
                smtp.Credentials = new System.Net.NetworkCredential(feld_Mail_Von.Text, eMailData.Passwort);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                MessageBox.Show("EMail wurde an " + feld_Mail_An.Text + " erfolgreich versendet", "Info!!");
                //log
                Log.Information("EMail von:{0} \nAn:{1}\nCc:{2}\nBcc:{2} \nwurde erfolgreich versendet",
                    feld_Mail_Von.Text, feld_Mail_An.Text, feld_Mail_Cc.Text, feld_Mail_Bcc.Text);

                // Status der Rechnung updaten
                DataAccessLayer.update_RechnungStatus(rechnungsnummer, Prozessstatus.VERSANDT);
                Log.Information("Rechnung-Nr.{0} -> Status:{1}", rechnungsnummer, Prozessstatus.VERSANDT.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
                Log.Error(ex.Message);
            }
            return true;
        }
        protected override void _AfterSave()
        {
            Close();
        }
        protected override bool _PbSave_AllwayEnabled()
        {
            return true;
        }
        private void Click_Anhang(object sender, EventArgs e)
        {
            if (File.Exists(eMailData.Anhang1))
            {
                System.Diagnostics.Process.Start(eMailData.Anhang1);
            }
            else
            {
                MessageBox.Show("Anhang wurde nicht gefunden!", "Error!!");
            }

        }
        public void setParameters(EMailData mailData, string _rechnungsnummer)
        {
            rechnungsnummer = _rechnungsnummer;
            eMailData = mailData;
        }
    }
}