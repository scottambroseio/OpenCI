using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class RoleStore : IRoleStore<IdentityRole, int>
    {
        private readonly IConnectionHelper _connectionHelper;

        public RoleStore(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public void Dispose()
        {
        }

        public async Task CreateAsync(IdentityRole role)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("[Identity].[spCreateRole]", new {role.Name},
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(IdentityRole role)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("[Identity].[spDeleteRole]", new {role.Name},
                        commandType: CommandType.StoredProcedure)
                    .ConfigureAwait(false);
            }
        }

        public async Task<IdentityRole> FindByIdAsync(int roleId)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection
                    .QuerySingleOrDefaultAsync<IdentityRole>("[Identity].[spGetRoleById]",
                        new {Id = roleId}, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task<IdentityRole> FindByNameAsync(string roleName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection
                    .QuerySingleOrDefaultAsync<IdentityRole>("[Identity].[spGetRoleByName]",
                        new {Name = roleName}, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(IdentityRole role)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("[Identity].[spUpdateRole]",
                    new {role.Id, role.Name}, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
            }
        }
    }
}