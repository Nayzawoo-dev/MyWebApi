using Dapper;
using Microsoft.Data.SqlClient;

namespace Database.Shared
{
    public class DapperServices
    {
        private readonly SqlConnectionStringBuilder _connection;
        public DapperServices(SqlConnectionStringBuilder connection)
        {
            _connection = connection;
        }

        public List<T> Query<T>(string query, object? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var res = connection.Query<T>(query, parameters).ToList();
            connection.Close();
            return res;
        }

        public int Execute(string query, object? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var res = connection.Execute(query, parameters);
            connection.Close();
            return res;
        }


    }
}
