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

            var result = await userManager.CreateAsync(new IdentityUser() { UserName = "ScottRanger" });
            var user = await userManager.FindByIdAsync(1);
            user.UserName = "foo";
            var result2 = await userManager.UpdateAsync(user);
            var user2 = await userManager.FindByNameAsync("foo");

            var result3 = await userManager.DeleteAsync(user2);
        }
    }
}
