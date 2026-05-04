using MyControls;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FCC_Verwaltungssystem
{
    public class BackupService
    {
        private readonly string[] _systemDatabaseNames = { "master", "tempdb", "model", "msdb" };
        public BackupService()
        {

        }
        public void BackupAllUserDatabases()
        {
            foreach (string databaseName in GetAllUserDatabases())
            {
                BackupDatabase();
            }
        }
        public void BackupDatabase()
        {
            string filePath = BuildBackupPathWithFilename(Database.DatabaseName);

            //Transaction transaction = new Transaction();
            try
            {
                var backup = String.Format("BACKUP DATABASE [{0}] TO DISK='{1}'", Database.DatabaseName, filePath);
                Log.Information("Backup wird erstellt: BACKUP DATABASE [{0}] TO DISK='{1}'", Database.DatabaseName, filePath);
                try
                {
                    using (var command = new SqlCommand(backup, Database.Connection))
                    {
                        //command.Transaction = Database.Connection.BeginTransaction();
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Backup konnte nicht erstellt werden. Logfiles prüfen");
                    Log.Error(ex.Message);
                }
                MessageBox.Show("Backup wurde erfolgreich durchgeführt!", "Backup!!");
                Log.Information(backup);
            }

            catch (Exception ex)
            {
                Log.Error(ex.Message);

            }
        }
        private IEnumerable<string> GetAllUserDatabases()
        {
            var databases = new List<String>();

            DataTable databasesTable = Database.Connection.GetSchema("Databases");

            foreach (DataRow row in databasesTable.Rows)
            {
                string databaseName = row["database_name"].ToString();

                if (_systemDatabaseNames.Contains(databaseName))
                    continue;

                databases.Add(databaseName);
            }

            return databases;
        }
        private string BuildBackupPathWithFilename(string databaseName)
        {
            string filename = string.Format("{0}-{1}.bak", databaseName, DateTime.Now.ToString("yyyy-MM-dd"));
            //string workingDirectory = Environment.CurrentDirectory;
            Log.Information("Backup-Ordner :" + Database.BackupFolder + " Backup-Datei:" + filename);
            return Path.Combine(Database.BackupFolder, filename);
        }
    }
}