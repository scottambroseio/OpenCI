using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using Dapper;

namespace OpenCI.Identity.Dapper
{
    //public class RoleStore : IRoleStore<IdentityRole, int>
    //{
    //    private readonly IConnectionHelper _connectionHelper;

    //    public RoleStore(IConnectionHelper connectionHelper)
    //    {
    //        _connectionHelper = connectionHelper;
    //    }

    //    public async Task CreateAsync(IdentityRole role)
    //    {
    //        using (var connection = _connectionHelper.GetConnection())
    //        {
    //            await connection.ExecuteAsync("INSERT INTO [Role] (Name) VALUES (@Name)", role).ConfigureAwait(false);
    //        }
    //    }

    //    public async Task DeleteAsync(IdentityRole role)
    //    {
    //        using (var connection = _connectionHelper.GetConnection())
    //        {
    //            await connection.ExecuteAsync("DELETE FROM [Role] WHERE [Id] = @Id", role).ConfigureAwait(false);
    //        }
    //    }

    //    public async Task<IdentityRole> FindByIdAsync(int roleId)
    //    {
    //        using (var connection = _connectionHelper.GetConnection())
    //        {
    //            return await connection.QuerySingleOrDefaultAsync<IdentityRole>("SELECT * FROM [Role] WHERE [Id] = @Id", new { Id = roleId }).ConfigureAwait(false);
    //        }
    //    }

    //    public async Task<IdentityRole> FindByNameAsync(string roleName)
    //    {
    //        using (var connection = _connectionHelper.GetConnection())
    //        {
    //            return await connection.QuerySingleOrDefaultAsync<IdentityRole>("SELECT * FROM [Role] WHERE [Name] = @Name", new { Name = roleName }).ConfigureAwait(false);
    //        }
    //    }

    //    public async Task UpdateAsync(IdentityRole role)
    //    {
    //        using (var connection = _connectionHelper.GetConnection())
    //        {
    //            await connection.ExecuteAsync("UPDATE [Role] SET [Name] = @Name WHERE [Id] = @Id", role).ConfigureAwait(false);
    //        }
    //    }

    //    public void Dispose()
    //    {
            
    //    }
    //}
}
