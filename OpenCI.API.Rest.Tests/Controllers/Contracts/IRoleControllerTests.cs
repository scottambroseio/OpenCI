using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCI.API.Rest.Tests.Controllers.Contracts
{
    public interface IRoleControllerTests
    {
        Task CreateRole_ShouldReturnSuccessResponseWhenSuccessful();
        Task CreateRole_ShouldReturnBadRequestWhenUnsuccessful();
        Task UpdateRole_ShouldReturnSuccessResponseWhenSuccessful();
        Task UpdateRole_ShouldReturnBadRequestWhenUnsuccessful();
        Task DeleteRole_ShouldReturnSuccessResponseWhenSuccessful();
        Task DeleteRole_ShouldReturnBadRequestWhenUnsuccessful();
    }
}
