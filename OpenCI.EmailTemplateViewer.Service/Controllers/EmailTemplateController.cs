using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenCI.EmailTemplates.Contracts;

namespace OpenCI.EmailTemplateViewer.Service.Controllers
{
    [Route("/")]
    public class EmailTemplateController : Controller
    {
        private readonly IEmailTemplateService _emailTemplateService;

        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var urls = _emailTemplateService.GetAllTemplateGuids().Select(g => Url.Action("Template", new {guid = g}));

            return View(urls);
        }

        [HttpGet("{guid:Guid}")]
        public IActionResult Template([FromRoute] Guid guid)
        {
            return View(guid);
        }
    }
}