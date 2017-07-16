using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCI.Business.Models;

namespace OpenCI.Business.Contracts
{
    public interface IPlanOperations
    {
        Task<List<PlanModel>> GetAllPlans();
        Task<List<PlanModel>> GetAllPlansForProject(Guid projectGuid);
        Task<PlanModel> GetPlan(Guid planGuid);
        Task<PlanModel> CreatePlan(CreatePlanModel model);
        Task<PlanModel> UpdatePlan(Guid projectGuid, UpdatePlanModel model);
        Task<bool> DeletePlan(Guid planGuid);
    }
}