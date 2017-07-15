using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace OpenCI.Identity.Dapper
{
    public class UserStore :
        IUserStore<IdentityUser, int>,
        IUserClaimStore<IdentityUser, int>,
        IUserSecurityStampStore<IdentityUser, int>,
        IUserPasswordStore<IdentityUser, int>
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
                await connection.ExecuteAsync("INSERT INTO [USER] (UserName, SecurityStamp) VALUES (@UserName, @SecurityStamp)", new
                {
                    UserName = user.UserName,
                    SecurityStamp = user.SecurityStamp
                }).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync(@"
                    DELETE FROM [Claim] WHERE [UserId] = @Id;
                    DELETE FROM [USER] WHERE [Id] = @Id
                ", new
                {
                    Id = user.Id
                }).ConfigureAwait(false);
            }
        }

        public async Task<IdentityUser> FindByIdAsync(int userId)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [Id] = @Id", new {
                    Id = userId
                }).ConfigureAwait(false);
            }
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [UserName] = @UserName", new {
                    UserName = userName
                }).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            if (!user.IsTransient && !user.IsModified) return;

            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("UPDATE [USER] SET [UserName] = @UserName, [SecurityStamp] = @SecurityStamp, [PasswordHash] = @PasswordHash WHERE [Id] = @Id", new
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    SecurityStamp = user.SecurityStamp,
                    PasswordHash = user.PasswordHash
                }).ConfigureAwait(false);
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var results = await connection.QueryAsync<Claim>("SELECT * FROM [Claim] WHERE [UserId] = @UserId", new { UserId = user.Id }).ConfigureAwait(false);

                return results.ToList();
            }
        }

        public async Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("INSERT INTO [Claim] (UserId, Type, Value) VALUES (@UserId, @Type, @Value)", new
                {
                    UserId = user.Id,
                    Type = claim.Type,
                    Value = claim.Value
                }).ConfigureAwait(false);
            }
        }

        public async Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("DELETE FROM [Claim] WHERE [UserId] = @UserId", new
                {
                    UserId = user.Id
                }).ConfigureAwait(false);
            }
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash != default(string));
        }

        public void Dispose()
        {

        }
    }
}
