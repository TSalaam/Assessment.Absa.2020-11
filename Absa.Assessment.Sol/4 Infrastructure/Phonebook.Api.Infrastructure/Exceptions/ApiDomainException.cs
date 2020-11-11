using System;

namespace Phonebook.Api.Infrastructure.Exceptions {

    /// <summary>
    /// Exception type for application exceptions
    /// </summary>
    public class ApiDomainException : Exception {

        public ApiDomainException() { }

        public ApiDomainException(string message)
            : base(message) { }

        public ApiDomainException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
