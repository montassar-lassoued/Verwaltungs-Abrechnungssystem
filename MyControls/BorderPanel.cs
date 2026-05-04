using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class BorderedPanel : Panel
    {
        public Color BorderColor { get; set; } = Color.Black;
        public int BorderThickness { get; set; } = 1;

        public BorderedPanel()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var pen = new Pen(BorderColor, BorderThickness))
            {
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
    }
}
