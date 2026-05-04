using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
namespace MyControls
{
    public class Database
    {
        private static SqlConnection connection;
        private static string server_version;
        private static string server_name;
        private static string databaseName;
        private static ConnectionState connectionState;
        private static string backupFolder;
        private static string connectionString;
        private StringBuilder failedConnection = new StringBuilder();

        public bool tryToConnect(string server, string database, string ueserId, string password)
        {
            if (!(Validate(server, database, ueserId, password)))
            {
                MessageBox.Show(failedConnection.ToString());
                return false;
            }
            connection = new SqlConnection();
            ConnectionString = $@"Server = {server};Database={database};User Id={ueserId};Password={password};Integrated Security=True;";
            Connection.ConnectionString =ConnectionString;/*Integrated Security=True;";*/

            try
            {
                Connection.Open();
                setConnectionParam();

                return true;
            }
            catch (SqlException exception)
            {
                StringBuilder messageeError = new StringBuilder();
                for (int i = 0; i < exception.Errors.Count; i++)
                {

                    messageeError.Append("Index #" + i + "\n" +
                   "Message: " + exception.Errors[i].Message + "\n" +
                   "LineNumber: " + exception.Errors[i].LineNumber + "\n" +
                   "Source: " + exception.Errors[i].Source + "\n" +
                   "Procedure: " + exception.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(messageeError.ToString());
                Log.Error(messageeError.ToString());
                return false;
            }


        }
        public void Close()
        {
            if (ConnectionState.Equals(ConnectionState.Open))
            {
                Connection.Close();
                connection.Dispose();
            }
        }

        private bool Validate(string server, string database, string ueserId, string password)
        {
            if (server != "" & database != "" & ueserId != "" & password != "")
            {
                return true;
            }
            else
            {
                failedConnection.Append("parameters are missing: \n");
                if (server == "")
                {
                    failedConnection.Append("Server unknown \n");
                }
                if (database == "")
                {
                    failedConnection.Append("HelpKlasse unknown \n");
                }
                if (ueserId == "")
                {
                    failedConnection.Append("User-ID unknown \n");
                }
                if (password == "")
                {
                    failedConnection.Append("Password unknown \n");
                }
                return true;
            }

        }

        public void setConnectionParam()
        {
            Server_version = Connection.ServerVersion;
            Server_name = Connection.DataSource;
            DatabaseName = Connection.Database;
            ConnectionState = Connection.State;

        }


        public static SqlConnection Connection { get => connection; set => connection = value; }
        public static string Server_version { get => server_version; set => server_version = value; }
        public static string Server_name { get => server_name; set => server_name = value; }
        public static string DatabaseName { get => databaseName; set => databaseName = value; }
        public static ConnectionState ConnectionState { get => connectionState; set => connectionState = value; }
        public static string BackupFolder { get => backupFolder; set => backupFolder = value; }
        public static string ConnectionString { get => connectionString; set => connectionString = value; }
    }
}
