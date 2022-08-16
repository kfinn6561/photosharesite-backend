using MySql.Data.MySqlClient;
using System.Data;


namespace PhotoBackend.Data
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;

        public DatabaseConnection()
        {
            string connectionString = File.ReadAllText("secrets/connection-string.txt");
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }

        public void ExecuteNonQuery(string procName, Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();//convert null to empty list
            MySqlCommand cmd = new MySqlCommand(procName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            cmd.ExecuteNonQuery();
        }

        public DataTable ExecuteReader(string procName, Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            MySqlCommand cmd = new MySqlCommand(procName, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (KeyValuePair<string, object> param in parameters)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);
            }
            MySqlDataReader reader = cmd.ExecuteReader();

            DataTable output = new DataTable();

            output.Load(reader);

            return output;

        }
    }
}
