using Microsoft.WindowsAPICodePack.Dialogs;
using MyControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FCC_Verwaltungssystem
{
    public class Maske_KonfigDatabase : MyForm
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MyFieldText feld_Backup;
        private MyFieldText feld_Datenbank;
        private MyFieldText feld_server;
        private MyLabel myLabel3;
        private MyLabel myLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private MyPushButton pbBackup_Explorer;
        private MyPushButton pbDB_Explorer;
        private MyPushButton pbServer_Explorer;
        private MyLabel myLabel1;

        protected override string _name()
        {
            return "Datenbank-Konfiguration";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            feld_server.Texts = Database.Server_name;
            feld_Datenbank.Texts = Database.DatabaseName;
            feld_Backup.Texts = Database.BackupFolder;
            return true;
        }
        protected override bool _PlausibleBevorSave()
        {
            if (string.IsNullOrEmpty(feld_server.Texts))
            {
                errorProvider.SetError(feld_server, " Servername eingeben");
                return false;
            }
            if (string.IsNullOrEmpty(feld_Datenbank.Texts))
            {
                errorProvider.SetError(feld_Datenbank, " Datenbankname eingeben");
                return false;
            }
            if (string.IsNullOrEmpty(feld_Backup.Texts))
            {
                errorProvider.SetError(feld_Backup, " Backup-Verzeichnis eingeben");
                return false;
            }
            DialogResult result = MessageBox.Show("Die Änderungen erfordern neustart des Systems!.\nBitte die Anwendung neustarten.", 
                "Hinweis", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.Cancel))
            {
                return false;
            }

            return true;
        }
        protected override bool _Save()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string url = workingDirectory + @"\Konfiguration\Konfiguration.xml";
            XDocument doc = XDocument.Load(url);

            var dbName = doc.Descendants("Name").FirstOrDefault();
            if (dbName != null)
            {
                dbName.Value = feld_Datenbank.Texts;
            }
            var serverName = doc.Descendants("Server").FirstOrDefault();
            if (serverName != null)
            {
                serverName.Value = feld_server.Texts;
            }
            var backupFolder = doc.Descendants("Backup-Folder").FirstOrDefault();
            if (backupFolder != null)
            {
                backupFolder.Value = feld_Backup.Texts;
            }
            doc.Save(url);

            return true;
        }
        protected override void _AfterSave()
        {
            _Populate();
        }
        protected override void _Close()
        {
        }
        protected override void _InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_KonfigDatabase));
            myLabel1 = new MyLabel();
            myLabel2 = new MyLabel();
            myLabel3 = new MyLabel();
            feld_server = new MyFieldText();
            feld_Datenbank = new MyFieldText();
            feld_Backup = new MyFieldText();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            pbServer_Explorer = new MyPushButton();
            pbDB_Explorer = new MyPushButton();
            pbBackup_Explorer = new MyPushButton();
            ((System.ComponentModel.ISupportInitialize)(FileArchiv)).BeginInit();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            SuspendLayout();
            // 
            // pbOk
            // 
            pbOk.BackColor = System.Drawing.Color.Transparent;
            pbOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbOk.FlatAppearance.BorderSize = 2;
            pbOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbOk.Image = ((System.Drawing.Image)(resources.GetObject("pbOk.Image")));
            pbOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbOk.UseVisualStyleBackColor = false;
            // 
            // FileArchiv
            // 
            FileArchiv.BackColor = System.Drawing.Color.Transparent;
            FileArchiv.Image = ((System.Drawing.Image)(resources.GetObject("FileArchiv.Image")));
            FileArchiv.Size = new System.Drawing.Size(32, 32);
            FileArchiv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            FileArchiv.WaitOnLoad = true;
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(pbBackup_Explorer);
            BorderBody.Controls.Add(pbDB_Explorer);
            //BorderBody.Controls.Add(pbServer_Explorer);
            BorderBody.Controls.Add(label4);
            BorderBody.Controls.Add(label3);
            BorderBody.Controls.Add(pictureBox1);
            BorderBody.Controls.Add(label2);
            BorderBody.Controls.Add(label1);
            BorderBody.Controls.Add(feld_Backup);
            BorderBody.Controls.Add(feld_Datenbank);
            BorderBody.Controls.Add(feld_server);
            BorderBody.Controls.Add(myLabel3);
            BorderBody.Controls.Add(myLabel2);
            BorderBody.Controls.Add(myLabel1);
            // 
            // myLabel1
            // 
            myLabel1.AutoSize = true;
            myLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel1.ForeColor = System.Drawing.Color.Black;
            myLabel1.Location = new System.Drawing.Point(21, 191);
            myLabel1.Name = "myLabel1";
            myLabel1.Size = new System.Drawing.Size(53, 19);
            myLabel1.TabIndex = 0;
            myLabel1.Text = "Server:";
            myLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myLabel2
            // 
            myLabel2.AutoSize = true;
            myLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel2.ForeColor = System.Drawing.Color.Black;
            myLabel2.Location = new System.Drawing.Point(21, 294);
            myLabel2.Name = "myLabel2";
            myLabel2.Size = new System.Drawing.Size(83, 19);
            myLabel2.TabIndex = 1;
            myLabel2.Text = "Datenbank:";
            myLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // myLabel3
            // 
            myLabel3.AutoSize = true;
            myLabel3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            myLabel3.ForeColor = System.Drawing.Color.Black;
            myLabel3.Location = new System.Drawing.Point(21, 395);
            myLabel3.Name = "myLabel3";
            myLabel3.Size = new System.Drawing.Size(137, 19);
            myLabel3.TabIndex = 2;
            myLabel3.Text = "Backup-Verzeichnis:";
            myLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // feld_server
            // 
            feld_server.ForeColor = System.Drawing.Color.Black;
            feld_server.Location = new System.Drawing.Point(164, 188);
            feld_server.Name = "feld_server";
            feld_server.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_server.Size = new System.Drawing.Size(514, 27);
            feld_server.TabIndex = 3;
            feld_server.Texts = "";
            //feld_server.ReadOnly = true;
            // 
            // feld_Datenbank
            // 
            feld_Datenbank.ForeColor = System.Drawing.Color.Black;
            feld_Datenbank.Location = new System.Drawing.Point(164, 286);
            feld_Datenbank.Name = "feld_Datenbank";
            feld_Datenbank.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Datenbank.Size = new System.Drawing.Size(514, 27);
            feld_Datenbank.TabIndex = 4;
            feld_Datenbank.Texts = "";
            feld_Datenbank.ReadOnly = true;
            // 
            // feld_Backup
            // 
            feld_Backup.ForeColor = System.Drawing.Color.Black;
            feld_Backup.Location = new System.Drawing.Point(164, 392);
            feld_Backup.Name = "feld_Backup";
            feld_Backup.PlaceholderColor = System.Drawing.Color.DarkGray;
            feld_Backup.Size = new System.Drawing.Size(514, 27);
            feld_Backup.TabIndex = 5;
            feld_Backup.Texts = "";
            feld_Backup.ReadOnly = true;
            // 
            // label1
            // 
            label1.ForeColor = System.Drawing.Color.Gray;
            label1.Location = new System.Drawing.Point(161, 227);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(561, 32);
            label1.TabIndex = 6;
            label1.Text = "Hier wird angegeben, zu welchem Server die Anwendung die Datenbankverbindung aufb" +
    "aut.Anpassungen in diesem Feld ändert den Ziel-Datenbankserver.";
            // 
            // label2
            // 
            label2.ForeColor = System.Drawing.Color.Gray;
            label2.Location = new System.Drawing.Point(161, 49);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(613, 99);
            label2.TabIndex = 7;
            label2.Text = resources.GetString("label2.Text");
            // 
            // pictureBox1
            // 
            pictureBox1.Image = global::FCC_Verwaltungssystem.Properties.Resources.pngegg_1_;
            pictureBox1.Location = new System.Drawing.Point(25, 49);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(50, 50);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.ForeColor = System.Drawing.Color.Gray;
            label3.Location = new System.Drawing.Point(161, 319);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(561, 32);
            label3.TabIndex = 9;
            label3.Text = "Name der Datenbank, zu der die Anwendung eine Verbindung herstellt.";
            // 
            // label4
            // 
            label4.ForeColor = System.Drawing.Color.Gray;
            label4.Location = new System.Drawing.Point(161, 426);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(561, 32);
            label4.TabIndex = 10;
            label4.Text = "Hier wird festgelegt, wo die Anwendung Sicherungskopien der Datenbank ablegt.\r\nÄn" +
    "derungen an diesem Pfad können den Speicherort vorhandener Backups verändern ode" +
    "r zu Datenverlust führen.";
            // 
            // pbServer_Explorer
            // 
            pbServer_Explorer.BackColor = System.Drawing.Color.Transparent;
            pbServer_Explorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbServer_Explorer.FlatAppearance.BorderSize = 2;
            pbServer_Explorer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbServer_Explorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbServer_Explorer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbServer_Explorer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbServer_Explorer.Location = new System.Drawing.Point(678, 188);
            pbServer_Explorer.Name = "pbServer_Explorer";
            pbServer_Explorer.ReadOnly = false;
            pbServer_Explorer.Size = new System.Drawing.Size(64, 27);
            pbServer_Explorer.TabIndex = 11;
            pbServer_Explorer.Text = "...";
            pbServer_Explorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbServer_Explorer.UseVisualStyleBackColor = false;
            pbServer_Explorer.Enabled = false;
            // 
            // pbDB_Explorer
            // 
            pbDB_Explorer.BackColor = System.Drawing.Color.Transparent;
            pbDB_Explorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbDB_Explorer.FlatAppearance.BorderSize = 2;
            pbDB_Explorer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbDB_Explorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbDB_Explorer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbDB_Explorer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbDB_Explorer.Location = new System.Drawing.Point(678, 286);
            pbDB_Explorer.Name = "pbDB_Explorer";
            pbDB_Explorer.ReadOnly = false;
            pbDB_Explorer.Size = new System.Drawing.Size(64, 27);
            pbDB_Explorer.TabIndex = 12;
            pbDB_Explorer.Text = "...";
            pbDB_Explorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbDB_Explorer.UseVisualStyleBackColor = false;
            pbDB_Explorer.Click += pbOpenFileD_Click;
            // 
            // pbBackup_Explorer
            // 
            pbBackup_Explorer.BackColor = System.Drawing.Color.Transparent;
            pbBackup_Explorer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            pbBackup_Explorer.FlatAppearance.BorderSize = 2;
            pbBackup_Explorer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            pbBackup_Explorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            pbBackup_Explorer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pbBackup_Explorer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            pbBackup_Explorer.Location = new System.Drawing.Point(678, 392);
            pbBackup_Explorer.Name = "pbBackup_Explorer";
            pbBackup_Explorer.ReadOnly = false;
            pbBackup_Explorer.Size = new System.Drawing.Size(64, 27);
            pbBackup_Explorer.TabIndex = 13;
            pbBackup_Explorer.Text = "...";
            pbBackup_Explorer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            pbBackup_Explorer.UseVisualStyleBackColor = false;
            pbBackup_Explorer.Click += pbOpenFileD_Click;

            ((System.ComponentModel.ISupportInitialize)(FileArchiv)).EndInit();
            BorderBody.ResumeLayout(false);
            BorderBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private void pbOpenFileD_Click(object sender, EventArgs e)
        {
            MyPushButton pushButton = sender as MyPushButton;
            if(pushButton == null)
            {
                return;
            }
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            if (pushButton.Name.Equals(pbDB_Explorer.Name))
            {
                dialog.IsFolderPicker = false;
                dialog.Filters.Add(new CommonFileDialogFilter("Datenbankdateien", "*.mdf"));
            }
            if (pushButton.Name.Equals(pbBackup_Explorer.Name))
            {
                dialog.IsFolderPicker = true;
            }
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (pushButton.Name.Equals(pbDB_Explorer.Name))
                {
                    feld_Datenbank.Texts = Path.GetFileNameWithoutExtension(dialog.FileName);
                }
                else if (pushButton.Name.Equals(pbBackup_Explorer.Name))
                {
                    feld_Backup.Texts = dialog.FileName;
                }
                pbOk.Enabled = true;
            }
        }
    }
}
