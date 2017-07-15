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
    public class ProjectControllerTest : IProjectControllerTests
    {
        [TestMethod]
        public async Task GetProject_ShouldReturnTheCorrectProject()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var expected = new ProjectModel
            {
                Guid = Guid.NewGuid(),
                Name = "Test"
            };
            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.GetProject(guid)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.GetProject(guid).ConfigureAwait(false) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task GetProject_ShouldReturnBadRequestIfTheEntityDoesNotExist()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.GetProject(guid)).Throws<EntityNotFoundException>();

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.GetProject(guid).ConfigureAwait(false);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task GetAllProjects_ShouldReturnTheCorrectListOfProject()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();
            var expected = new List<ProjectModel> { new ProjectModel { Guid = Guid.NewGuid() } };

            projectOperations.Setup(o => o.GetAllProjects()).Returns(
                Task.FromResult(expected)
            );

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.GetAllProjects().ConfigureAwait(false) as OkNegotiatedContentResult<List<ProjectModel>>;

            CollectionAssert.AreEquivalent(expected, result.Content);
        }

        [TestMethod]
        public async Task CreateProject_ShouldReturnTheCreatedProject()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var model = new CreateProjectModel();
            var expected = new ProjectModel();

            projectOperations.Setup(o => o.CreateProject(model)).Returns(
                Task.FromResult(expected)
            );

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.CreateProject(model).ConfigureAwait(false) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task DeleteProject_ShouldReturnSuccessIfTheDeleteIsSuccessfull()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.DeleteProject(guid)).Returns(Task.FromResult(true));

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.DeleteProject(guid).ConfigureAwait(false) as OkResult;

            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task DeleteProject_ShouldReturnBadRequestIfTheDeleteIsUnsuccessfull()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.DeleteProject(guid)).Returns(Task.FromResult(false));

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.DeleteProject(guid).ConfigureAwait(false) as BadRequestResult;

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdateProject_ShouldReturnTheUpdatedProject()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var expected = new ProjectModel();
            var model = new UpdateProjectModel();
            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.UpdateProject(guid, model)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.UpdateProject(guid, model).ConfigureAwait(false) as OkNegotiatedContentResult<ProjectModel>;

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task UpdateProject_ShouldReturnBadRequestIfTheEntityDoesNotExist()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();
            var model = new UpdateProjectModel();

            projectOperations.Setup(o => o.UpdateProject(guid, model)).Throws<EntityNotFoundException>();

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.UpdateProject(guid, model).ConfigureAwait(false);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task GetPlansForProject_ShouldRrturnTheCorrectPlans()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();
            var guid = Guid.NewGuid();
            var expected = new List<PlanModel> { new PlanModel() };

            planOperations.Setup(o => o.GetAllPlansForProject(guid)).Returns(Task.FromResult(expected));

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.GetPlansForProject(guid).ConfigureAwait(false) as OkNegotiatedContentResult<List<PlanModel>>;

            CollectionAssert.AreEquivalent(expected, result.Content);
        }
    }
}
