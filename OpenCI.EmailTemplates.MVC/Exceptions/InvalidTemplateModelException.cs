using System;

namespace OpenCI.EmailTemplates.MVC.Exceptions
{
    public class InvalidTemplateModelException : Exception
    {
        public InvalidTemplateModelException(string message) : base(message)
        {
        }
    }
}