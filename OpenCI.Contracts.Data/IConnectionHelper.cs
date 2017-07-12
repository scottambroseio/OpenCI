using System.Data;

namespace OpenCI.Contracts.Data
{
    public interface IConnectionHelper
    {
        IDbConnection GetConnection();
    }
}
