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
                await connection.ExecuteAsync("INSERT INTO [Identity].[Role] (Name) VALUES (@Name);", new {role.Name})
                    .ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(IdentityRole role)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("DELETE FROM [Identity].[Role] WHERE [Name] = @Name;", new {role.Name})
                    .ConfigureAwait(false);
            }
        }

        public async Task<IdentityRole> FindByIdAsync(int roleId)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection
                    .QuerySingleOrDefaultAsync<IdentityRole>("SELECT * FROM [Identity].[Role] WHERE [Id] = @Id;",
                        new {Id = roleId}).ConfigureAwait(false);
            }
        }

        public async Task<IdentityRole> FindByNameAsync(string roleName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection
                    .QuerySingleOrDefaultAsync<IdentityRole>("SELECT * FROM [Identity].[Role] WHERE [Name] = @Name;",
                        new {Name = roleName}).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(IdentityRole role)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("UPDATE [Identity].[Role] SET [Name] = @Name WHERE [Id] = @Id;",
                    new {role.Id, role.Name}).ConfigureAwait(false);
            }
        }
    }
}