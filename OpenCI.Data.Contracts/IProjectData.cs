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
        Task<Project> GetProject(Guid guid);
        Task<Project> CreateProject(CreateProjectModel model);
        Task DeleteProject(Guid guid);
        Task<Project> UpdateProject(Guid guid, UpdateProjectModel model);
    }
}
