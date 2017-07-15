using OpenCI.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenCI.Business.Contracts
{
    public interface IProjectOperations
    {
        Task<List<ProjectModel>> GetAllProjects();
        Task<ProjectModel> GetProject(Guid guid);
        Task<ProjectModel> CreateProject(CreateProjectModel model);
        Task<bool> DeleteProject(Guid guid);
        Task<ProjectModel> UpdateProject(Guid guid, UpdateProjectModel model);
    }
}
