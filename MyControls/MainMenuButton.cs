using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyControls
{
    public class MainMenuButton
    {
        public const Int32 uMSG = 2025;
        //include SendMessage
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        //current Window
        private Form form;

        private List<Button> buttons = new List<Button>();
        private Button button;
        private ToolTip toolTip = new ToolTip();


        public MainMenuButton(Form _form)
        {
            form = _form;
            toolTip.AutoPopDelay = 5000;   // wie lange sichtbar (ms)
            toolTip.InitialDelay = 500;    // Wartezeit bis zum ersten Anzeigen (ms)
            toolTip.ReshowDelay = 200;     // Wartezeit beim erneuten Anzeigen
            toolTip.ShowAlways = false;     // auch anzeigen, wenn das Formular nicht aktiv ist
            toolTip.IsBalloon = true;        // macht eine "Sprechblasen-Form"
            toolTip.ToolTipTitle = "Info";   // Überschrift
            toolTip.ToolTipIcon = ToolTipIcon.Info; // Symbol (Info, Warnung, Fehler, etc.)
        }

        public void pbStandard(int _ident, string _Name, string _imagesource, string tooltipString=null)
        {
            button = new Button();

            button.Tag = _ident;
            button.Name = _Name;
            button.Text = _Name;
            //button.BackColor = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            button.BackColor = Color.Transparent;
            button.Cursor = Cursors.Hand;
            //button.FlatStyle = FlatStyle.System;
            button.BackgroundImageLayout = ImageLayout.Zoom;
            button.Font = new Font("Comic Sans MS", 14F, FontStyle.Italic, GraphicsUnit.Point);
            button.Cursor = Cursors.Hand;
            button.Image = (Image)Properties.Resources.ResourceManager.GetObject(_imagesource);
            button.RightToLeft = RightToLeft.No;
            button.TabIndex = 0;
            button.ForeColor = SystemColors.Highlight;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.UseVisualStyleBackColor = false;
            button.Click += new EventHandler(OnButtonClick);
            button.MouseEnter += new EventHandler(Button_PZoom);
            button.MouseLeave += new EventHandler(Button_MZoom);
            button.AutoEllipsis = true;
            if (!string.IsNullOrEmpty(tooltipString))
            {
                toolTip.SetToolTip(button, tooltipString);
            }
            

            buttons.Add(button);
        }



        private void SetLocation()
        {
            //{Width = 859 Height = 464}
            int pbX = form.ClientSize.Width - 243;
            int pbY = form.ClientSize.Height - 230;
            int spaceBetweenPb = 10;
            //
            for (int i = 0; i < buttons.Count; i++)
            {
                if (i != 0 & i % 3 == 0)
                {
                    pbX = form.ClientSize.Width - 243;
                    pbY = pbY + spaceBetweenPb + 48;
                }
                buttons[i].Location = new Point(pbX, pbY);
                buttons[i].Size = new Size(193, 48);
                pbX -= 193 + spaceBetweenPb;
            }

        }


        private void Button_PZoom(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Size = new Size(button.Width + 5, button.Height + 5);
        }
        private void Button_MZoom(object sender, EventArgs e)
        {
            if (sender.GetType() == button.GetType())
            {
                Button _myPb = sender as Button;
                _myPb.Size = new Size(_myPb.Width - 5, _myPb.Height - 5);
            }

        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            OnButtonClick(uMSG, Convert.ToInt32(button.Tag));

        }
        public void OnButtonClick(int uMsg, Int32 _Button_ID)
        {

            SendMessage(form.Handle, uMsg, _Button_ID, 0);
        }
        public void Add_All()
        {
            SetLocation();
            for (int i = 0; i < buttons.Count; i++)
            {
                form.Controls.Add(buttons[i]);
            }

        }
    }
}
