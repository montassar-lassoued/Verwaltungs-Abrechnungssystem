using Serilog;
using System;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class Maske_RechnungBearbeiten : Maske_Rechnung
    {
        private string Rechnungsnummer;
        public Maske_RechnungBearbeiten(string _rechnungsnummer)
        {
            Rechnungsnummer = _rechnungsnummer;
        }
        protected override bool _Enable_MenuOption(MenuItem menu_Option)
        {
            MenuItem delete = new MenuItem();
            delete.Text = "Rechnung löschen";
            delete.BarBreak = false;
            delete.Enabled = false;
            delete.Click += new System.EventHandler(rechnung_loeschen);
            menu_Option.MenuItems.Add(delete);
            return true;
        }
        protected override void _OnLoad(EventArgs e)
        {
            InitializeMask();
            //Suchmodus aktivieren
            item_SearchMode.PerformClick();

            feld_Rechnungsnummer.Texts = Rechnungsnummer;
            // Click OK
            InvokeOnClick(pbOk, EventArgs.Empty);
        }
        protected override void _AfterSave()
        {
            Close();
        }
        private void rechnung_loeschen(object sender, EventArgs e)
        {
            try
            {
                if (!User.Rechte.RECHNUNG_LOESCHEN())
                {
                    MessageBox.Show("Kein Berechtigung Rechnungen zu löschen!\nAdmin kontaktieren", "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                DialogResult result = MessageBox.Show("Rechnung mit der Nr." + Rechnungsnummer + " löschen?", "Warnung!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    DataAccessLayer.delete_RechnungByRechnungsnummer(Rechnungsnummer);
                    Log.Error("Rechnung-Nr. {0} wure gelöscht", Rechnungsnummer);
                    MessageBox.Show("Rechnung mit der Nr." + Rechnungsnummer + " wurde gelöscht", "Info!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Close();
                }
            }
            catch (Exception exp)
            {
                Log.Error(exp.Source + "\n" + exp.TargetSite.DeclaringType + "\n" + exp.Message);
            }
        }
    }
}