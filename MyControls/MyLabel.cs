using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyLabel : LabelBase
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
        }
        public override void OnEnableControl()
        {
        }
    }
}
