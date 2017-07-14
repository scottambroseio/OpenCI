using OpenCI.Business.Models;
using System;
using System.Threading.Tasks;

namespace OpenCI.Contracts.Business
{
    public interface IProjectOperations
    {
        Task<ProjectModel> GetProject(Guid guid);
    }
}
