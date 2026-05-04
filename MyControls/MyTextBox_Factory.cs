using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class MyTextBox_Factory : TextBox_Base
    {
        //private string standardMask;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            string t = Text;
            //Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            //standardMask = Text;
            Text = t;
            TextAlign = HorizontalAlignment.Center;
        }
        /*protected override string _StandardMask()
        {
            return standardMask;
        }*/
       /* [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new string Mask
        {
            get => base.Mask;
            private set => base.Mask = value; // nur intern/privat
        }*/
        /*[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        }*/

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 45)
            {

                int cursorPosition = Text.Length - SelectionStart;      // Text in the box and Cursor position

                if (e.KeyChar == 45)
                    Text = Text[0] == 45 ? Text = Text : "-" + Text;
                else
                    if (Text.Length < 20)
                    Text = (decimal.Parse(Text.Insert(SelectionStart, e.KeyChar.ToString())
                                            .Replace(",", "").Replace(".", "")) / 100).ToString("N2");

                SelectionStart = (Text.Length - cursorPosition < 0 ? 0 : Text.Length - cursorPosition);
            }
            e.Handled = true;
            base.OnKeyPress(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)     // Deals with BackSpace e Delete keys
            {
                int cursorPosition = Text.Length - SelectionStart;

                string Left = Text.Substring(0, Text.Length - cursorPosition).Replace(".", "").Replace(",", "");
                string Right = Text.Substring(Text.Length - cursorPosition).Replace(".", "").Replace(",", "");

                if (Left.Length > 0)
                {
                    Left = Left.Remove(Left.Length - 1);                            // Take out the rightmost digit
                    Text = (decimal.Parse(Left + Right) / 100).ToString("N2");
                    SelectionStart = (Text.Length - cursorPosition < 0 ? 0 : Text.Length - cursorPosition);
                }
                e.Handled = true;
            }

            if (e.KeyCode == Keys.End)                                  // Treats End key
            {
                SelectionStart = Text.Length;                       // Moves the cursor o the rightmost position
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Home)                                 // Trata tecla Home
            {
                Text = 0.ToString("N2");                              // Set field value to zero 
                SelectionStart = Text.Length;                       // Moves the cursor o the rightmost position
                e.Handled = true;
            }
        }
        protected override void OnEnter(EventArgs e)
        {
            SelectionStart = Text.Length;
            base.OnEnter(e);
        }

    }
}
