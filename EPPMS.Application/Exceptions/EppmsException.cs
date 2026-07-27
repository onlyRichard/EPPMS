using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Application.Exceptions
{

    /// <summary>
    /// Represents the base exception for all business and application-level exceptions
    /// within the EPPMS application.
    /// </summary>
    public abstract class EppmsException : Exception
    {
        protected EppmsException()
        {
        }

        protected EppmsException(string message)  : base(message)
        {
        }

        protected EppmsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
