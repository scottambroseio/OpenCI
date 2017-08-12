using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenCI.EmailTemplates.MVC.Attributes;
using OpenCI.EmailTemplates.MVC.Exceptions;
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
            var models = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(EmailTemplateModel)))
                .Select(t => new EmailTemplateDescriptor
                {
                    Guid = t.GetTypeInfo().GetCustomAttribute<TemplateGuidAttribute>()?.Guid ??
                           throw new InvalidTemplateModelException($"Unable to find template guid for {t.Name}"),
                    Name = t.GetProperty("Name").GetValue(Activator.CreateInstance(t)) as string ??
                           throw new InvalidTemplateModelException($"Unable to find template name for {t.Name}")
                });

            return View(models);
        }

        [HttpGet("{guid:Guid}")]
        public async Task<ActionResult> Template([FromRoute] Guid guid, [FromQuery] EmailTemplateModel model)
        {
            var view = model.GetType().GetTypeInfo().GetCustomAttribute<TemplateViewAttribute>().View;

            if (model.Preview)
            {
                return View(view, model);
            }

            var renderedEmail = await _viewRenderService.RenderToString($"EmailTemplate/{view}", model);

            return Ok(renderedEmail);
        }
    }
}