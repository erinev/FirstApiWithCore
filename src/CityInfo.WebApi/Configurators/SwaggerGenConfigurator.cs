using System.IO;
using CityInfo.Configuration.Swagger.Examples;
using CityInfo.Configuration.Swagger.Request;
using CityInfo.Configuration.Swagger.Response;
using CityInfo.WebApi.Examples;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.WebApi.Configurators
{
    /// <summary>
    /// Swagger options configurator
    /// </summary>
    public static class SwaggerGenConfigurator
    {
        /// <summary>
        /// Configures Swagger options
        /// </summary>
        /// <param name="options">swagger gen options</param>
        /// <param name="appSettingsConfigurationReader">app.settings configuration reader</param>
        public static void Configure(SwaggerGenOptions options, IConfigurationRoot appSettingsConfigurationReader)
        {
            AddApiInfoToSwagger(options);

            ConfigureXmlCommentsForSwagger(options, appSettingsConfigurationReader);

            options.DescribeAllEnumsAsStrings();
            options.DescribeAllParametersInCamelCase();
            options.DescribeStringEnumsInCamelCase();

            options.SchemaFilter<ExampleSchemaFilter>(new ExamplesProvider());
            options.OperationFilter<DefaultRequestHeadersOperationFilter>();
            options.OperationFilter<DefaultResponseMessagesOperationFilter>();
            options.OperationFilter<DefaultResponseHeadersOpearationFilter>(); //This filter must be last because it adds returned headers foll all response messages
        }

        private static void AddApiInfoToSwagger(SwaggerGenOptions options)
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

        private static void ConfigureXmlCommentsForSwagger(SwaggerGenOptions options, IConfigurationRoot appSettingsConfigurationReader)
        {
            string pathToDoc = appSettingsConfigurationReader["swagger:xmlDocsFileName"];
            string fullXmlDocsFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathToDoc);
            options.IncludeXmlComments(fullXmlDocsFilePath);
        }
    }
}
