using System;
using System.Net.Http;

using Microsoft.Extensions.Logging;

using Polly;
using Polly.Registry;

namespace PhoneBook.Web.Extensions.Policies {

    /// <summary>
    /// 
    /// </summary>
    public static class RegistryExtensions {

        /// <summary>
        /// Using Polly, define a Retry policy with the number of retries, the exponential backoff configuration, 
        /// and the actions to take when there is an HTTP exception, such as logging the error.  
        /// </summary>
        /// <remarks>Policy is configured to try six times with an exponential retry, starting at two seconds.</remarks>
        /// <see cref="https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly"/>
        /// <param name="policyRegistry">The policy registry.</param>
        /// <param name="maxRetries">The maximum number of retries that will be made.</param>
        /// <returns></returns>
        public static IPolicyRegistry<string> AddBasicRetryPolicy(this IPolicyRegistry<string> policyRegistry, int maxRetries) {

            var retryPolicy = Policy
                .Handle<Exception>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(3, retryCount => TimeSpan.FromSeconds(10), (result, timeSpan, retryCount, context) => {

                    if (!context.TryGetLogger(out var logger)) return;

                    if (result.Exception != null) {
                        logger.LogError(result.Exception, "An exception occurred on retry {RetryAttempt} for {PolicyKey}", retryCount, context.PolicyKey);
                    }
                    else {
                        logger.LogError("A non success code {StatusCode} was received on retry {RetryAttempt} for {PolicyKey}",
                            (int)result.Result.StatusCode, retryCount, context.PolicyKey);
                    }
                })
                .WithPolicyKey(PolicyNames.BasicRetry);

            policyRegistry.Add(PolicyNames.BasicRetry, retryPolicy);

            return policyRegistry;
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
        /// <param name="policyRegistry">The policy registry.</param>
        /// <param name="maxRetries">The maximum number of retries that will be made.</param>       
        /// <returns></returns>        
        public static IPolicyRegistry<string> AddJitteredRetryPolicy(this IPolicyRegistry<string> policyRegistry, int maxRetries) {

            Random jitterer = new Random();

            var retryPolicy = Policy
                .Handle<Exception>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync(maxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterer.Next(0, 1000)), (result, timeSpan, retryCount, context) => {

                    if (!context.TryGetLogger(out var logger)) return;

                    if (result.Exception != null) {
                        logger.LogError(result.Exception, "An exception occurred on retry {RetryAttempt} for {PolicyKey}", retryCount, context.PolicyKey);
                    }
                    else {
                        logger.LogError("A non success code {StatusCode} was received on retry {RetryAttempt} for {PolicyKey}",
                            (int)result.Result.StatusCode, retryCount, context.PolicyKey);
                    }
                })
                .WithPolicyKey(PolicyNames.JitteredRetry);

            policyRegistry.Add(PolicyNames.JitteredRetry, retryPolicy);

            return policyRegistry;
        }
    }
}
