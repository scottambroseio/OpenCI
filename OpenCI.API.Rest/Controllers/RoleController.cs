using OpenCI.API.Rest.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using OpenCI.Identity.Dapper;
using OpenCI.API.Rest.Models.Roles;

namespace OpenCI.API.Rest.Controllers
{
    [RoutePrefix("Role")]
    public class RoleController : ApiController, IRoleController
    {
        private readonly RoleManager<IdentityRole, int> _roleManager;

        public RoleController(RoleManager<IdentityRole, int> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route]
        public async Task<IHttpActionResult> CreateRole([FromBody] CreateRoleModel model)
        {
            var role = new IdentityRole(model.RoleName);
            var result = await _roleManager.CreateAsync(role).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpDelete]
        [Route("{roleName}")]
        public async Task<IHttpActionResult> DeleteRole([FromUri] string roleName)
        {
            var role = new IdentityRole(roleName);
            var result = await _roleManager.DeleteAsync(role).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPut]
        [Route("{roleName}")]
        public async Task<IHttpActionResult> UpdateRole([FromUri] string roleName, [FromBody] UpdateRoleModel model)
        {
            var role = await _roleManager.FindByNameAsync(roleName).ConfigureAwait(false);

            if (role == null) return BadRequest();

            role.Name = model.UpdatedRoleName;

            var result = await _roleManager.UpdateAsync(role).ConfigureAwait(false);

            if (result.Succeeded) return Ok();

            return BadRequest();
        }
    }
}
