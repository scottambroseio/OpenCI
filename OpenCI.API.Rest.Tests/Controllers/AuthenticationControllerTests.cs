﻿using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCI.API.Rest.Tests.Controllers.Contracts;
using OpenCI.API.Rest.Controllers;
using Moq;
using OpenCI.Identity.Dapper;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Models;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace OpenCI.API.Rest.Tests.Controllers
{
    [TestClass]
    public class AuthenticationControllerTests : IAuthenticationControllerTests
    {
        [TestMethod]
        public async Task PasswordSignIn_ShouldReturnSuccessResponseWhenSignInAttemptIsSuccessful()
        {
            var signInManager = GetMockedSignInManager();

            signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()
           )).ReturnsAsync(SignInStatus.Success);

            var controller = new AuthenticationController(signInManager.Object);

            var result = await controller.PasswordSignIn(new PasswordSignInModel()) as OkResult;

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        [TestMethod]
        public async Task PasswordSignIn_ShouldReturnBadRequestWhenSignInAttemptIsUnsuccessful()
        {
            var signInManager = GetMockedSignInManager();

            signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()
           )).ReturnsAsync(SignInStatus.Failure);

            var controller = new AuthenticationController(signInManager.Object);

            var result = await controller.PasswordSignIn(new PasswordSignInModel()) as BadRequestResult;

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }

        [TestMethod]
        public async Task PasswordSignIn_ShouldReturnBadRequestWhenUserIsLockedOut()
        {
            var signInManager = GetMockedSignInManager();

            signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()
           )).ReturnsAsync(SignInStatus.LockedOut);

            var controller = new AuthenticationController(signInManager.Object);

            var result = await controller.PasswordSignIn(new PasswordSignInModel()) as BadRequestResult;

            Assert.AreSame(typeof(BadRequestResult), result.GetType());
        }
        [TestMethod]

        public async Task PasswordSignIn_ShouldReturnSuccessResponseWhenTwoFactorVerificationIsRequired()
        {
            var signInManager = GetMockedSignInManager();

            signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()
           )).ReturnsAsync(SignInStatus.RequiresVerification);

            var controller = new AuthenticationController(signInManager.Object);

            var result = await controller.PasswordSignIn(new PasswordSignInModel()) as OkResult;

            Assert.AreSame(typeof(OkResult), result.GetType());
        }

        private Mock<SignInManager<IdentityUser, int>> GetMockedSignInManager()
        {
            var connectionHelper = new Mock<IConnectionHelper>();
            var userStore = new Mock<UserStore>(connectionHelper.Object);
            var userManager = new Mock<UserManager<IdentityUser, int>>(userStore.Object);
            var authManager = new Mock<IAuthenticationManager>();
            var signInManager = new Mock<SignInManager<IdentityUser, int>>(userManager.Object, authManager.Object);

            return signInManager;
        }
    }
}