using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(false)]
    public abstract class MyPushButtonBase : Button, ICustomControl
    {
        private bool _readOnly= false;

        public abstract ControlRole Role { get; }
        #region override
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = Color.Transparent;
            BackgroundImageLayout = ImageLayout.None;
            FlatAppearance.BorderSize = 2;
            FlatAppearance.MouseDownBackColor = Color.Lime;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            //Image = ButtonImage();
            ImageAlign = ContentAlignment.TopLeft;
            //TextAlign = ContentAlignment.MiddleRight;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            UseVisualStyleBackColor = false;

            Click += ClickEvent;
        }
        #endregion

        #region private
        private void ClickEvent(object sender, EventArgs e)
        {
            if (!ReadOnly)
            {
                OnClickEvent(sender, e);
            }
        }
        protected virtual void OnClickEvent(object sender, EventArgs e)
        {

        }
        public bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                if (_readOnly)
                {
                    BackColor = SystemColors.Control;
                    ForeColor = SystemColors.GrayText;
                    FlatAppearance.BorderSize = 0;
                    FlatAppearance.MouseDownBackColor = SystemColors.Control;
                    FlatAppearance.MouseOverBackColor = SystemColors.Control;
                }
                else
                {
                    BackColor = Color.Transparent;
                    ForeColor = Color.Black;
                    FlatAppearance.BorderSize = 2;
                    FlatAppearance.MouseDownBackColor = Color.Lime;
                    FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 255,192);
                }
            }
        }
        #endregion
        #region abstract
        //protected abstract string ButtonName();
        //protected abstract string ButtonText();
        public abstract void ClearField();
        public abstract void ActivateSearchMode();
        public abstract void DeactivateSearchMode();
        public abstract void OnEnableControl();
        public abstract void OnDisableControl();
        public abstract void RegisterTo(Form parentForm);
        #endregion
    }
}
