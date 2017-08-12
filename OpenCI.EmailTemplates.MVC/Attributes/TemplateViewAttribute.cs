using System;

namespace OpenCI.EmailTemplates.MVC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TemplateViewAttribute : Attribute
    {
        public string View { get; }

        public TemplateViewAttribute(string view)
        {
            View = view;
        }
    }
}
