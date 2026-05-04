using System.Xml;

namespace FCC_Verwaltungssystem
{
    public class Konfiguration
    {
        private static string server;
        private static string database;
        private static string user;
        private static string password;
        private static string backupFolder;

        public string BackupFolder { get => backupFolder; set => backupFolder = value; }
        public string Server { get => server; set => server = value; }
        public string Database { get => database; set => database = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }

        public bool readKonfigData(string url)
        {
            using (XmlReader reader = XmlReader.Create(url))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Name":
                                Database = reader.ReadString();
                                break;
                            case "Server":
                                Server = reader.ReadString();
                                break;
                            case "User":
                                User = reader.ReadString();
                                break;
                            case "Password":
                                Password = reader.ReadString();
                                break;
                            case "Backup-Folder":
                                BackupFolder = reader.ReadString();
                                break;
                        }
                    }
                }
                reader.Close();
            }
            if (!(string.IsNullOrEmpty(Server) && string.IsNullOrEmpty(Database)
                && string.IsNullOrEmpty(User) && string.IsNullOrEmpty(Password)))
            {
                return true;
            }
            return false;
        }
    }
}