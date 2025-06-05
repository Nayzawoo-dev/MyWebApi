using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Database.Shared
{
    public class DapperServices
    {
        private readonly SqlConnectionStringBuilder _connection;
        public DapperServices(IConfiguration connection)
        {
            _connection = new SqlConnectionStringBuilder(connection.GetConnectionString("Database"));
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
