using System;

namespace OpenCI.EmailTemplates.MVC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TemplateNameAttribute : Attribute
    {
        public string Name { get; }

        public TemplateNameAttribute(string name)
        {
            Name = name;
        }
    }
}