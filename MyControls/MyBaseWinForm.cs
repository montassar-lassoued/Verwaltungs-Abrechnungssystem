using System;
using System.Windows.Forms;

namespace MyControls
{
    public abstract class MyBaseWinForm : Form, Intf_WinFormsBase
    {
        public abstract void OnCloseWindow(object sender, EventArgs e);
        public abstract void OnEditMask(object sender, EventArgs e);
        public abstract void OnSaveData(object sender, EventArgs e);
        public abstract void OnSearchModeCheckBoxStateChanged(object sender, MyEventArgs e);
        public abstract void RegisterControl(ICustomControl control);
        public abstract void OnArchivDragEnter(object sender, EventArgs e);
    }
}
