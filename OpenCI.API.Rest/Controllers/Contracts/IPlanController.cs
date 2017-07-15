using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IPlanController
    {
        Task<IHttpActionResult> GetPlan(Guid planGuid);
        Task<IHttpActionResult> GetAllPlans();
        Task<IHttpActionResult> DeletePlan(Guid planGuid);
    }
}
