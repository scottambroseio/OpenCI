using OpenCI.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCI.Business.Contracts
{
    public interface IPlanOperations
    {
        Task<List<PlanModel>> GetAllPlans();
        Task<List<PlanModel>> GetPlansForProject(Guid projectGuid);
        Task<PlanModel> GetPlan(Guid planGuid);
    }
}
