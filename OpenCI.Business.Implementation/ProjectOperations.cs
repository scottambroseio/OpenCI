using OpenCI.Business.Models;
using OpenCI.Contracts.Business;
using OpenCI.Data.Contracts;

namespace OpenCI.Implementation.Business
{
    public class ProjectOperations : IProjectOperations
    {
        private IProjectData _projectData;

        public ProjectOperations(IProjectData projectData)
        {
            _projectData = projectData;
        }

        public ProjectModel GetProjectById(int id)
        {
            var entity = _projectData.GetProjectById(id);
            var model = new ProjectModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return model;
        }
    }
}
