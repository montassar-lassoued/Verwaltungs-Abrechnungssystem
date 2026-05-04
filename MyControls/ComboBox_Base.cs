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
    public abstract class ComboBox_Base : ComboBox, ICustomControl
    {
        #region Variablen
        private delegate void OnTextChangeEditHandler(Object sender, EventArgs e);
        private event OnTextChangeEditHandler TextBoxEdit;

        #endregion


        #region override
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            ForeColor = Color.Black;
            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                TextBoxEdit += handler.OnEditMask;
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (Enabled)
                onTextBoxEdit();
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (Enabled) { }
                //onTextBoxEdit();
        }
        protected override void OnSelectedItemChanged(EventArgs e)
        {
            base.OnSelectedItemChanged(e);
            /*if (Enabled)
                onTextBoxEdit();*/
        }
        protected override void OnSelectionChangeCommitted(EventArgs e)
        {
            base.OnSelectionChangeCommitted(e);
            if (Enabled)
                onTextBoxEdit();
        }
        #endregion

        #region virtual
        protected virtual void onTextBoxEdit()
        {
            if (TextBoxEdit != null)
            {
                TextBoxEdit(this, EventArgs.Empty);
            }
        }
        #endregion

        #region abstract

        public abstract void ClearField();
        public abstract void ActivateSearchMode();
        public abstract void DeactivateSearchMode();
        public void OnEnableControl()
        {
            Enabled = true;
        }
        public void OnDisableControl()
        {
            Enabled = false;
        }

        public abstract void RegisterTo(Form parentForm);
        public abstract ControlRole Role { get; }
        #endregion

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Font Font
        {
            get => base.Font;
            private set => base.Font = value; // nur intern/privat
        }
        
    }
}
