using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.ExtensionPoints.Services
{
    // ReSharper disable once InconsistentNaming
    public class IdentitySMSService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}