using System.IO;
using CityInfo.WebApi.Configurators;
using CityInfo.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CityInfo.WebApi
{
    /// <summary>
    /// Startup of application
    /// </summary>
    public class Startup
    {
        private static IConfigurationRoot AppSettingsConfigurationReader { get; set; }
        private static ILogger<Startup> Logger { get; set; }

        /// <summary>
        /// Startup contructor
        /// </summary>
        /// <param name="logger"></param>
        public Startup(ILogger<Startup> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">By default injected services list</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureAppSettingsConfigReader();

            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    MvcOptionsConfigurator.Configure(options, Logger);
                });

            services.AddSwaggerGen(options =>
            {
                SwaggerGenConfigurator.Configure(options, AppSettingsConfigurationReader);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application request pipline configurator</param>
        /// <param name="env">Web hosting environment information</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // TODO: timings middleware
            // TODO: exception handling middleware
            app.UseCorrelationIdHeaderMiddleware();
            app.UseRequestIdHeaderMiddleware();
            // TODO: authentication middlerware (api key)

            app.UseMvc();

            app.UseSwagger(swaggerOptions =>
            {
                swaggerOptions.PreSerializeFilters.Add((swagger, httpRequest) => swagger.Host = httpRequest.Host.Value);
            });
            app.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                swaggerUiOptions.ShowRequestHeaders();
            });
        }

        #region Private Functions

        private void ConfigureAppSettingsConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            AppSettingsConfigurationReader = builder.Build();
        }

        #endregion
    }
}
