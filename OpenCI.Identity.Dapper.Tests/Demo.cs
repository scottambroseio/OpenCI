using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace OpenCI.Identity.Dapper.Tests
{
    [TestClass]
    public class Demo
    {
        [TestMethod]
        public async Task DoWork()
        {
            var connectionHelper = new ConnectionHelper("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OpenCIIdentityDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var userStore = new UserStore(connectionHelper);
            var userManager = new UserManager<IdentityUser, int>(userStore);
            var user = await userManager.FindByIdAsync(4).ConfigureAwait(false);
            string stamp;
            stamp = await userManager.GetSecurityStampAsync(4);
            Console.WriteLine(stamp);
            var x = await userManager.AddPasswordAsync(4, "insecurepassword").ConfigureAwait(false);
            stamp = await userManager.GetSecurityStampAsync(4);
            Console.WriteLine(stamp);
        }
    }
}
