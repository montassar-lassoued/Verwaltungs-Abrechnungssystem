using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public class MyFormCalendar_Base : MyBaseForm
    {
        protected sealed override void OnCreateControl()
        {
            base.OnCreateControl();
            Icon = Properties.Resources.Google_Calendar_ico;
            ClientSize = new Size(1231, 881);
            //MaximizeBox = true;
        }
        protected sealed override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        protected sealed override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
        }
        public sealed override void ResetBackColor()
        {
            base.ResetBackColor();
        }
        public sealed override void ResetFont()
        {
            base.ResetFont();
        }
        public sealed override void ResetForeColor()
        {
            base.ResetForeColor();
        }
        public sealed override void ResetText()
        {
            base.ResetText();
        }

        protected override void _InitializeComponent()
        {
        }

        protected override string _name()
        {
            return "";
        }

        protected override void _OnLoad(EventArgs e)
        {
        }

        protected override bool _Save()
        {
            return true;
        }

        protected override bool _Populate()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle
        {
            get => base.FormBorderStyle;
            private set => base.FormBorderStyle = value; // nur intern/privat
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new Size ClientSize
        {
            get => base.ClientSize;
            private set => base.ClientSize = value; // nur intern/privat
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new SizeF AutoScaleDimensions
        {
            get => base.AutoScaleDimensions;
            private set => base.AutoScaleDimensions = value; // nur intern/privat
        }
    }
}
