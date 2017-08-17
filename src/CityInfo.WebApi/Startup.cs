using System.Buffers;
using System.Collections.Generic;
using System.IO;
using CityInfo.Configuration.Swagger.Examples;
using CityInfo.Configuration.Swagger.Responses;
using CityInfo.WebApi.Examples;
using CityInfo.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.WebApi
{
    /// <summary>
    /// Startup of application
    /// </summary>
    public class Startup
    {
        private static IConfigurationRoot Configuration { get; set; }
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
            ConfigureApplicationConfigReader();

            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    ConfigureInputFormatters(options);
                    ConfigureOutputFormatters(options);
                });

            services.AddSwaggerGen(ConfigureSwaggerGen);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application request pipline configurator</param>
        /// <param name="env">Web hosting environment information</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCorrelationIdHeaderMiddleware();
            app.UseRequestIdHeaderMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpRequest) => swagger.Host = httpRequest.Host.Value);
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.ShowRequestHeaders();
            });
        }

        #region Private Functions

        private void ConfigureApplicationConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
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

        private void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            AddApiInfoToSwagger(options);

            ConfigureXmlCommentsForSwagger(options);

            options.DescribeAllEnumsAsStrings();
            options.DescribeAllParametersInCamelCase();
            options.DescribeStringEnumsInCamelCase();

            options.SchemaFilter<ExampleSchemaFilter>(new ExamplesProvider());
            options.OperationFilter<DefaultResponseOperationFilter>();
            options.OperationFilter<DefaultResponseHeadersOpearationFilter>(); //This filter must be last because it adds returned headers foll all response messages
        }

        private void AddApiInfoToSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1",
                new Info
                {
                    Title = "City Info API",
                    Version = "v1",
                    Description = "A simple API which allows to query and modify cities information.",
                    TermsOfService = "None",
                    License = new License
                    {
                        Name = "The GNU General Public License v3.0",
                        Url = "https://github.com/erinev/FirstApiWithCore/blob/master/LICENCE"
                    },
                    Contact = new Contact
                    {
                        Name = "Erikas Neverdauskas",
                        Email = "erikasnever@hotmail.com",
                        Url = "https://github.com/erinev"
                    }
                }
            );
        }

        private void ConfigureXmlCommentsForSwagger(SwaggerGenOptions options)
        {
            string pathToDoc = Configuration["swagger:xmlDocsFileName"];
            string fullXmlDocsFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
            options.IncludeXmlComments(fullXmlDocsFilePath);
        }

        #endregion
    }
}
