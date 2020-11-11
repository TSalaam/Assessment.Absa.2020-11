using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;

using Models = PhoneBook.Api.Infrastructure.Models;

namespace PhoneBook.Api.Infrastructure.Extensions {

    public static class ExtensionMethods {

        /// <summary>
        /// Get the SqlException severity.
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-us/sql/relational-databases/errors-events/database-engine-error-severities?view=sql-server-ver15#levels-of-severity"/>
        /// <param name="sqlException">The SQL exception.</param>
        /// <returns></returns>
        public static int SqlExceptionSeverity(this SqlException sqlException) {

            return sqlException.Errors[0].Class;
        }

        public static HttpStatusCode GetHttpStatusCode(this Exception exception) {

            Type exceptionType = exception.GetType();

            HttpStatusCode sqlExceptionStatusCode = HttpStatusCode.InternalServerError;

            if (exception != null && exception is SqlException) {

                var e = (System.Data.SqlClient.SqlException)exception;

                if (e.SqlExceptionSeverity() == 16) {
                    sqlExceptionStatusCode = HttpStatusCode.UnprocessableEntity;
                }
            }

            Models.Enums.Exceptions tryParseResult;

            if (Enum.TryParse<Models.Enums.Exceptions>(exceptionType.Name, out tryParseResult)) {

                switch (tryParseResult) {

                    case Models.Enums.Exceptions.NullReferenceException:
                        return HttpStatusCode.LengthRequired;

                    case Models.Enums.Exceptions.FileNotFoundException:
                        return HttpStatusCode.NotFound;

                    case Models.Enums.Exceptions.OverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Models.Enums.Exceptions.OutOfMemoryException:
                        return HttpStatusCode.ExpectationFailed;

                    case Models.Enums.Exceptions.InvalidCastException:
                        return HttpStatusCode.PreconditionFailed;

                    case Models.Enums.Exceptions.ObjectDisposedException:
                        return HttpStatusCode.Gone;

                    case Models.Enums.Exceptions.UnauthorizedAccessException:
                        return HttpStatusCode.Unauthorized;

                    case Models.Enums.Exceptions.NotImplementedException:
                        return HttpStatusCode.NotImplemented;

                    case Models.Enums.Exceptions.NotSupportedException:
                        return HttpStatusCode.NotAcceptable;

                    case Models.Enums.Exceptions.InvalidOperationException:
                        return HttpStatusCode.MethodNotAllowed;

                    case Models.Enums.Exceptions.TimeoutException:
                        return HttpStatusCode.RequestTimeout;

                    case Models.Enums.Exceptions.ArgumentException:
                        return HttpStatusCode.BadRequest;

                    case Models.Enums.Exceptions.StackOverflowException:
                        return HttpStatusCode.RequestedRangeNotSatisfiable;

                    case Models.Enums.Exceptions.FormatException:
                        return HttpStatusCode.UnsupportedMediaType;

                    case Models.Enums.Exceptions.IOException:
                        return HttpStatusCode.NotFound;

                    case Models.Enums.Exceptions.IndexOutOfRangeException:
                        return HttpStatusCode.ExpectationFailed;

                    case Models.Enums.Exceptions.SqlException:
                        return sqlExceptionStatusCode;

                    default:
                        return HttpStatusCode.InternalServerError;
                }
            }
            else {
                return HttpStatusCode.InternalServerError;
            }
        }

        public static IEnumerable<Exception> InnerExceptions(this Exception ex) {

            if (ex == null) {
                throw new ArgumentNullException("Exception cannot be null");
            }

            var innerException = ex;
            do {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }

        public static Exception InnermostException(this Exception ex) {

            List<Exception> exceptions;

            exceptions = GetInnerExceptions(ex).ToList();

            Exception innermostException = exceptions[exceptions.Count - 1];

            return innermostException;
        }

        private static IEnumerable<Exception> GetInnerExceptions(Exception ex) {

            if (ex == null) {
                throw new ArgumentNullException("Exception cannot be null");
            }

            var innerException = ex;
            do {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}
