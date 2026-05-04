using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyControls
{
    public partial class AuswahlDialog : Form
    {
        private List<string> selectedColumns;
        private DataGridView dataGridView;
        public AuswahlDialog(DataGridView dgv)
        {
            dataGridView = dgv;
            InitializeComponent();
            
        }
        protected override void OnLoad(EventArgs e)
        {
            getColumnsFromDataGrid(dataGridView);
        }
        private void getColumnsFromDataGrid(DataGridView dgv)
        {   
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                checkedListColumns.Items.Add(column.Name, true);
            }
        }
        public List<string> getColumnSelected()
        {
            return selectedColumns;
        }
        private void PbSpeichern_Click(object sender, System.EventArgs e)
        {
            selectedColumns = new List<string>();
            selectedColumns.AddRange(checkedListColumns.CheckedItems.OfType<string>().ToList());
            this.DialogResult = DialogResult.OK;
            Close();

        }

    }
}
