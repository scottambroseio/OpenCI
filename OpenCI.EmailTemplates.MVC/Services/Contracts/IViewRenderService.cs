using System.Threading.Tasks;
using OpenCI.EmailTemplates.MVC.Models;

namespace OpenCI.EmailTemplates.MVC.Services.Contracts
{
    public interface IViewRenderService
    {
        Task<string> RenderToString(string viewName, EmailTemplateModel model);
    }
}