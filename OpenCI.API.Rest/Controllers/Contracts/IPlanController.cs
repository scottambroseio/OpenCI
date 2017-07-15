using OpenCI.Business.Models;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IPlanController
    {
        Task<IHttpActionResult> GetPlan(Guid planGuid);
        Task<IHttpActionResult> GetAllPlans();

        Task<IHttpActionResult> CreatePlan([FromBody]CreatePlanModel model);

        Task<IHttpActionResult> UpdatePlan([FromUri]Guid planGuid, [FromBody]UpdatePlanModel model);

        Task<IHttpActionResult> DeletePlan(Guid planGuid);
    }
}
