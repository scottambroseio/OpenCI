using System.Data;

namespace OpenCI.Data.Contracts
{
    public interface IConnectionHelper
    {
        IDbConnection GetConnection();
    }
}