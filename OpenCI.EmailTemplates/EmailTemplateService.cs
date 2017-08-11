using System;
using System.Collections.Generic;
using System.Linq;
using OpenCI.EmailTemplates.Attributes;
using OpenCI.EmailTemplates.Contracts;

namespace OpenCI.EmailTemplates
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public IEnumerable<Guid> GetAllTemplateGuids()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttributes(typeof(TemplateGuidAttribute), true).Length > 0 &&
                            typeof(IEmailTemplateModel).IsAssignableFrom(t))
                .SelectMany(
                    t => t.GetCustomAttributes(typeof(TemplateGuidAttribute), true) as
                        IEnumerable<TemplateGuidAttribute>).Select(a => a.Guid);
        }

        public string RenderTemplate<T>(T model) where T : IEmailTemplateModel
        {
            throw new NotImplementedException();
        }
    }
}
