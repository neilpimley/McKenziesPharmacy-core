using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pharmacy.Exceptions
{
    [Serializable]
    public class DataRetrieverException : Exception
    {
        private string _message;
        // Constructors
        public DataRetrieverException(string message)
            : base(message)
        {
            _message = message;
        }

        public override string Message
        {
            get
            {
                return String.Format("Unexpected error. Couuld not retrive {0} data.", _message);
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
        protected DataRetrieverException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        { }
    }
}
