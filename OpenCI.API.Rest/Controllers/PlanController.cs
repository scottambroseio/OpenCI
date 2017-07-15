using OpenCI.API.Rest.Controllers.Contracts;
using System;
using System.Web.Http;
using System.Threading.Tasks;
using OpenCI.Business.Contracts;
using System.Data.SqlClient;
using OpenCI.Exceptions;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("plan")]
    public class PlanController : ApiController, IPlanController
    {
        private readonly IPlanOperations _planOperations;

        public PlanController(IPlanOperations planOperations)
        {
            _planOperations = planOperations;
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetAllPlans()
        {
            var results = await _planOperations.GetAllPlans().ConfigureAwait(false);

            return Ok(results);
        }

        [HttpGet]
        [Route("{guid:Guid}")]
        public async Task<IHttpActionResult> GetPlan([FromUri]Guid guid)
        {
            try
            {
                var result = await _planOperations.GetPlan(guid).ConfigureAwait(false);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}