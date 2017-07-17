namespace OpenCI.API.Rest.Models.Registration
{
    public class ResetPasswordSubmissionModel
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}