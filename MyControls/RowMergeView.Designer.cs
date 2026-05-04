
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    partial class RowMergeView
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();

            // RowMergeView

            /*AllowUserToAddRows = allowAddAndDeleteRows;
            AllowUserToDeleteRows = allowAddAndDeleteRows;*/
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            BackgroundColor = Color.White;
            BorderStyle = BorderStyle.Fixed3D;
            MultiSelect = false;
            //ReadOnly = !allowAddAndDeleteRows;

            ColumnHeadersHeight = 25;
            RowHeadersWidth = 20;
            RowTemplate.Height = 20;
            RowTemplate.MinimumHeight = 20;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DefaultCellStyle.SelectionBackColor = Color.Khaki;
            DefaultCellStyle.SelectionForeColor = DefaultCellStyle.ForeColor;
            RowHeadersDefaultCellStyle.SelectionBackColor = RowHeadersDefaultCellStyle.BackColor;

            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = false;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            EnableHeadersVisualStyles = false;
            RowHeadersVisible = true;

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader|DataGridViewAutoSizeColumnsMode.DisplayedCells;
            CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            DefaultCellStyle.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(102,178,255);
            MergeColumnHeaderBackColor = ColumnHeadersDefaultCellStyle.BackColor;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = ColumnHeadersDefaultCellStyle.BackColor;
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ResizeRedraw = true;
            DoubleBuffered = true;

            MouseClick += _MouseClick;

            // 
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
