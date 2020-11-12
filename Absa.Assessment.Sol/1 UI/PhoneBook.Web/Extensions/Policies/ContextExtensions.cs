using Microsoft.Extensions.Logging;

using Polly;

namespace PhoneBook.Web.Extensions.Policies {

    /// <summary>
    /// 
    /// </summary>
    public static class ContextExtensions {

        /// <summary>
        /// Rertrieves the logger object from Polly.Context
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger">The logger.</param>
        /// <returns></returns>
        public static bool TryGetLogger(this Context context, out ILogger logger) {

            if (context.TryGetValue(PolicyContextItems.Logger, out var loggerObject) && loggerObject is ILogger theLogger) {
                logger = theLogger;
                return true;
            }

            logger = null;
            return false;
        }
    }
}
