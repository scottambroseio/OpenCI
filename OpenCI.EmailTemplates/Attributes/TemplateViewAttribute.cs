using System;

namespace OpenCI.EmailTemplates.Attributes
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
