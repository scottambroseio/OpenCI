using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.Roles
{
    public class UpdateRoleModel
    {
        [Required]
        public string UpdatedRoleName { get; set; }
    }
}