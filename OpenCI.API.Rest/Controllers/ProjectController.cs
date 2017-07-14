using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.Contracts.Business;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.Business.Models;

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
        public async Task<IHttpActionResult> GetProject([FromUri]Guid guid)
        {
            var result = await _projectOperations.GetProject(guid).ConfigureAwait(false);

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
            var results = await _projectOperations.GetAllProjects().ConfigureAwait(false);

            return Ok(results);
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> CreateProject([FromBody]CreateProjectModel model)
        {
            var result = await _projectOperations.CreateProject(model).ConfigureAwait(false);

            return Ok(result);
        }
    }
}
