using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class RoleStore : IRoleStore<IdentityRole, int>
    {
        private readonly IDbConnection _connection;

        public RoleStore(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task CreateAsync(IdentityRole role)
        {
            await _connection.ExecuteAsync("[Identity].[spCreateRole]", new {role.Name},
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(IdentityRole role)
        {
            await _connection.ExecuteAsync("[Identity].[spDeleteRole]", new {role.Name},
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public async Task<IdentityRole> FindByIdAsync(int roleId)
        {
            return await _connection
                .QuerySingleOrDefaultAsync<IdentityRole>("[Identity].[spGetRoleById]",
                    new {Id = roleId}, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }

        public async Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return await _connection
                .QuerySingleOrDefaultAsync<IdentityRole>("[Identity].[spGetRoleByName]",
                    new {Name = roleName}, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }

        public async Task UpdateAsync(IdentityRole role)
        {
            await _connection.ExecuteAsync("[Identity].[spUpdateRole]",
                new {role.Id, role.Name}, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
        }
    }
}