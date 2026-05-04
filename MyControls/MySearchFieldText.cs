using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MySearchFieldText : MyTextBox_Text
    {
        public override ControlRole Role => ControlRole.Search;

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
        public override void DeactivateSearchMode()
        {
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = Color.PaleTurquoise;
        }

        public override void ClearField()
        {
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
