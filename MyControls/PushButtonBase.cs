using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    [ToolboxItem(false)]
    public abstract class PushButtonBase : Button
    {
        #region override
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            BackColor = Color.Transparent;
            BackgroundImageLayout = ImageLayout.None;
            FlatAppearance.BorderSize = 2;
            FlatAppearance.MouseDownBackColor = Color.Lime;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            UseVisualStyleBackColor = false;
        }
        #endregion
    }
}
