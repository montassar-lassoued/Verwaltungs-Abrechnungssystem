using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class MyNumericTextBox : TextBox_Base
    {
        private bool DelOrBack = false;
        private bool IsDigit;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            TextAlign = HorizontalAlignment.Left;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                DelOrBack = true;
            }
            // Ziffernreihe oben auf der Tastatur (Keys.D0 - Keys.D9)
            bool isDigit = (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9);
            // Ziffernblock (Keys.NumPad0 - Keys.NumPad9)
            bool isNumpadDigit = (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9);

            if (isDigit || isNumpadDigit)
            {
                IsDigit = true;
            }
            else
            {
                // Nicht-Zahl
                IsDigit = false;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!IsDigit && !DelOrBack)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyPress(e);
            }

            DelOrBack = false;
        }
    }
}
