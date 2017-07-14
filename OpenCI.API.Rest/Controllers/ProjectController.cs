using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.Contracts.Business;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("project")]
    public class ProjectController : ApiController, IProjectController
    {
        private readonly IProjectOperations _projectOperations;

        public ProjectController(IProjectOperations projectOperations)
        {
            _projectOperations = projectOperations;
        }

        [HttpGet]
        [Route("project/{guid:Guid}")]
        public async Task<IHttpActionResult> GetProject(Guid guid)
        {
            var result = await _projectOperations.GetProject(guid);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAllProjects()
        {
            var results = await _projectOperations.GetAllProjects();

            return Ok(results);
        }
    }
}
