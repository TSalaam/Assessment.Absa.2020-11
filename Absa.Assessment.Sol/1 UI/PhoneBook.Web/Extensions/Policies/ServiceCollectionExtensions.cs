using System;
using System.Net.Http;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Polly.Extensions.Http;
using Polly;

using Newtonsoft.Json.Serialization;

using PhoneBook.Web.Configuration;
using PhoneBook.Web.Clients;

using PhoneBook.Api.Infrastructure.Filters;
using PhoneBook.Web.Clients.Interfaces;

namespace PhoneBook.Web.Extensions.Policies {

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions {

        /// <summary>
        /// Adds the Polly policies.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddPollyPolicies(this IServiceCollection services) {

            var registry = services.AddPolicyRegistry();

            registry.AddBasicRetryPolicy(4);
            registry.AddJitteredRetryPolicy(4);

            return services;
        }

        /// <summary>
        /// Adds the custom MVC.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration) {

            services.AddOptions();

            services.Configure<AppSettings>(configuration);

            services.AddMvc(options => {

                //options.Filters.Add(new AddHeader("Author", "Thaabiet Salaam"));

                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {

                    //options.SerializerSettings.ContractResolver = new DefaultContractResolver();

                    //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                    //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            services.AddMvcCore(options => {
                options.RequireHttpsPermanent = true; // does not affect api requests
            });

            services.AddSession();

            return services;
        }

        /// <summary>
        /// Adds all Http client services (like Service-Agents) using resilient Http requests based on HttpClient factory and Polly's policies 
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration) {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Set 5 min as the lifetime for each HttpMessageHandler int the pool

            services.AddHttpClient("extendedhandlerlifetime").SetHandlerLifetime(TimeSpan.FromMinutes(5));

            var defaultRetryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .RetryAsync(3);

            // Build a policy that will execute without any custom behavior

            var noOp = Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>();

            // Add http client services            

            services.AddHttpClient<IPhoneBookClient, PhoneBookClient>()
                   .SetHandlerLifetime(TimeSpan.FromMinutes(2))  // Default lifetime is 2 minutes
                   .AddPolicyHandler(request => request.Method == HttpMethod.Get ? PolicyHandler.GetJitteredRetryPolicy(4) : noOp) // ONLY apply Retry when the request is an HTTP GET
                   .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy());
            
            return services;
        }
    }
}
