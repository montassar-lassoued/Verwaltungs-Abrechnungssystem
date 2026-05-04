using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyGroupBox : GroupBox
    {
        public Color BorderColor { get; set; } = Color.Red;   // Standard-Rahmenfarbe
        public int BorderThickness { get; set; } = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Basis zeichnen lassen (Text usw.)
            base.OnPaint(e);

            // Rahmen neu zeichnen
            var g = e.Graphics;
            var rect = ClientRectangle;

            // Textbreite berechnen, damit der Rahmen nicht durch den Text geht
            Size textSize = TextRenderer.MeasureText(Text, Font);
            int textOffset = textSize.Width > 0 ? textSize.Width + 8 : 0;

            using (var pen = new Pen(BorderColor, BorderThickness))
            {
                // Obere Linie: von links bis zum Textanfang
                g.DrawLine(pen, rect.Left, rect.Top + (Font.Height / 2), rect.Left + 5, rect.Top + (Font.Height / 2));
                g.DrawLine(pen, rect.Left + textOffset, rect.Top + (Font.Height / 2), rect.Right - 2, rect.Top + (Font.Height / 2));

                // Restliche Linien
                g.DrawLine(pen, rect.Left, rect.Top + (Font.Height / 2), rect.Left, rect.Bottom - 2);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right - 2, rect.Bottom - 2);
                g.DrawLine(pen, rect.Right - 2, rect.Top + (Font.Height / 2), rect.Right - 2, rect.Bottom - 2);
            }
        }
    }
}
