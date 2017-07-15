using OpenCI.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenCI.Business.Contracts
{
    public interface IPlanOperations
    {
        Task<List<PlanModel>> GetAllPlans();
        Task<List<PlanModel>> GetAllPlansForProject(Guid projectGuid);
        Task<PlanModel> GetPlan(Guid planGuid);
        Task<PlanModel> CreatePlan(CreatePlanModel model);
        Task<bool> DeletePlan(Guid planGuid);
        //Task<PlanModel> CreatePlan(CreatePlanModel model);
    }
}
