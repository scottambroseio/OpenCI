using Microsoft.AspNetCore.Mvc;
using OpenCI.EmailTemplates.MVC.ModelBinders;

namespace OpenCI.EmailTemplates.MVC.Models
{
    [ModelBinder(BinderType = typeof(EmailTemplateModelBinder))]
    public abstract class EmailTemplateModel
    {
        public abstract string Name { get; }
        public bool Preview { get; set; } = false;
    }
}
