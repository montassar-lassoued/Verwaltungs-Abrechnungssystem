using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class TreeGridView : DataGridView
    {
        public delegate void TreeGridViewCellEventHandler(object sender, TreeGridViewCellEventArgs e);
        public event TreeGridViewCellEventHandler CellDoubleClick;

        private List<TreeGridNode> _rootNodes = new List<TreeGridNode>();

        public TreeGridView()
        {
            /*AllowUserToAddRows = allowAddAndDeleteRows;
             AllowUserToDeleteRows = allowAddAndDeleteRows;*/
            AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
            BackgroundColor = Color.White;
            BorderStyle = BorderStyle.Fixed3D;
            MultiSelect = false;
            //ReadOnly = !allowAddAndDeleteRows;

            ColumnHeadersHeight = 25;
            /*RowHeadersWidth = 20;
            RowTemplate.Height = 30;
            RowTemplate.MinimumHeight = 30;*/
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DefaultCellStyle.SelectionBackColor = DefaultCellStyle.BackColor;
            //DefaultCellStyle.SelectionForeColor = DefaultCellStyle.ForeColor;
            RowHeadersDefaultCellStyle.SelectionBackColor = Color.Khaki;

            AllowUserToResizeRows = false;
            AllowUserToResizeColumns = false;
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            EnableHeadersVisualStyles = false;
            RowHeadersVisible = true;

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;// | DataGridViewAutoSizeColumnsMode.DisplayedCells;
            CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            DefaultCellStyle.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(102, 178, 255);
            ColumnHeadersDefaultCellStyle.SelectionBackColor = ColumnHeadersDefaultCellStyle.BackColor;
            DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ResizeRedraw = true;
            DoubleBuffered = true;

            MouseClick += _MouseClick;
        }
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
        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= Rows.Count)
                return;

            DataGridViewSelectedCellCollection cellCollection = SelectedCells;

            if (Rows[e.RowIndex] is TreeGridNode node && node.Children.Count > 0 && e.ColumnIndex == 0)
            {
                if (node.Tag is Rectangle glyphRect)
                {
                    // Koordinaten umrechnen – Mausposition innerhalb der Zelle:
                    Rectangle cellRect = GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

                    Point relativeClick = new Point(e.X + cellRect.X, e.Y + cellRect.Y);

                    if (glyphRect.Contains(relativeClick))
                    {
                        node.IsExpanded = !node.IsExpanded;
                        RebuildRows();
                    }
                }
            }
            ClearSelection();
            foreach (DataGridViewCell cell in cellCollection)
            {
                if (cell.Visible)
                {
                    cell.Selected = true;
                }
            }
        }
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            base.OnCellDoubleClick(e);
            if (e.RowIndex < 0 || e.RowIndex >= Rows.Count)
                return;
            if (Rows[e.RowIndex] is TreeGridNode node)
            {
                TreeGridViewCellEventArgs e1 = new TreeGridViewCellEventArgs(e.ColumnIndex, e.RowIndex, node);
                CellDoubleClick?.Invoke(this, e1);
            }
        }
        public void AddRoot(TreeGridNode node)
        {
            _rootNodes.Add(node);
            RebuildRows();
        }

        private void RebuildRows()
        {
            Rows.Clear();
            foreach (var node in _rootNodes)
            {
                AddNodeRecursive(node);
            }
        }

        private void AddNodeRecursive(TreeGridNode node)
        {
            Rows.Add(node);
            if (node.IsExpanded)
            {
                foreach (var child in node.Children)
                    AddNodeRecursive(child);
            }
        }

        public void BuildTreeRecursive(DataTable dataTable, string[] hierarchyColumns, string treeColumnName)
        {
            Rows.Clear();
            Columns.Clear();
            DataSource = null;
            _rootNodes.Clear();
            dataTable.DefaultView.Sort = dataTable.Columns[0].ColumnName + " ASC," + dataTable.Columns[1].ColumnName + " ASC";
            dataTable = dataTable.DefaultView.ToTable();

            CreateColumns(dataTable, hierarchyColumns, treeColumnName);
            var rootNodes = new Dictionary<string, TreeGridNode>();

            foreach (DataRow row in dataTable.Rows)
            {
                AddNodeRecursive(row, hierarchyColumns, 0, null, rootNodes);
            }
        }

        private void CreateColumns(DataTable dataTable, string[] hierarchyColumns, string treeColumnName)
        {
            Columns.Add(new TreeGridColumn { HeaderText = treeColumnName, 
                Name = treeColumnName, 
                SortMode = DataGridViewColumnSortMode.NotSortable,
                HeaderCell = getHeaderCell(),     
            });
            foreach (DataColumn cl in dataTable.Columns)
            {
                if (!hierarchyColumns.Contains(cl.ColumnName))
                {
                    Columns.Add(new DataGridViewTextBoxColumn { 
                        HeaderText = cl.ColumnName, 
                        Name = cl.ColumnName, 
                        SortMode = DataGridViewColumnSortMode.NotSortable,
                        HeaderCell= getHeaderCell(),
                });
                }
            }
        }
        private DataGridViewColumnHeaderCell getHeaderCell()
        {
            DataGridViewColumnHeaderCell HeaderCell = new DataGridViewColumnHeaderCell();
            HeaderCell.Style.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HeaderCell.Style.BackColor = Color.FromArgb(102, 178, 255);
            HeaderCell.Style.SelectionBackColor = HeaderCell.Style.BackColor;
            

            return HeaderCell;
        }
        private void AddNodeRecursive(
       DataRow row,
       string[] hierarchyColumns,
       int level,
       TreeGridNode parent,
       Dictionary<string, TreeGridNode> cache)
        {
            if (level >= hierarchyColumns.Length)
            {
                // Letzte Ebene – füge Datenzeile hinzu
                var values = new List<object>();

                foreach (DataColumn col in row.Table.Columns)
                {
                    string val = row[col.ColumnName]?.ToString() ?? string.Empty;

                    // Letzte Hierarchie-Spalte leer setzen
                    if (hierarchyColumns.LastOrDefault()?.Equals(col.ColumnName) == true)
                    {
                        val = string.Empty;
                    }
                    // Hierarchie-Spalten überspringen
                    else if (hierarchyColumns.Contains(col.ColumnName))
                    {
                        continue;
                    }

                    values.Add(val);
                }

                var dataNode = new TreeGridNode(values.ToArray());
                if (parent != null)
                {
                    parent.Children.Add(dataNode);
                    dataNode.Parent = parent;
                }
                else
                {
                    AddRoot(dataNode);
                }

                return;
            }

            string columnName = hierarchyColumns[level];
            string key = GetNodeKey(row, hierarchyColumns, level);

            if (!cache.TryGetValue(key, out TreeGridNode node))
            {
                // --- NEU: Parents ebenfalls mit Werten + ID erstellen ---
                var values = new object[this.Columns.Count];
                for (int i = 0; i < values.Length; i++)
                    values[i] = string.Empty;

                // ID setzen (falls vorhanden)
                if (row.Table.Columns.Contains("ID"))
                {
                    string idValue = row["ID"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(idValue))
                        values[Columns["ID"].Index] = idValue;
                }

                // Hierarchie-Text setzen
                int colIndex = row.Table.Columns.IndexOf(columnName);
                if (colIndex >= 0)
                    values[0] = row[columnName]?.ToString() ?? string.Empty;

                node = new TreeGridNode(values);

                cache[key] = node;

                if (parent != null)
                {
                    parent.Children.Add(node);
                    node.Parent = parent;
                }
                else
                {
                    AddRoot(node);
                }
            }

            AddNodeRecursive(row, hierarchyColumns, level + 1, node, cache);
        }
        string GetNodeKey(DataRow row, string[] hierarchyColumns, int depth)
        {
            return string.Join("|", hierarchyColumns.Take(depth + 1).Select(col => row[col].ToString()));
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
