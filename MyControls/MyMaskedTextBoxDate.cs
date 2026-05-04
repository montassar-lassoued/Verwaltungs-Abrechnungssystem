using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class MyMaskedTextBoxDate : MaskedTextBox_Base
    {
        public string ToolTip { get; set; } = " ";
        private string standardMask;
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            string t = Text;
            Font = new Font("Calibri", 12F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            Mask = "00/00/0000";
            standardMask = Text;
            Text = t;
            ValidatingType = typeof(DateTime);
            TextAlign = HorizontalAlignment.Center;
            //Size = new Size(203, 24);
            string s = Text;
            DateTimePicker datebtn = new DateTimePicker();
            datebtn.Name = "datebtn";
            //datebtn.Size = new Drawing.Size(20, this.Height);
            datebtn.Format = DateTimePickerFormat.Short;
            datebtn.Size = new Size(20, ClientSize.Height + 2);
            datebtn.Location = new Point(ClientSize.Width - datebtn.Width, -1);
            datebtn.Cursor = Cursors.Hand;
            datebtn.ValueChanged += new EventHandler(picker_ValueChanged);
            Controls.Add(datebtn);

            PictureBox picture = new PictureBox();
            picture.SizeMode = PictureBoxSizeMode.Zoom;
            picture.Size = new Size(20, this.Height);
            picture.Cursor = Cursors.Hand;
            picture.Image = Properties.Resources.Korb1;
            picture.Click += new EventHandler(click_Delete);
            Controls.Add(picture);


            var myToolTip = new ToolTip
            {
                IsBalloon = true,
                ToolTipTitle = "Info",
                ToolTipIcon = ToolTipIcon.Info,
                AutomaticDelay = 5000,
                AutoPopDelay = 50000,
                InitialDelay = 100,
                ReshowDelay = 500
            };

            myToolTip.SetToolTip(this, ToolTip);

            // Send EM_SETMARGINS to prevent text from disappearing underneath the button
            SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(datebtn.Width << 16));
            //
        }
        #region override
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // feste Größe erzwingen
            base.SetBoundsCore(x, y, 203, 24, specified);
        }
        protected override string _StandardMask()
        {
            return standardMask;
        }
        #endregion

        #region abstract

        #endregion

        #region private       
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new string Mask
        {
            get => base.Mask;
            private set => base.Mask = value; // nur intern/privat
        }
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
        private void picker_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker dateTime = sender as DateTimePicker;
            Text = dateTime.Value.Date.ToString();
            onTextBoxEdit();
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private void click_Delete(object sender, EventArgs e)
        {
            ResetText();
            Focus();
        }
        #endregion
    }
}
