using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

using PhoneBook.Api.Configuration;
using PhoneBook.Api.Repositories;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services;
using PhoneBook.Api.Services.Interfaces;
using PhoneBook.Api.Validators;

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

            services.AddControllers()
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
