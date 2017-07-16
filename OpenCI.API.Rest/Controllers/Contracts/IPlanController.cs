using System;
using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.Business.Models;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IPlanController
    {
        Task<IHttpActionResult> GetPlan(Guid planGuid);
        Task<IHttpActionResult> GetAllPlans();
        Task<IHttpActionResult> CreatePlan(CreatePlanModel model);
        Task<IHttpActionResult> UpdatePlan(Guid planGuid, UpdatePlanModel model);
        Task<IHttpActionResult> DeletePlan(Guid planGuid);
    }
}