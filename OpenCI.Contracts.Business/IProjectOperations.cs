using OpenCI.Data.Entities;

namespace OpenCI.Contracts.Business
{
    public interface IProjectOperations
    {
        Project GetProjectById(int id);
    }
}
