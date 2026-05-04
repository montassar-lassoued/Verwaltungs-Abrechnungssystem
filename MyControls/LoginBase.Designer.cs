
using System.Windows.Forms;

namespace MyControls
{
    partial class LoginBase
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginBase));
            this.feld_Passwort = new MyControls.MyFieldText();
            this.feld_Addresse = new MyControls.MyFieldText();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbAnmelden = new MyControls.myPbSave();
            this.pbAbbrechen = new MyControls.myPbClose();
            this.Meldung = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textLogin = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_AnmeldungMerken = new MyControls.MyDuseCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // feld_Passwort
            // 
            this.feld_Passwort.ForeColor = System.Drawing.Color.Black;
            this.feld_Passwort.IsPasswordChar = true;
            this.feld_Passwort.Location = new System.Drawing.Point(109, 155);
            this.feld_Passwort.Name = "feld_Passwort";
            this.feld_Passwort.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Passwort.Size = new System.Drawing.Size(242, 27);
            this.feld_Passwort.TabIndex = 2;
            this.feld_Passwort.Texts = "";
            this.feld_Passwort.UseSystemPasswordChar = true;
            this.feld_Passwort.GotFocus += new System.EventHandler(this.HideWarning);
            // 
            // feld_Addresse
            // 
            this.feld_Addresse.ForeColor = System.Drawing.Color.Black;
            this.feld_Addresse.Location = new System.Drawing.Point(109, 116);
            this.feld_Addresse.Name = "feld_Addresse";
            this.feld_Addresse.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.feld_Addresse.Size = new System.Drawing.Size(242, 27);
            this.feld_Addresse.TabIndex = 1;
            this.feld_Addresse.Texts = "";
            this.feld_Addresse.GotFocus += new System.EventHandler(this.HideWarning);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Passwort:";
            // 
            // pbAnmelden
            // 
            this.pbAnmelden.BackColor = System.Drawing.Color.Transparent;
            this.pbAnmelden.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbAnmelden.FlatAppearance.BorderSize = 2;
            this.pbAnmelden.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbAnmelden.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbAnmelden.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbAnmelden.Image = ((System.Drawing.Image)(resources.GetObject("pbAnmelden.Image")));
            this.pbAnmelden.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbAnmelden.Location = new System.Drawing.Point(20, 239);
            this.pbAnmelden.Name = "pbAnmelden";
            this.pbAnmelden.ReadOnly = false;
            this.pbAnmelden.Size = new System.Drawing.Size(181, 42);
            this.pbAnmelden.TabIndex = 4;
            this.pbAnmelden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pbAnmelden.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbAnmelden.UseVisualStyleBackColor = false;
            // 
            // pbAbbrechen
            // 
            this.pbAbbrechen.BackColor = System.Drawing.Color.Transparent;
            this.pbAbbrechen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbAbbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.pbAbbrechen.FlatAppearance.BorderSize = 2;
            this.pbAbbrechen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbAbbrechen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbAbbrechen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbAbbrechen.Image = ((System.Drawing.Image)(resources.GetObject("pbAbbrechen.Image")));
            this.pbAbbrechen.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbAbbrechen.Location = new System.Drawing.Point(210, 239);
            this.pbAbbrechen.Name = "pbAbbrechen";
            this.pbAbbrechen.ReadOnly = false;
            this.pbAbbrechen.Size = new System.Drawing.Size(181, 42);
            this.pbAbbrechen.TabIndex = 3;
            this.pbAbbrechen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pbAbbrechen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbAbbrechen.UseVisualStyleBackColor = false;
            // 
            // Meldung
            // 
            this.Meldung.BackColor = System.Drawing.Color.Transparent;
            this.Meldung.ForeColor = System.Drawing.Color.Red;
            this.Meldung.Location = new System.Drawing.Point(75, 58);
            this.Meldung.Name = "Meldung";
            this.Meldung.Size = new System.Drawing.Size(253, 43);
            this.Meldung.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MyControls.Properties.Resources.pngegg_1_;
            this.pictureBox1.Location = new System.Drawing.Point(30, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // textLogin
            // 
            this.textLogin.AutoSize = true;
            this.textLogin.BackColor = System.Drawing.Color.Transparent;
            this.textLogin.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textLogin.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Italic);
            this.textLogin.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.textLogin.Location = new System.Drawing.Point(20, 9);
            this.textLogin.Name = "textLogin";
            this.textLogin.Size = new System.Drawing.Size(128, 35);
            this.textLogin.TabIndex = 0;
            this.textLogin.Text = "Anmelden";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(20, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(371, 114);
            this.label3.TabIndex = 7;
            // 
            // cb_AnmeldungMerken
            // 
            this.cb_AnmeldungMerken.Location = new System.Drawing.Point(179, 194);
            this.cb_AnmeldungMerken.Name = "cb_AnmeldungMerken";
            this.cb_AnmeldungMerken.Size = new System.Drawing.Size(175, 23);
            this.cb_AnmeldungMerken.TabIndex = 8;
            this.cb_AnmeldungMerken.Text = "Anmeldedaten merken";
            // 
            // LoginBase
            // 
            this.AcceptButton = this.pbAnmelden;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CancelButton = this.pbAbbrechen;
            this.ClientSize = new System.Drawing.Size(403, 292);
            this.Controls.Add(this.cb_AnmeldungMerken);
            this.Controls.Add(this.textLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Meldung);
            this.Controls.Add(this.pbAbbrechen);
            this.Controls.Add(this.pbAnmelden);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.feld_Passwort);
            this.Controls.Add(this.feld_Addresse);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::MyControls.Properties.Resources.Login_ico;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Name = "LoginBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected MyFieldText feld_Passwort;
        protected MyFieldText feld_Addresse;
        protected Label label1;
        protected Label label2;
        protected myPbSave pbAnmelden;
        protected myPbClose pbAbbrechen;
        protected static bool succes;
        protected Label Meldung;
        protected PictureBox pictureBox1;
        protected Label textLogin;
        protected Label label3;
        protected MyDuseCheckBox cb_AnmeldungMerken;
    }
}

