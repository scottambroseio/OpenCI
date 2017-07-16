using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCI.Business.Models;
using OpenCI.Data.Entities;

namespace OpenCI.Data.Contracts
{
    public interface IPlanData
    {
        Task<List<Plan>> GetAllPlans();
        Task<List<Plan>> GetPlansForProject(Guid projectGuid);
        Task<Plan> GetPlan(Guid planGuid);
        Task<Plan> CreatePlan(CreatePlanModel model);
        Task<Plan> UpdatePlan(Guid planGuid, UpdatePlanModel model);
        Task<bool> DeletePlan(Guid planGuid);
    }
}