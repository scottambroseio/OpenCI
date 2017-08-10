using System;
using Microsoft.AspNetCore.Mvc;

namespace OpenCI.EmailTemplateViewer.Service.Controllers
{
    public class EmailTemplateController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{guid:Guid}")]
        public IActionResult Template([FromQuery] Guid guid)
        {
            return View();
        }
    }
}