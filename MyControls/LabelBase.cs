using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class LabelBase : Label, ICustomControl
    {
        public abstract ControlRole Role { get; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            AutoSize = true;
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            TextAlign = ContentAlignment.MiddleCenter;
            ForeColor = Color.Black;
        }
        public abstract void ActivateSearchMode();
        public abstract void ClearField();
        public abstract void DeactivateSearchMode();
        public abstract void OnDisableControl();
        public abstract void OnEnableControl();
        public abstract void RegisterTo(Form parentForm);
    }
}
