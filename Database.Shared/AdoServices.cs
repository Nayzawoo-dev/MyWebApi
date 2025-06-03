using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Database.Shared
{
    internal class AdoServices
    {
        private readonly SqlConnectionStringBuilder _connection;
        public AdoServices(SqlConnectionStringBuilder connection)
        {
            _connection = connection;
        }

        public List<T> Query<T>(string query, params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            connection.Close();
            var res = JsonConvert.SerializeObject(dt);
            var result = JsonConvert.DeserializeObject<List<T>>(res)!;
            return result;

        }

        public int Execute(string query, params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters);
            var res = cmd.ExecuteNonQuery();
            connection.Close();
            return res;
        }
    }
}
