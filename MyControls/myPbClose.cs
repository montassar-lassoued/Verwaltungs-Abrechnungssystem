using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(true)]
    public class myPbClose : MyPushButtonBase
    {
        public override ControlRole Role => ControlRole.None;
        
        protected override void OnHandleCreated(EventArgs e)
        {
            if (FindForm() is Intf_WinFormsBase host)
                RegisterTo(FindForm());
        }
        public override void RegisterTo(Form parentForm)
        {
            if (parentForm is Intf_WinFormsBase host)
            {
                host.RegisterControl(this);
            }
        }
        #region Variablen
        private delegate void OnClickHandler(Object sender, EventArgs e);
        private event OnClickHandler ClickEvent;
        #endregion
        #region override
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public new string Text
        {
            get => base.Text;
            private set => base.Text = value; // nur intern/privat
        }
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            // feste Größe erzwingen
            base.SetBoundsCore(x, y, 181, 42, specified);
        }
        #endregion

        #region abstract
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Text = "Abbrechen";
            Image = Properties.Resources.Cancel_icon_icons_com_73703;
            var b = FindForm();
            if (b is Intf_WinFormsBase handler)
            {
                ClickEvent += handler.OnCloseWindow;
            }
        }
        public override void ClearField()
        {
        }
        public override void ActivateSearchMode()
        {
        }
        public override void DeactivateSearchMode()
        {
        }
        public override void OnEnableControl()
        {
            Enabled = true;
        }

        public override void OnDisableControl()
        {
            Enabled = false;
        }
        #endregion
    }
}
