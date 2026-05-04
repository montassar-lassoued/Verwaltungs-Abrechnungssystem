using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyPushButton : MyPushButtonBase
    {
        public override ControlRole Role => ControlRole.None;

        protected override void OnHandleCreated(EventArgs e)
        {
            if (FindForm() is Intf_WinFormsBase host)
                RegisterTo(FindForm());
        }
        public override void RegisterTo(Form parentForm)
        {
            if (parentForm is Intf_WinFormsBase host)
            {
                host.RegisterControl(this);
            }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            RectangleF Rect = new RectangleF(0, 0, Width, Height);
            using (GraphicsPath GraphPath = GetRoundPath(Rect, 10))
            {
                Region = new Region(GraphPath);
                using (Pen pen = new Pen(Color.Transparent))
                {
                    pen.Alignment = PenAlignment.Center;
                    pe.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }
        public override void ActivateSearchMode()
        {
        }
        public override void ClearField()
        {
        }

        public override void DeactivateSearchMode()
        {
        }

        public override void OnDisableControl()
        {
            Enabled = false;
        }

        public override void OnEnableControl()
        {
            Enabled = true;
        }
        private GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                    Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }
    }
}
