using MyControls;
using System;
using System.Data;

namespace FCC_Verwaltungssystem
{
    public class Maske_BlackList : MyForm
    {
        private RowMergeView dataGridView;

        protected override bool _PbSave_AllwayEnabled()
        {
            return true;
        }
        protected override bool _EnableArchiv()
        {
            return true;
        }
        protected override DocumentArchiv _DocumentArchivData(DocumentArchiv dokument)
        {
            dokument.IdColumn = User.ID;
            dokument.TableName = "BLACKLIST";

            return dokument;
        }
        protected override string _name()
        {
            return "Blacklist";
        }
        protected override void _OnLoad(EventArgs e)
        {
            _Populate();
        }
        protected override bool _Populate()
        {
            if (dataGridView.RowCount > 0)
            {
                // die Daten einmal anzeigen, weil nach Speichern
                // wird die Methode automatisch aufgerufen.
                return false;
            }
            DataTable dataTable = DataAccessLayer.getBlacklist();
            dataGridView.DataSource = dataTable;
            dataGridView.Columns["ID"].Visible = false;

            return true;
        }
        protected override bool _Save()
        {
            DataAccessLayer.UpdateBlacklist(dataGridView);
            return true;
        }
        protected override void _InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Maske_BlackList));
            dataGridView = new MyControls.RowMergeView();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = true;
            dataGridView.AllowUserToDeleteRows = true;
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.AllowUserToResizeRows = true;
            dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Location = new System.Drawing.Point(14, 147);
            dataGridView.Size = new System.Drawing.Size(787, 484);

            // 
            // Maske_BlackList
            // 
            Controls.Add(dataGridView);
            Controls.SetChildIndex(BorderBody, 0);
            Controls.SetChildIndex(dataGridView, 0);
            Icon = Properties.Resources.Blacklist;

            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
    }
}