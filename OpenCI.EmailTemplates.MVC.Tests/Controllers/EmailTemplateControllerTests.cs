using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenCI.EmailTemplates.MVC.Controllers;
using OpenCI.EmailTemplates.MVC.Models;
using OpenCI.EmailTemplates.MVC.Services.Contracts;
using OpenCI.EmailTemplates.MVC.Tests.Controllers.Contracts;

namespace OpenCI.EmailTemplates.MVC.Tests.Controllers
{
    [TestClass]
    public class EmailTemplateControllerTests : IEmailTemplateControllerTests
    {
        [TestMethod]
        public void Index_ShouldSuccessfullyReturnViewResult()
        {
            var engine = new Mock<IViewRenderService>().Object;
            var controller = new EmailTemplateController(engine);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Template_ShouldSuccessfullyReturnViewResultWhenInPreviewMode()
        {
            var engine = new Mock<IViewRenderService>().Object;
            var controller = new EmailTemplateController(engine);

            var result = await controller.Template("", new ResetPasswordModel {Preview = true});

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task Template_ShouldSuccessfullyReturnStringContent()
        {
            var engine = new Mock<IViewRenderService>();
            engine.Setup(e => e.RenderToString(It.IsAny<string>(), It.IsAny<EmailTemplateModel>()))
                .ReturnsAsync("TEST");

            var controller = new EmailTemplateController(engine.Object);

            var result = await controller.Template("", new ResetPasswordModel {Preview = false}) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(result.StatusCode, (int) HttpStatusCode.OK);
            Assert.AreEqual("TEST", result.Value);
        }
    }
}