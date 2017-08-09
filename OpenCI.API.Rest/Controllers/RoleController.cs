using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OpenCI.API.Rest.Controllers.Contracts;
using OpenCI.API.Rest.Models.Roles;
using OpenCI.Identity.Dapper;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("Role")]
    public class RoleController : ApiController, IRoleController
    {
        private RoleManager<IdentityRole, int> _roleManager;

        public RoleManager<IdentityRole, int> RoleManager
        {
            get { return _roleManager ?? HttpContext.Current.GetOwinContext().Get<RoleManager<IdentityRole, int>>(); }
            set { _roleManager = value; }
        }

        [HttpPost]
        [Route(Name = nameof(CreateRole))]
        public async Task<IHttpActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            var role = new IdentityRole(model.RoleName);
            var result = await RoleManager.CreateAsync(role).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("{roleName}", Name = nameof(DeleteRole))]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await RoleManager.DeleteAsync(role).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPut]
        [Route("{roleName}", Name = nameof(UpdateRole))]
        public async Task<IHttpActionResult> UpdateRole([FromUri] string roleName, [FromBody] UpdateRoleModel model)
        {
            var role = await RoleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null) return BadRequest();

            role.Name = model.UpdatedRoleName;

            var result = await RoleManager.UpdateAsync(role).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}