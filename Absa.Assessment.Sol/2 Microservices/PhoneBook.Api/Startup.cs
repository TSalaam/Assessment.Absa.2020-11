using System;
using System.IO;
using System.Reflection;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;

using PhoneBook.Api.Configuration;
using PhoneBook.Api.Repositories;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services;
using PhoneBook.Api.Services.Interfaces;
using PhoneBook.Api.Validators;

using PhoneBook.Api.Infrastructure.Filters;

using NLog;

namespace PhoneBook.Api {

    /// <summary>
    /// 
    /// </summary>
    public class Startup {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {

            services.Configure<AppSettings>(Configuration);

            var perfMonEnabled = Configuration.GetSection("Settings").GetValue<bool>("PerfMonEnabled");

            services
                .AddControllers(options => {

                    if (perfMonEnabled) {
                        options.Filters.Add(typeof(ActionLogFilter));
                    }

                    options.Filters.Add(typeof(ValidateFilterAttribute));
                })
                .AddFluentValidation(fv => { 
                    fv.RegisterValidatorsFromAssemblyContaining<EntryRequestValidator>(); 
                });            

            services.AddScoped<IPhoneBookService, PhoneBookService>();
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = "PhoneBook API",
                    Version = "v1",
                    Description = "Sample service for PhoneBook API",
                });
            });

            var connection = $"Data Source=PhoneBookDb.db";

            services.AddDbContext<PhoneBookContext>(options => {
                options.UseSqlite(connection);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var applicationName = Configuration.GetSection("Settings").GetValue<string>("AppName");
            var loggingDirectory = Configuration.GetSection("Settings").GetValue<string>("LoggingDirectory");

            LogManager.Configuration.Variables["appName"] = applicationName;
            LogManager.Configuration.Variables["configDir"] = loggingDirectory;

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "documentation";
            });
        }

        /// <summary>
        /// Set the comments path for the swagger json and ui
        /// </summary>
        /// <value>
        /// The XML comments file path
        /// </value>
        static string XmlCommentsFilePath {
            get => Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml");
        }
    }
}
