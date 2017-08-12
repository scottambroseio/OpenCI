using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class UserStore : IUserClaimStore<IdentityUser, int>,
        IUserEmailStore<IdentityUser, int>,
        IUserLockoutStore<IdentityUser, int>,
        IUserPasswordStore<IdentityUser, int>,
        IUserPhoneNumberStore<IdentityUser, int>,
        IUserRoleStore<IdentityUser, int>,
        IUserSecurityStampStore<IdentityUser, int>,
        IUserTwoFactorStore<IdentityUser, int>,
        IUserLoginStore<IdentityUser, int>
    {
        private readonly IDbConnection _connection;

        public UserStore(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task CreateAsync(IdentityUser user)
        {
            await _connection.ExecuteAsync(@"
                    INSERT INTO [Identity].[User] (UserName, SecurityStamp, PasswordHash, Email)
                    VALUES (@UserName, @SecurityStamp, @PasswordHash, @Email);
                ", new
            {
                user.UserName,
                user.SecurityStamp,
                user.PasswordHash,
                user.Email
            }).ConfigureAwait(false);

            user.IsTransient = false;
        }

        public async Task DeleteAsync(IdentityUser user)
        {
            await _connection.ExecuteAsync(@"
                    DELETE FROM [Identity].[Claim] WHERE [UserId] = @Id;
                    DELETE FROM [Identity].[User] WHERE [Id] = @Id;
                ", new
            {
                user.Id
            }).ConfigureAwait(false);
        }

        public async Task<IdentityUser> FindByIdAsync(int userId)
        {
            return await _connection.QuerySingleOrDefaultAsync<IdentityUser>(@"
                    SELECT * FROM [Identity].[User]
                    WHERE [Id] = @Id;
                ", new
            {
                Id = userId
            }).ConfigureAwait(false);
        }

        public async Task<IdentityUser> FindByNameAsync(string userName)
        {
            return await _connection.QuerySingleOrDefaultAsync<IdentityUser>(@"
                    SELECT * FROM [Identity].[User]
                    WHERE [UserName] = @UserName;
                ", new
            {
                UserName = userName
            }).ConfigureAwait(false);
        }

        public async Task UpdateAsync(IdentityUser user)
        {
            if (!user.IsTransient && !user.IsModified) return;

            await _connection.ExecuteAsync(@"
                    UPDATE [Identity].[User] SET
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
                    [TwoFactorEnabled] = @TwoFactorEnabled
                    WHERE [Id] = @Id
                ;", new
            {
                user.UserName,
                user.Email,
                user.LockoutEndDate,
                user.LockoutEnabled,
                user.AccessFailedCount,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                user.EmailConfirmed,
                user.PasswordHash,
                user.SecurityStamp,
                user.TwoFactorEnabled,
                user.Id
            }).ConfigureAwait(false);

            user.IsTransient = false;
            user.IsModified = false;
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var results = await _connection.QueryAsync<Claim>(@"
                    SELECT [Type], [Value] FROM [Identity].[Claim]
                    WHERE [UserId] = @UserId;
                ", new
            {
                UserId = user.Id
            }).ConfigureAwait(false);

            return results.ToList();
        }

        public async Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            await _connection.ExecuteAsync(@"
                    INSERT INTO [Identity].[Claim] (UserId, Type, Value)
                    VALUES (@UserId, @Type, @Value);", new
            {
                UserId = user.Id,
                claim.Type,
                claim.Value
            }).ConfigureAwait(false);
        }

        public async Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            await _connection.ExecuteAsync(@"
                    DELETE FROM [Identity].[Claim]
                    WHERE [UserId] = @UserId;
                ", new
            {
                UserId = user.Id
            }).ConfigureAwait(false);
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
            return await _connection.QuerySingleOrDefaultAsync<IdentityUser>(@"
                    SELECT * FROM [Identity].[User]
                    WHERE [Email] = @Email;
                ", new
            {
                Email = email
            }).ConfigureAwait(false);
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

        public async Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            await _connection.ExecuteAsync(@"
                    INSERT INTO [Identity].[UserLogin] (LoginProvider, ProviderKey, UserId)
                    VALUES (@LoginProvider, @ProviderKey, @UserId);
                ", new
            {
                UserId = user.Id,
                login.ProviderKey,
                login.LoginProvider
            }).ConfigureAwait(false);
        }

        public async Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            await _connection.ExecuteAsync(@"
                    DELETE FROM [Identity].[UserLogin]
                    WHERE [UserId] = @UserId;
                ", new
            {
                UserId = user.Id
            }).ConfigureAwait(false);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            var results = await _connection.QueryAsync<UserLoginInfo>(@"
                    SELECT [LoginProvider], [ProviderKey] FROM [Identity].[UserLogin]
                    WHERE [UserId] = @UserId
                ", new
            {
                UserId = user.Id
            }).ConfigureAwait(false);

            return results.ToList();
        }

        public async Task<IdentityUser> FindAsync(UserLoginInfo login)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<IdentityUser>(@"
                    SELECT u.* FROM [Identity].[UserLogin]
                    INNER JOIN [Identity].[User] u
                    ON [Identity].[UserLogin].[Id] = u.Id
                    WHERE [UserLogin].LoginProvider = @LoginProvider
                    AND [UserLogin].ProviderKey = @ProviderKey
                ", new
            {
                login.ProviderKey,
                login.LoginProvider
            }).ConfigureAwait(false);

            return result;
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

        public async Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            var roleId = await _connection.ExecuteScalarAsync<int>(@"
                    SELECT [Id] FROM [Identity].[Role]
                    WHERE [Name] = @Name
                ", new
            {
                Name = roleName
            }).ConfigureAwait(false);

            await _connection.ExecuteAsync(@"
                    INSERT INTO [Identity].[UserRole] (UserId, RoleId)
                    VALUES (@UserId, @RoleId);
                ", new
            {
                UserId = user.Id,
                RoleId = roleId
            }).ConfigureAwait(false);
        }

        public async Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            await _connection.ExecuteAsync(@"
                    DELETE ur FROM [Identity].[UserRole] ur
                    INNER JOIN [Identity].[Role] r
                    ON ur.[RoleId] = r.[Id]                    
                    WHERE r.[Name] = @Name
                    AND ur.[UserId] = @UserId", new
            {
                UserId = user.Id,
                Name = roleName
            }).ConfigureAwait(false);
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            var results = await _connection.QueryAsync<string>(@"
                    SELECT r.[Name] FROM [Identity].[Role] r
                    INNER JOIN [Identity].[UserRole] ur
                    ON r.[Id] = ur.[RoleId]
                    WHERE ur.[UserId] = @UserId
                ", new
            {
                UserId = user.Id
            }).ConfigureAwait(false);

            return results.ToList();
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            var result = await _connection.ExecuteScalarAsync<int>(@"
                    SELECT COUNT(*) FROM [Identity].[UserRole] ur
                    INNER JOIN [Identity].[Role] r
                    ON r.[Id] = ur.[RoleId]
                    WHERE ur.[UserId] = @UserId
                    AND r.[Name] = @Name
                ", new
            {
                UserId = user.Id,
                Name = roleName
            }).ConfigureAwait(false);

            return result == 1;
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

        public Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            user.TwoFactorEnabled = enabled;
            user.IsModified = true;

            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }
    }
}