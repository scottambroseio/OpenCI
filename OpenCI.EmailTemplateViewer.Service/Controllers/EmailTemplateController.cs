using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OpenCI.EmailTemplates.Contracts;
using OpenCI.EmailTemplates.Models;

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
            var html = _emailTemplateService.RenderTemplate(new ResetPasswordModel());

            return Content(html, "text/html");
        }
    }
}