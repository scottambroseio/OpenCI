using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenCI.EmailTemplates.MVC.Attributes;
using OpenCI.EmailTemplates.MVC.Models;
using OpenCI.EmailTemplates.MVC.Services.Contracts;

namespace OpenCI.EmailTemplates.MVC.Controllers
{
    [Route("/")]
    public class EmailTemplateController : Controller
    {
        private readonly IViewRenderService _viewRenderService;

        public EmailTemplateController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var models = typeof(EmailTemplateModel).GetTypeInfo().Assembly
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(EmailTemplateModel)))
                .Select(t => new EmailTemplateDescriptor
                {
                    Name = t.GetTypeInfo().GetCustomAttribute<TemplateNameAttribute>().Name
                });

            return View(models);
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> Template([FromRoute] string name, [FromQuery] EmailTemplateModel model)
        {
            if (model.Preview)
                return View(name, model);

            var renderedEmail = await _viewRenderService.RenderToString($"EmailTemplate/{name}", model);

            return Ok(renderedEmail);
        }
    }
}