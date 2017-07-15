using OpenCI.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCI.Data.Entities;
using Dapper;
using System.Linq;
using OpenCI.Business.Models;
using OpenCI.Exceptions;

namespace OpenCI.Data.Implementation
{
    public class PlanData : IPlanData
    {
        private IConnectionHelper _connectionHelper;

        public PlanData(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<Plan> CreatePlan(CreatePlanModel model)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var projectId = await connection.ExecuteScalarAsync<int>("SELECT [ID] FROM [Project] WHERE [Guid] = @Guid", new { Guid = model.ProjectGuid }).ConfigureAwait(false);

                if (projectId == default(int)) throw new EntityNotFoundException($"Unable to find the project with the guid: {model.ProjectGuid}");

                var id = await connection.ExecuteScalarAsync<int>(@"
                    INSERT INTO [Plan] ([Name], [Description], [ProjectId], [ProjectGuid], [Enabled])
                    VALUES (@Name, @Description, @ProjectId, @ProjectGuid, @Enabled)
                    SELECT SCOPE_IDENTITY()",
                    new
                    {
                        ProjectId = projectId,
                        Name = model.Name,
                        Description = model.Description,
                        ProjectGuid = model.ProjectGuid,
                        Enabled = model.Enabled
                    }
                ).ConfigureAwait(false);

                return await connection.QuerySingleOrDefaultAsync<Plan>(@"SELECT * FROM [Plan] WHERE [Id] = @Id", new { Id = id }).ConfigureAwait(false);
            }
        }

        public async Task<bool> DeletePlan(Guid planGuid)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var result = await connection.ExecuteAsync("DELETE FROM [Plan] WHERE [Guid] = @Guid", new { Guid = planGuid }).ConfigureAwait(false);

                return result == 1;
            }
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
