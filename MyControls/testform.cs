using System;

namespace MyControls
{
    public class testform : MyForm
    {

        protected override void _InitializeComponent()
        {

        }

        protected override string _name()
        {
            return "";
        }

        protected override void _OnLoad(EventArgs e)
        {

        }

        protected override bool _Populate()
        {
            return false;
        }

        protected override bool _Save()
        {
            return true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(testform));
            ((System.ComponentModel.ISupportInitialize)(this.FileArchiv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            // FileArchiv
            // 
            this.FileArchiv.BackColor = System.Drawing.Color.Transparent;
            this.FileArchiv.Image = ((System.Drawing.Image)(resources.GetObject("FileArchiv.Image")));
            this.FileArchiv.Size = new System.Drawing.Size(32, 32);
            this.FileArchiv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FileArchiv.WaitOnLoad = true;
            ((System.ComponentModel.ISupportInitialize)(this.FileArchiv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
