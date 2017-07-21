using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Services
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
