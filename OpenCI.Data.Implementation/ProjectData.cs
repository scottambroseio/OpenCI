using System;
using OpenCI.Data.Contracts;
using OpenCI.Data.Entities;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using OpenCI.Business.Models;

namespace OpenCI.Data.Implementation
{
    public class ProjectData : IProjectData
    {
        private IConnectionHelper _connectionHelper;

        public ProjectData(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task<Project> CreateProject(CreateProjectModel model)
        {
            using (var connection = _connectionHelper.GetConnection())
            {

                var id = await connection.ExecuteScalarAsync<int>("INSERT INTO PROJECT (Name, Description) VALUES (@Name, @Description) SELECT SCOPE_IDENTITY()", model).ConfigureAwait(false);
                var entity = await connection.QuerySingleAsync<Project>("SELECT * FROM PROJECT WHERE Id = @Id", new { Id = id }).ConfigureAwait(false);

                return entity;
            }
        }

        public async Task<List<Project>> GetAllProjects()
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var results = await connection.QueryAsync<Project>("SELECT * FROM PROJECT").ConfigureAwait(false);

                return results.ToList();
            }
        }

        public async Task<Project> GetProject(Guid guid)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var result = await connection.QueryAsync<Project>("SELECT * FROM PROJECT WHERE Guid = @Guid", new { Guid = guid }).ConfigureAwait(false);

                return result.SingleOrDefault();
            }
        }
    }
}
