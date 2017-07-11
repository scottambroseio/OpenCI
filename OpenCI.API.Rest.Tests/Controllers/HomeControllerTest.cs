using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Controllers;
using System.Web.Http.Results;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Get_ShouldReturnTrue()
        {
            var controller = new HomeController();

            var result = controller.Get() as OkNegotiatedContentResult<bool>;

            Assert.AreEqual(true, result.Content);
        }
    }
}
