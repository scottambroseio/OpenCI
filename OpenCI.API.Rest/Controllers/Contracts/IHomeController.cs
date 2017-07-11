using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IHomeController
    {
        IHttpActionResult Get();
    }
}
