using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.Account
{
    public class ResetPasswordRequestModel
    {
        [Required]
        public string UserName { get; set; }
    }
}