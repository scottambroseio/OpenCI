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
    public class PlanControllerTests : IPlanControllerTests
    {
        [TestMethod]
        public async Task CreatePlan_ShouldReturnTheCreatedPlan()
        {
            var planOperations = new Mock<IPlanOperations>();

            var model = new CreatePlanModel();
            var expected = new PlanModel();

            planOperations.Setup(o => o.CreatePlan(model)).ReturnsAsync(expected);

            var controller = new PlanController(planOperations.Object);

            var result =
                await controller.CreatePlan(model).ConfigureAwait(false) as OkNegotiatedContentResult<PlanModel>;

            if (result == null) Assert.Fail();

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task DeletePlan_ShouldReturnBadRequestIfTheDeleteIsUnsuccessful()
        {
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            planOperations.Setup(o => o.DeletePlan(guid)).ReturnsAsync(false);

            var controller = new PlanController(planOperations.Object);

            var result = await controller.DeletePlan(guid).ConfigureAwait(false);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task DeletePlan_ShouldReturnSuccessIfTheDeleteIsSuccessful()
        {
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            planOperations.Setup(o => o.DeletePlan(guid)).ReturnsAsync(true);

            var controller = new PlanController(planOperations.Object);

            var result = await controller.DeletePlan(guid).ConfigureAwait(false);

            Assert.AreEqual(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task GetAllPlans_ShouldReturnTheCorrectListOfPlans()
        {
            var planOperations = new Mock<IPlanOperations>();

            var expected = new List<PlanModel> {new PlanModel {Guid = Guid.NewGuid()}};

            planOperations.Setup(o => o.GetAllPlans()).ReturnsAsync(expected);

            var controller = new PlanController(planOperations.Object);

            var result =
                await controller.GetAllPlans().ConfigureAwait(false) as OkNegotiatedContentResult<List<PlanModel>>;

            if (result == null) Assert.Fail();

            CollectionAssert.AreEquivalent(expected, result.Content);
        }

        [TestMethod]
        public async Task GetPlan_ShouldReturnBadRequestIfTheEntityDoesNotExist()
        {
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();

            planOperations.Setup(o => o.GetPlan(guid)).Throws<EntityNotFoundException>();

            var controller = new PlanController(planOperations.Object);

            var result = await controller.GetPlan(guid).ConfigureAwait(false);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task GetPlan_ShouldReturnTheCorrectPlan()
        {
            var planOperations = new Mock<IPlanOperations>();

            var expected = new PlanModel
            {
                Guid = Guid.NewGuid(),
                Name = "Test"
            };
            var guid = Guid.NewGuid();

            planOperations.Setup(o => o.GetPlan(guid)).ReturnsAsync(expected);

            var controller = new PlanController(planOperations.Object);

            var result = await controller.GetPlan(guid).ConfigureAwait(false) as OkNegotiatedContentResult<PlanModel>;

            if (result == null) Assert.Fail();

            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public async Task UpdatePlan_ShouldReturnBadRequestIfTheEntityDoesNotExist()
        {
            var planOperations = new Mock<IPlanOperations>();

            var guid = Guid.NewGuid();
            var model = new UpdatePlanModel();

            planOperations.Setup(o => o.UpdatePlan(guid, model)).Throws<EntityNotFoundException>();

            var controller = new PlanController(planOperations.Object);

            var result = await controller.UpdatePlan(guid, model).ConfigureAwait(false);

            Assert.AreEqual(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task UpdatePlan_ShouldReturnTheUpdatedPlan()
        {
            var planOperations = new Mock<IPlanOperations>();

            var expected = new PlanModel();
            var model = new UpdatePlanModel();
            var guid = Guid.NewGuid();

            planOperations.Setup(o => o.UpdatePlan(guid, model)).ReturnsAsync(expected);
            ;

            var controller = new PlanController(planOperations.Object);

            var result =
                await controller.UpdatePlan(guid, model).ConfigureAwait(false) as OkNegotiatedContentResult<PlanModel>;

            if (result == null) Assert.Fail();

            Assert.AreEqual(expected, result.Content);
        }
    }
}