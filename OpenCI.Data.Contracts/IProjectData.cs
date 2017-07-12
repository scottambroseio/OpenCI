using OpenCI.Data.Entities;

namespace OpenCI.Data.Contracts {
    public interface IProjectData
    {
        Project GetProjectById(int id);
    }
}
