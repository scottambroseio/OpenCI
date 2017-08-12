using System;

namespace OpenCI.Exceptions
{
    public class EmailRenderException : Exception
    {
        public EmailRenderException(string message): base(message)
        {
            
        }
    }
}
