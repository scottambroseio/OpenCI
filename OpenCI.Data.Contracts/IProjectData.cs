using OpenCI.Business.Models;
using OpenCI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenCI.Data.Contracts
{
    public interface IProjectData
    {
        Task<List<Project>> GetAllProjects();
        Task<Project> GetProject(Guid projectGuid);
        Task<Project> CreateProject(CreateProjectModel model);
        Task<bool> DeleteProject(Guid projectGuid);
        Task<Project> UpdateProject(Guid projectGuid, UpdateProjectModel model);
    }
}
