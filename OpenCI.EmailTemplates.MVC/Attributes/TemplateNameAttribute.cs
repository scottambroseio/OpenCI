using System;

namespace OpenCI.EmailTemplates.MVC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TemplateNameAttribute : Attribute
    {
        public TemplateNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}