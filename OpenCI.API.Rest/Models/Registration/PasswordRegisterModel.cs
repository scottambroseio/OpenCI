using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.Registration
{
    public class PasswordRegisterModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}