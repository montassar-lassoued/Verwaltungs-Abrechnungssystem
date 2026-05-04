using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class ModusCheckBox : CheckBox, ICustomControl
    {
        public delegate void OnStateChangeEditHandler(Object sender, MyEventArgs e);
        public event OnStateChangeEditHandler StateEdit;
        //public event OnStateChangeEditHandler StateEdit;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            Text = "Suchmodus - ctrl+s";
            Name = "Suchmodus";
            UseVisualStyleBackColor = true;

            //****event regestrieren
            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                StateEdit += handler.OnSearchModeCheckBoxStateChanged;
            }

        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new string Text
        {
            get => base.Text;
            private set => base.Text = value; // nur intern/privat
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Color BackColor
        {
            get => base.BackColor;
            private set => base.BackColor = value; // nur intern/privat
        }
        public abstract ControlRole Role { get; }

        protected override void OnCheckStateChanged(EventArgs e)
        {
            base.OnCheckStateChanged(e);
            onEditState();
        }
        protected virtual void onEditState()
        {
            MyEventArgs e = new MyEventArgs(Checked);

            StateEdit?.Invoke(this, e);
        }

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
    }
}
