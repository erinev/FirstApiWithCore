using System.Buffers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CityInfo.WebApi.Configurators
{
    /// <summary>
    /// Mvc options configurator
    /// </summary>
    public class MvcOptionsConfigurator
    {
        /// <summary>
        /// Configures Mvc options
        /// </summary>
        public static void Configure(MvcOptions options, ILogger<Startup> logger)
        {
            ConfigureInputFormatters(options, logger);
            ConfigureOutputFormatters(options);
            
        }

        private static void ConfigureInputFormatters(MvcOptions options, ILogger<Startup> logger)
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
            options.InputFormatters.Add(new JsonInputFormatter(logger, sereliazerSettings, ArrayPool<char>.Shared, new DefaultObjectPoolProvider()));
        }

        private static void ConfigureOutputFormatters(MvcOptions options)
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
    }
}
