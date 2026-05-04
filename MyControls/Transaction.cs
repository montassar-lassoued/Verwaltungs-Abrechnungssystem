using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MyControls
{
    public class Transaction : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private bool _disposed = false;

        public Transaction()
        {
            _connection = new SqlConnection(Database.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        private SqlCommand CreateCommand(string sql, Dictionary<string, object> parameters = null)
        {
            var cmd = _connection.CreateCommand();
            cmd.Transaction = _transaction;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            if (parameters != null)
            {
                foreach (var param in parameters)
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
            }

            return cmd;
        }

        // --- Datenoperationen ---
        public int Insert(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = CreateCommand(sql+ "SELECT CAST(SCOPE_IDENTITY() AS int)", parameters))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = CreateCommand(sql, parameters))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = CreateCommand(sql, parameters))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable Query(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = CreateCommand(sql, parameters))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public SqlDataReader QuerySqlDataReader(string sql, Dictionary<string, object> parameters = null)
        {
            var cmd = CreateCommand(sql, parameters);
            // ⚠️ Reader darf Connection offenhalten → Command nicht disposen!
            return cmd.ExecuteReader();
        }

        public int GetID_IfExist(string sql, Dictionary<string, object> parameters = null)
        {
            using (var cmd = CreateCommand(sql + "; SELECT SCOPE_IDENTITY();", parameters))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // --- Transaktions-Handling ---
        public void Commit()
        {
            _transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            Dispose();
        }

        // --- IDisposable ---
        public void Dispose()
        {
            if (!_disposed)
            {
                try { _transaction?.Dispose(); } catch { }
                try { _connection?.Dispose(); } catch { }
                _disposed = true;
            }
        }
    }
}
