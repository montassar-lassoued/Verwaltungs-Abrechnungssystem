using System;

namespace MyControls
{
    interface Intf_WinFormsBase
    {
        void OnEditMask(object sender, EventArgs e);
        void OnSaveData(object sender, EventArgs e);
        void OnSearchModeCheckBoxStateChanged(object sender, MyEventArgs e);
        void OnCloseWindow(object sender, EventArgs e);
        void RegisterControl(ICustomControl control);
        void OnArchivDragEnter(object sender, EventArgs e);
    }
}
