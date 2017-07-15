using System.Data;

namespace OpenCI.Identity.Dapper
{
    public interface IConnectionHelper
    {
        IDbConnection GetConnection();
    }
}
