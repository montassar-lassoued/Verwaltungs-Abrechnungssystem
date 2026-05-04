
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    partial class MyTreeDataGridView
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
            this.components = new System.ComponentModel.Container();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            // RowMergeView

            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            BackgroundColor = Color.White;
            BorderStyle = BorderStyle.Fixed3D;
            MultiSelect = false;
            Name = "DataGridView";
            ReadOnly = true;

            ColumnHeadersHeight = 40;
            RowHeadersWidth = 20;
            RowTemplate.Height = 30;
            RowTemplate.MinimumHeight = 30;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            DefaultCellStyle.SelectionBackColor = DefaultCellStyle.BackColor;
            DefaultCellStyle.SelectionForeColor = DefaultCellStyle.ForeColor;

            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = true;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            EnableHeadersVisualStyles = false;
            RowHeadersVisible = true;

            //AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            DefaultCellStyle.Font = new Font("Tahoma", 12F, GraphicsUnit.Pixel);
            ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 12F, GraphicsUnit.Pixel);
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            ResizeRedraw = true;
            // 
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}