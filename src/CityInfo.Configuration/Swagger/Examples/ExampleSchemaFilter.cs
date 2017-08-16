using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Examples
{
    public interface IExampleProvider
    {
        object Get(Type key);
    }

    public class ExampleSchemaFilter : ISchemaFilter
    {
        private readonly IExampleProvider _exampleProvider;

        public ExampleSchemaFilter(IExampleProvider exampleProvider)
        {
            _exampleProvider = exampleProvider;
        }

        public void Apply(Schema model, SchemaFilterContext context)
        {
            object exampleValue = _exampleProvider.Get(context.SystemType) ?? DefaultExamplesProvider.Get(context.SystemType);
            if (exampleValue != null)
            {
                model.Example = ConvertToCamelCase(exampleValue);
            }
        }

        private object ConvertToCamelCase(object exampleValue)
        {
            string jsonString = JsonConvert.SerializeObject(exampleValue, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter
                    {
                        CamelCaseText = true,
                        AllowIntegerValues = false
                    }
                }
            });
            return JsonConvert.DeserializeObject(jsonString);
        }
    }
}
