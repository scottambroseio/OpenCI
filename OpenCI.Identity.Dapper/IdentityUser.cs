using System;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class IdentityUser : IUser<int>
    {
        // Used by dapper
        internal IdentityUser()
        {
            IsModified = false;
            IsTransient = false;
        }

        // Used for creating a new user
        public IdentityUser(string userName)
        {
            UserName = userName;
            IsTransient = true;
            IsModified = false;
        }

        public string Email { get; set; }

        internal bool TwoFactorEnabled { get; set; }
        internal DateTime LockoutEndDate { get; set; }
        internal bool LockoutEnabled { get; set; }
        internal int AccessFailedCount { get; set; }
        internal string PhoneNumber { get; set; }
        internal bool PhoneNumberConfirmed { get; set; }
        internal bool EmailConfirmed { get; set; }
        internal string PasswordHash { get; set; }
        internal string SecurityStamp { get; set; }
        internal bool IsModified { get; set; }
        internal bool IsTransient { get; set; }

        public int Id { get; internal set; }
        public string UserName { get; set; }
    }
}