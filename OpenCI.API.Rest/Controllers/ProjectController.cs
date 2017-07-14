using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.Contracts.Business;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.Business.Models;
using System.Data.SqlClient;

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
        [Route("{guid:Guid}")]
        public async Task<IHttpActionResult> GetProject([FromUri]Guid guid)
        {
            try
            {
                var result = await _projectOperations.GetProject(guid).ConfigureAwait(false);

                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAllProjects()
        {
            try
            {
                var results = await _projectOperations.GetAllProjects().ConfigureAwait(false);

                return Ok(results);
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
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
        [Route("{guid:Guid}")]
        public async Task<IHttpActionResult> UpdateProject([FromUri]Guid guid, [FromBody]UpdateProjectModel model)
        {
            try
            {
                var result = await _projectOperations.UpdateProject(guid, model).ConfigureAwait(false);

                return Ok(result);
            } catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (SqlException)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{guid:Guid}")]
        public async Task<IHttpActionResult> DeleteProject(Guid guid)
        {
            try
            {
                var result = await _projectOperations.DeleteProject(guid).ConfigureAwait(false);

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
    }
}
