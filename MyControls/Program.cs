using System;
using System.Windows.Forms;

namespace MyControls
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            testform t = new testform();
            t.ShowDialog();
        }
    }
}
