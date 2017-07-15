using OpenCI.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCI.Data.Entities;
using Dapper;
using System.Linq;

namespace OpenCI.Data.Implementation
{
    public class PlanData : IPlanData
    {
        private IConnectionHelper _connectionHelper;

        public PlanData(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<List<Plan>> GetAllPlans()
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var results = await connection.QueryAsync<Plan>("SELECT * FROM [PLAN]").ConfigureAwait(false);

                return results.ToList();
            }
        }

        public async Task<Plan> GetPlan(Guid planGuid)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Plan>("SELECT * FROM [PLAN] WHERE [Guid] = @Guid", new { Guid = planGuid }).ConfigureAwait(false);
            }
        }

        public async Task<List<Plan>> GetPlansForProject(Guid projectGuid)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var results = await connection.QueryAsync<Plan>("SELECT * FROM [PLAN] WHERE [ProjectGuid] = @ProjectGuid", new { ProjectGuid = projectGuid }).ConfigureAwait(false);

                return results.ToList();
            }
        }
    }
}
