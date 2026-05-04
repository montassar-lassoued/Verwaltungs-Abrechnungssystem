
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    partial class AuswahlDialog
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
            this.pMyLogo = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkedListColumns = new MyControls.MyCheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbSpeichern = new System.Windows.Forms.Button();
            this.pbAbbrechen = new System.Windows.Forms.Button();
            this.tAddresse = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pMyLogo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMyLogo
            // 
            this.pMyLogo.Image = global::MyControls.Properties.Resources.toro;
            this.pMyLogo.Location = new System.Drawing.Point(8, 9);
            this.pMyLogo.Name = "pMyLogo";
            this.pMyLogo.Size = new System.Drawing.Size(97, 94);
            this.pMyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pMyLogo.TabIndex = 20;
            this.pMyLogo.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkedListColumns);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 123);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 276);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Daten";
            // 
            // checkedListColumns
            // 
            this.checkedListColumns.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListColumns.CheckOnClick = true;
            //this.checkedListColumns.ColumnWidth = 32;
            this.checkedListColumns.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListColumns.FormattingEnabled = true;
            this.checkedListColumns.Location = new System.Drawing.Point(25, 81);
            this.checkedListColumns.MultiColumn = true;
            this.checkedListColumns.Name = "checkedListColumns";
            this.checkedListColumns.Size = new System.Drawing.Size(432, 154);
            this.checkedListColumns.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(369, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Die gewünschten Spalten zum Ausdrucken unten markieren";
            // 
            // pbSpeichern
            // 
            this.pbSpeichern.BackColor = System.Drawing.Color.Transparent;
            this.pbSpeichern.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbSpeichern.FlatAppearance.BorderSize = 2;
            this.pbSpeichern.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.pbSpeichern.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pbSpeichern.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbSpeichern.Image = global::MyControls.Properties.Resources.cart_ecommerce_shopping_verified_store_icon_227310;
            this.pbSpeichern.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pbSpeichern.Location = new System.Drawing.Point(128, 9);
            this.pbSpeichern.Name = "pbSpeichern";
            this.pbSpeichern.Size = new System.Drawing.Size(172, 42);
            this.pbSpeichern.TabIndex = 31;
            this.pbSpeichern.Text = "Speichern";
            this.pbSpeichern.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.pbSpeichern.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbSpeichern.UseVisualStyleBackColor = false;
            pbSpeichern.Click += PbSpeichern_Click;
            // 
            // pbAbbrechen
            // 
            this.pbAbbrechen.AutoEllipsis = true;
            this.pbAbbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.pbAbbrechen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pbAbbrechen.Image = global::MyControls.Properties.Resources.Cancel_icon_icons_com_73703;
            this.pbAbbrechen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pbAbbrechen.Location = new System.Drawing.Point(315, 9);
            this.pbAbbrechen.Name = "pbAbbrechen";
            this.pbAbbrechen.Size = new System.Drawing.Size(172, 42);
            this.pbAbbrechen.TabIndex = 32;
            this.pbAbbrechen.Text = "Abbrechen";
            this.pbAbbrechen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pbAbbrechen.UseVisualStyleBackColor = true;
            // 
            // tAddresse
            // 
            this.tAddresse.BackColor = System.Drawing.Color.Transparent;
            this.tAddresse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tAddresse.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.tAddresse.ForeColor = System.Drawing.Color.DimGray;
            this.tAddresse.Location = new System.Drawing.Point(120, 48);
            this.tAddresse.Name = "tAddresse";
            this.tAddresse.Size = new System.Drawing.Size(238, 47);
            this.tAddresse.TabIndex = 33;
            this.tAddresse.Text = "Scheffelstraße 112 - D 09120 Chemnitz";
            // 
            // AuswahlDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.pbAbbrechen;
            this.ClientSize = new System.Drawing.Size(504, 417);
            this.Controls.Add(this.pMyLogo);
            this.Controls.Add(this.pbSpeichern);
            this.Controls.Add(this.pbAbbrechen);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuswahlDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.pMyLogo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        protected System.Windows.Forms.Label tAddresse;
        protected System.Windows.Forms.Button pbSpeichern;
        protected System.Windows.Forms.Button pbAbbrechen;
        protected System.Windows.Forms.PictureBox pMyLogo;
        private GroupBox groupBox2;
        private Label label1;


        #endregion

        private MyCheckedListBox checkedListColumns;
    }
}