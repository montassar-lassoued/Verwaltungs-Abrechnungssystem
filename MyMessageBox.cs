using System;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public partial class MyMessageBox : Form
    {
        private string result;

        public string Result { get => result; set => result = value; }

        public MyMessageBox()
        {
            InitializeComponent();
        }


        public MyMessageBox(string message)
        {
            InitializeComponent();
            myMessage.Text = message;
        }

        private void Mail_Click(object sender, EventArgs e)
        {
            Result = "MAIL";
            Close();
        }

        private void Druck_Click(object sender, EventArgs e)
        {
            Result = "DRUCK";
            Close();
        }
        private void abbrechen_Click(object sender, EventArgs e)
        {
            Result = "ABBRECHEN";
            Close();
        }
    }
}
