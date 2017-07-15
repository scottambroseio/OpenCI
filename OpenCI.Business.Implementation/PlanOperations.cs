using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCI.Business.Models;
using OpenCI.Data.Contracts;
using AutoMapper;
using OpenCI.Business.Contracts;

namespace OpenCI.Business.Implementation
{
    public class PlanOperations : IPlanOperations
    {
        private readonly IPlanData _planData;
        private readonly IMapper _mapper;

        public PlanOperations(IPlanData planData, IMapper mapper)
        {
            _planData = planData;
            _mapper = mapper;
        }

        public async Task<List<PlanModel>> GetAllPlans()
        {
            var entities = await _planData.GetAllPlans().ConfigureAwait(false);

            var mappedModels = _mapper.Map<List<PlanModel>>(entities);

            return mappedModels;
        }

        public async Task<PlanModel> GetPlan(Guid planGuid)
        {
            var entity = await _planData.GetPlan(planGuid).ConfigureAwait(false);

            var mappedModel = _mapper.Map<PlanModel>(entity);

            return mappedModel;
        }

        public async Task<List<PlanModel>> GetPlansForProject(Guid projectGuid)
        {
            var entities = await _planData.GetPlansForProject(projectGuid).ConfigureAwait(false);

            var mappedModels = _mapper.Map<List<PlanModel>>(entities);

            return mappedModels;
        }
    }
}
