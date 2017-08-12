using OpenCI.EmailTemplates.MVC.Attributes;

namespace OpenCI.EmailTemplates.MVC.Models
{
    [TemplateView("ResetPassword")]
    [TemplateGuid("ac4cd776-0626-44f3-bbee-9086504f8e1d")]
    public class ResetPasswordModel : EmailTemplateModel
    {
        public override string Name => "ResetPassword";

        public string Link { get; set; }
        public string Token { get; set; }
    }
}