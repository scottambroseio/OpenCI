using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class IdentityUser : IUser<int>
    {
        // Used by dapper
        internal IdentityUser() {
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

        public int Id { get; internal set; }
        public string UserName { get; set; }

        internal string PasswordHash { get; set; }
        internal string SecurityStamp { get; set; }
        internal bool IsModified { get; set; }
        internal bool IsTransient { get; private set; }
    }
}
