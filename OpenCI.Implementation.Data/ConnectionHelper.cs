using OpenCI.Contracts.Data;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OpenCI.Implementation.Data
{
    public class ConnectionHelper : IConnectionHelper
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["OpenCI"].ConnectionString);
        }
    }
}
