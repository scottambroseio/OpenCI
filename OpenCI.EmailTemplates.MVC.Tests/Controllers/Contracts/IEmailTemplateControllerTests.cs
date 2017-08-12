using System.Threading.Tasks;

namespace OpenCI.EmailTemplates.MVC.Tests.Controllers.Contracts
{
    public interface IEmailTemplateControllerTests
    {
        void Index_ShouldSuccessfullyReturnViewResult();
        Task Template_ShouldSuccessfullyReturnViewResultWhenInPreviewMode();
        Task Template_ShouldSuccessfullyReturnStringContent();
    }
}