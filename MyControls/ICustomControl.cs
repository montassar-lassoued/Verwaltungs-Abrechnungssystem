using System.Windows.Forms;

namespace MyControls
{
    public interface ICustomControl
    {
        ControlRole Role { get; }
        void RegisterTo(Form parentForm);
        void ClearField();
        void ActivateSearchMode();
        void DeactivateSearchMode();
        void OnEnableControl();
        void OnDisableControl();
    }
}
