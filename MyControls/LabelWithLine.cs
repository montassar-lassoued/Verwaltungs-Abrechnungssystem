using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class LabelWithLine : UserControl
    {
        private Label label;
        private Panel line;
        public Color LineColor { get; set; } = Color.AliceBlue;
        public LabelWithLine()
        {
            AutoSize = false;
            Height = 40; // Standardhöhe

            label = new Label
            {
                AutoSize = true,
                Location = new Point(0, 0)
            };

            line = new Panel
            {
                Height = 2,
                BackColor = Color.Red
            };

            Controls.Add(label);
            Controls.Add(line);

            Resize += (s, e) => UpdateLayout();
        }

        public string LabelText
        {
            get => label.Text;
            set
            {
                label.Text = value;
                UpdateLayout();
            }
        }

        private void UpdateLayout()
        {
            label.Location = new Point(0, 0);

            int lineY = label.Bottom + 5;
            int lineWidth = Parent?.ClientSize.Width / 2 ?? 100;

            line.Location = new Point(0, lineY);
            line.Width = lineWidth;
        }
    }
}
