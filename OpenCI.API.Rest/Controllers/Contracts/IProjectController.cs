using OpenCI.Business.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IProjectController
    {
        Task<IHttpActionResult> GetProject(Guid projectGuid);
        Task<IHttpActionResult> GetAllProjects();
        Task<IHttpActionResult> GetPlansForProject(Guid projectGuid);
        Task<IHttpActionResult> CreateProject(CreateProjectModel model);
        Task<IHttpActionResult> UpdateProject(Guid projectGuid, UpdateProjectModel model);
        Task<IHttpActionResult> DeleteProject(Guid projectGuid);
    }
}
