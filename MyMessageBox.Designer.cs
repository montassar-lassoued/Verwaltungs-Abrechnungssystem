
namespace FCC_Verwaltungssystem
{
    partial class MyMessageBox
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
            this.mail = new System.Windows.Forms.Button();
            this.druck = new System.Windows.Forms.Button();
            this.myMessage = new System.Windows.Forms.Label();
            this.pbvorschau = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.mail.Location = new System.Drawing.Point(32, 96);
            this.mail.Name = "Ok";
            this.mail.Size = new System.Drawing.Size(89, 22);
            this.mail.TabIndex = 0;
            this.mail.Text = "Mail senden";
            this.mail.UseVisualStyleBackColor = true;
            this.mail.Click += new System.EventHandler(this.Mail_Click);
            // 
            // No
            // 
            this.druck.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.druck.Location = new System.Drawing.Point(176, 96);
            this.druck.Name = "No";
            this.druck.Size = new System.Drawing.Size(89, 22);
            this.druck.TabIndex = 2;
            this.druck.Text = "Ausdrucken";
            this.druck.UseVisualStyleBackColor = true;
            this.druck.Click += new System.EventHandler(this.Druck_Click);
            // 
            // myMessage
            // 
            this.myMessage.Location = new System.Drawing.Point(51, 35);
            this.myMessage.Name = "myMessage";
            this.myMessage.Size = new System.Drawing.Size(192, 58);
            this.myMessage.TabIndex = 3;
            this.myMessage.Text = "my message";
            // 
            // abbrechen
            // 
            this.pbvorschau.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.pbvorschau.Location = new System.Drawing.Point(315, 96);
            this.pbvorschau.Name = "pbvorschau";
            this.pbvorschau.Size = new System.Drawing.Size(89, 22);
            this.pbvorschau.TabIndex = 4;
            this.pbvorschau.Text = "Ansicht";
            this.pbvorschau.UseVisualStyleBackColor = true;
            this.pbvorschau.Click += new System.EventHandler(this.abbrechen_Click);
            // 
            // MyMessageBox
            // 
            this.AcceptButton = this.mail;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.druck;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(414, 124);
            this.ControlBox = false;
            this.Controls.Add(this.pbvorschau);
            this.Controls.Add(this.myMessage);
            this.Controls.Add(this.druck);
            this.Controls.Add(this.mail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MyMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Warning!!!";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button mail;
        private System.Windows.Forms.Button druck;
        private System.Windows.Forms.Label myMessage;
        private System.Windows.Forms.Button pbvorschau;
    }
}