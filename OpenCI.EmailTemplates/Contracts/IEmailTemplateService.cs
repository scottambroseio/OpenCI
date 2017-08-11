using System;
using System.Collections.Generic;

namespace OpenCI.EmailTemplates.Contracts
{
    public interface IEmailTemplateService
    {
        IEnumerable<Guid> GetAllTemplateGuids();
        string RenderTemplate<T>(T model) where T: IEmailTemplateModel;
    }
}
