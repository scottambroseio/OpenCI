using System.Threading.Tasks;
using System.Web.Http;
using OpenCI.API.Rest.Models.Roles;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IRoleController
    {
        Task<IHttpActionResult> CreateRole(CreateRoleModel model);
        Task<IHttpActionResult> UpdateRole(string roleName, UpdateRoleModel model);
        Task<IHttpActionResult> DeleteRole(string roleName);
    }
}