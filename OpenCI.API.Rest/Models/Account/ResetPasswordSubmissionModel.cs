using System.ComponentModel.DataAnnotations;

namespace OpenCI.API.Rest.Models.Account
{
    public class ResetPasswordSubmissionModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}