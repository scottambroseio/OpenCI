using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.Contracts.Business;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers
{
    public class Model
    {
        public string Name { get; set; }
    }

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

        public IHttpActionResult Put(int id, [FromBody]Model model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            return Ok(nameof(Put));
        }

        public IHttpActionResult Post(int id, [FromBody]Model model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            return Ok(nameof(Post));
        }

        public IHttpActionResult Delete(int id)
        {
            return Ok(nameof(Delete));
        }
    }
}
