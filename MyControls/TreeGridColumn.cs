using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public class TreeGridColumn : DataGridViewTextBoxColumn
    {
        public TreeGridColumn()
        {
            HeaderCell.Style.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HeaderCell.Style.BackColor = Color.FromArgb(102, 178, 255);
            HeaderCell.Style.SelectionBackColor = HeaderCell.Style.BackColor;
            
            CellTemplate = new TreeGridCell();     
        }
    }
}