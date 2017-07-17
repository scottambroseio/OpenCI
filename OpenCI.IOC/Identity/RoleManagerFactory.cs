using Microsoft.AspNet.Identity;
using OpenCI.Identity.Dapper;

namespace OpenCI.IOC.Identity
{
    public class RoleManagerFactory
    {
        public static RoleManager<IdentityRole, int> Create(IRoleStore<IdentityRole, int> store)
        {
            var roleManager = new RoleManager<IdentityRole, int>(store);

            return roleManager;
        }
    }
}
