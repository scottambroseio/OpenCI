using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Services
{
    // ReSharper disable once InconsistentNaming
    public class SMSService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
