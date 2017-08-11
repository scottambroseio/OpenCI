using OpenCI.EmailTemplates.Attributes;
using OpenCI.EmailTemplates.Contracts;

namespace OpenCI.EmailTemplates.Models
{
    [TemplateGuid("ac4cd776-0626-44f3-bbee-9086504f8e1d")]
    public class ResetPasswordModel : IEmailTemplateModel
    {
        public string Name => "ResetPassword";
    }
}