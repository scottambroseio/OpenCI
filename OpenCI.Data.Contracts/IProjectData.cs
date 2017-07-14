using OpenCI.Data.Entities;
using System;
using System.Threading.Tasks;

namespace OpenCI.Data.Contracts
{
    public interface IProjectData
    {
        Task<Project> GetProject(Guid guid);
    }
}
