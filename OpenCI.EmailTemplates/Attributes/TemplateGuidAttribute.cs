using System;

namespace OpenCI.EmailTemplates.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TemplateGuidAttribute : Attribute
    {
        public Guid Guid { get; }

        public TemplateGuidAttribute(string guid)
        {
            Guid = Guid.Parse(guid);
        }
    }
}
