using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyNumericField : MyNumericTextBox
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
        public override void ActivateSearchMode()
        {
        }

        public override void ClearField()
        {
            ResetText();
            SetPlaceholder();
        }

        public override void DeactivateSearchMode()
        {
        }
    }
}
