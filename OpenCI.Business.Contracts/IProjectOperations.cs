using OpenCI.Business.Models;

namespace OpenCI.Contracts.Business
{
    public interface IProjectOperations
    {
        ProjectModel GetProjectById(int id);
    }
}
