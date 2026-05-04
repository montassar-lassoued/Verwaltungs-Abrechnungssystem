using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            inialize();
        }
        protected virtual void _Load(object sender, EventArgs e)
        {
            Focus();
        }
        protected void setConnectionData(ConnectionState connectionState)
        {
            if (connectionState.Equals(ConnectionState.Open))
            {
                tVerbindung.Text = "verbunden";
                tVerbindung.BackColor = Color.PaleGreen;
            }
            else
            {
                tVerbindung.Text = "getrennt";
                tVerbindung.BackColor = Color.OrangeRed;
            }

        }
        protected virtual void speichern(object sender, EventArgs e)
        {

        }
        protected virtual void close(object sender, EventArgs e)
        {

        }
        protected virtual string _name()
        {
            return "";
        }
        private void inialize()
        {
            MenuButtons();
            //paintCalendar();
            _inialize();
        }
        protected virtual void _inialize()
        {

        }

        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);
            p.Graphics.DrawString(box.Text, box.Font, Brushes.Red, 20, 200);
        }

        protected virtual void MenuButtons()
        {

        }

        protected virtual bool Enable_MenuOption(MenuItem menu_Option)
        {
            return false;
        }
        protected virtual bool Enable_MenuKurs(MenuItem menu_Kurs)
        {
            return false;
        }
        protected virtual bool Enable_MenuEinstellungen(MenuItem menu_Einstellungen)
        {
            return false;
        }

        protected virtual bool Enable_MenuFirma(MenuItem menu_Firma)
        {
            return false;
        }
        protected virtual bool Enable_MenuFormular(MenuItem menu_Formular)
        {
            return false;
        }
        protected virtual bool Enable_MenuKonfiguration(MenuItem menu_Konfiguration)
        {
            return false;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("dd.MMMM.yyyy") + "   " + DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
