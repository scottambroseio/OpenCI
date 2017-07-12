using OpenCI.Contracts.Business;
using OpenCI.Data.Contracts;
using OpenCI.Data.Entities;

namespace OpenCI.Implementation.Business
{
    public class ProjectOperations : IProjectOperations
    {
        private IProjectData _projectData;

        public ProjectOperations(IProjectData projectData)
        {
            _projectData = projectData;
        }

        public Project GetProjectById(int id)
        {
            return _projectData.GetProjectById(id);
        }
    }
}
