using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyFieldText : MyTextBox_Text
    {
        public override ControlRole Role => ControlRole.Input;

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
            BackColor = SystemColors.ControlLightLight;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Color BackColor
        {
            get => base.BackColor;
            private set => base.BackColor = value; // nur intern/privat
        }
        public override void ActivateSearchMode()
        {
        }
        public override void DeactivateSearchMode()
        {
        }
        public override void ClearField()
        {
            ResetText();
            Invalidate();
            SetPlaceholder();
        }
    }
}
