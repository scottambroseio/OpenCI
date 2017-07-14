using System;
using OpenCI.Data.Contracts;
using OpenCI.Data.Entities;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace OpenCI.Data.Implementation
{
    public class ProjectData : IProjectData
    {
        private IConnectionHelper _connectionHelper;

        public ProjectData(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
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
