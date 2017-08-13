using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
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

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">By default injected services list</param>
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureApplicationConfigReader();

            services.AddMvc()
                .AddJsonOptions(ConfigureNamingStrategy)
                .AddMvcOptions(ConfigureOutputFormatters);

            services.AddSwaggerGen(ConfigureSwaggerGen);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">Application request pipline configurator</param>
        /// <param name="env">Web hosting environment information</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

        private void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            string pathToDoc = Configuration["swagger:xmlDocsFileName"];

            options.SwaggerDoc("v1",
                new Info
                {
                    Title = "City Info API",
                    Version = "v1",
                    Description = "A simple API which allows to query and modify cities information.",
                    TermsOfService = "None"
                }
            );

            string fullXmlDocsFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
            options.IncludeXmlComments(fullXmlDocsFilePath);

            options.DescribeAllEnumsAsStrings();
        }

        private void ConfigureOutputFormatters(MvcOptions options)
        {
            options.OutputFormatters.Add(
                new XmlDataContractSerializerOutputFormatter()
            );
        }

        private void ConfigureNamingStrategy(MvcJsonOptions options)
        {
            var contractResolver = options.SerializerSettings?.ContractResolver as DefaultContractResolver;

            if (contractResolver != null)
            {
                contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
            }
        }

        #endregion
    }
}
