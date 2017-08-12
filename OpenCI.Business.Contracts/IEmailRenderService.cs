using System.Threading.Tasks;

namespace OpenCI.Contracts.Business
{
    public interface IEmailRenderService
    {
        Task<string> GetRenderedConfirmEmailTemplate(int id, string token, string link);
        Task<string> GetRenderedResetPasswordTemplate(int id, string token, string link);
    }
}