using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.Authentication
{
    public class PasswordSignInModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}