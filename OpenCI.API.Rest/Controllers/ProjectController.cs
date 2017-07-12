using OpenCI.API.Rest.Controllers.Contracts;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers
{
    public class ProjectController : ApiController, IProjectController
    {
        public IHttpActionResult Get()
        {
            return Ok(true);
        }
    }
}
