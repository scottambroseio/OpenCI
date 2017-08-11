using OpenCI.EmailTemplates.Attributes;
using OpenCI.EmailTemplates.Contracts;

namespace OpenCI.EmailTemplates.Models
{
    [TemplateView("ResetPassword.cshtml")]
    [TemplateGuid("ac4cd776-0626-44f3-bbee-9086504f8e1d")]
    public class ResetPasswordModel : IEmailTemplateModel
    {
        public string Name => "ResetPassword";

        public int Id { get; set; }
        public string Link { get; set; }
        public string Token { get; set; }
    }
}