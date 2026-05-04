using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class MaskedTextBox_Base : MaskedTextBox, ICustomControl
    {
        #region Variablen
        private delegate void OnTextChangeEditHandler(Object sender, EventArgs e);
        private event OnTextChangeEditHandler TextBoxEdit;
        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;
        private bool valueHasChanged = false;
        #endregion

        #region Eigenschaft
        public string Texts
        {
            get
            {
                if (isPlaceholder) return "";
                else if (Text.Equals(_StandardMask()))
                    return "";
                else return Text;
            }
            set
            {
                Text = value;
                SetPlaceholder();
            }
        }

        protected virtual string _StandardMask()
        {
            return "";
        }

        public Color PlaceholderColor
        {
            get => placeholderColor;
            set
            {
                placeholderColor = value;
                if (isPlaceholder)
                    ForeColor = value;
            }
        }
        public string PlaceholderText
        {
            get => placeholderText;
            set
            {
                placeholderText = value;
                Text = "";
                SetPlaceholder();
            }
        }

        public abstract ControlRole Role { get; }
        #endregion

        #region private
        protected void SetPlaceholder()
        {
            if ((Text.Equals("  .  .") || string.IsNullOrWhiteSpace(Text)) && placeholderText != "")
            {
                isPlaceholder = true;
                Text = placeholderText;
                ForeColor = placeholderColor;
                if (isPasswordChar)
                    UseSystemPasswordChar = false;
            }
        }
        private void RemovePlaceholder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                Text = "";
                ForeColor = this.ForeColor;
                if (isPasswordChar)
                    UseSystemPasswordChar = true;
            }
        }
        #endregion

        #region override
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                TextBoxEdit += handler.OnEditMask;
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            valueHasChanged = true;
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (valueHasChanged)
                onTextBoxEdit();
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            Invalidate();
            RemovePlaceholder();
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Invalidate();
            SetPlaceholder();
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
        public void OnEnableControl()
        {
            Enabled = true;
        }
        public void OnDisableControl()
        {
            Enabled = false;
        }

        public abstract void ClearField();
        public abstract void ActivateSearchMode();
        public abstract void DeactivateSearchMode();
        public abstract void RegisterTo(Form parentForm);
        #endregion

    }
}
