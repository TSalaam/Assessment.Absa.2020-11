using System;

namespace Phonebook.Api.Infrastructure.Exceptions {

    public class ApiArgumentException : ArgumentException {

        public ApiArgumentException() { }

        public ApiArgumentException(string message)
            : base(message) { }

        public ApiArgumentException(string message, Exception innerException)
            : base(message, innerException) { }

        public ApiArgumentException(string message, string paramName)
            : base(message, paramName) { }

        public ApiArgumentException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException) { }
    }
}
