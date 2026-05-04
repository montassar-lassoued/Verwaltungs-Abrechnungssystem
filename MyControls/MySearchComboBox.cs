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
    public class MySearchComboBox : ComboBox_Base
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
                //host.RegisterControl(this);
            }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = Color.PaleTurquoise;
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
    }
}
