using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(false)]
    public partial class MyTreeDataGridView : DataGridView
    {
        private List<string> treeColumns = new List<string>();
        private Color treeLineColor;

        public MyTreeDataGridView()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {

            base.OnPaint(pe);
        }
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > 0 && e.ColumnIndex >= 0 && treeColumns.Count > 0 && treeColumns.Contains(Columns[e.ColumnIndex].Name) &&
                Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value))
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.Background);

                if (treeLineColor.IsEmpty)
                {
                    treeLineColor = Color.Black;
                }
                var pen = new Pen(treeLineColor);
                pen.DashStyle = DashStyle.Dot;

                e.Graphics.DrawLine(pen, e.CellBounds.Left + 5, e.CellBounds.Top + 1, e.CellBounds.Left + 5, e.CellBounds.Bottom - (e.CellBounds.Height / 2));
                e.Graphics.DrawLine(pen, e.CellBounds.Left + 5, e.CellBounds.Bottom - (e.CellBounds.Height / 2), e.CellBounds.Right - (e.CellBounds.Width / 2), e.CellBounds.Bottom - (e.CellBounds.Height / 2));

                e.Handled = true;
            }
            else
            {
                base.OnCellPainting(e);
            }
            //base.OnCellPainting(e);
        }
        public void addTreeColumns(params string[] _columns)
        {
            if (_columns.Length == 0)
            {
                return;
            }
            treeColumns = new List<string>(_columns);
        }
        public Color TreeLineColor { get => treeLineColor; set => treeLineColor = value; }
    }
}
