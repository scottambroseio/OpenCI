using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using Dapper;

namespace OpenCI.Identity.Dapper
{
    public class UserStore : IUserStore<IdentityUser, int>
    {
        private readonly IConnectionHelper _connectionHelper;

        public UserStore(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task CreateAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("INSERT INTO [USER] (UserName) VALUES (@UserName)", user);
            }
        }

        public async Task DeleteAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("DELETE FROM [USER] WHERE [Id] = @Id", user);
            }
        }

        public async Task<IdentityUser> FindByIdAsync(int userId)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [Id] = @Id", new { Id = userId });
            }
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [UserName] = @UserName", new { UserName = userName });
            }
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("UPDATE [USER] SET [UserName] = @UserName WHERE [Id] = @Id", user);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
