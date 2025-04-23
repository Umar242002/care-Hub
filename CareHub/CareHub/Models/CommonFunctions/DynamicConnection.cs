using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CareHub.Models.CommonFunctions
{
    public class DynamicConnection
    {
        private readonly string _connectionString;

        public DynamicConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<dynamic> ExecuteStoredProcedure(string procedureName, DynamicParameters parameters)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                return dbConnection.Query<dynamic>(procedureName, parameters, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
