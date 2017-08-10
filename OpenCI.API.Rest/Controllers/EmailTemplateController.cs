using System;
using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.EmailTemplate;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("EmailTemplate")]
    public class EmailTemplateController : ApiController, IEmailTemplateController
    {
        [HttpGet]
        [Route("{guid:Guid}/Render", Name = nameof(RenderEmailTemplate))]
        public async Task<IHttpActionResult> RenderEmailTemplate([FromUri] Guid Guid)
        {
            return Ok();
        }
    }
}
