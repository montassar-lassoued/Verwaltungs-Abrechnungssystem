using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MyControls
{
    [ToolboxItem(true)]
    public class MyCheckedListBox : CheckedListBox, ICustomControl
    {
        private delegate void OnTextChangeEditHandler(Object sender, EventArgs e);
        private event OnTextChangeEditHandler TextBoxEdit;
        public ControlRole Role => ControlRole.Input;

        public void ActivateSearchMode()
        {
        }

        public void ClearField()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                SetItemChecked(i, false);
            }
        }
        public void DeactivateSearchMode()
        {
        }

        public void OnDisableControl()
        {
            Enabled = false;
        }

        public void OnEnableControl()
        {
            Enabled = false;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (FindForm() is Intf_WinFormsBase host)
                RegisterTo(FindForm());
        }
        public void RegisterTo(Form parentForm)
        {
            if (parentForm is Intf_WinFormsBase host)
            {
                host.RegisterControl(this);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = System.Drawing.SystemColors.Control;
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FormattingEnabled = true;
            MultiColumn = true;
            Name = "MyCheckedListBox";
            TabIndex = 0;
            CheckOnClick = true;
            DrawMode = DrawMode.OwnerDrawVariable;
            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                TextBoxEdit += handler.OnEditMask;
            }
        }
        protected override void OnItemCheck(ItemCheckEventArgs ice)
        {
            base.OnItemCheck(ice);
            if (TextBoxEdit != null)
            {
                TextBoxEdit(this, EventArgs.Empty);
            }
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e = new DrawItemEventArgs(e.Graphics,
                                         e.Font,
                                         e.Bounds,
                                         e.Index,
                                         (e.State & DrawItemState.Focus) == DrawItemState.Focus ? DrawItemState.Focus : DrawItemState.None,
                                         Color.Black,
                                         SystemColors.Control); // Choose the color.
            base.OnDrawItem(e);

            int maxWidth = 0;
            foreach (object obj in Items)
            {

                string text = obj?.ToString() ?? string.Empty;
                int w = TextRenderer.MeasureText(text, Font).Width;
                if (w > maxWidth) maxWidth = w;
            }

            // Platz für Checkbox + etwas Puffer hinzufügen (je nach DPI evtl. anpassen)
            ColumnWidth = maxWidth + 32;
        }
    }
}