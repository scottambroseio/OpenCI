using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.Roles
{
    public class CreateRoleModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}