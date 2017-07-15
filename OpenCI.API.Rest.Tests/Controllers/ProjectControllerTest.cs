using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Controllers;
using System.Web.Http.Results;
using Moq;
using OpenCI.Business.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using OpenCI.API.Rest.Tests.Controllers.Contracts;
using OpenCI.Business.Contracts;
using OpenCI.Exceptions;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest : IProjectControllerTest
    {
        [TestMethod]
        public async Task GetProject_ShouldReturnTheCorrectProject()
        {
            var operations = new Mock<IProjectOperations>();
            var expected = new ProjectModel
            {
                Guid = Guid.NewGuid(),
                Name = "Test"
            };
            var guid = Guid.NewGuid();

            operations.Setup(o => o.GetProject(guid)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(operations.Object);

            var result = await controller.GetProject(guid) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task GetProject_ShouldReturnBadRequestIfTheEntityDoesNotExist()
        {
            var operations = new Mock<IProjectOperations>();

            var guid = Guid.NewGuid();

            operations.Setup(o => o.GetProject(guid)).Throws<EntityNotFoundException>();

            var controller = new ProjectController(operations.Object);

            var result = await controller.GetProject(guid);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task GetAllProjects_ShouldReturnTheCorrectListOfProject()
        {
            var operations = new Mock<IProjectOperations>();
            var guid = Guid.NewGuid();
            var expected = new List<ProjectModel> { new ProjectModel { Guid = Guid.NewGuid() } };

            operations.Setup(o => o.GetAllProjects()).Returns(
                Task.FromResult(expected)
            );

            var controller = new ProjectController(operations.Object);

            var result = await controller.GetAllProjects() as OkNegotiatedContentResult<List<ProjectModel>>;

            CollectionAssert.AreEquivalent(expected, result.Content);
        }

        [TestMethod]
        public async Task CreateProject_ShouldReturnTheCreatedProject()
        {
            var operations = new Mock<IProjectOperations>();
            var model = new CreateProjectModel();
            var expected = new ProjectModel();

            operations.Setup(o => o.CreateProject(model)).Returns(
                Task.FromResult(expected)
            );

            var controller = new ProjectController(operations.Object);

            var result = await controller.CreateProject(model) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task DeleteProject_ShouldReturnSuccessIfTheDeleteIsSuccessfull()
        {
            var operations = new Mock<IProjectOperations>();
            var guid = Guid.NewGuid();

            operations.Setup(o => o.DeleteProject(guid)).Returns(Task.FromResult(true));

            var controller = new ProjectController(operations.Object);

            var result = await controller.DeleteProject(guid) as OkResult;

            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task DeleteProject_ShouldReturnBadRequestIfTheDeleteIsUnsuccessfull()
        {
            var operations = new Mock<IProjectOperations>();
            var guid = Guid.NewGuid();

            operations.Setup(o => o.DeleteProject(guid)).Returns(Task.FromResult(false));

            var controller = new ProjectController(operations.Object);

            var result = await controller.DeleteProject(guid) as BadRequestResult;

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateProject_ShouldReturnTheUpdatedProject()
        {
            var operations = new Mock<IProjectOperations>();
            var expected = new ProjectModel();
            var model = new UpdateProjectModel();
            var guid = Guid.NewGuid();

            operations.Setup(o => o.UpdateProject(guid, model)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(operations.Object);

            var result = await controller.UpdateProject(guid, model) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task UpdateProject_ShouldReturnBadRequestIfTheEntityDoesNotExist()
        {
            var operations = new Mock<IProjectOperations>();

            var guid = Guid.NewGuid();
            var model = new UpdateProjectModel();

            operations.Setup(o => o.UpdateProject(guid, model)).Throws<EntityNotFoundException>();

            var controller = new ProjectController(operations.Object);

            var result = await controller.UpdateProject(guid, model);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }
    }
}
