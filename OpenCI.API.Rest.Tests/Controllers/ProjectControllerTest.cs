using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Controllers;
using System.Web.Http.Results;
using Moq;
using OpenCI.Business.Models;
using OpenCI.Contracts.Business;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public void Get_ShouldReturnTheCorrectProject()
        {
            var operations = new Mock<IProjectOperations>();
            var expected = new ProjectModel
            {
                Id = 1,
                Name = "Test"
            };

            operations.Setup(o => o.GetProjectById(1)).Returns(expected);

            var controller = new ProjectController(operations.Object);

            var result = controller.Get(1) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }
    }
}
