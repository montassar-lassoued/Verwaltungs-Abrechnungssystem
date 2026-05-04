using MyControls;
using Serilog;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class FightClubChemnitz : MainForm
    {
        protected override string _name()
        {
            return "Fight Club Chemnitz";
        }
        protected override bool Enable_MenuOption(MenuItem menu_Option)
        {
            // Backup
            MenuItem item_Backup = new MenuItem();
            item_Backup.Text = "Backup erstellen";
            item_Backup.Shortcut = Shortcut.CtrlK;
            item_Backup.ShowShortcut = true;
            item_Backup.BarBreak = true;
            item_Backup.Click += new System.EventHandler(Backup);
            menu_Option.MenuItems.Add(item_Backup);

            return true;
        }
        protected override bool Enable_MenuKurs(MenuItem menu_Kurs)
        {
            MenuItem item_pflegen = new MenuItem();
            item_pflegen.Text = "Bearbeiten";
            item_pflegen.Shortcut = Shortcut.CtrlB;
            item_pflegen.ShowShortcut = true;
            item_pflegen.BarBreak = true;
            item_pflegen.Click += new System.EventHandler(Kurse);
            menu_Kurs.MenuItems.Add(item_pflegen);

            return true;
        }
        protected override bool Enable_MenuEinstellungen(MenuItem menu_Konfig)
        {
            //Konten
            MenuItem item_Konten = new MenuItem();
            item_Konten.Text = "Konten";
            item_Konten.Shortcut = Shortcut.CtrlK;
            item_Konten.ShowShortcut = true;
            item_Konten.BarBreak = true;
            item_Konten.Click += new System.EventHandler(Konten);
            menu_Konfig.MenuItems.Add(item_Konten);


            return true;
        }
        protected override bool Enable_MenuFirma(MenuItem menu_Firma)
        {
            //Daten
            MenuItem item_Stammdaten = new MenuItem();
            item_Stammdaten.Text = "Stammdaten";
            item_Stammdaten.Shortcut = Shortcut.CtrlS;
            item_Stammdaten.ShowShortcut = true;
            item_Stammdaten.BarBreak = true;
            item_Stammdaten.Click += new System.EventHandler(Stammdaten);
            menu_Firma.MenuItems.Add(item_Stammdaten);
            //
            MenuItem item_Blacklist = new MenuItem();
            item_Blacklist.Text = "BlackList";
            item_Blacklist.Shortcut = Shortcut.CtrlB;
            item_Blacklist.ShowShortcut = true;
            item_Blacklist.BarBreak = false;
            item_Blacklist.Click += new System.EventHandler(BlackList);
            menu_Firma.MenuItems.Add(item_Blacklist);

            return true;
        }
        protected override bool Enable_MenuFormular(MenuItem menu_Formular)
        {
            //Angebot
            MenuItem item_Angebot = new MenuItem();
            item_Angebot.Text = "Angebot";
            item_Angebot.Shortcut = Shortcut.CtrlA;
            item_Angebot.ShowShortcut = true;
            item_Angebot.BarBreak = true;
            item_Angebot.Click += new System.EventHandler(Angebotsformular);
            menu_Formular.MenuItems.Add(item_Angebot);
            //Rechnung
            MenuItem item_Rechnung = new MenuItem();
            item_Rechnung.Text = "Rechnung";
            item_Rechnung.Shortcut = Shortcut.CtrlR;
            item_Rechnung.ShowShortcut = true;
            item_Rechnung.BarBreak = false;
            item_Rechnung.Click += new System.EventHandler(Rechnungsformular);
            menu_Formular.MenuItems.Add(item_Rechnung);

            return true;
        }
        protected override bool Enable_MenuKonfiguration(MenuItem menu_Konfiguration)
        {
            //Kalendar
            MenuItem item_Kalendar = new MenuItem();
            item_Kalendar.Text = "Kalender";
            item_Kalendar.Shortcut = Shortcut.CtrlK;
            item_Kalendar.ShowShortcut = true;
            item_Kalendar.BarBreak = true;
            item_Kalendar.Click += new System.EventHandler(Kalender);
            menu_Konfiguration.MenuItems.Add(item_Kalendar);
            //Mail
            MenuItem item_Mail = new MenuItem();
            item_Mail.Text = "Mailing";
            item_Mail.Shortcut = Shortcut.CtrlM;
            item_Mail.ShowShortcut = true;
            item_Mail.BarBreak = false;
            item_Mail.Click += new System.EventHandler(Mailing);
            menu_Konfiguration.MenuItems.Add(item_Mail);
            //Nummernkreis
            MenuItem item_NrKreis = new MenuItem();
            item_NrKreis.Text = "Nummernkreis";
            item_NrKreis.Shortcut = Shortcut.CtrlN;
            item_NrKreis.ShowShortcut = true;
            item_NrKreis.BarBreak = false;
            item_NrKreis.Click += new System.EventHandler(Nummernkreis);
            menu_Konfiguration.MenuItems.Add(item_NrKreis);
            //Serverdaten
            MenuItem item_Serverdaten = new MenuItem();
            item_Serverdaten.Text = "Server/Datenbank";
            item_Serverdaten.Shortcut = Shortcut.CtrlS;
            item_Serverdaten.ShowShortcut = true;
            item_Serverdaten.BarBreak = false;
            item_Serverdaten.Click += new System.EventHandler(ServerEinstellungen);
            menu_Konfiguration.MenuItems.Add(item_Serverdaten);
            return true;
        }
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == Globals.uMSG)
            {
                EventArgs _e = new EventArgs();
                int _Msg = (int)m.WParam;
                switch (_Msg)
                {
                    case Globals.VERTRAG_ERFASSEN:
                        Vertragserfassung();
                        break;
                    case Globals.VERTRAG_SUCHEN:
                        VertragSuchen();
                        break;
                    case Globals.KUNDE:
                        Kunde();
                        break;
                    case Globals.RECHNUNG:
                        Rechnung();
                        break;
                    case Globals.JOURNAL:
                        Maske_Journal();
                        break;
                    case Globals.KALENDAR:
                        Maske_Kalendar();
                        break;
                    case Globals.UMSATZ:
                        Umsatz();
                        break;
                    case Globals.EINNAHMENUEBERSCHUSS:
                        Maske_Ueberschuss();
                        break;
                    case Globals.KOSTEN:
                        Maske_Kosten();
                        break;
                }
            }
            base.WndProc(ref m);
        }
        private void Mailing(object sender, EventArgs e)
        {
            if (!User.Rechte.KALENDER_MAIL_KONFIG())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_KonfigMail konfigMail = new Maske_KonfigMail();
            konfigMail.ShowDialog();
        }
        private void BlackList(object sender, EventArgs e)
        {
            if (!User.Rechte.BLACKLIST_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_BlackList maske_BlackList = new Maske_BlackList();
            maske_BlackList.ShowDialog();
        }
        private void Angebotsformular(object sender, EventArgs e)
        {
            if (!User.Rechte.RECHNUNG_KONFIG())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_AngebotsFormular formular = new Maske_AngebotsFormular();
            formular.ShowDialog();
        }
        private void Rechnungsformular(object sender, EventArgs e)
        {
            if (!User.Rechte.RECHNUNG_KONFIG())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_RechnungsFormular formular = new Maske_RechnungsFormular();
            formular.ShowDialog();
        }
        private void Maske_Kosten()
        {
            if (!User.Rechte.KOSTEN_UEBERSICHT())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_Kosten kosten = new Maske_Kosten();
            kosten.ShowDialog();
        }
        private void Maske_Ueberschuss()
        {
            if (!User.Rechte.EINNAHME_UEBERSICHT())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }
        private void Umsatz()
        {
            if (!User.Rechte.UMSATZ_UEBERSICHT())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_Umsatz umsatz = new Maske_Umsatz();
            umsatz.ShowDialog();
        }
        private void Maske_Kalendar()
        {
            if (User.Rechte.KALENDAR_NUTZEN())
            {
                Thread thread = new Thread(() =>
                {
                    Maske_Kalendar kalendar = new Maske_Kalendar();
                    Application.Run(kalendar);
                });

                thread.SetApartmentState(ApartmentState.STA); // Muss **vor Start()** gesetzt werden
                thread.Start();

            }
        }
        private void Maske_Journal()
        {
            if (User.Rechte.JOURNAL_NUTZEN())
            {
                Maske_Journal journal = new Maske_Journal();
                journal.ShowDialog();
            }
        }
        private void Rechnung()
        {
            // Benutzer-Rechte werden nach dem Aufruf der Maske geprüft
            Maske_Rechnung maske_Rechnung = new Maske_Rechnung();
            maske_Rechnung.ShowDialog();
        }
        protected override void _Load(object sender, EventArgs e)
        {
            base._Load(sender, e);
            setConnectionData(Database.ConnectionState);
        }
        protected override void MenuButtons()
        {
            MainMenuButton menu1 = new MainMenuButton(this);
            menu1.pbStandard(Globals.VERTRAG_ERFASSEN, "Vertrag", "Vertrag_png", "neue Verträge erfassen, suchen, bearbeiten oder löschen");
            menu1.pbStandard(Globals.VERTRAG_SUCHEN, "Vertrag suchen", "Vertrag_suchen_png", "Verträge nach beliebegen Kriterien suchen und bearbeiten");
            menu1.pbStandard(Globals.KUNDE, "Kunden", "Kunde_png", "Kunden erfassen und bearbeiten");
            menu1.pbStandard(Globals.RECHNUNG, "Rechnung", "rechnung", "Rechnung erfassen, bearbeiten, sende, drucken oder löschen");
            menu1.pbStandard(Globals.JOURNAL, "Journal", "Journal_png", "Verträge nach beliebegen Kriterien suchen und bearbeiten");
            menu1.pbStandard(Globals.KALENDAR, "Kalendar", "Calendar_png", "Termine anzeigen, estellen, bearbeiten oder löschen");
            menu1.pbStandard(Globals.UMSATZ, "Umsatz", "Rechnung_png", "Umsätze anzeigen");
            menu1.pbStandard(Globals.EINNAHMENUEBERSCHUSS, "Überschuss", "Kosten_png", "Überschüsse anzeigen");
            menu1.pbStandard(Globals.KOSTEN, "Kosten", "Finanz_png_k", "Kosten nach beliebegem Zeitraum anzeigen");

            menu1.Add_All();
        }
        private void Vertragserfassung()
        {
            if (!User.Rechte.VERTRAG_ERFASSEN())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_Vertragerfassung vertrag = new Maske_Vertragerfassung();
            vertrag.ShowDialog();
        }
        private void VertragSuchen()
        {
            if (!User.Rechte.VERTRAG_SUCHEN())
            {
                MessageBox.Show("Keine Berechtigung", "Stop!!!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Maske_VertragSuchen suche = new Maske_VertragSuchen();
            suche.ShowDialog();

        }
        private void Kunde()
        {
            if (!User.Rechte.KUNDEN_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung Kundendaten zu bearbeiten-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Maske_Kunde _Kunde = new Maske_Kunde();
            _Kunde.ShowDialog();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Transaction transaction = new Transaction();
            string sql_update = "Update _USER SET STATUS='disconnected' WHERE ID =" + User.ID;
            transaction.Update(sql_update);
            transaction.Commit();

            Log.Information("Programm wurde ordnungsgemäß beendet Start");

            base.OnClosing(e);
        }
        private void Kurse(object sender, System.EventArgs e)
        {
            if (!User.Rechte.KURSE_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung Kurse zu bearbeiten-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_Kurse maske_Kurse = new Maske_Kurse();
            maske_Kurse.ShowDialog();
        }
        private void Kalender(object sender, EventArgs e)
        {
            if (!User.Rechte.KALENDER_EINSTELLUNG())
            {
                MessageBox.Show("Keine Berechtigung die Kalender-Einstellungen zu sehen/ändern-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_KonfigKalendar kalendar = new Maske_KonfigKalendar();
            kalendar.ShowDialog();
        }
        private void Konten(object sender, EventArgs e)
        {
            if (!User.Rechte.KONTEN_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung Konten zu bearbeiten-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_KonfigKonten konten = new Maske_KonfigKonten();
            konten.ShowDialog();
        }
        private void Nummernkreis(object sender, EventArgs e)
        {
            if (!User.Rechte.RECHNUNG_KONFIG())
            {
                MessageBox.Show("Keine Berechtigung!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_KonfigNummernkreis stammdaten = new Maske_KonfigNummernkreis();
            stammdaten.ShowDialog();
        }
        private void Stammdaten(object sender, EventArgs e)
        {
            if (!User.Rechte.STAMMDATEN_BEARBEITEN())
            {
                MessageBox.Show("Keine Berechtigung Stammdaten zu bearbeiten-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            Maske_Stammdaten stammdaten = new Maske_Stammdaten();
            stammdaten.ShowDialog();
        }
        private void Backup(object sender, EventArgs e)
        {
            if (!User.Rechte.BACKUP_ERSTELLEN())
            {
                MessageBox.Show("Keine Berechtigung Backup zu erstellen-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            DialogResult result = MessageBox.Show("Backup erstellen?\nSpeichern und alle Fenster schließen." +
                "\nDie Datenbankverbindung wird getrennt.\nFortfahren?", "Achtung",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
                Log.Information("Backup für " + Database.DatabaseName + " wird erstellt");
                BackupService backupService = new BackupService();
                backupService.BackupDatabase();
            }
        }
        private void ServerEinstellungen(object sender, EventArgs e)
        {
            if (!User.Rechte.BACKUP_EINSTELLUNG())
            {
                MessageBox.Show("Keine Berechtigung Severdaten zu ändern-> Admin kontaktieren", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            Maske_KonfigDatabase _KonfigDatabase = new Maske_KonfigDatabase();
            _KonfigDatabase.ShowDialog();
        }
    }
}