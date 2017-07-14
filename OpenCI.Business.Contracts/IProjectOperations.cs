using OpenCI.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenCI.Contracts.Business
{
    public interface IProjectOperations
    {
        Task<List<ProjectModel>> GetAllProjects();
        Task<ProjectModel> GetProject(Guid guid);
        Task<ProjectModel> CreateProject(CreateProjectModel model);
    }
}
