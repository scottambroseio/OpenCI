using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenCI.EmailTemplates.MVC.Attributes;
using OpenCI.EmailTemplates.MVC.Exceptions;
using OpenCI.EmailTemplates.MVC.Models;

namespace OpenCI.EmailTemplates.MVC.ModelBinders
{
    public class EmailTemplateModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var stringGuid = (string) bindingContext.ActionContext.RouteData.Values["guid"];

            var guid = Guid.Parse(stringGuid);

            var modelType = Assembly.GetEntryAssembly()
                                .GetTypes()
                                .FirstOrDefault(t => t.GetTypeInfo().IsSubclassOf(typeof(EmailTemplateModel)) &&
                                                     t.GetTypeInfo().GetCustomAttribute<TemplateGuidAttribute>().Guid ==
                                                     guid) ?? throw new InvalidTemplateModelException(
                                $"Unable to find the model for guid {guid}");

            var model = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.SetMethod.IsPublic && p.GetMethod.IsPublic)
                .Aggregate(Activator.CreateInstance(modelType), (m, p) =>
                {
                    var routeValue = bindingContext.ValueProvider.GetValue(p.Name).FirstValue;

                    p.SetValue(m, routeValue != null ? Convert.ChangeType(routeValue, p.PropertyType) : p.GetValue(m, null));

                    return m;
                });


            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}