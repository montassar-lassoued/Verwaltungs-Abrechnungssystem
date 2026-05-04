using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyControls
{
    public class TreeGridCell : DataGridViewTextBoxCell
    {
        Color cLevel0 = Color.PaleGreen;
        Color cLevel1 = Color.LightCyan;
        Color cLevel2 = Color.LightYellow;
        Color cLevel3 = Color.WhiteSmoke;
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
     int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue,
     string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
     DataGridViewPaintParts paintParts)
        {
            cellStyle.SelectionBackColor = cellStyle.BackColor;
            cellStyle.SelectionForeColor = Color.Black;
            cellStyle.Font = new Font("Calibri", 11F, FontStyle.Italic, GraphicsUnit.Point, 0);
            if (OwningRow is TreeGridNode node && OwningColumn is TreeGridColumn)
            {

                int indentPerLevel = 20;
                int level = node.Level;
                int baseX = cellBounds.X;
                int baseY = cellBounds.Y;
                int height = cellBounds.Height;
                int glyphX = baseX + level * indentPerLevel + 4;
                int glyphY = baseY + (height - 12) / 2;

                switch (level)
                {
                    case 0:
                        DataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = cLevel0;
                        break;
                    case 1:
                        DataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = cLevel1;
                        break;
                    case 2:
                        DataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = cLevel2;
                        break;
                    case 3:
                        DataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = cLevel3;
                        break;
                    default:
                        DataGridView.Rows[rowIndex].DefaultCellStyle.BackColor = Color.White;
                        break;
                }
                

                // Zeichne Hintergrund, Rahmen, Auswahl etc. ohne Text
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue,
                    errorText, cellStyle, advancedBorderStyle,
                    paintParts & ~DataGridViewPaintParts.ContentForeground);

                using (var pen = new Pen(Color.Gray))
                {
                    pen.DashStyle = DashStyle.Dot;

                    int centerY = baseY + height / 2;

                    // Vertikale Linien für Eltern-Levels
                    for (int levelIndex = 0; levelIndex < node.Level; levelIndex++)
                    {
                        TreeGridNode parent = node.GetAncestor(levelIndex);
                        if (parent == null) continue;

                        bool isLastVisibleChild = node.IsLastVisibleChild();
                        // Nur Linie zeichnen, wenn dieser ParentNode noch weitere Children hat
                        if (!isLastVisibleChild)
                        {
                            int x = baseX + levelIndex * indentPerLevel + indentPerLevel / 2;
                            graphics.DrawLine(pen, x, baseY, x, baseY + height);
                        }
                    }

                    // Horizontale Linie zur Glyph-Box (nur für aktuelle Node)
                    if (node.Level > 0)
                    {
                        int horizontalLineXStart = baseX + (node.Level - 1) * indentPerLevel + indentPerLevel / 2;
                        int horizontalLineXEnd = glyphX;
                        graphics.DrawLine(pen, horizontalLineXStart, centerY, horizontalLineXEnd, centerY);
                    }
                }
                // Plus/Minus-Glyphe
                if (node.Children.Count > 0)
                {
                    // Create pen.
                    Pen blackPen = new Pen(Color.Gray);


                    Rectangle glyphRect = new Rectangle(glyphX, glyphY - 2, 12, 12);

                    string glyph = node.IsExpanded ? "-" : "+";
                    TextRenderer.DrawText(graphics, glyph, cellStyle.Font, glyphRect, Color.Gray);
                    // Draw rectangle to screen.
                    graphics.DrawRectangle(blackPen, new Rectangle(glyphX - 1, glyphY - 1, 12, 12));
                    node.Tag = glyphRect;
                }

                // Text nach rechts verschieben, um Platz für Glyph zu schaffen
                int textIndent = (level + 1) * indentPerLevel;
                Rectangle textBounds = new Rectangle(cellBounds.X + textIndent, cellBounds.Y - 2,
                    cellBounds.Width - textIndent, cellBounds.Height);

                // Text manuell zeichnen
                TextRenderer.DrawText(graphics, formattedValue?.ToString() ?? "",
                    cellStyle.Font, textBounds, cellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);

                return; // verhindern, dass base.Paint nochmal Text zeichnet
            }

            // Normale Zellen wie gewohnt malen
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue,
                errorText, cellStyle, advancedBorderStyle, paintParts);

        }
    }
}
