using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace MyControls
{
    public partial class RowMergeView : DataGridView
    {
        private List<string> SumaryColumns;
        private const int SumaryRowIndex = 0;
        private bool ShowSumaryRow = false;
        private Dictionary<int, SpanInfo> SpanRows = new Dictionary<int, SpanInfo>();
        private Color _mergecolumnheaderbackcolor = System.Drawing.SystemColors.Control;
        private bool pbSpeichernActivated;
        public delegate void OnTextChangeEditHandler(Object sender, EventArgs e);
        public event OnTextChangeEditHandler TextBoxEdit;

        public RowMergeView()
        {
            InitializeComponent();
        }
        public void setForm(Form form)
        {
            Intf_WinFormsBase b = form as Intf_WinFormsBase;
            if (b != null)
            {
                TextBoxEdit += b.OnEditMask;
            }
        }
        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            base.OnCellBeginEdit(e);
            if (!pbSpeichernActivated && TextBoxEdit != null && Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                TextBoxEdit(this, EventArgs.Empty);
            }
        }
        internal void Clear()
        {
            SumaryColumns = new List<string>();
            ShowSumaryRow = false;
            SpanRows.Clear();
        }

        #region 
        protected override void OnPaint(PaintEventArgs pe)
        {

            base.OnPaint(pe);
        }
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (ShowSumaryRow)
                {
                    if (e.RowIndex == SumaryRowIndex && !SumaryColumns.Contains(Columns[e.ColumnIndex].Name))
                    {
                        // Hier wird die Zelle versteckt (nicht gezeichnet).
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Border | DataGridViewPaintParts.Background);
                        e.Handled = true;  // Markieren Sie die Zelle als behandelt, damit keine weitere Darstellung erfolgt.
                    }
                }

                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    DrawCell(e);
                }
                else
                {
                    if (e.RowIndex == -1)
                    {
                        if (SpanRows.ContainsKey(e.ColumnIndex))
                        {
                            Graphics g = e.Graphics;
                            e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                            int left = e.CellBounds.Left, top = e.CellBounds.Top + 2,
                            right = e.CellBounds.Right, bottom = e.CellBounds.Bottom;

                            switch (SpanRows[e.ColumnIndex].Position)
                            {
                                case 1:
                                    left += 2;
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    right -= 2;
                                    break;
                            }

                            g.FillRectangle(new SolidBrush(this._mergecolumnheaderbackcolor), left, top,
                            right - left, (bottom - top) / 2);

                            g.DrawLine(new Pen(this.GridColor), left, (top + bottom) / 2,
                            right, (top + bottom) / 2);

                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Center;

                            g.DrawString(e.Value + "", e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, (top + bottom) / 2, right - left, (bottom - top) / 2), sf);
                            left = this.GetColumnDisplayRectangle(SpanRows[e.ColumnIndex].Left, true).Left - 2;

                            if (left < 0) left = this.GetCellDisplayRectangle(-1, -1, true).Width;
                            right = this.GetColumnDisplayRectangle(SpanRows[e.ColumnIndex].Right, true).Right - 2;
                            if (right < 0) right = this.Width;

                            g.DrawString(SpanRows[e.ColumnIndex].Text, e.CellStyle.Font, Brushes.Black,
                            new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                            e.Handled = true;
                        }
                    }
                }

                base.OnCellPainting(e);
            }
            catch
            { }
        }

        internal void EndEditMode()
        {
            pbSpeichernActivated = false;
        }
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
        }
        #endregion
        #region
        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush gridBrush = new SolidBrush(this.GridColor);
            SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int cellwidth;
            int UpRows = 0;
            int DownRows = 0;
            int count = 0;
            if (MergeColumnNames != null && MergeColumnNames.Contains(this.Columns[e.ColumnIndex].Name) && e.RowIndex != -1)
            {
                cellwidth = e.CellBounds.Width;
                Pen gridLinePen = new Pen(gridBrush);
                string curValue = e.Value == null ? "" : e.Value.ToString().Trim();
                string curSelected = this.CurrentRow.Cells[e.ColumnIndex].Value == null ? "" : this.CurrentRow.Cells[e.ColumnIndex].Value.ToString().Trim();
                if (!string.IsNullOrEmpty(curValue))
                {
                    #region 
                    for (int i = e.RowIndex; i < this.Rows.Count; i++)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                            DownRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    #region
                    for (int i = e.RowIndex; i >= 0; i--)
                    {
                        if (this.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;
                            UpRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < this.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : this.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    #endregion
                    count = DownRows + UpRows - 1;
                    if (count < 2)
                    {
                        return;
                    }
                }
                if (this.Rows[e.RowIndex].Selected)
                {
                    backBrush.Color = e.CellStyle.SelectionBackColor;
                    fontBrush.Color = e.CellStyle.SelectionForeColor;
                }
                e.Graphics.FillRectangle(backBrush, e.CellBounds);
                PaintingFont(e, cellwidth, UpRows, DownRows, count);
                if (DownRows == 1)
                {
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    count = 0;
                }
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                e.Handled = true;
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cellwidth"></param>
        /// <param name="UpRows"></param>
        /// <param name="DownRows"></param>
        /// <param name="count"></param>
        private void PaintingFont(System.Windows.Forms.DataGridViewCellPaintingEventArgs e, int cellwidth, int UpRows, int DownRows, int count)
        {
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int fontheight = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
            int fontwidth = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width;
            int cellheight = e.CellBounds.Height;

            if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.BottomRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y + cellheight * DownRows - fontheight);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopLeft)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else if (e.CellStyle.Alignment == DataGridViewContentAlignment.TopRight)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellwidth - fontwidth, e.CellBounds.Y - cellheight * (UpRows - 1));
            }
            else
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
        }
        #endregion
        #region
        /// <summary>
        /// </summary>
        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        public List<string> MergeColumnNames
        {
            get => _mergecolumnname;
            set => _mergecolumnname = value;
        }
        private List<string> _mergecolumnname = new List<string>();
        #endregion
        #region 
        private struct SpanInfo
        {
            public SpanInfo(string Text, int Position, int Left, int Right)
            {
                this.Text = Text;
                this.Position = Position;
                this.Left = Left;
                this.Right = Right;
            }

            public string Text; //
            public int Position; //
            public int Left; //
            public int Right; //
        }
        //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ColIndex"></param>
        /// <param name="ColCount"></param>
        /// <param name="Text"></param>
        public void AddSpanHeader(int ColIndex, int ColCount, string Text)
        {
            if (ColCount < 2)
            {
                throw new Exception("");
            }
            //
            int Right = ColIndex + ColCount - 1; //
            SpanRows[ColIndex] = new SpanInfo(Text, 1, ColIndex, Right); //
            SpanRows[Right] = new SpanInfo(Text, 3, ColIndex, Right); //
            for (int i = ColIndex + 1; i < Right; i++) //
            {
                SpanRows[i] = new SpanInfo(Text, 2, ColIndex, Right);
            }
        }
        public void addSummaryRow(params string[] _columns)
        {
            if (_columns.Length == 0)
            {
                return;
            }
            SumaryColumns = new List<string>(_columns);
            ShowSumaryRow = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClearSpanInfo()
        {
            SpanRows.Clear();
            //ReDrawHead();
        }
        public void ReDrawHead()
        {
            foreach (int si in SpanRows.Keys)
            {
                this.Invalidate(this.GetCellDisplayRectangle(si, -1, true));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color MergeColumnHeaderBackColor
        {
            get => this._mergecolumnheaderbackcolor;
            set => this._mergecolumnheaderbackcolor = value;
        }
        #endregion
        public override DataGridViewAdvancedBorderStyle AdjustColumnHeaderBorderStyle(
            DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput,
            DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceHolder,
            bool firstDisplayedColumn,
            bool lastVisibleColumn)
        {
            // Customize the left border of the first column header and the
            // bottom border of all the column headers. Use the input style for 
            // all other borders.
            dataGridViewAdvancedBorderStylePlaceHolder.Left = firstDisplayedColumn ?
                DataGridViewAdvancedCellBorderStyle.OutsetDouble :
                DataGridViewAdvancedCellBorderStyle.None;
            dataGridViewAdvancedBorderStylePlaceHolder.Bottom =
                DataGridViewAdvancedCellBorderStyle.Single;

            dataGridViewAdvancedBorderStylePlaceHolder.Right =
                dataGridViewAdvancedBorderStyleInput.Right;
            dataGridViewAdvancedBorderStylePlaceHolder.Top =
                dataGridViewAdvancedBorderStyleInput.Top;

            return dataGridViewAdvancedBorderStylePlaceHolder;
        }
        public void SetDataSource(System.Data.DataTable dataTable)
        {
            DataSource = null;
            DataSource = dataTable;

            if (ShowSumaryRow)
            {
                DataRow newRow = dataTable.NewRow();
                dataTable.Rows.InsertAt(newRow, SumaryRowIndex);
                foreach (string col in SumaryColumns)
                {
                    var sum = dataTable.Compute("Sum(" + col + ")", "");
                    newRow[col] = Convert.ToDecimal(sum== DBNull.Value? 0 : sum);
                }

                dataTable.AcceptChanges();
                newRow.SetModified();

                FormattingSumaryRow();
            }


        }
        private void FormattingSumaryRow()
        {
            Rows[SumaryRowIndex].DefaultCellStyle.BackColor = Color.LightGray;
            Rows[SumaryRowIndex].DefaultCellStyle.SelectionBackColor = Color.LightGray;
            Rows[SumaryRowIndex].HeaderCell.Style.BackColor = Color.Orange;
            Rows[SumaryRowIndex].HeaderCell.Value = "$";
            foreach (string col in SumaryColumns)
            {
                Rows[SumaryRowIndex].Cells[col].Style.BackColor = Color.Orange;
                Rows[SumaryRowIndex].Cells[col].Style.SelectionBackColor = Color.Orange;
            }
            Rows[SumaryRowIndex].Frozen = true;
            for (int i = 0; i < Columns.Count; i++)
            {
                Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        private void _MouseClick(object sender, MouseEventArgs e)
        {
            // Prüfen, ob Klick im Top-Left Header war
            Rectangle headerCell = GetCellDisplayRectangle(-1, -1, true);
            if (headerCell.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                ShowColorSummaryMenu(e.Location);
            }
        }
        private void ShowColorSummaryMenu(Point location)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            // Farben zählen
            var colorCounts = new Dictionary<Color, int>();

            foreach (DataGridViewRow row in Rows)
            {
                //HeaderCell.Style.BackColor
                if (row.IsNewRow) continue; // letzte leere Zeile überspringen
                Color c = row.HeaderCell.Style.BackColor;
                if (colorCounts.ContainsKey(c))
                    colorCounts[c]++;
                else
                    colorCounts[c] = 1;
            }

            // Menüitems erstellen
            foreach (var kvp in colorCounts)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = $" {kvp.Value}"; // Anzahl
                item.Image = CreateColorCircleIcon(kvp.Key, 16); // Kreis in Farbe
                menu.Items.Add(item);
            }

            menu.Show(this, location);
        }
        private Bitmap CreateColorCircleIcon(Color color, int size)
        {
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Brush b = new SolidBrush(color))
                {
                    g.FillEllipse(b, 0, 0, size - 1, size - 1);
                }
            }
            return bmp;
        }
    }
}
