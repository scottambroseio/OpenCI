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
    }
}
