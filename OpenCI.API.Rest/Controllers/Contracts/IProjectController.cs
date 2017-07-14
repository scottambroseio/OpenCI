using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IProjectController
    {
        Task<IHttpActionResult> GetProject(Guid guid);
        Task<IHttpActionResult> GetAllProjects();
    }
}
