using MyControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class MyTable : MyForm
    {
        private DataGridView dataGrid;
        private List<KeyValuePair<string, string>> list;
        private bool dataSelected;
        DataTable dataTable;

        public MyTable(DataTable _datatable)
        {
            dataTable = _datatable;
        }
        protected override void _InitializeComponent()
        {
            dataGrid = new DataGridView();
            BorderBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGrid)).BeginInit();
            SuspendLayout();
            // 
            // BorderBody
            // 
            BorderBody.Controls.Add(dataGrid);
            // 
            // dataGrid
            // 
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGrid.Dock = DockStyle.Fill;
            dataGrid.Location = new Point(3, 16);
            dataGrid.Name = "dataGrid";
            dataGrid.ReadOnly = true;
            dataGrid.Size = new Size(800, 490);
            dataGrid.TabIndex = 0;
            dataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            dataGrid.BackgroundColor = Color.White;
            dataGrid.BorderStyle = BorderStyle.None;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid.AllowUserToResizeRows = false;
            dataGrid.AllowUserToResizeColumns = false;
            //RowHeadersWidth = 12,
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.RowHeadersVisible = true;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            dataGrid.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Control;
            //dataGrid.DefaultCellStyle.BackColor = Color.LightYellow;
            dataGrid.RowsDefaultCellStyle.BackColor = Color.LightYellow;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Yellow;
            dataGrid.CellDoubleClick += new DataGridViewCellEventHandler(dataGrid_CellDoubleClick);
            BorderBody.ResumeLayout(false);
            Icon = Properties.Resources.Tabelle_ico;
            ((System.ComponentModel.ISupportInitialize)(errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGrid)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        protected override void _OnLoad(EventArgs e)
        {
            dataGrid.DataSource = dataTable;
            dataGrid.ClearSelection();
        }
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            list = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                string colName = dataGrid.Rows[e.RowIndex].Cells[i].OwningColumn.DataPropertyName;
                string value = dataGrid.Rows[e.RowIndex].Cells[colName].Value.ToString();

                list.Add(new KeyValuePair<string, string>(colName, value));
            }
            dataSelected = true;
            Close();
        }
        public List<KeyValuePair<string, string>> GetSelectedRow()
        {
            return list;
        }
        protected override string _name()
        {
            return "Suchergbnis";
        }
        public bool DataSelected()
        {
            return dataSelected;
        }
    }
}
