using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    // Klasse with placeholderText für TextBox
    public abstract class TextBox_Base : TextBox, ICustomControl
    {
        #region Variablen
        private delegate void OnTextChangeEditHandler(Object sender, EventArgs e);
        private event OnTextChangeEditHandler TextBoxEdit;
        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool textHasChanged = false;
        private bool isPasswordChar = false;

        #endregion

        #region Eigenschaft
        [DefaultValue(false)]
        public bool IsPasswordChar
        {
            get => isPasswordChar;
            set
            {
                isPasswordChar = value;
                if (!isPlaceholder)
                    UseSystemPasswordChar = value;
            }
        }
        public string Texts
        {
            get
            {
                if (isPlaceholder) return "";
                else return Text;
            }
            set
            {
                Text = value;
                SetPlaceholder();
            }
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
        [DefaultValue("")]
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
        #endregion

        #region private
        protected void SetPlaceholder()
        {
            if (string.IsNullOrEmpty(Text) && placeholderText != "")
            {
                isPlaceholder = true;
                Text = placeholderText;
                ForeColor = placeholderColor;
                if (isPasswordChar)
                    UseSystemPasswordChar = false;
            }
            else
            {
                isPlaceholder = false;
                ForeColor = Color.Black;
                if (isPasswordChar)
                    UseSystemPasswordChar = true;
            }
        }
        private void RemovePlaceholder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                textHasChanged = false;
                Text = "";
                ForeColor = Color.Black;
                if (isPasswordChar)
                    UseSystemPasswordChar = true;
            }
        }
        #endregion

        #region override
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                TextBoxEdit += handler.OnEditMask;
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            textHasChanged = true;
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (textHasChanged && Enabled)
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Delete))
            {
                textHasChanged = true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
        public abstract ControlRole Role { get; }
    }
}
