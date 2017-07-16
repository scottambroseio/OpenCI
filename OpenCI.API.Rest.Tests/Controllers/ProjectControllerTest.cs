using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenCI.API.Rest.Controllers;
using OpenCI.API.Rest.Tests.Controllers.Contracts;
using OpenCI.Business.Contracts;
using OpenCI.Business.Models;
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

            projectOperations.Setup(o => o.GetProject(guid)).ReturnsAsync(expected);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result =
                await controller.GetProject(guid).ConfigureAwait(false) as OkNegotiatedContentResult<ProjectModel>;

            if (result == null) Assert.Fail();

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

            var result = await controller.GetProject(guid).ConfigureAwait(false) as BadRequestResult;

            if (result == null) Assert.Fail();

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task GetAllProjects_ShouldReturnTheCorrectListOfProject()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();
            var expected = new List<ProjectModel> {new ProjectModel {Guid = Guid.NewGuid()}};

            projectOperations.Setup(o => o.GetAllProjects()).ReturnsAsync(expected);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result =
                await controller.GetAllProjects()
                    .ConfigureAwait(false) as OkNegotiatedContentResult<List<ProjectModel>>;

            if (result == null) Assert.Fail();

            CollectionAssert.AreEquivalent(expected, result.Content);
        }

        [TestMethod]
        public async Task CreateProject_ShouldReturnTheCreatedProject()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var model = new CreateProjectModel();
            var expected = new ProjectModel();

            projectOperations.Setup(o => o.CreateProject(model)).ReturnsAsync(expected);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result =
                await controller.CreateProject(model).ConfigureAwait(false) as OkNegotiatedContentResult<ProjectModel>;

            if (result == null) Assert.Fail();

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task DeleteProject_ShouldReturnSuccessIfTheDeleteIsSuccessful()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.DeleteProject(guid)).ReturnsAsync(true);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.DeleteProject(guid).ConfigureAwait(false) as OkResult;

            if (result == null) Assert.Fail();

            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task DeleteProject_ShouldReturnBadRequestIfTheDeleteIsUnsuccessful()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            projectOperations.Setup(o => o.DeleteProject(guid)).ReturnsAsync(false);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result = await controller.DeleteProject(guid).ConfigureAwait(false) as BadRequestResult;

            if (result == null) Assert.Fail();

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

            projectOperations.Setup(o => o.UpdateProject(guid, model)).ReturnsAsync(expected);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result =
                await controller.UpdateProject(guid, model).ConfigureAwait(false) as
                    OkNegotiatedContentResult<ProjectModel>;

            if (result == null) Assert.Fail();

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

            var result = await controller.UpdateProject(guid, model).ConfigureAwait(false) as BadRequestResult;

            if (result == null) Assert.Fail();

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task GetPlansForProject_ShouldReturnTheCorrectPlans()
        {
            var projectOperations = new Mock<IProjectOperations>();
            var planOperations = new Mock<IPlanOperations>();
            var guid = Guid.NewGuid();
            var expected = new List<PlanModel> {new PlanModel()};

            planOperations.Setup(o => o.GetAllPlansForProject(guid)).ReturnsAsync(expected);

            var controller = new ProjectController(projectOperations.Object, planOperations.Object);

            var result =
                await controller.GetPlansForProject(guid).ConfigureAwait(false) as
                    OkNegotiatedContentResult<List<PlanModel>>;

            if (result == null) Assert.Fail();

            CollectionAssert.AreEquivalent(expected, result.Content);
        }
    }
}