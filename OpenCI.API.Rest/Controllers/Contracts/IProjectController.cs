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
        Task<IHttpActionResult> CreateProject(CreateProjectModel model);
        Task<IHttpActionResult> DeleteProject(Guid projectGuid);
        Task<IHttpActionResult> UpdateProject([FromUri]Guid projectGuid, [FromBody]UpdateProjectModel model);
        Task<IHttpActionResult> GetPlansForProject(Guid projectGuid);
    }
}
