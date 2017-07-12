using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using OpenCI.Data.Contracts;

namespace OpenCI.Data.Implementation
{
    public class ConnectionHelper : IConnectionHelper
    {
        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["OpenCI"].ConnectionString);
        }
    }
}
