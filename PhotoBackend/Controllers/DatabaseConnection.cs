using MySqlConnector;


namespace PhotoBackend.Controllers
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;

        public DatabaseConnection() {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = "35.230.136.114",
                UserID = "backend",
                Password = "test",
                Database = "photosharesite",
                SslCert = "secrets/client-cert.pem",
                SslKey = "secrets/client-key.pem",
                SslCa = "secrets/server-ca.pem",
                SslMode = MySqlSslMode.VerifyCA,
            };
            connection = new MySqlConnection(csb.ConnectionString);
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public void ExecuteNonQuery(string procName, Dictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(procName, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            cmd.ExecuteNonQuery();
        }

        public MySqlDataReader ExecuteReader(string procName, Dictionary<string, object> parameters)
        {
            MySqlCommand cmd = new MySqlCommand(procName, connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            return cmd.ExecuteReader();
        }
    }
}
