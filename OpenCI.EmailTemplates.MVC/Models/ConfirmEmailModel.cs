using OpenCI.EmailTemplates.MVC.Attributes;

namespace OpenCI.EmailTemplates.MVC.Models
{
    [TemplateName("ConfirmEmail")]
    public class ConfirmEmailModel : EmailTemplateModel
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Token { get; set; }
    }
}
