using Microsoft.AspNet.Identity;
using OpenCI.Identity.Dapper;

namespace OpenCI.IOC.Identity
{
    public class UserManagerFactory
    {
        public static UserManager<IdentityUser, int> Create(IUserStore<IdentityUser, int> store)
        {
            var userManager = new UserManager<IdentityUser, int>(store);

            return userManager;
        }
    }
}
