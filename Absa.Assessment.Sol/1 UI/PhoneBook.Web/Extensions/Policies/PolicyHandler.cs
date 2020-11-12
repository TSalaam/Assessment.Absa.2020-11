using System;
using System.Net.Http;

using Microsoft.Extensions.Logging;

using Polly;
using Polly.Extensions.Http;

namespace PhoneBook.Web.Extensions.Policies {

    /// <summary>
    /// 
    /// </summary>
    public static class PolicyHandler {

        #region Retry

        /// <summary>
        /// Using Polly, define a Retry policy with the number of retries, the exponential backoff configuration, 
        /// and the actions to take when there is an HTTP exception, such as logging the error.  
        /// </summary>
        /// <remarks>Policy is configured to try six times with an exponential retry, starting at two seconds.</remarks>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly"/>
        /// <param name="maxRetries">The maximum number of retries that will be made.</param>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetBasicRetryPolicy(int maxRetries) {

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (result, timeSpan, retryCount, context) => {

                    if (!context.TryGetLogger(out var logger)) return;

                    if (result.Exception != null) {
                        logger.LogError(result.Exception, "An exception occurred on retry {RetryAttempt} for {PolicyKey}", retryCount, context.PolicyKey);
                    }
                    else {
                        logger.LogError("A non success code {StatusCode} was received on retry {RetryAttempt} for {PolicyKey}",
                            (int)result.Result.StatusCode, retryCount, context.PolicyKey);
                    }
                });
        }

        /// <summary>
        /// Using Polly, define a Retry policy with the number of retries, the exponential backoff configuration,
        /// and the actions to take when there is an HTTP exception, such as logging the error.
        /// </summary>
        /// <remarks>
        /// Policy is configured to use retries with jittered exponential backoff. 
        /// Jitter is a decorrelation strategy which adds randomness to retry intervals to spread out load and thus avoid spikes.
        /// </remarks>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly" />
        /// <seealso cref="https://github.com/App-vNext/Polly/wiki/Retry-with-jitter" />
        /// <param name="maxRetries">The maximum number of retries that will be made.</param>        
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetJitteredRetryPolicy(int maxRetries) {

            Random jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)), (result, timeSpan, retryCount, context) => {

                    if (!context.TryGetLogger(out var logger)) return;

                    if (result.Exception != null) {
                        logger.LogError(result.Exception, "An exception occurred on retry {RetryAttempt} for {PolicyKey}", retryCount, context.PolicyKey);
                    }
                    else {
                        logger.LogError("A non success code {StatusCode} was received on retry {RetryAttempt} for {PolicyKey}",
                            (int)result.Result.StatusCode, retryCount, context.PolicyKey);
                    }
                });
        }

        #endregion

        #region Circuit Breaker

        /// <summary>
        /// The circuit breaker policy is configured so it breaks or opens the circuit when there have been x number of consecutive faults when retrying the Http requests. 
        /// When that happens, the circuit will break for y seconds: in that period, calls will be failed immediately by the circuit-breaker rather than actually be placed. 
        /// The policy automatically interprets relevant exceptions and HTTP status codes as faults.
        /// </summary>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 2,
                    durationOfBreak: TimeSpan.FromMinutes(1)
                );

        #endregion

        #region Timeout

        /// <summary>
        /// Configures a timeout policy
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(int seconds = 2) =>
            Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(seconds));

        #endregion        
    }
}
