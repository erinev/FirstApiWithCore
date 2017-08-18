using System.Buffers;
using System.Collections.Generic;
using System.IO;
using CityInfo.WebApi.Middlewares;
using CityInfo.WebApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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
                    ConfigureInputFormatters(options);
                    ConfigureOutputFormatters(options);
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

        private void ConfigureInputFormatters(MvcOptions options)
        {
            options.InputFormatters.Clear();

            var sereliazerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Include,
                ContractResolver = new DefaultContractResolver(),
                SerializationBinder = new DefaultSerializationBinder(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter
                    {
                        AllowIntegerValues = true,
                        CamelCaseText = true
                    }
                }
            };
            options.InputFormatters.Add(new JsonInputFormatter(Logger, sereliazerSettings, ArrayPool<char>.Shared, new DefaultObjectPoolProvider()));
        }

        private void ConfigureOutputFormatters(MvcOptions options)
        {
            options.OutputFormatters.Clear();

            var sereliazerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter(true)
                }
            };
            options.OutputFormatters.Add(new JsonOutputFormatter(sereliazerSettings, ArrayPool<char>.Shared));
        }

        #endregion
    }
}
