using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using OpenCI.EmailTemplates.Attributes;
using OpenCI.EmailTemplates.Contracts;
using RazorEngine;
using RazorEngine.Templating;

namespace OpenCI.EmailTemplates
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public IEnumerable<Guid> GetAllTemplateGuids()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttribute<TemplateGuidAttribute>() != null &&
                            typeof(IEmailTemplateModel).IsAssignableFrom(t))
                .Select(
                    t => t.GetCustomAttribute<TemplateGuidAttribute>()
                ).Select(a => a.Guid);
        }

        public string RenderTemplate<T>(T model) where T : IEmailTemplateModel
        {
            return Engine.Razor.RunCompile(GetView(model), model.Name, null, model);
        }

        private static string GetView<T>(T model) where T : IEmailTemplateModel
        {
            var configuration = "Release";

#if DEBUG
            configuration = "Debug";
#endif

            var path = Path.Combine(
                Environment.CurrentDirectory,
                $"bin\\{configuration}\\net462\\win7-x86\\Templates",
                model.GetType().GetCustomAttribute<TemplateViewAttribute>().View
            );

            return File.ReadAllText(path);
        }
    }
}