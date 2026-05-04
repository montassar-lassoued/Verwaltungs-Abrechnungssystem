using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class RadioButtonBase : RadioButton, ICustomControl
    {
        public delegate void OnStateChangeEditHandler(Object sender, EventArgs e);
        public event OnStateChangeEditHandler StateEdit;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            AutoSize = true;
            UseVisualStyleBackColor = true;

            //****event regestrieren
            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                StateEdit += handler.OnEditMask;
            }

        }
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            onEditState();
        }
        protected virtual void onEditState()
        {
            StateEdit?.Invoke(this, EventArgs.Empty);
        }
        #region abstract
        public abstract void ActivateSearchMode();
        public abstract void ClearField();
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
        #endregion
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new bool AutoSize
        {
            get => base.AutoSize;
            private set => base.AutoSize = value; // nur intern/privat
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Font Font
        {
            get => base.Font;
            private set => base.Font = value; // nur intern/privat
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new bool UseVisualStyleBackColor
        {
            get => base.UseVisualStyleBackColor;
            private set => base.UseVisualStyleBackColor = value; // nur intern/privat
        }
        public abstract ControlRole Role { get; }
    }
}
