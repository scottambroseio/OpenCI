using System.Web.Http;

namespace OpenCI.API.Rest.Controllers.Contracts
{
    public interface IProjectController
    {
        IHttpActionResult Get(int id);
        IHttpActionResult Put(int id, Model model);
        IHttpActionResult Post(int id, Model model);
        IHttpActionResult Delete(int id);
    }
}
