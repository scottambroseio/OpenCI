using OpenCI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenCI.Data.Contracts
{
    public interface IPlanData
    {
        Task<List<Plan>> GetAllPlans();
        Task<List<Plan>> GetPlansForProject(Guid projectGuid);
        Task<Plan> GetPlan(Guid planGuid);
        //Task<Plan> CreatePlan(CreatePlanModel model);
        //Task<bool> DeletePlan(Guid planGuid);
        //Task<Plan> UpdatePlan(Guid planGuid, UpdatePlanModel model);
    }
}
