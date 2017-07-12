using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.Contracts.Business;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers
{
    public class ProjectController : ApiController, IProjectController
    {
        private readonly IProjectOperations _projectOperations;

        public ProjectController(IProjectOperations projectOperations)
        {
            _projectOperations = projectOperations;
        }

        public IHttpActionResult Get(int id)
        {
            var model = _projectOperations.GetProjectById(id);

            return Ok(model);
        }
    }
}
