using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IEmailTemplateController
    {
        Task<IHttpActionResult> RenderEmailTemplate(Guid guid);
    }
}