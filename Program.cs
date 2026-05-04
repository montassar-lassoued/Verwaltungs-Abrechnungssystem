using MyControls;
using Serilog;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            string appName = Assembly.GetEntryAssembly()?.GetName().Name;

            using (Mutex mutex = new Mutex(true, appName, out createdNew))
            {
                if (!createdNew)
                {
                    return;
                }

                SetupLogging.Init();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Log.Information("System gestartet");
                Konfiguration konfig = new Konfiguration();
                string workingDirectory = Environment.CurrentDirectory;
                bool sucess = konfig.readKonfigData(workingDirectory + @"\Konfiguration\Konfiguration.xml");
                if (sucess)
                {

                    Database database = new Database();
                    bool connected = database.tryToConnect(konfig.Server, konfig.Database, konfig.User, konfig.Password);
                    if (connected)
                    {
                        Log.Information("System mit der Datenbank '{0}' verbunden", konfig.Database);
                        Database.BackupFolder = konfig.BackupFolder;
                        Login login = new Login();
                        login.ShowDialog();
                        if (login.Succes().Equals(DialogResult.OK))
                        {
                            FightClubChemnitz fightClubChemnitz = new FightClubChemnitz();
                            DialogResult result = fightClubChemnitz.ShowDialog();
                            if (result.Equals(DialogResult.Cancel))
                            {
                                Log.Information("System wurde ordnungsgemäß beendet");
                            }
                        }
                    }
                    else
                    {
                        Log.Error("System konnte keine Verbindung zur Datenbank '{0}' aufbauen", konfig.Database);
                    }
                    database.Close();
                }
            }
        }
    }
}
