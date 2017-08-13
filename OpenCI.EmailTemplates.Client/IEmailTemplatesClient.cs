using System.Threading.Tasks;

namespace OpenCI.EmailTemplates.Client
{
    public interface IEmailTemplatesClient
    {
        Task<string> GetRenderedConfirmEmailTemplate(int id, string token, string link);
        Task<string> GetRenderedResetPasswordTemplate(int id, string token, string link);
    }
}
