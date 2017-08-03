using System;
using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.Business.Contracts;
using OpenCI.Business.Models;
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
        public async Task<IHttpActionResult> GetPlan([FromUri] Guid guid)
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

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> CreatePlan([FromBody] CreatePlanModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _planOperations.CreatePlan(model).ConfigureAwait(false);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{planGuid:Guid}")]
        public async Task<IHttpActionResult> DeletePlan([FromUri] Guid planGuid)
        {
            var result = await _planOperations.DeletePlan(planGuid).ConfigureAwait(false);

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpPut]
        [Route("{planGuid:Guid}")]
        public async Task<IHttpActionResult> UpdatePlan([FromUri] Guid planGuid, [FromBody] UpdatePlanModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = await _planOperations.UpdatePlan(planGuid, model).ConfigureAwait(false);

                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}