using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Controllers;
using System.Web.Http.Results;
using Moq;
using OpenCI.Contracts.Business;
using OpenCI.Data.Entities;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public void Get_ShouldReturnTheCorrectProject()
        {
            var operations = new Mock<IProjectOperations>();
            var expected = new Project
            {
                Id = 1,
                Name = "Test"
            };

            operations.Setup(o => o.GetProjectById(1)).Returns(expected);

            var controller = new ProjectController(operations.Object);

            var result = controller.Get(1) as OkNegotiatedContentResult<Project>;

            Assert.AreEqual(expected, result.Content);
        }
    }
}
