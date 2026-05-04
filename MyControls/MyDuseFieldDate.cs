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
    public class MyDuseFieldDate : MyMaskedTextBoxDate
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
        public override void ActivateSearchMode()
        {
            BackColor = Color.PaleTurquoise;
        }
        public override void DeactivateSearchMode()
        {
            BackColor = SystemColors.ControlLightLight;
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = SystemColors.ControlLightLight;
        }

        public override void ClearField()
        {
            ResetText();
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
