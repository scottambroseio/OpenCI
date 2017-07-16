using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System;

namespace OpenCI.Identity.Dapper
{
    public class UserStore :
        IUserStore<IdentityUser, int>,
        IUserClaimStore<IdentityUser, int>,
        IUserEmailStore<IdentityUser, int>,
        IUserLockoutStore<IdentityUser, int>,
        IUserPasswordStore<IdentityUser, int>,
        IUserPhoneNumberStore<IdentityUser, int>,
        IUserRoleStore<IdentityUser, int>,
        IUserSecurityStampStore<IdentityUser, int>
        //token
        //twofactpr
        
        
    {
        private readonly IConnectionHelper _connectionHelper;

        public UserStore(IConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public void Dispose()
        {

        }

        public async Task CreateAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync(@"
                    INSERT INTO [USER] (UserName, SecurityStamp, Email, EmailConfirmed)
                    VALUES (@UserName, @SecurityStamp, @Email, @EmailConfirmed);
                ", new
                {
                    UserName = user.UserName,
                    SecurityStamp = user.SecurityStamp,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed
                }).ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync(@"
                    DELETE FROM [Claim] WHERE [UserId] = @Id;
                    DELETE FROM [USER] WHERE [Id] = @Id;
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
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [Id] = @Id;", new {
                    Id = userId
                }).ConfigureAwait(false);
            }
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [UserName] = @UserName;", new {
                    UserName = userName
                }).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            if (!user.IsTransient && !user.IsModified) return;

            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync(@"
                    UPDATE [USER] SET
                    [UserName] = @UserName,
                    [Email] = @Email,
                    [LockoutEndDate] = @LockoutEndDate,
                    [LockoutEnabled] = @LockoutEnabled,
                    [AccessFailedCount] = @AccessFailedCount,
                    [PhoneNumber] = @PhoneNumber,
                    [PhoneNumberConfirmed] = @PhoneNumberConfirmed,
                    [EmailConfirmed] = @EmailConfirmed,
                    [PasswordHash] = @PasswordHash,
                    [SecurityStamp] = @SecurityStamp,
                    WHERE [Id] = @Id;", new
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    LockoutEndDate = user.LockoutEndDate,
                    LockoutEnabled = user.LockoutEnabled,
                    AccessFailedCount = user.AccessFailedCount,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    SecurityStamp = user.SecurityStamp,
                    Id = user.Id
                }).ConfigureAwait(false);
            }
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var results = await connection.QueryAsync<Claim>("SELECT * FROM [Claim] WHERE [UserId] = @UserId;", new { UserId = user.Id }).ConfigureAwait(false);

                return results.ToList();
            }
        }

        public async Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync("INSERT INTO [Claim] (UserId, Type, Value) VALUES (@UserId, @Type, @Value);", new
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
                await connection.ExecuteAsync("DELETE FROM [Claim] WHERE [UserId] = @UserId;", new
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

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            user.Email = email;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public async Task<IdentityUser> FindByEmailAsync(string email)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<IdentityUser>("SELECT * FROM [USER] WHERE [Email] = @Email;", new
                {
                    Email = email
                }).ConfigureAwait(false);
            }
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            return Task.FromResult(new DateTimeOffset(user.LockoutEndDate));
        }

        public Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDate = lockoutEnd.UtcDateTime;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            user.AccessFailedCount++;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            user.AccessFailedCount = 0;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public async Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var roleId = await connection.ExecuteScalarAsync<int>(@"
                    SELECT [Id] FROM [Role] WHERE [Name] = @Name
                ", new
                {
                    Name = roleName
                }).ConfigureAwait(false);

                await connection.ExecuteAsync("INSERT INTO [UserRole] (UserId, RoleId) VALUES (@UserId, @RoleId);", new
                {
                    UserId = user.Id,
                    RoleId = roleId
                }).ConfigureAwait(false);
            }
        }

        public async Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                await connection.ExecuteAsync(@"
                    DELETE ur FROM [UserRole] ur
                    INNER JOIN [Role] r
                    ON ur.[RoleId] = r.[Id]                    
                    WHERE r.[Name] = @Name
                    AND ur.[UserId] = @UserId",
                new
                {
                    UserId = user.Id,
                    Name = roleName
                }).ConfigureAwait(false);
            }
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var results = await connection.QueryAsync<string>(@"
                    SELECT [Role].[Name] FROM [Role]
                    INNER JOIN [UserRole]
                    ON [Role].[Id] = [UserRole].[RoleId]
                    WHERE [UserRole].[UserId] = @UserId
                ", new
                {
                    UserId = user.Id
                }).ConfigureAwait(false);

                return results.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            using (var connection = _connectionHelper.GetConnection())
            {
                var result = await connection.ExecuteScalarAsync<int>(@"
                    SELECT COUNT(*) FROM [UserRole]
                    INNER JOIN [Role]
                    ON [Role].[Id] = [UserRole].[RoleId]
                    WHERE [UserRole].[UserId] = @UserId
                    AND [Role].[Name] = @Name
                ", new
                {
                    UserId = user.Id,
                    Name = roleName
                }).ConfigureAwait(false);

                return result == 1;
            }
        }

        public Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
            user.IsModified = true;

            return Task.FromResult(0);
        }
    }
}
