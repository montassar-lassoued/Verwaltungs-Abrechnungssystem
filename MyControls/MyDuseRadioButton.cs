using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyDuseRadioButton : RadioButtonBase
    {
        public override ControlRole Role => ControlRole.Both;

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
            BackColor = Color.Transparent;
        }
        public override void ActivateSearchMode()
        {
            BackColor = Color.PaleTurquoise;
        }

        public override void ClearField()
        {
            Checked = false;
        }

        public override void DeactivateSearchMode()
        {
            BackColor = Color.Transparent;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Color BackColor
        {
            get => base.BackColor;
            private set => base.BackColor = value; // nur intern/privat
        }
    }
}
