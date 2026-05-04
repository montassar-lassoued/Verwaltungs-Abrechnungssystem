using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyCircle : Control
    {
        private Color fillColor = Color.Blue;

        public Color FillColor
        {
            get => fillColor;
            set
            {
                fillColor = value;
                Invalidate(); // neu zeichnen
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.DoubleBuffered = true; // Flackern vermeiden
            //this.Size = new Size(100, 100); // Standardgröße
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (SolidBrush brush = new SolidBrush(fillColor))
            {
                e.Graphics.FillEllipse(brush, ClientRectangle);
            }
        }
    }
}
