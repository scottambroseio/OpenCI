using OpenCI.API.Rest.Controllers.Contracts;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.Business.Models;
using System.Data.SqlClient;
using OpenCI.Business.Contracts;
using OpenCI.Exceptions;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("project")]
    public class ProjectController : ApiController, IProjectController
    {
        private readonly IProjectOperations _projectOperations;
        private readonly IPlanOperations _planOperations;

        public ProjectController(
            IProjectOperations projectOperations,
            IPlanOperations planOperations
        )
        {
            _projectOperations = projectOperations;
            _planOperations = planOperations;
        }

        [HttpGet]
        [Route("{projectGuid:Guid}")]
        public async Task<IHttpActionResult> GetProject([FromUri]Guid projectGuid)
        {
            try
            {
                var result = await _projectOperations.GetProject(projectGuid).ConfigureAwait(false);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
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
            try
            {
                var result = await _projectOperations.CreateProject(model).ConfigureAwait(false);

                return Ok(result);
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("{projectGuid:Guid}")]
        public async Task<IHttpActionResult> UpdateProject([FromUri]Guid projectGuid, [FromBody]UpdateProjectModel model)
        {
            try
            {
                var result = await _projectOperations.UpdateProject(projectGuid, model).ConfigureAwait(false);

                return Ok(result);
            } catch (EntityNotFoundException)
            {
                return BadRequest();
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{projectGuid:Guid}")]
        public async Task<IHttpActionResult> DeleteProject(Guid projectGuid)
        {
            try
            {
                var result = await _projectOperations.DeleteProject(projectGuid).ConfigureAwait(false);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("{projectGuid:Guid}/plans")]
        public async Task<IHttpActionResult> GetPlansForProject(Guid projectGuid)
        {
            var results = await _planOperations.GetAllPlansForProject(projectGuid).ConfigureAwait(false);

            return Ok(results);
        }
    }
}
