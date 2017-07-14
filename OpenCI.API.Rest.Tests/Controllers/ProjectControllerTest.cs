using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Controllers;
using System.Web.Http.Results;
using Moq;
using OpenCI.Business.Models;
using OpenCI.Contracts.Business;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        [TestMethod]
        public async Task GetProject_ShouldReturnTheCorrectProject()
        {
            var operations = new Mock<IProjectOperations>();
            var expected = new ProjectModel
            {
                Id = 1,
                Name = "Test"
            };
            var guid = Guid.NewGuid();

            operations.Setup(o => o.GetProject(guid)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(operations.Object);

            var result = await controller.GetProject(guid) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task GetProject_ShouldReturnNullIfTheProjectDoesntExist()
        {
            var operations = new Mock<IProjectOperations>();
            var guid = Guid.NewGuid();

            operations.Setup(o => o.GetProject(guid)).Returns(Task.FromResult<ProjectModel>(null));

            var controller = new ProjectController(operations.Object);

            var result = await controller.GetProject(guid) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public async Task GetAllProjects_ShouldReturnTheCorrectListOfProject()
        {
            var operations = new Mock<IProjectOperations>();
            var guid = Guid.NewGuid();
            var expected = new List<ProjectModel> { new ProjectModel {Id = 123 } };

            operations.Setup(o => o.GetAllProjects()).Returns(
                Task.FromResult(expected)
            );

            var controller = new ProjectController(operations.Object);

            var result = await controller.GetAllProjects() as OkNegotiatedContentResult<List<ProjectModel>>;

            CollectionAssert.AreEquivalent(expected, result.Content);
        }
    }
}
