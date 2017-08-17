using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.Configuration.Swagger.Responses
{
    public class DefaultResponseHeadersOpearationFilter : IOperationFilter
    {
        private readonly string _correlationIdHeaderName = "CorrelationId";
        private readonly string _requestIdHeaderName = "RequestId";

        public void Apply(Operation operation, OperationFilterContext context)
        {
            foreach (KeyValuePair<string, Response> response in operation.Responses)
            {
                if (response.Value.Headers?.Count > 0)
                {
                    if (response.Value.Headers != null && !response.Value.Headers.ContainsKey(_correlationIdHeaderName))
                    {
                        response.Value.Headers.Add(_correlationIdHeaderName, new Header { Type = "string" });
                    }

                    if (response.Value.Headers != null && !response.Value.Headers.ContainsKey(_requestIdHeaderName))
                    {
                        response.Value.Headers.Add(_requestIdHeaderName, new Header { Type = "string" });
                    }
                }
                else
                {
                    response.Value.Headers = new Dictionary<string, Header>
                    {
                        {_correlationIdHeaderName, new Header { Type = "string"} },
                        {_requestIdHeaderName, new Header { Type = "string"} }
                    };
                }
            }
        }
    }
}
