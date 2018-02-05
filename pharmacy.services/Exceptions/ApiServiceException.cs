using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pharmacy.Exceptions
{
    [Serializable]
    public class ApiServiceException : Exception
    {
        private string _message;
        // Constructors
        public ApiServiceException(string message)
            : base(message)
        {
            _message = message;
        }

        public override string Message
        {
            get
            {
                return String.Format("Unexpected error. Getting data from {0} failed", _message);
            }
        }

        public override string StackTrace
        {
            get
            {
                return "";
            }
        }

        // Ensure Exception is Serializable
        protected ApiServiceException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }
}
