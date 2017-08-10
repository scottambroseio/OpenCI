using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OpenCI.EmailTemplateViewer.Service.Controllers
{
    public class EmailTemplateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}